namespace ZeroProximity.DeviceDetection
{
    public class MatchingDevice
    {
        public DeviceOs MostLikelyDeviceOs { get; set; }
        public bool IsMobile { get; set; }
        public bool IsTablet { get; set; }
    }
}