Mobile Device Detection
===============
Available on nuget: 
https://nuget.org/packages/ZeroProximity.DeviceDetection

Allows simple, fast detection of mobile devices giving you what you need to know to redirect to a mobile or tablet site.

Usage:

IMobileDeviceDetection _deviceDeviceDetection = DeviceDetectionFactory.GetDefaultImplementation();
var result = _deviceDeviceDetection.Match("Mozilla/4.0 (compatible; MSIE 7.0; Windows Phone OS 7.0; Trident/3.1; IEMobile/7.0; DELL; Venue Pro)");

Assert.AreEqual(DeviceOs.WindowsPhone, result.MostLikelyDeviceOs);
Assert.AreEqual(true, result.IsMobile);
Assert.AreEqual(false, result.IsTablet);
