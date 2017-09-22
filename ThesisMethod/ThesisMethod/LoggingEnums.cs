using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ThesisMethod
{
    public enum InfoApp //Info
    {
        appUniqueId,
        appVersion,
        appForeground,
        appBackground,
        appIntializing
    }
    public enum InfoDevice //Info
    {
        deviceUniqueId,
        screenDimensions,
        manufacturerAndModel,
        batteryStatus,
        operatingSystemAndVersion
    }
    public enum InfoNavigational //Info
    {
        pageName,
        previousPage,
        nextPage,
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
