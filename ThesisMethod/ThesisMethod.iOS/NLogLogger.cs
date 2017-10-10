using System;
using NLog;
using ThesisMethod.iOS;
using Xamarin.Forms;

[assembly: Dependency(typeof(NLogLogger))]
namespace ThesisMethod.iOS
{
    public class NLogLogger : ILogger
    {
        private Logger log;

        public NLogLogger(Logger log)
        {
            this.log = log;
        }

        public void InfoApp(Enum e, params object[] args)
        {

            if (args.Length != 0)
            {
                foreach (object arg in args)
                {
                    string text = e.ToString() + "|" + arg + "|";

                    log.Info(text);
                }
            }
            else
            {

                log.Info(e.ToString() + "|");
            }

        }

        public void InfoDevice(Enum e, params object[] args)
        {
            if (args.Length != 0)
            {
                foreach (object arg in args)
                {
                    string text = e.ToString() + "|" + arg + "|";

                    log.Info(text);
                }
            }
            else
            {

                log.Info(e.ToString() + "|");
            }
        }

        public void InfoNavigational(Enum e, params object[] args)
        {
            if (args.Length != 0)
            {
                foreach (object arg in args)
                {
                    string text = e.ToString() + "|" + arg + "|";

                    log.Info(text);
                }
            }
            else
            {

                log.Info(e.ToString() + "|");
            }
        }

        public void InfoTouch(Enum e, params object[] args)
        {
            if (args.Length != 0)
            {
                foreach (object arg in args)
                {
                    string text = e.ToString() + "|" + arg + "|";

                    log.Info(text);
                }
            }
            else
            {

                log.Info(e.ToString() + "|");
            }
        }

        public void InfoCrash(Enum e, params object[] args)
        {

            if (args.Length != 0)
            {
                foreach (object arg in args)
                {
                    string text = e.ToString() + "|" + arg + "|";

                    log.Error(text);
                }
            }
            else
            {

                log.Info(e.ToString() + "|");
            }
        }

        public void InfoFrameworkCrash(string message, params object[] args)
        {

            if (args.Length != 0)
            {
                foreach (object arg in args)
                {
                    string text = message + "|" + arg + "|";

                    log.Info(text + "|");
                }
            }
            else
            {

                log.Fatal("InfoFrameworkCrash|" + message + "|");
            }
        }

        public void WriteHeaders(string message)
        {
            log.Info(message + "|"); ;
        }
    }
}