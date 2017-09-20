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
           
            
            Console.WriteLine("args length " + args.Length);
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
                Console.WriteLine("e value " + e.ToString());
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
    }
}