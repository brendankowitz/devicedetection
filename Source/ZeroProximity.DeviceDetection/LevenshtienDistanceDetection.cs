using System;
using System.Collections.Generic;
using System.Linq;
using ZeroProximity.DeviceDetection.Defaults;

namespace ZeroProximity.DeviceDetection
{
    public class LevenshtienDistanceDetection : IMobileDetection
    {
        private readonly DetectionOptions _options;
        private readonly IDictionary<string, DeviceType> _mobileDeviceComparisonList;
        private readonly IDictionary<string, DeviceType> _mobileTabletOverrideComparisonList;

        public LevenshtienDistanceDetection(DetectionOptions options = null)
        {
            _options = options ?? new DetectionOptions();
            _mobileDeviceComparisonList = MobileDetectionUserAgents.MobileComparisonList;
            _mobileTabletOverrideComparisonList = MobileDetectionUserAgents.TabletOverrideComparisonList;
        }

        public LevenshtienDistanceDetection(IDictionary<string, DeviceType> mobileUserAgentStrings,
            IDictionary<string, DeviceType> tabletOverrideUserAgentStrings, DetectionOptions options = null)
        {
            _options = options ?? new DetectionOptions();
            _mobileDeviceComparisonList = mobileUserAgentStrings;
            _mobileTabletOverrideComparisonList = tabletOverrideUserAgentStrings;
        }

        public MatchingDevice Match(string userAgent)
        {
            var userAgentLower = userAgent.ToLower();
            var result = new MatchingDevice{ IsMobile = false, IsTablet = false, MostLikelyDeviceOs = DeviceType.Unknown };

            if (_options.AllowEarlyExitForDesktopBrowsers)
            {
                //exit early for desktop browsers
                if (((userAgentLower.Contains("windows nt") && !userAgentLower.Contains("up.link") &&
                      !userAgentLower.Contains("bolt")) ||
                     userAgentLower.Contains("macintosh") ||
                     userAgentLower.Contains("x11")) && !userAgentLower.Contains("qt/"))
                {
                    return result;
                }
            }

            if (_options.AllowGenericChecks)
            {
                //most likely an android phone
                //http://dev.opera.com/articles/view/opera-mobile-emulator/
                if ((userAgentLower.Contains("mobile") && userAgentLower.Contains("android")) ||
                    (userAgentLower.Contains("opera mobi") && userAgentLower.Contains("android")))
                {
                    result.IsMobile = true;
                    result.MostLikelyDeviceOs = DeviceType.Android;
                }
                else if (userAgentLower.Contains("android"))
                {
                    //android tablets have the keyword "android" but not "mobile"
                    result.IsTablet = true;
                    result.IsMobile = true;
                    result.MostLikelyDeviceOs = DeviceType.Android;
                }

                //ipad
                if (userAgentLower.Contains("apple") && userAgentLower.Contains("ipad"))
                {
                    result.IsMobile = true;
                    result.IsTablet = true;
                    result.MostLikelyDeviceOs = DeviceType.iOS;
                    return result;
                }

                //generic checks
                if (userAgentLower.Contains("tablet"))
                {
                    result.IsTablet = true;
                }
                if (userAgentLower.Contains("mobile"))
                {
                    result.IsMobile = true;
                }
            }

            //guess device type
            var resultWalk = new List<KeyValuePair<DeviceType, int>>();
            foreach(var pair in _mobileDeviceComparisonList)
            {
                var distance = Distance(pair.Key.ToLower(), userAgentLower);
                resultWalk.Add(new KeyValuePair<DeviceType, int>(pair.Value, distance));
            }
            var resultTabletWalk = new List<KeyValuePair<DeviceType, int>>();
            foreach (var pair in _mobileTabletOverrideComparisonList)
            {
                var distance = Distance(pair.Key.ToLower(), userAgentLower);
                resultTabletWalk.Add(new KeyValuePair<DeviceType, int>(pair.Value, distance));
            }

            var bestMobile = resultWalk.FirstOrDefault(x => x.Value == resultWalk.Min(y => y.Value));
            if (bestMobile.Value < userAgentLower.Length / 2)
            {
                result.IsMobile = true;
                result.MostLikelyDeviceOs = bestMobile.Key;

                var bestTablet = resultTabletWalk.FirstOrDefault(x => x.Value == resultTabletWalk.Min(y => y.Value));
                if (bestTablet.Value < bestMobile.Value && bestTablet.Value < 10)
                {
                    result.MostLikelyDeviceOs = bestTablet.Key;
                    result.IsTablet = true;
                }
            }

            return result;
        }

        private static int Distance(string row, string col)
        {
            var rowLength = row.Length;
            var columnLength = col.Length;

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

            for (var rowIndex = 1; rowIndex <= rowLength; rowIndex++)
            {
                v0[rowIndex] = rowIndex;
            }

            for (var columnIndex = 1; columnIndex <= columnLength; columnIndex++)
            {
                v1[0] = columnIndex;
                int columnOrdinal = col[columnIndex - 1];

                for (var rowIndex = 1; rowIndex <= rowLength; rowIndex++)
                {
                    var rowOrdinal = row[rowIndex - 1];
                    var cost = rowOrdinal == columnOrdinal ? 0 : 1;

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