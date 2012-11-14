using System.Collections.Generic;

namespace ZeroProximity.DeviceDetection.Defaults
{
    public static class MobileDetectionUserAgents
    {
        //Sample useragent strings to perform Levenshtien distance against
        public static readonly IDictionary<string, DeviceConfiguration> MobileComparisonList = new Dictionary<string, DeviceConfiguration>
                    {
                        {"Mozilla/5.0 (compatible; MSIE 9.0; Windows Phone OS 7.5; Trident/5.0; IEMobile/9.0; <manufacturer>; <model> [;<operator])", new DeviceConfiguration(DeviceOs.WindowsPhone, true, false)},
                        {"Mozilla/5.0 (Linux; U; Android 2.3.3; en-us; <model> Build/<build>) AppleWebKit/533.1 (KHTML, like Gecko) Version/4.0 Mobile Safari/533.1", new DeviceConfiguration(DeviceOs.Android, true, false)},
                        {"Mozilla/5.0 (Linux; U; Android 2.2; xx-xx; SGH-XXXXX Build/FROYO) AppleWebKit/533.1 (KHTML, like Gecko) Version/4.0 Mobile Safari/533.1", new DeviceConfiguration(DeviceOs.Android, true, false)},
                        {"Mozilla/5.0 (BlackBerry; U; BlackBerry 9900; en) AppleWebKit/534.11+ (KHTML, like Gecko) Version/7.1.0.346 Mobile Safari/534.11+", new DeviceConfiguration(DeviceOs.BlackBerry, true, false)},
                        {"BlackBerry8110/4.3.0 Profile/MIDP-2.0 Configuration/CLDC-1.1 VendorID/118", new DeviceConfiguration(DeviceOs.BlackBerry, true, false)},
                        {"Opera/9.80 (J2ME/MIDP; Opera Mini/9 (Compatible; [platform]; AppleWebKit/24.746; U; en) Presto/2.5.25 Version/10.54", new DeviceConfiguration(DeviceOs.Other, true, false)},
                        {"Opera/9.50 (J2ME/MIDP; Opera Mini/4.0.10031/298; U; en)", new DeviceConfiguration(DeviceOs.Other, true, false)},
                        {"Mozilla/5.0 (Maemo; Linux armv7l; rv:10.0.1) Gecko/20100101 Firefox/10.0.1 Fennec/10.0.1", new DeviceConfiguration(DeviceOs.Other, true, false)},
                        {"Mozilla/5.0 (Android; Mobile; rv:10.0.5) Gecko/10.0.5 Firefox/10.0.5 Fennec/10.0.5", new DeviceConfiguration(DeviceOs.Android, true, false)},
                        {"Mozilla/5.0 (iPhone; U; CPU iOS 2_0 like Mac OS X; en-us) AppleWebKit/525.18.1 (KHTML, like Gecko) Version/3.1.1 Mobile/XXXXX Safari/525.20", new DeviceConfiguration(DeviceOs.iOS, true, false)},
                        {"Cricket-A410/1.0 Polaris/v6.17", new DeviceConfiguration(DeviceOs.Other, true, false)},
                        {"MOT-V9mm/00.62 UP.Browser/6.2.3.4.c.1.123 (GUI) MMP/2.0", new DeviceConfiguration(DeviceOs.Other, true, false)},
                        {"PalmCentro/v0001 Mozilla/4.0 (compatible; MSIE 6.0; Windows 98; PalmSource/Palm-D061; Blazer/4.5) 16;320×320", new DeviceConfiguration(DeviceOs.Other, true, false)},
                        {"Mozilla/5.0 (webOS/1.0; U; en-US) AppleWebKit/525.27.1 (KHTML, like Gecko) Version/1.0 Safari/525.27.1 Pre/1.0", new DeviceConfiguration(DeviceOs.Other, true, false)},
                        {"HTC-ST7377/1.59.502.3 (67150) Opera/9.50 (Windows NT 5.1; U; en) UP.Link/6.3.1.17.0", new DeviceConfiguration(DeviceOs.Other, true, false)},
                        {"Mozilla/5.0 (Windows; U; Windows NT 5.1; en-US; BOLT/2.800) AppleWebKit/534.6 (KHTML, like Gecko) Version/5.0 Safari/534.6.3", new DeviceConfiguration(DeviceOs.Other, true, false)},
                        {"Mozilla/5.0 (Danger hiptop 3.4; U; AvantGo 3.2)", new DeviceConfiguration(DeviceOs.Other, true, false)}
                    };

        //use this list for the exceptions where tablet UA string doesn't fall into generic convensions
        public static readonly IDictionary<string, DeviceConfiguration> TabletOverrideComparisonList = new Dictionary<string, DeviceConfiguration>
                    {
                        //galaxy tab 7
                        {"Mozilla/5.0 (Linux; U; Android 2.2; xx-xx; GT-XXXXX Build/FROYO) AppleWebKit/533.1 (KHTML, like Gecko) Version/4.0 Mobile Safari/533.1", new DeviceConfiguration(DeviceOs.Android, true, true)},
                        {"Mozilla/5.0 (Linux; U; Android 2.2; xx-xx; SHW-XXXXX Build/FROYO) AppleWebKit/525.10 (KHTML, like Gecko) Version/3.0.4 Mobile Safari/523.12.2", new DeviceConfiguration(DeviceOs.Android, true, true)},
                        {"Mozilla/5.0 (Android; Linux armv7l; rv:2.1) Gecko/20110318 Firefox/4.0b13pre Fennec/4.0", new DeviceConfiguration(DeviceOs.Android, true, true)},
                        //windows8
                        {"Mozilla/5.0 (compatible; MSIE 10.0; Windows NT 6.2; WOW64; Trident/6.0; Touch)", new DeviceConfiguration(DeviceOs.Windows8, false, true)}
                    };
    }
}