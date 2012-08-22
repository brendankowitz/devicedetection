namespace ZeroProximity.DeviceDetection
{
    public interface IMobileDeviceDetection
    {
        MatchingDevice Match(string userAgent);
    }
}