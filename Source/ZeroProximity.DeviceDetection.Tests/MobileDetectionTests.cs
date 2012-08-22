using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ZeroProximity.DeviceDetection.Tests
{
    //tests based on:
    //http://www.justindocanto.com/scripts/detect-a-user-on-a-mobile-browser-or-device

    [TestClass]
    public class MobileDetectionTests
    {
        private readonly IMobileDeviceDetection _deviceDeviceDetection = DeviceDetectionFactory.GetDefaultImplementation();

        [TestMethod]
        public void Is_an_iphone()
        {
            var d =
                _deviceDeviceDetection.Match("Mozilla/5.0 (iPhone; CPU iPhone OS 5_1 like Mac OS X) AppleWebKit/534.46 (KHTML, like Gecko) Version/5.1 Mobile/9B176 Safari/7534.48.3");

            Assert.AreEqual(DeviceOs.iOS, d.MostLikelyDeviceOs);
            Assert.AreEqual(true, d.IsMobile);
            Assert.AreEqual(false, d.IsTablet);
        }

        [TestMethod]
        public void Is_an_ipad()
        {
            var d =
                _deviceDeviceDetection.Match("Mozilla/5.0 (iPad; U; CPU OS 4_3_1 like Mac OS X; en-us) AppleWebKit/533.17.9 (KHTML, like Gecko) Version/5.0.2 Mobile/8G4 Safari/6533.18.5");

            Assert.AreEqual(DeviceOs.iOS, d.MostLikelyDeviceOs);
            Assert.AreEqual(true, d.IsMobile);
            Assert.AreEqual(true, d.IsTablet);
        }

        [TestMethod]
        public void Is_a_playbook()
        {
            var d =
                _deviceDeviceDetection.Match("Mozilla/5.0 (PlayBook; U; RIM Tablet OS 2.0.1; en-US) AppleWebKit/535.8+ (KHTML, like Gecko) Version/7.2.0.1 Safari/535.8+");

            Assert.AreEqual(DeviceOs.BlackBerry, d.MostLikelyDeviceOs);
            Assert.AreEqual(true, d.IsMobile);
            Assert.AreEqual(true, d.IsTablet);
        }

        [TestMethod]
        public void Is_android_tablet()
        {
            var d =
                _deviceDeviceDetection.Match("Mozilla/5.0 (Linux; U; Android 3.2.1; en-gb; A501 Build/HTK55D) AppleWebKit/534.13 (KHTML, like Gecko) Version/4.0 Safari/534.13");

            Assert.AreEqual(DeviceOs.Android, d.MostLikelyDeviceOs);
            Assert.AreEqual(true, d.IsMobile);
            Assert.AreEqual(true, d.IsTablet);
        }

        [TestMethod]
        public void Is_a_dell_windows_phone()
        {
            var d =
                _deviceDeviceDetection.Match("Mozilla/4.0 (compatible; MSIE 7.0; Windows Phone OS 7.0; Trident/3.1; IEMobile/7.0; DELL; Venue Pro)");

            Assert.AreEqual(DeviceOs.WindowsPhone, d.MostLikelyDeviceOs);
            Assert.AreEqual(true, d.IsMobile);
            Assert.AreEqual(false, d.IsTablet);
        }

        [TestMethod]
        public void Is_dolphin_on_android()
        {
            var d = _deviceDeviceDetection.Match("Mozilla/5.0 (Linux; U; Android 4.0.4; pl-pl; HTC Vision Build/IMM76I; CyanogenMod-meXdroidMod_Fried_IceCream3) AppleWebKit/534.30 (KHTML, like Gecko) Version/4.0 Mobile Safari/534.30");

            Assert.AreEqual(DeviceOs.Android, d.MostLikelyDeviceOs);
            Assert.AreEqual(true, d.IsMobile);
            Assert.AreEqual(false, d.IsTablet);
        }

        [TestMethod]
        public void Is_opera_mobile_on_android()
        {
            var d = _deviceDeviceDetection.Match("Opera/9.80 (Android 4.0.4; Linux; Opera Mobi/ADR-1205181138; U; pl) Presto/2.10.254 Version/12.00");

            Assert.AreEqual(DeviceOs.Android, d.MostLikelyDeviceOs);
            Assert.AreEqual(true, d.IsMobile);
            Assert.AreEqual(false, d.IsTablet);
        }

        [TestMethod]
        public void Is_opera_mobile_on_android_tablet()
        {
            var d = _deviceDeviceDetection.Match("Opera/9.80 (Android 4.0.4; Linux; Opera Tablet/ADR-1205181138; U; pl) Presto/2.10.254 Version/12.00");

            Assert.AreEqual(DeviceOs.Android, d.MostLikelyDeviceOs);
            Assert.AreEqual(true, d.IsMobile);
            Assert.AreEqual(true, d.IsTablet);
        }

        [TestMethod]
        public void Is_firefox_on_n900()
        {
            var d = _deviceDeviceDetection.Match("Mozilla/5.0 (Maemo; Linux armv7l; rv:10.0.1) Gecko/20100101 Firefox/10.0.1 Fennec/10.0.1");

            Assert.AreEqual(DeviceOs.Other, d.MostLikelyDeviceOs);
            Assert.AreEqual(true, d.IsMobile);
            Assert.AreEqual(false, d.IsTablet);
        }

        [TestMethod]
        public void Is_legacy_firefox_on_tablet()
        {
            var d = _deviceDeviceDetection.Match("Mozilla/5.0 (Android; Linux armv7l; rv:2.1) Gecko/20110318 Firefox/4.0b13pre Fennec/4.0");

            Assert.AreEqual(DeviceOs.Android, d.MostLikelyDeviceOs);
            Assert.AreEqual(true, d.IsMobile);
            Assert.AreEqual(true, d.IsTablet);
        }

        [TestMethod]
        public void Is_blackberry_pearl()
        {
            var d = _deviceDeviceDetection.Match("BlackBerry8110/4.3.0 Profile/MIDP-2.0 Configuration/CLDC-1.1 VendorID/118");

            Assert.AreEqual(DeviceOs.BlackBerry, d.MostLikelyDeviceOs);
            Assert.AreEqual(true, d.IsMobile);
            Assert.AreEqual(false, d.IsTablet);
        }

        [TestMethod]
        public void Is_blackberry_pearl_opera_mini()
        {
            var d = _deviceDeviceDetection.Match("Opera/9.50 (J2ME/MIDP; Opera Mini/4.0.10031/298; U; en)");

            Assert.AreEqual(DeviceOs.Other, d.MostLikelyDeviceOs);
            Assert.AreEqual(true, d.IsMobile);
            Assert.AreEqual(false, d.IsTablet);
        }

        [TestMethod]
        public void Is_blackberry_tourch()
        {
            var d = _deviceDeviceDetection.Match("Mozilla/5.0 (BlackBerry; U; BlackBerry 9860; en-GB) AppleWebKit/534.11+ (KHTML, like Gecko) Version/7.0.0.296 Mobile Safari/534.11+");

            Assert.AreEqual(DeviceOs.BlackBerry, d.MostLikelyDeviceOs);
            Assert.AreEqual(true, d.IsMobile);
            Assert.AreEqual(false, d.IsTablet);
        }

        [TestMethod]
        public void Is_cricket_a410()
        {
            var d = _deviceDeviceDetection.Match("Cricket-A410/1.0 Polaris/v6.17");

            Assert.AreEqual(DeviceOs.Other, d.MostLikelyDeviceOs);
            Assert.AreEqual(true, d.IsMobile);
            Assert.AreEqual(false, d.IsTablet);
        }

        [TestMethod]
        public void Is_hiptop()
        {
            var d = _deviceDeviceDetection.Match("Mozilla/5.0 (Danger hiptop 3.4; U; AvantGo 3.2)");

            Assert.AreEqual(DeviceOs.Other, d.MostLikelyDeviceOs);
            Assert.AreEqual(true, d.IsMobile);
            Assert.AreEqual(false, d.IsTablet);
        }

        [TestMethod]
        public void Is_palm_treo()
        {
            var d = _deviceDeviceDetection.Match("Mozilla/5.0 (webOS/1.0; U; en-US) AppleWebKit/525.27.1 (KHTML, like Gecko) Version/1.0 Safari/525.27.1 Pre/1.0");

            Assert.AreEqual(DeviceOs.Other, d.MostLikelyDeviceOs);
            Assert.AreEqual(true, d.IsMobile);
            Assert.AreEqual(false, d.IsTablet);
        }

        [TestMethod]
        public void Is_win98_ie6_realplayer()
        {
            var d = _deviceDeviceDetection.Match("Mozilla/4.0 (compatible; MSIE 6.0; Windows 98; Rogers Hi·Speed Internet; (R1 1.3))");

            Assert.AreEqual(DeviceOs.Unknown, d.MostLikelyDeviceOs);
            Assert.AreEqual(false, d.IsMobile);
            Assert.AreEqual(false, d.IsTablet);
        }

        [TestMethod]
        public void Is_htc_touch_pro2()
        {
            var d = _deviceDeviceDetection.Match("HTC-ST7377/1.59.502.3 (67150) Opera/9.50 (Windows NT 5.1; U; en) UP.Link/6.3.1.17.0");

            Assert.AreEqual(DeviceOs.Other, d.MostLikelyDeviceOs);
            Assert.AreEqual(true, d.IsMobile);
            Assert.AreEqual(false, d.IsTablet);
        }

        [TestMethod]
        public void Is_htc_touch_pad()
        {
            var d = _deviceDeviceDetection.Match("Mozilla/5.0 (hp-tablet; Linux; hpwOS/3.0.2; U; en-US) AppleWebKit/534.6 (KHTML, like Gecko) wOSBrowser/234.40.1 Safari/534.6 TouchPad/1.0");

            Assert.AreEqual(DeviceOs.Other, d.MostLikelyDeviceOs);
            Assert.AreEqual(true, d.IsMobile);
            Assert.AreEqual(true, d.IsTablet);
        }

        [TestMethod]
        public void Is_2nd_gen_ipad_touch()
        {
            var d = _deviceDeviceDetection.Match("Mozilla/5.0 (iPod; U; CPU iPhone OS 2_0 like Mac OS X; de-de) AppleWebKit/525.18.1 (KHTML, like Gecko) Version/3.1.1 Mobile/5A347 Safari/525.20");

            Assert.AreEqual(DeviceOs.iOS, d.MostLikelyDeviceOs);
            Assert.AreEqual(true, d.IsMobile);
            Assert.AreEqual(false, d.IsTablet);
        }

        [TestMethod]
        public void Is_lg_optimus_one()
        {
            var d = _deviceDeviceDetection.Match("mozilla/5.0 (linux; u; android 2.3.4; en-us; lg-p500 build/gingerbread) applewebkit/533.1 (khtml, like gecko) version/4.0 mobile safari/533.1");

            Assert.AreEqual(DeviceOs.Android, d.MostLikelyDeviceOs);
            Assert.AreEqual(true, d.IsMobile);
            Assert.AreEqual(false, d.IsTablet);
        }

        [TestMethod]
        public void Is_galaxy_tab_SHW_M180L()
        {
            var d = _deviceDeviceDetection.Match("Mozilla/5.0 (Linux; U; Android 2.2; en-au; SHW-M180L Build/FROYO) AppleWebKit/525.10 (KHTML, like Gecko) Version/3.0.4 Mobile Safari/523.12.2");

            Assert.AreEqual(DeviceOs.Android, d.MostLikelyDeviceOs);
            Assert.AreEqual(true, d.IsMobile);
            Assert.AreEqual(true, d.IsTablet);
        }

        [TestMethod]
        public void Is_galaxy_tab_SHW_M180S()
        {
            var d = _deviceDeviceDetection.Match("Mozilla/5.0 (Linux; U; Android 2.2; xx-xx; SHW-M180S Build/FROYO) AppleWebKit/525.10 (KHTML, like Gecko) Version/3.0.4 Mobile Safari/523.12.2");

            Assert.AreEqual(DeviceOs.Android, d.MostLikelyDeviceOs);
            Assert.AreEqual(true, d.IsMobile);
            Assert.AreEqual(true, d.IsTablet);
        }

        [TestMethod]
        public void Is_galaxy_tab_7()
        {
            var d = _deviceDeviceDetection.Match("Mozilla/5.0 (Linux; U; Android 2.2; en-gb; GT-P1000 Build/FROYO) AppleWebKit/533.1 (KHTML, like Gecko) Version/4.0 Mobile Safari/533.1");

            Assert.AreEqual(DeviceOs.Android, d.MostLikelyDeviceOs);
            Assert.AreEqual(true, d.IsMobile);
            Assert.AreEqual(true, d.IsTablet);
        }

        [TestMethod]
        public void Is_galaxy_s()
        {
            var d = _deviceDeviceDetection.Match("Mozilla/5.0 (Linux; U; Android 2.2; en-ca; SGH-T959D Build/FROYO) AppleWebKit/533.1 (KHTML, like Gecko) Version/4.0 Mobile Safari/533.1");

            Assert.AreEqual(DeviceOs.Android, d.MostLikelyDeviceOs);
            Assert.AreEqual(true, d.IsMobile);
            Assert.AreEqual(false, d.IsTablet);
        }

        [TestMethod]
        public void Is_android_4_1()
        {
            var d = _deviceDeviceDetection.Match("Mozilla/5.0 (Linux; U; Android 4.1.1; de-de; Full Android on Crespo Build/JRO03C) AppleWebKit/534.30 (KHTML, like Gecko) Version/4.0 Mobile Safari/534.30");

            Assert.AreEqual(DeviceOs.Android, d.MostLikelyDeviceOs);
            Assert.AreEqual(true, d.IsMobile);
            Assert.AreEqual(false, d.IsTablet);
        }


        [TestMethod]
        public void Is_seamonkey_on_windows()
        {
            var d = _deviceDeviceDetection.Match("Mozilla/5.0 (Windows NT 5.2; rv:10.0.1) Gecko/20100101 Firefox/10.0.1 SeaMonkey/2.7.1");

            Assert.AreEqual(DeviceOs.Unknown, d.MostLikelyDeviceOs);
            Assert.AreEqual(false, d.IsMobile);
            Assert.AreEqual(false, d.IsTablet);
        }

        [TestMethod]
        public void Is_sony_bolt()
        {
            var d = _deviceDeviceDetection.Match("Mozilla/5.0 (Windows; U; Windows NT 5.1; en-US; BOLT/2.800) AppleWebKit/534.6 (KHTML, like Gecko) Version/5.0 Safari/534.6.3");

            Assert.AreEqual(DeviceOs.Other, d.MostLikelyDeviceOs);
            Assert.AreEqual(true, d.IsMobile);
            Assert.AreEqual(false, d.IsTablet);
        }

        [TestMethod]
        public void Is_razr2()
        {
            var d = _deviceDeviceDetection.Match("MOT-V9mm/00.62 UP.Browser/6.2.3.4.c.1.123 (GUI) MMP/2.0");

            Assert.AreEqual(DeviceOs.Other, d.MostLikelyDeviceOs);
            Assert.AreEqual(true, d.IsMobile);
            Assert.AreEqual(false, d.IsTablet);
        }

        [TestMethod]
        public void Is_motorola_xoom()
        {
            var d = _deviceDeviceDetection.Match("Mozilla/5.0 (Linux; U; Android 3.0; en-us; Xoom Build/HRI39) AppleWebKit/534.13 (KHTML, like Gecko) Version/4.0 Safari/534.13");

            Assert.AreEqual(DeviceOs.Android, d.MostLikelyDeviceOs);
            Assert.AreEqual(true, d.IsMobile);
            Assert.AreEqual(true, d.IsTablet);
        }

        [TestMethod]
        public void Is_linux_mint8()
        {
            var d = _deviceDeviceDetection.Match("Opera/9.80 (X11; Linux x86_64; U; Linux Mint; en) Presto/2.2.15 Version/10.10");

            Assert.AreEqual(DeviceOs.Unknown, d.MostLikelyDeviceOs);
            Assert.AreEqual(false, d.IsMobile);
            Assert.AreEqual(false, d.IsTablet);
        }

        [TestMethod]
        public void Is_chrome_fedora()
        {
            var d = _deviceDeviceDetection.Match("Mozilla/5.0 (X11; U; Linux i686; en-US) AppleWebKit/534.16 (KHTML, like Gecko) Chrome/10.0.648.127 Safari/534.16");

            Assert.AreEqual(DeviceOs.Unknown, d.MostLikelyDeviceOs);
            Assert.AreEqual(false, d.IsMobile);
            Assert.AreEqual(false, d.IsTablet);
        }

        [TestMethod]
        public void Is_win7_ie9()
        {
            var d = _deviceDeviceDetection.Match("Mozilla/5.0 (compatible; MSIE 9.0; Windows NT 6.1; WOW64; Trident/5.0)");

            Assert.AreEqual(DeviceOs.Unknown, d.MostLikelyDeviceOs);
            Assert.AreEqual(false, d.IsMobile);
            Assert.AreEqual(false, d.IsTablet);
        }

        [TestMethod]
        public void Is_winxp_ie8()
        {
            var d = _deviceDeviceDetection.Match("Mozilla/4.0 (compatible; MSIE 8.0; Windows NT 5.1; Trident/4.0; chromeframe/13.0.782.218; chromeframe; .NET CLR 1.1.4322; .NET CLR 2.0.50727; .NET CLR 3.0.04506.30; .NET CLR 3.0.04506.648; .NET CLR 3.5.21022; .NET CLR 3.0.4506.2152; .NET CLR 3.5.30729)");

            Assert.AreEqual(DeviceOs.Unknown, d.MostLikelyDeviceOs);
            Assert.AreEqual(false, d.IsMobile);
            Assert.AreEqual(false, d.IsTablet);
        }

        [TestMethod]
        public void Is_apple_safari()
        {
            var d = _deviceDeviceDetection.Match("Mozilla/5.0 (Macintosh; Intel Mac OS X 10_7_2) AppleWebKit/534.52.7 (KHTML, like Gecko) Version/5.1.2 Safari/534.52.7");

            Assert.AreEqual(DeviceOs.Unknown, d.MostLikelyDeviceOs);
            Assert.AreEqual(false, d.IsMobile);
            Assert.AreEqual(false, d.IsTablet);
        }

        [TestMethod]
        public void Is_apple_chrome13()
        {
            var d = _deviceDeviceDetection.Match("Mozilla/5.0 (Macintosh; Intel Mac OS X 10_7_2) AppleWebKit/535.1 (KHTML, like Gecko) Chrome/13.0.782.215 Safari/535.1");

            Assert.AreEqual(DeviceOs.Unknown, d.MostLikelyDeviceOs);
            Assert.AreEqual(false, d.IsMobile);
            Assert.AreEqual(false, d.IsTablet);
        }

        [TestMethod]
        public void Is_apple_firefox()
        {
            var d = _deviceDeviceDetection.Match("Mozilla/5.0 (Macintosh; Intel Mac OS X 10.7; rv:9.0.1) Gecko/20100101 Firefox/9.0.1");

            Assert.AreEqual(DeviceOs.Unknown, d.MostLikelyDeviceOs);
            Assert.AreEqual(false, d.IsMobile);
            Assert.AreEqual(false, d.IsTablet);
        }
   
    }
}