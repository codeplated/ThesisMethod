using System;

namespace ThesisMethod
{
    public interface ILogger
    {

        void InfoApp(Enum e, params object[] args);
        void InfoDevice(Enum e, params object[] args);
        void InfoNavigational(Enum e, params object[] args);
        void InfoTouch(Enum e, params object[] args);
        void InfoCrash(Enum e, params object[] args);
        void InfoFrameworkCrash(string message , params object[] args);
        void WriteHeaders(string message);

    }
}
