using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ThesisMethod
{
    public interface ILogManager
    {   
        
        ILogger GetLog([System.Runtime.CompilerServices.CallerFilePath]string callerFilePath = "");
        void HttpUploadFile();
        void checkFileSizeAndUpload();
    }
}
