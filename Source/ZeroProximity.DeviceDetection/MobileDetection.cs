using System;
using System.Collections.Generic;
using System.Linq;

namespace ZeroProximity.DeviceDetection
{
    public enum DeviceType
    {
        iOS,
        WindowsPhone,
        BlackBerry,
        Android,
        Other
    }

    public class MatchingDevice
    {
        public DeviceType MostLikelyDeviceType { get; set; }
        public bool IsMobile { get; set; }
        public bool IsTablet { get; set; }
    }

    public class MobileDetection
    {
        //Sample useragent strings to perform Levenshtien distance against
        private readonly Dictionary<string, DeviceType> _mobileDeviceComparisonList = new Dictionary<string, DeviceType>
            {
                {"Mozilla/5.0 (compatible; MSIE 9.0; Windows Phone OS 7.5; Trident/5.0; IEMobile/9.0; <manufacturer>; <model> [;<operator])", DeviceType.WindowsPhone},
                {"Mozilla/5.0 (Linux; U; Android 2.3.3; en-us; <model> Build/<build>) AppleWebKit/533.1 (KHTML, like Gecko) Version/4.0 Mobile Safari/533.1", DeviceType.Android},
                {"Mozilla/5.0 (Linux; U; Android 2.2; xx-xx; SGH-XXXXX Build/FROYO) AppleWebKit/533.1 (KHTML, like Gecko) Version/4.0 Mobile Safari/533.1", DeviceType.Android},
                {"Mozilla/5.0 (BlackBerry; U; BlackBerry 9900; en) AppleWebKit/534.11+ (KHTML, like Gecko) Version/7.1.0.346 Mobile Safari/534.11+", DeviceType.BlackBerry },
                {"BlackBerry8110/4.3.0 Profile/MIDP-2.0 Configuration/CLDC-1.1 VendorID/118", DeviceType.BlackBerry },
                {"Opera/9.80 (J2ME/MIDP; Opera Mini/9 (Compatible; [platform]; AppleWebKit/24.746; U; en) Presto/2.5.25 Version/10.54", DeviceType.Other},
                {"Opera/9.50 (J2ME/MIDP; Opera Mini/4.0.10031/298; U; en)", DeviceType.Other},
                {"Mozilla/5.0 (Maemo; Linux armv7l; rv:10.0.1) Gecko/20100101 Firefox/10.0.1 Fennec/10.0.1", DeviceType.Other},
                {"Mozilla/5.0 (Android; Mobile; rv:10.0.5) Gecko/10.0.5 Firefox/10.0.5 Fennec/10.0.5", DeviceType.Android},
                {"Mozilla/5.0 (iPhone; U; CPU iOS 2_0 like Mac OS X; en-us) AppleWebKit/525.18.1 (KHTML, like Gecko) Version/3.1.1 Mobile/XXXXX Safari/525.20", DeviceType.iOS},
                {"Cricket-A410/1.0 Polaris/v6.17", DeviceType.Other},
                {"MOT-V9mm/00.62 UP.Browser/6.2.3.4.c.1.123 (GUI) MMP/2.0", DeviceType.Other},
                {"PalmCentro/v0001 Mozilla/4.0 (compatible; MSIE 6.0; Windows 98; PalmSource/Palm-D061; Blazer/4.5) 16;320×320", DeviceType.Other},
                {"Mozilla/5.0 (webOS/1.0; U; en-US) AppleWebKit/525.27.1 (KHTML, like Gecko) Version/1.0 Safari/525.27.1 Pre/1.0", DeviceType.Other},
                {"HTC-ST7377/1.59.502.3 (67150) Opera/9.50 (Windows NT 5.1; U; en) UP.Link/6.3.1.17.0", DeviceType.Other},
                {"Mozilla/5.0 (Windows; U; Windows NT 5.1; en-US; BOLT/2.800) AppleWebKit/534.6 (KHTML, like Gecko) Version/5.0 Safari/534.6.3", DeviceType.Other},
                {"Mozilla/5.0 (Danger hiptop 3.4; U; AvantGo 3.2)", DeviceType.Other}
            };

        //use this list for the exceptions where tablet UA string doesn't fall into generic convensions
        private readonly Dictionary<string, DeviceType> _mobileTabletComparisonList = new Dictionary<string, DeviceType>
            {
                //galaxy tab 7
                {"Mozilla/5.0 (Linux; U; Android 2.2; xx-xx; GT-XXXXX Build/FROYO) AppleWebKit/533.1 (KHTML, like Gecko) Version/4.0 Mobile Safari/533.1", DeviceType.Android},
                {"Mozilla/5.0 (Linux; U; Android 2.2; xx-xx; SHW-XXXXX Build/FROYO) AppleWebKit/525.10 (KHTML, like Gecko) Version/3.0.4 Mobile Safari/523.12.2", DeviceType.Android},
                {"Mozilla/5.0 (Android; Linux armv7l; rv:2.1) Gecko/20110318 Firefox/4.0b13pre Fennec/4.0", DeviceType.Android}
            };


