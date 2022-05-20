using HCMDataAccess;
using HCMModels;
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

namespace SendMailWs.Services
{
    public class UnitOfWorkService : IUnitOfWork
    {
        private readonly ILogger<UnitOfWorkService> _logger;
        private readonly IMailService _mailService;
        private readonly IMailQueue _mailQueue;
        private readonly ISettingsData _hcmSettings;
        private readonly sppMailSettings _smtpSettings;

        public UnitOfWorkService(ILogger<UnitOfWorkService> logger,
            ISettingsData hcmSettings, IMailQueue mailQueue, IMailService mailService,
            IConfiguration conf)
        {
            _logger = logger;
            _hcmSettings = hcmSettings;
            _mailQueue = mailQueue;
            _mailService = mailService;
            _smtpSettings = conf.GetSection("sppMailSettings").Get<sppMailSettings>();
        }


        public async Task ProcessMailQueue()
        {            
            var smtpSettings = await GetSmtpSettings();


            _logger.LogDebug("Start reading the mail queue ...");
            var mailQ = await _mailQueue.GetTop();

            foreach (var item in mailQ)
            {
                await ProcessMail(smtpSettings, item);

                await _mailQueue.SetStatus(item.recID, 50);
                _logger.LogDebug("Status of the mail item set as completed.");
            }

            _logger.LogDebug("... Mail queue finished!");

        }

        protected async Task ProcessMail(sppMailSettings smtpSettings, MailQueueModel item)
        {
            _logger.LogDebug("Start processing mail item ...");

            var mailRequest = new sppMailRequest();
            mailRequest.Settings = smtpSettings;
            mailRequest.FromMail = item.FormAddr;
            mailRequest.FromDisplayName = item.FormAddr;
            mailRequest.ToEmail = item.ToAddr;
            mailRequest.BccEmail = item.Bcc;
            mailRequest.Subject = item.Subj;
            mailRequest.Body = item.Body;            


            _logger.LogDebug("Generating ZIP file to attach ...");

            var zipFile = CreateZipAttachment(item);
            if (zipFile != null)
            {
                _logger.LogDebug("Generated: {fileName}", zipFile.FileName);
                mailRequest.Attachments.Add(zipFile);
            }
            else
            {
                _logger.LogDebug("No file to attach!");
            }

            _logger.LogDebug("MailRequest : {req}", JsonConvert.SerializeObject(mailRequest));

            _logger.LogInformation("Send Mail id: {id} from: {from} to: {to}", item.recID, item.FormAddr, item.ToAddr);

            await _mailService.SendEmailAsync(mailRequest);

            _logger.LogDebug("... Processing mail item completed.");

        }

        protected sppAttachment CreateZipAttachment(MailQueueModel mailItem)
        {
            var attFile = new sppAttachment();

            var CaseEventData = new XmlDocument();
            CaseEventData.LoadXml(mailItem.CaseEventData);
            XmlNode reportNode = CaseEventData.SelectSingleNode("/CaseDocs/AddReport");
            if (reportNode != null )
            {
                attFile.FileName = DateTime.Now.ToString("yyMMddHHmm") + "_" + "HcmReport.html";
                attFile.Content = System.Convert.FromBase64String(reportNode.InnerText);

                var filesToZip = new List<sppAttachment>();
                filesToZip.Add(attFile);

                var archivedFile = sppZip.GetZipArchive(filesToZip);

                attFile.FileName = DateTime.Now.ToString("yyMMddHHmm") + "_" + "HcmReport.zip";
                attFile.Content = archivedFile;
                attFile.MediaType = "application";
                attFile.MediaSubType = "zip";

            }

            return attFile;
        }

        protected async Task<sppMailSettings> GetSmtpSettings()
        {
            var mailSettings = _smtpSettings;

            _logger.LogDebug("Getting the SMTP Settings ...");
            var appSettings = await _hcmSettings.GetSettings();
            if (appSettings != null)
            {                
                mailSettings.Host = appSettings.SMTPServer;
                mailSettings.Port = Int32.Parse(appSettings.SMTPServerPort);
                mailSettings.UseAuthentication = appSettings.SMTPServerAuth;
                mailSettings.Login = appSettings.SMTPServerUser;
                mailSettings.Password = appSettings.SMTPServerPass;
            }
            _logger.LogDebug("SMTP Settings: {settings}", JsonConvert.SerializeObject(mailSettings));
            return mailSettings;

        }
    }


}
