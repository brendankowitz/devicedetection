namespace ZeroProximity.DeviceDetection
{
    public interface IMobileDetection
    {
        MatchingDevice Match(string userAgent);
    }
}