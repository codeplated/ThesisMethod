
using System;
using System.IO;
using NLog;
using NLog.Config;
using NLog.Targets;
using ThesisMethod.Droid;
using Xamarin.Forms;
using System.Net;
using Plugin.DeviceInfo;

[assembly: Dependency(typeof(NLogManager))]

namespace ThesisMethod.Droid
{
    public class NLogManager : ILogManager
    {
        private ILogger logger;
        private static string TAG = "------------NlogManager.cs ";
        private static string imei = CrossDevice.Hardware.DeviceId;
        private static string screenDimension = CrossDevice.Hardware.ScreenHeight + "*" + CrossDevice.Hardware.ScreenWidth;
        private static string brandModel = CrossDevice.Hardware.Manufacturer + "," + CrossDevice.Hardware.Model;
        private static string operatingSys = CrossDevice.Hardware.OperatingSystem + "," + CrossDevice.Hardware.OperatingSystemVersion;
        private static string appVersion = CrossDevice.App.Version;

        private static string logFileName = "SplitLogFileTxt|";
        private static string logFileExt= ".txt";
        private static long maxFileSize = 1000;
        private static string header;
        private static HttpWebRequest wr;

        public NLogManager()
        {
      
            var config = new LoggingConfiguration();

            var consoleTarget = new ConsoleTarget();
            config.AddTarget("console", consoleTarget);

            var consoleRule = new LoggingRule("*", LogLevel.Trace, consoleTarget);
            config.LoggingRules.Add(consoleRule);

            var fileTarget = new FileTarget();
            string folder = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            fileTarget.Layout = @" ${message} ${longdate} ";
            header = @"--------------------------${newline}imei|"+imei+"${newline}screenSize|"+screenDimension+"${newline}brand|"+brandModel+"${newline}os|"+ operatingSys+"${newline}appVersion|"+ appVersion+ "${newline}starttime|${longdate}${newline}--------------------------${newline}";
            fileTarget.Header = header;
            
            //${callsite} ${callsite} ${counter}

            fileTarget.FileName = Path.Combine(folder, logFileName+imei+logFileExt);
            Console.WriteLine(TAG + " check file name = " + fileTarget.FileName);
            //fileTarget.FileName = Path.Combine(InfoDevice.deviceUniqueId.ToString(), @"${longdate}", "-LogFile.txt");
            config.AddTarget("file", fileTarget);

            var fileRule = new LoggingRule("*", LogLevel.Info, fileTarget);
            config.LoggingRules.Add(fileRule);
            LogManager.Configuration = config;
            logger = GetLog();
        }
        public void fileHeader()
        {
            Console.WriteLine(TAG + "file header called");
            logger.WriteHeaders("--------------------------");
            logger.WriteHeaders("imei|"+imei);
            logger.WriteHeaders("screenSize|"+screenDimension);
            logger.WriteHeaders("brand|"+ brandModel);
            logger.WriteHeaders("os|" + operatingSys);
            logger.WriteHeaders("appVersion|"+ appVersion); 
            logger.WriteHeaders("starttime|" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.ms"));
            logger.WriteHeaders("--------------------------");
        }
        public ILogger GetLog([System.Runtime.CompilerServices.CallerFilePath] string callerFilePath = "")
        {
            
            string fileName = callerFilePath;

            if (fileName.Contains("/"))
            {
                fileName = fileName.Substring(fileName.LastIndexOf("/", StringComparison.CurrentCultureIgnoreCase) + 1);
            }

            var logger = LogManager.GetLogger(fileName);
            return new NLogLogger(logger);
        }

        public void deleteLogFile()
        {
            Console.WriteLine(TAG + "DeleteLogFileCalled");
            //string fileDirectory = @"/data/user/0/com.companyname.ThesisMethod/files";
            string fileDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            int counter = 0;
            try
            {
                string[] txtList = Directory.GetFiles(fileDirectory, "*.txt");

                // Delete source files that were copied.
                foreach (string f in txtList)
                {
                    counter++;
                    File.Delete(f);
                }
                fileHeader();
                Console.WriteLine(TAG + "DeleteLogFile" + counter + " Log file(s) deleted successfully!");
                counter = 0;
            }
            catch (DirectoryNotFoundException dirNotFound)
            {
                Console.WriteLine(TAG + "DeleteLogFileCalled message = " + dirNotFound.Message);
                logger.InfoFrameworkCrash("deleteLogFile-dirNotFound.Message");
            }
        }
        
        public void checkFileSizeAndUpload()
        {
     
            string folder = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            string file = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), logFileName.ToString() + imei.ToString() + logFileExt.ToString());
            Console.WriteLine(TAG + "checkFileSizeAndUpload Called");
            Console.WriteLine(TAG + "file name = "+ file);
            if (File.Exists(file))
            {
                long size = new System.IO.FileInfo(file).Length;
                Console.WriteLine(TAG + "file size" + size);
                Console.WriteLine(TAG + "I should be reading it");
                if (size > maxFileSize)
                {
                    try
                    {
                        HttpUploadFile();
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(TAG + "Internet not available" + ex);
                        logger.InfoFrameworkCrash("Internet not available, file upload failed");
                    }
                }
                else
                {
                    Console.WriteLine(TAG + "file size(" + size + ") is not big enough to upload ");
                }
            }else
            {
                Console.WriteLine(TAG + "checkFileSizeAndUpload - Log file not found");
                logger.InfoFrameworkCrash("checkFileSizeAndUpload-Log file not found");

            }
             

        }
        public async void HttpUploadFile()
        {
            Console.WriteLine(TAG + "httpUploadFile called");
            string folder = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            string file = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), logFileName.ToString() + imei.ToString() + logFileExt.ToString());
            if (File.Exists(file))
            {
                string url = "http://codeplated.pythonanywhere.com/uploadFile";
                string paramName = "file";
                string contentType = "text/plain";
                Console.WriteLine(TAG + string.Format("Uploading {0} to {1}", file, url));
                string boundary = "---------------------------" + DateTime.Now.Ticks.ToString("x");
                byte[] boundarybytes = System.Text.Encoding.ASCII.GetBytes("\r\n--" + boundary + "\r\n");
                try
                {
                    wr = (HttpWebRequest)WebRequest.Create(url);
                    wr.ContentType = "multipart/form-data; boundary=" + boundary;
                    wr.Method = "POST";
                    wr.KeepAlive = true;
                    wr.Credentials = System.Net.CredentialCache.DefaultCredentials;

                    Stream rs = wr.GetRequestStream();
                    rs.Write(boundarybytes, 0, boundarybytes.Length);

                    string headerTemplate = "Content-Disposition: form-data; name=\"{0}\"; filename=\"{1}\"\r\nContent-Type: {2}\r\n\r\n";
                    string header = string.Format(headerTemplate, paramName, file, contentType);
                    byte[] headerbytes = System.Text.Encoding.UTF8.GetBytes(header);
                    rs.Write(headerbytes, 0, headerbytes.Length);

                    FileStream fileStream = new FileStream(file, FileMode.Open, FileAccess.Read);
                    byte[] buffer = new byte[4096];
                    int bytesRead = 0;
                    while ((bytesRead = fileStream.Read(buffer, 0, buffer.Length)) != 0)
                    {
                        rs.Write(buffer, 0, bytesRead);
                    }
                    fileStream.Close();

                    byte[] trailer = System.Text.Encoding.ASCII.GetBytes("\r\n--" + boundary + "--\r\n");
                    rs.Write(trailer, 0, trailer.Length);
                    rs.Close();
                }
                catch (Exception e) {
                    Console.WriteLine(TAG + "Internet Not avaiable" + e);
                    logger.InfoFrameworkCrash("HttpUploadFile-Error uploading file,Internet Not avaiable");
                    if (wr != null)
                    {
                        
                        wr= null;
                    }

                }
                

                HttpWebResponse wresp = null;
                try
                {
                    wresp = (HttpWebResponse)await wr.GetResponseAsync();
                    Stream stream2 =wresp.GetResponseStream();
                    StreamReader reader2 = new StreamReader(stream2);
                    Console.WriteLine(TAG + string.Format("File uploaded successfully, server response is: " + reader2.ReadToEnd()) + wresp.StatusCode);
                    Console.WriteLine(TAG + "Deleting uploaded log file now!");
                    deleteLogFile();
                }
                catch (Exception ex)
                {
                    Console.WriteLine(TAG + "Error uploading file" + ex);
                    logger.InfoFrameworkCrash("HttpUploadFile-Error uploading file");
                    if (wresp != null)
                    {
                        wresp.Close();
                        wresp = null;
                    }
                }
                finally
                {
                    wr = null;
                }

            }
            else
            {
                Console.WriteLine(TAG + "Log files do not exist!");
                logger.InfoFrameworkCrash("HttpUploadFile-Log file not found");
            }
        }
    }
}