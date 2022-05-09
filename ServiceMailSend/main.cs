using System;
using System.Data;
using System.IO;

using System.Reflection;
using System.Data.SqlClient;
using Common;
using System.Xml;
using HCMConfig;

namespace ServiceMailSend
{
	/// <summary>
	/// Summary description for Class1.
	/// </summary>
	class DPFServiceMailSend
	{
		static string ConnectionString = "";
		static string SmtpServer = "";
		static bool UseSmtpServerAuth = false;
		static string SmtpServerPort = "";
		static string SmtpServerUser = "";
		static string SmtpServerPwd = "";
		static string eMail = "";
		static string eMailFile = "";

		static bool DpfDebug = false;
        static string DpfErrLogPath = "";

		[STAThread]
		static void Main(string[] args)
		{

			if ( GetSystemSettings() )
			{
				DPFSendMail();
			}
		}

		#region DPFSendMail
		static void DPFSendMail()
		{
			DataTable _dt = new DataTable();
			int recid = -1;
			int top = 10;
			int TypeID = -1;
            XmlDocument CaseEventData = null;
            MemoryStream stremReportFile = null;

			try
			{
                top = Convert.ToInt32(HCMConfig.SystemConfig.GetSetting("HCMMailSend", "HCM.MAILQUEUE"));
			}
			catch
			{};
	
			if ( top < 0 )
				top = 10;

            if (DpfDebug)
            {
                Console.WriteLine("DEBUG, MAILQUEUE: " + top.ToString());
                ErrorLog.WriteLog("DEBUG, MAILQUEUE: " + top.ToString(), DpfErrLogPath);
            }

			try
			{

				_dt = Common.SystemSettings.MailQueueRetrive( top );

				System.Text.Encoding utfcoding = System.Text.Encoding.GetEncoding("iso-8859-1");

				for ( int i=0; i<_dt.Rows.Count;i++)
				{
					/*****************************************************************************************
					 * settings
					 * **************************************************************************************/
					recid = Convert.ToInt32( _dt.Rows[i]["recID"] );
					TypeID = Convert.ToInt32( _dt.Rows[i]["TypeID"] );

                    stremReportFile = null;

                    if (!string.IsNullOrEmpty(_dt.Rows[i]["CaseEventData"].ToString()))
                    {
                        CaseEventData = new XmlDocument();
                        CaseEventData.LoadXml(_dt.Rows[i]["CaseEventData"].ToString());

                        XmlNode addReport = CaseEventData.SelectSingleNode("/CaseDocs/AddReport");
                        if (addReport != null)
                        {
                            byte[] base64Html = System.Convert.FromBase64String(CaseEventData.SelectSingleNode("/CaseDocs/AddReport").InnerText);
                            string htmlString = System.Text.Encoding.UTF8.GetString(base64Html);
                            

                            stremReportFile = new MemoryStream();

                            string _html_file_name = "hcm_report_" + DateTime.Now.Year.ToString();
                            _html_file_name += "_" + DateTime.Now.Month.ToString();
                            _html_file_name += "_" + DateTime.Now.Day.ToString();
                            _html_file_name += "_" + DateTime.Now.Hour.ToString();
                            _html_file_name += "_" + DateTime.Now.Minute.ToString();
                            _html_file_name += ".html";

                            using (Ionic.Zip.ZipFile zip = new Ionic.Zip.ZipFile())
                            {
                                MemoryStream stream = new MemoryStream(base64Html);
                                stream.Seek(0, SeekOrigin.Begin);

                                zip.AddEntry(_html_file_name, stream); 
                                zip.Save(stremReportFile);
                            }
                            
                        }
                    }

                    //return;
                    

                    if ( DpfDebug )
                    {
                        Console.WriteLine( "DEBUG, RecID: " + recid.ToString() );
                        Console.WriteLine( "DEBUG, TypeID: " + TypeID.ToString() );
                        Console.WriteLine( "DEBUG, FileName: " + eMailFile );

                        ErrorLog.WriteLog("DEBUG, RecID: " + recid.ToString(), DpfErrLogPath);
                        ErrorLog.WriteLog("DEBUG, TypeID: " + TypeID.ToString(), DpfErrLogPath);
                        ErrorLog.WriteLog("DEBUG, FileName: " + eMailFile, DpfErrLogPath);
                    }

					/*****************************************************************************************
					 * create email object
					 * **************************************************************************************/
					EmailMessage PublicEmailMessage = new EmailMessage();

					/*****************************************************************************************
					 * add FROM address
					 * **************************************************************************************/
                    PublicEmailMessage.FromAddress = new EmailAddress(_dt.Rows[i]["FormAddr"].ToString());

					/*****************************************************************************************
					 * add subject
					 * **************************************************************************************/
                    PublicEmailMessage.Subject = _dt.Rows[i]["Subj"].ToString();	


					/*****************************************************************************************
					 * attach file
					 * **************************************************************************************/
					if ( eMailFile.Trim() != "" )
					{
						if ( File.Exists( eMailFile ) )
						{
							FileAttachment fileAttachment = new FileAttachment(new FileInfo( eMailFile ));	
							fileAttachment.ContentType = "text/html";			
							PublicEmailMessage.AddMixedAttachment(fileAttachment);
						}
					}


                    if ( DpfDebug )
                    {
                        Console.WriteLine( "DEBUG, FromAddress: " + _dt.Rows[ i ][ "FormAddr" ].ToString() );
                        Console.WriteLine( "DEBUG, Subject: " + _dt.Rows[ i ][ "Subj" ].ToString() );

                        ErrorLog.WriteLog("DEBUG, FromAddress: " + _dt.Rows[i]["FormAddr"].ToString(), DpfErrLogPath);
                        ErrorLog.WriteLog("DEBUG, Subject: " + _dt.Rows[i]["Subj"].ToString(), DpfErrLogPath);
                    }

					/*****************************************************************************************
					 * add TO address
					 * **************************************************************************************/
                    if (_dt.Rows[i]["ToAddr"].ToString().Trim() == "")
					{
                        for (int y = 0; y < _dt.Rows[i]["Bcc"].ToString().Split(';').Length; y++)
						{
                            eMail = _dt.Rows[i]["Bcc"].ToString().Split(';')[y].Trim();
							PublicEmailMessage.AddToAddress(new EmailAddress( eMail ));

							if ( DpfDebug )	Console.WriteLine("DEBUG, Bcc: "+eMail );
						}
					}
					else
					{
                        for (int y = 0; y < _dt.Rows[i]["ToAddr"].ToString().Split(';').Length; y++)
						{
                            eMail = _dt.Rows[i]["ToAddr"].ToString().Split(';')[y].Trim();
							PublicEmailMessage.AddToAddress(new EmailAddress( eMail ));

							if ( DpfDebug )	Console.WriteLine("DEBUG, ToAddr: "+eMail );
						}

                        if (_dt.Rows[i]["Bcc"].ToString().Trim() != "")
						{
                            for (int y = 0; y < _dt.Rows[i]["Bcc"].ToString().Split(';').Length; y++)
							{
                                eMail = _dt.Rows[i]["Bcc"].ToString().Split(';')[y].Trim();
								PublicEmailMessage.AddBccAddress(new EmailAddress( eMail ));

								if ( DpfDebug )	Console.WriteLine("DEBUG, Bcc: "+eMail );
							}
						}						
					}

					/*****************************************************************************************
					 * email char set
					 * **************************************************************************************/
					PublicEmailMessage.HeaderCharSet = utfcoding;

                    string strMessage = _dt.Rows[i]["Body"].ToString();

                    if (DpfDebug)
                    {
                        Console.WriteLine("DEBUG, Message: " + _dt.Rows[i]["Body"].ToString());
                        ErrorLog.WriteLog("DEBUG, Message: " + _dt.Rows[i]["Body"].ToString(), DpfErrLogPath);
                    }

					PublicEmailMessage.HtmlPart = new HtmlAttachment( strMessage );
					PublicEmailMessage.HtmlPart.CharSet = utfcoding;


                    /*****************************************************************************************
					 * zip report
					 * **************************************************************************************/
                    if (stremReportFile != null)
                    {
                        stremReportFile.Seek(0, SeekOrigin.Begin);
                        byte[] zipBytes = stremReportFile.ToArray();
                        stremReportFile.Close();

                        FileAttachment reportFile = new FileAttachment(zipBytes);

                        string _html_file_name = "hcm_report_" + DateTime.Now.Year.ToString();
                        _html_file_name += "_" + DateTime.Now.Month.ToString();
                        _html_file_name += "_" + DateTime.Now.Day.ToString();
                        _html_file_name += "_" + DateTime.Now.Hour.ToString();
                        _html_file_name += "_" + DateTime.Now.Minute.ToString();
                        _html_file_name += ".zip";

                        reportFile.FileName = _html_file_name;
                        reportFile.ContentType = "application/zip";
                        PublicEmailMessage.AddMixedAttachment(reportFile);
                    }
                    
                    

					/*****************************************************************************************
					 * SMTP server
					 * **************************************************************************************/
					SmtpServer smtpServer=new SmtpServer( SmtpServer );
                    //smtpServer.ServerTimeout = 1;

                    if ( DpfDebug )
                    {
                        Console.WriteLine( "DEBUG, SmtpServer: " + SmtpServer );
                        Console.WriteLine( "DEBUG, SmtpServerPort: " + SmtpServerPort );

                        ErrorLog.WriteLog("DEBUG, SmtpServer: " + SmtpServer, DpfErrLogPath);
                        ErrorLog.WriteLog("DEBUG, SmtpServerPort: " + SmtpServerPort, DpfErrLogPath);
                    }

			
					/*****************************************************************************************
					 * authotication
					 * **************************************************************************************/
					if ( UseSmtpServerAuth )
					{
						smtpServer.SmtpAuthToken = new DotNetOpenMail.SmtpAuth.SmtpAuthToken(SmtpServerUser, SmtpServerPwd);
                        if ( DpfDebug )
                        {
                            Console.WriteLine( "DEBUG, Use Smtp Auth" );
                            Console.WriteLine( "DEBUG, SmtpServerUser: " + SmtpServerUser );
                            Console.WriteLine( "DEBUG, SmtpServerPwd: " + SmtpServerPwd );

                            ErrorLog.WriteLog("DEBUG, Use Smtp Auth", DpfErrLogPath);
                            ErrorLog.WriteLog("DEBUG, SmtpServerUser: " + SmtpServerUser, DpfErrLogPath);
                            ErrorLog.WriteLog("DEBUG, SmtpServerPwd: " + SmtpServerPwd, DpfErrLogPath);
                        }
					}
			
					/*****************************************************************************************
					 * SEND
					 * **************************************************************************************/
					try
					{						
						if ( DpfDebug )	Console.WriteLine("DEBUG, Start send message" );

						PublicEmailMessage.Send( smtpServer );

						if ( DpfDebug )	Console.WriteLine("DEBUG, End send message" );
						
						Common.SystemSettings.MailQueueUpdate( recid, 50 );
					}
					catch ( Exception ex)
					{
                        if (DpfDebug)
                        {
                            Console.WriteLine("DEBUG, Error sending: " + ex.Message);
                            ErrorLog.WriteLog("DEBUG, Error sending: " + ex.Message, DpfErrLogPath);
                        }

						try
						{
							switch ( TypeID )
							{
								case 1:
									Common.EventLog.Insert(228,"Ex - "+ ex.Message);
									break;
								case 2:
									Common.EventLog.Insert(258,"Ex - "+ ex.Message);
									break;
								case 3:
									Common.EventLog.Insert(208,"Ex - "+ ex.Message);
									break;
								case 4:
									Common.EventLog.Insert(640,"Ex - "+ ex.Message);
									break;
								case 5:
									Common.EventLog.Insert(641,"Ex - "+ ex.Message);
									break;
							}
							

							Common.SystemSettings.MailQueueUpdate( recid, 30 );
						}
                        catch (Exception ex1)
                        {
                            Console.WriteLine("Error 101, Exception: " + ex1.Message);
                        };
					}
				}
			}
            catch (Exception ex)
            {
                Console.WriteLine("Error 101, Exception: " + ex.Message);
            };
		}
		#endregion

