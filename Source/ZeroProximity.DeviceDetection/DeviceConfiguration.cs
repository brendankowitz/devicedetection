namespace ZeroProximity.DeviceDetection
{
    public class DeviceConfiguration
    {
        public DeviceConfiguration(DeviceOs deviceOs, bool isMobile, bool isTabletOrTouchEnabled)
        {
            DeviceOs = deviceOs;
            IsMobile = isMobile;
            IsTabletOrTouchEnabled = isTabletOrTouchEnabled;
        }

        public DeviceOs DeviceOs { get; set; }
        public bool IsMobile { get; set; }
        public bool IsTabletOrTouchEnabled { get; set; }
    }
}