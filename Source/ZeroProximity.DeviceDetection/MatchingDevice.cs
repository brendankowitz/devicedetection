namespace ZeroProximity.DeviceDetection
{
    public class MatchingDevice
    {
        public DeviceType MostLikelyDeviceOs { get; set; }
        public bool IsMobile { get; set; }
        public bool IsTablet { get; set; }
    }
}