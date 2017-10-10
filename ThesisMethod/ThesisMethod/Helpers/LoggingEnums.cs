using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ThesisMethod
{
    public enum InfoApp //Info
    {
        appForeground,
        appBackground,
        appIntializing
    }
    public enum InfoDevice //Info
    {
        appVersion,
        deviceUniqueId,
        screenDimensions,
        manufacturerAndModel,
        batteryStatus,
        operatingSystemAndVersion
    }
    public enum InfoNavigational //Info
    {
        pageName,
        backButtonPressed,
        pageTextColor,
        pageBackgroundColor,
        pageMainColor
    }
    public enum InfoTouch //Info
    {
        cordinateX,
        cordinateY
    }
    public enum InfoCrash //Error
    {
         exception, 
    }
}
