
using System;
using System.IO;
using NLog;
using NLog.Config;
using NLog.Targets;
using ThesisMethod.Droid;
using Xamarin.Forms;
using System.Net;
using System.Diagnostics;

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

            var consoleRule = new LoggingRule("*", LogLevel.Info, consoleTarget);
            config.LoggingRules.Add(consoleRule);

            var fileTarget = new FileTarget();
            string folder = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            fileTarget.FileName = Path.Combine(folder, "Log.txt");
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
        public void HttpUploadFile()
        {
            string url = "http://codeplated.pythonanywhere.com/uploadFile";
            string file = @"/data/user/0/com.companyname.ThesisMethod/files/Log.txt";
            string paramName = "file";
            string contentType = "text/plain";
            Debug.WriteLine(string.Format("Uploading {0} to {1}", file, url));
            string boundary = "---------------------------" + DateTime.Now.Ticks.ToString("x");
            byte[] boundarybytes = System.Text.Encoding.ASCII.GetBytes("\r\n--" + boundary + "\r\n");

            HttpWebRequest wr = (HttpWebRequest)WebRequest.Create(url);
            wr.ContentType = "multipart/form-data; boundary=" + boundary;
            wr.Method = "POST";
            wr.KeepAlive = true;
            wr.Credentials = System.Net.CredentialCache.DefaultCredentials;

            Stream rs = wr.GetRequestStream();

            //string formdataTemplate = "Content-Disposition: form-data; name=\"{0}\"\r\n\r\n{1}";
            //foreach (string key in nvc.Keys)
            //{
            //    rs.Write(boundarybytes, 0, boundarybytes.Length);
            //    string formitem = string.Format(formdataTemplate, key, nvc[key]);
            //    byte[] formitembytes = System.Text.Encoding.UTF8.GetBytes(formitem);
            //    rs.Write(formitembytes, 0, formitembytes.Length);
            //}

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

            WebResponse wresp = null;
            try
            {
                wresp = wr.GetResponse();
                
                Stream stream2 = wresp.GetResponseStream();
                StreamReader reader2 = new StreamReader(stream2);
                Debug.WriteLine("yes I am here");
                Debug.WriteLine(string.Format("File uploaded, server response is: {1}"+ reader2.ReadToEnd()));
                
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
        }
    }
}