using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ThesisMethod
{
    public interface ILogger
    {
        void Trace(string text, params object[] args);
        void Debug(string text, params object[] args);
        void Info(string text, params object[] args);
        void Warn(string text, params object[] args);
        void Error(string text, params object[] args);
        void Fatal(string text, params object[] args);
    }
}
