
using System;
using System.IO;
using NLog;
using NLog.Config;
using NLog.Targets;
using ThesisMethod.Droid;
using Xamarin.Forms;
using System.Net;
using System.Diagnostics;
using Plugin.DeviceInfo;

[assembly: Dependency(typeof(NLogManager))]

namespace ThesisMethod.Droid
{
    
 
    public class NLogManager : ILogManager
    {
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
            //${callsite} ${callsite} ${counter}
            Console.WriteLine("creating fucking new file");
            fileTarget.FileName = Path.Combine(InfoDevice.deviceUniqueId.ToString(), @"${longdate}", "-LogFile.txt");
            config.AddTarget("file", fileTarget);

            var fileRule = new LoggingRule("*", LogLevel.Info, fileTarget);
            config.LoggingRules.Add(fileRule);
            LogManager.Configuration = config;
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
            string fileDirectory = @"/data/user/0/com.companyname.ThesisMethod/files";
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
                Debug.WriteLine(counter + " Log file(s) deleted successfully!");
                counter = 0;
            }
            catch (DirectoryNotFoundException dirNotFound)
            {
                Console.WriteLine(dirNotFound.Message);
            }
        }
        public void fileHeader()
        {
            ILogger logger = DependencyService.Get<ILogManager>().GetLog();
            logger.InfoDevice(InfoDevice.deviceUniqueId, CrossDevice.Hardware.DeviceId);
            logger.InfoDevice(InfoDevice.screenDimensions, CrossDevice.Hardware.ScreenHeight +"*"+ CrossDevice.Hardware.ScreenWidth);
            logger.InfoDevice(InfoDevice.manufacturerAndModel, CrossDevice.Hardware.Manufacturer + "," + CrossDevice.Hardware.Model);
            logger.InfoDevice(InfoDevice.operatingSystenAndVersion, CrossDevice.Hardware.OperatingSystem + "," + CrossDevice.Hardware.OperatingSystemVersion);
            logger.InfoApp(InfoApp.appVersion, CrossDevice.App.Version);
            logger.InfoApp(InfoApp.appUniqueId, CrossDevice.Hardware.DeviceId);
        }
        public void checkFileSizeAndUpload()
        {
            string file = @"/data/user/0/com.companyname.ThesisMethod/files/Log.txt";
            long size = new System.IO.FileInfo(file).Length;
            long maxSize = 500;
            Debug.WriteLine("file size" + size);
            if (size>maxSize)
            {
                fileHeader();
                HttpUploadFile();
            }else
            {
                Debug.WriteLine("file size("+ size +") is not big enough to upload ");
            }

        }
        public async void HttpUploadFile()
        {
            string file = @"/data/user/0/com.companyname.ThesisMethod/files/Log.txt";
            if (File.Exists(file))
            {
                string url = "http://codeplated.pythonanywhere.com/uploadFile";
                string paramName = "file";
                string contentType = "text/plain";
                //Debug.WriteLine(string.Format("Uploading {0} to {1}", file, url));
                string boundary = "---------------------------" + DateTime.Now.Ticks.ToString("x");
                byte[] boundarybytes = System.Text.Encoding.ASCII.GetBytes("\r\n--" + boundary + "\r\n");
                HttpWebRequest wr = (HttpWebRequest)WebRequest.Create(url);
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


                HttpWebResponse wresp = null;
                try
                {
                    wresp = (HttpWebResponse)await wr.GetResponseAsync();
                    Stream stream2 =wresp.GetResponseStream();
                    StreamReader reader2 = new StreamReader(stream2);
                    Debug.WriteLine(string.Format("File uploaded successfully, server response is: " + reader2.ReadToEnd()) + wresp.StatusCode);
                    Debug.WriteLine("Deleting uploaded log file now!");
                    deleteLogFile();
                }
                catch (Exception ex)
                {
                    Debug.WriteLine("Error uploading file" + ex);
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
            }else
            {
                Debug.WriteLine("Log files do not exist!");
            }
        }
    }
}