        public MatchingDevice Match(string userAgent)
        {
            var userAgentLower = userAgent.ToLower();
            var result = new MatchingDevice{ IsMobile = false, IsTablet = false, MostLikelyDeviceType = DeviceType.Other };

            //exit early for desktop browsers
            if ((userAgentLower.Contains("windows nt") && !userAgentLower.Contains("up.link") && !userAgentLower.Contains("bolt") ||
                    userAgentLower.Contains("macintosh") ||
                    userAgentLower.Contains("x11")) && !userAgentLower.Contains("qt/"))
            {
                return result;
            }

            //most likely an android phone
            //http://dev.opera.com/articles/view/opera-mobile-emulator/
            if ((userAgentLower.Contains("mobile") && userAgentLower.Contains("android")) ||
                (userAgentLower.Contains("opera mobi") && userAgentLower.Contains("android"))
                )
            {
                result.IsMobile = true;
                result.MostLikelyDeviceType = DeviceType.Android;
            }
            else if (userAgentLower.Contains("android"))
            { //android tablets have the keyword "android" but not "mobile"
                result.IsTablet = true;
                result.IsMobile = true;
                result.MostLikelyDeviceType = DeviceType.Android;
            }

            //ipad
            if (userAgentLower.Contains("apple") && userAgentLower.Contains("ipad"))
            {
                result.IsMobile = true;
                result.IsTablet = true;
                result.MostLikelyDeviceType = DeviceType.iOS;
                return result;
            }

            //generic checks
            if(userAgentLower.Contains("tablet"))
            {
                result.IsTablet = true;
            }
            if (userAgentLower.Contains("mobile"))
            {
                result.IsMobile = true;
            }

            //guess device type
            var resultWalk = new List<KeyValuePair<DeviceType, int>>();
            foreach(var pair in _mobileDeviceComparisonList)
            {
                var distance = Distance(pair.Key.ToLower(), userAgentLower);
                resultWalk.Add(new KeyValuePair<DeviceType, int>(pair.Value, distance));
            }
            var resultTabletWalk = new List<KeyValuePair<DeviceType, int>>();
            foreach (var pair in _mobileTabletComparisonList)
            {
                var distance = Distance(pair.Key.ToLower(), userAgentLower);
                resultTabletWalk.Add(new KeyValuePair<DeviceType, int>(pair.Value, distance));
            }

            var bestMobile = resultWalk.FirstOrDefault(x => x.Value == resultWalk.Min(y => y.Value));
            if (bestMobile.Value < userAgentLower.Length / 2)
            {
                result.IsMobile = true;
                result.MostLikelyDeviceType = bestMobile.Key;

                var bestTablet = resultTabletWalk.FirstOrDefault(x => x.Value == resultTabletWalk.Min(y => y.Value));
                if (bestTablet.Value < bestMobile.Value && bestTablet.Value < 10)
                {
                    result.MostLikelyDeviceType = bestTablet.Key;
                    result.IsTablet = true;
                }
            }

            return result;
        }

        private static int Distance(string row, string col)
        {
            int rowLength = row.Length;
            int columnLength = col.Length;

            if (Math.Max(row.Length, col.Length) > Math.Pow(2, 31))
                throw (new Exception("\nMaximum string length for distance calculation is " + Math.Pow(2, 31) + ".\nYours is " + Math.Max(row.Length, col.Length) + "."));

            if (rowLength == 0)
            {
                return columnLength;
            }

            if (columnLength == 0)
            {
                return rowLength;
            }

            var v0 = new int[rowLength + 1];
            var v1 = new int[rowLength + 1];

            int rowOrdinal;
            int columnOrdinal;
            int cost;

            for (var rowIndex = 1; rowIndex <= rowLength; rowIndex++)
            {
                v0[rowIndex] = rowIndex;
            }

            for (var columnIndex = 1; columnIndex <= columnLength; columnIndex++)
            {
                v1[0] = columnIndex;
                columnOrdinal = col[columnIndex - 1];

                for (var rowIndex = 1; rowIndex <= rowLength; rowIndex++)
                {
                    rowOrdinal = row[rowIndex - 1];

                    cost = rowOrdinal == columnOrdinal ? 0 : 1;

                    var minimum = v0[rowIndex] + 1;
                    var b = v1[rowIndex - 1] + 1;
                    var c = v0[rowIndex - 1] + cost;

                    if (b < minimum)
                    {
                        minimum = b;
                    }
                    if (c < minimum)
                    {
                        minimum = c;
                    }

                    v1[rowIndex] = minimum;
                }

                var vTmp = v0;
                v0 = v1;
                v1 = vTmp;
            }

            var max = Math.Max(rowLength, columnLength);
            return ((100 * v0[rowLength]) / max);
        }
    }
}