using System;
using System.IO;

namespace Csharp_LogFileGeneration
{
    public class LogFileGenertor
    {
        //GlobalVariableDeclaration
        //LogFileName
        private static string logfilename = "AutomationLogReport " + DateTime.Now.ToString("d-M-yyyy") + " Time " + DateTime.Now.ToString("hhmmss tt");
        //Writer
        private static StreamWriter streamwrite = null;
       
        private static string LogfilePath;
        
        //Define Log File Path
        public static void GetLogFilePath()
        {
            string currentDirectoryPath = Environment.CurrentDirectory;
            string actualPath = currentDirectoryPath.Substring(0, currentDirectoryPath.LastIndexOf("bin"));
            string projectPath = new Uri(actualPath).LocalPath;
            LogfilePath = projectPath + "\\Logs\\";
        }

        //Step 1 - Delete the Old Exisitng Log File
        public static void DeleteLogFile()
        {
            GetLogFilePath();
            if (Directory.Exists(LogfilePath))
            {
                string[] LogFiles = Directory.GetFiles(LogfilePath);
                foreach(string files in LogFiles)
                {
                    File.Delete(LogfilePath);
                }
            }
        }
        //Step 2 - Define the Log file path where to be created
        public static void CreateLogFile()
        {
            GetLogFilePath();
            //Check if File Exists already
            if (Directory.Exists(LogfilePath))
            {
                streamwrite = File.AppendText(LogfilePath + logfilename + ".log");
            }
            else
            {
                Directory.CreateDirectory(LogfilePath);
                streamwrite = File.AppendText(LogfilePath + logfilename + ".log");
            }
            
        }
        //Step 3 - Write the Log Message
        public static void Write (string logMessage)
        {
            CreateLogFile();
            streamwrite.Write("{0} {1}", DateTime.Now.ToLongTimeString(), DateTime.Now.ToLongDateString());
            streamwrite.WriteLine("     {0}", logMessage);
            streamwrite.Flush();
        }
    }
}
