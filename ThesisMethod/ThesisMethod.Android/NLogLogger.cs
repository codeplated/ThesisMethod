using System;
using NLog;
using ThesisMethod.Droid;
using Xamarin.Forms;

[assembly: Dependency(typeof(NLogLogger))]
namespace ThesisMethod.Droid
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
                    string text = e.ToString() + "-" + arg;
                    
                    log.Info(text);
                }
            }
            else
            {
                
                log.Info(e.ToString());
            }
           
        }

        public void InfoDevice(Enum e, params object[] args)
        {
            foreach (object arg in args)
            {
                string text = e.ToString() + "-" + arg;
                log.Info(text);
            }
        }

        public void InfoNavigational(Enum e, params object[] args)
        {
            foreach (object arg in args)
            {
                string text = e.ToString() + "-" + arg;
                log.Info(text);
            }
        }

        public void InfoTouch(Enum e, params object[] args)
        {
            foreach (object arg in args)
            {
                string text = e.ToString() + "-" + arg;
                log.Info(text);
            }
        }
        
        public void InfoCrash(Enum e, params object[] args)
        {
           
            foreach (object arg in args)
            {
                string text = e.ToString() + "-" + arg;
                log.Error(text);
            }
        }

        public void InfoFrameworkCrash(string message, params object[] args)
        {
            log.Fatal("InfoFrameworkCrash-"+message);
        }
    }
}