		#region GetSystemSettings
		static bool GetSystemSettings()
		{
			try
			{
                /********************************************************
                * LOG PATH
                * *****************************************************/
                DpfErrLogPath = System.IO.Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) + @"\log\";


				/********************************************************
				* CONNECTION STRING
				* *****************************************************/
                ConnectionString = SystemConfig.GetSetting("HCMCommon", "HCM.Connection");				
				if ( ConnectionString ==  null || ConnectionString == "")
				{
					throw new Exception("Can't find ServiceMailSend connection string.");
				}
				DAO.dbConnString = ConnectionString;


				/********************************************************
				* SMTP SETTINGS
				* *****************************************************/
				DataTable _dt;
				using ( _dt = new DataTable() )
				{
					_dt = Common.SystemSettings.Retrive();
					if ( _dt.Rows.Count < 1 )
						throw new Exception("No SMTP settigns data found.");

                    SmtpServer = _dt.Rows[0]["SMTPServer"].ToString();
                    UseSmtpServerAuth = Convert.ToBoolean(_dt.Rows[0]["SMTPServerAuth"]);
                    SmtpServerPort = _dt.Rows[0]["SMTPServerPort"].ToString();
                    SmtpServerUser = _dt.Rows[0]["SMTPServerUser"].ToString();
                    SmtpServerPwd = _dt.Rows[0]["SMTPServerPass"].ToString();
				}	
	
				/********************************************************
				* DEBUG SERVICE
				* *****************************************************/
				try
				{
                    DpfDebug = (SystemConfig.GetSetting("HCMMailSend", "HCM.Debug") == "1" ? true : false);
				}
				catch{};

				return true;
			}
			catch ( Exception ex )
			{
				Console.WriteLine("Error 101, Exception: " + ex.Message);
				return false;
			}
		}
		#endregion
	}
}
