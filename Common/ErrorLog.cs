using System;
using System.IO;
using System.Text;
using System.Reflection;

namespace Common
{
    public class ErrorLog
    {
        public static void WriteLog(string input, string path)
        {
            WriteLog(input, path, "");
        }

        public static void WriteLog(string input, string path, string sectionTitle)
        {
            string logname = System.IO.Path.GetFileNameWithoutExtension(Assembly.GetCallingAssembly().CodeBase);
            string LogFilePath = path + logname + ".log";
            try
            {
                if (!System.IO.Directory.Exists(path))
                    System.IO.Directory.CreateDirectory(path);

                //FileInfo logFile = new FileInfo(LogFilePath);
                FileStream fs = new FileStream(LogFilePath, FileMode.OpenOrCreate, FileAccess.ReadWrite);
                StreamWriter w = new StreamWriter(fs);
                w.BaseStream.Seek(0, SeekOrigin.End);
                w.WriteLine("Begin section - " + sectionTitle + " -----------------------------------------------------");
                w.WriteLine("// Log Entry: {0} {1} //", DateTime.Now.ToLongTimeString(), DateTime.Now.ToLongDateString());
                w.WriteLine("// " + input + " ");
                w.WriteLine("End section - " + sectionTitle + " -------------------------------------------------------");
                w.WriteLine();
                w.Flush();
                w.Close();
            }
            //catch ( Exception ex )
            catch
            {
                //int a = 1;
            };
        }
    }
}
