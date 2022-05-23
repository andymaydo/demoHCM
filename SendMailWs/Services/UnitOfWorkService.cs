using MailLib.Interfaces;
using MailLib.Models;
using SendMailWs.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using Newtonsoft.Json;
using SendMailWs.Models;

namespace SendMailWs.Services
{
    public class UnitOfWorkService : IUnitOfWork
    {
        private readonly ILogger<UnitOfWorkService> _logger;
        private readonly IMailService _mailService;
        private readonly IMailQueue _mailQueue;
        private readonly IMailClientSettings _mailClientSettings;
        private readonly ISqlLog _sqlLogger;


        public UnitOfWorkService(ILogger<UnitOfWorkService> logger,
            IMailClientSettings mailClientSettings, IMailQueue mailQueue, IMailService mailService,
            ISqlLog sqlLogger)
        {
            _logger = logger;
            _mailClientSettings = mailClientSettings;
            _mailQueue = mailQueue;
            _mailService = mailService;
            _sqlLogger = sqlLogger;
        }


        public async Task ProcessMailQueue()
        {
            _logger.LogDebug("Getting the SMTP Settings ...");
                var smtpSettings = await _mailClientSettings.GetSettings();
            _logger.LogDebug("SMTP Settings: {settings}", JsonConvert.SerializeObject(smtpSettings));


            _logger.LogDebug("Start reading the mail queue ...");
                var mailQ = await _mailQueue.GetTop();

                foreach (var item in mailQ)
                {
                    try
                    {
                        await ProcessMail(smtpSettings, item);
                       
                    }
                    catch(Exception ex)
                    {
                        _logger.LogDebug($"Status of the mail item {item.itemId} set to failed.");
                        _logger.LogError(ex, ex.Message);

                        await _mailQueue.SetStatus(item.itemId, "30");

                        var logMsg = $"Mail Id: {item.itemId}, {ex.Message}";
                        await _sqlLogger.AddSqlLog("1000", logMsg, ex);

                        continue;
                    }



                    await _mailQueue.SetStatus(item.itemId, "50");
                    _logger.LogDebug($"Status of the mail item {item.itemId} set to completed.");


            }
            _logger.LogDebug("... Mail queue finished!");

        }

        protected async Task ProcessMail(sppMailSettings smtpSettings, MailQueueItem item)
        {
            _logger.LogDebug("Start processing mail item ...");
            var mailRequest = new sppMailRequest
            {
                MailItem = item,
                Settings = smtpSettings
            };
            _logger.LogDebug("MailRequest : {req}", JsonConvert.SerializeObject(mailRequest));

            _logger.LogInformation("Send Mail id: {id} from: {from} to: {to}", item.itemId, item.FromMail, item.ToEmail);
                await _mailService.SendEmailAsync(mailRequest);
            _logger.LogDebug("... Processing mail item completed.");

        }

    }


}
