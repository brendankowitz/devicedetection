namespace ZeroProximity.DeviceDetection
{
    public class DetectionOptions
    {
        public DetectionOptions()
        {
            AllowGenericChecks = true;
            AllowEarlyExitForDesktopBrowsers = true;
        }

        public bool AllowGenericChecks { get; set; }
        public bool AllowEarlyExitForDesktopBrowsers { get; set; }
    }
}