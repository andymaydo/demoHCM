using Dapper;
using MailLib.Models;
using SendMailWs.Interfaces;
using SendMailWs.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace SendMailWs.Services
{
    public class HcmMailQueueService : IMailQueue
    {
        private string _sqlConnStr;
        private readonly ILogger<HcmMailQueueService> _logger;


        public HcmMailQueueService(IConfiguration conf, ILogger<HcmMailQueueService> logger)
        {
            _sqlConnStr = conf.GetSection("ConnectionStrings:Default").Value;
            _logger = logger;
        }

        public async Task<List<MailQueueItem>> GetTop(int top)
        {
            string sql = "[dbo].[pMailQueue_Retrive]";

            var p = new DynamicParameters();

            p.Add("Top", top); 
           
            using (var connection = new SqlConnection(_sqlConnStr))
            {
                connection.Open();
                var orgResult = await connection.QueryAsync<HcmMailQueueItem>(sql, p, commandType: CommandType.StoredProcedure);

                var targetRes = orgResult
                    .Select(x => new MailQueueItem()
                    {
                        itemId = x.recID.ToString(),
                        FromMail = x.FormAddr,
                        FromDisplayName = x.FormAddr,
                        ToEmail = (!String.IsNullOrEmpty(x.ToAddr)? x.ToAddr.Split(";").ToList() : new List<string>()),
                        BccEmail = (!String.IsNullOrEmpty(x.Bcc) ? x.Bcc.Split(";").ToList() : new List<string>()),
                        Subject = x.Subj,
                        Body = x.Body,
                        Attachments = new List<sppAttachment>() { GetAttachedReport(x.CaseEventData) } 
                    });
                    
                    
                return targetRes.AsList();
            }
        }


        public async Task SetStatus(string recId, string statusId)
        {
            await DbUpdateMailStatus(recId, statusId);

        }

        





        private class HcmMailQueueItem
        {
            public int recID { get; set; }
            public string FormAddr { get; set; }
            public string ToAddr { get; set; }
            public string Bcc { get; set; }
            public string Subj { get; set; }
            public string Body { get; set; }
            public string CaseEventData { get; set; }  //xml

        }

        private sppAttachment GetAttachedReport(string caseEventData)
        {
            var htmlResult = new sppAttachment();
            var zipResult = new sppAttachment();

            
            //try
            //{
                var CaseEventDataXml = new XmlDocument();
                CaseEventDataXml.LoadXml(caseEventData);
                XmlNode? reportNode = CaseEventDataXml.SelectSingleNode("/CaseDocs/AddReport");

                if (reportNode != null)
                {
                    htmlResult.FileName = DateTime.Now.ToString("yyMMddHHmm") + "_" + "HcmReport.html";
                    htmlResult.Content = System.Convert.FromBase64String(reportNode.InnerText);

                    var filesToZip = new List<sppAttachment>();
                    filesToZip.Add(htmlResult);

                    var archivedFile = sppZip.GetZipArchive(filesToZip);

                    zipResult.FileName = DateTime.Now.ToString("yyMMddHHmm") + "_" + "HcmReport.zip";
                    zipResult.Content = archivedFile;
                    zipResult.MediaType = "application";
                    zipResult.MediaSubType = "zip";

                }
            //}
            //catch (Exception ex)
            //{
            //    _logger.LogError(ex, ex.Message);
            //    return new sppAttachment();
            //}
            
            return zipResult;

        }

        private async Task DbUpdateMailStatus(string recId, string statusId)
        {
            string sql = "[dbo].[pMailQueue_Update]";

            var p = new DynamicParameters();
            p.Add("@recId", Int32.Parse(recId));
            p.Add("@StatusId", Int32.Parse(statusId)); //50: OK, 30: failed


            using (var connection = new SqlConnection(_sqlConnStr))
            {
                connection.Open();
                await connection.ExecuteAsync(sql, p, commandType: CommandType.StoredProcedure);
            }
        }

       
    }
}
