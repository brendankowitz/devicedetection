namespace ZeroProximity.DeviceDetection
{
    public static class DeviceDetectionFactory
    {
        /// <summary>
        /// Gets the best default device detection implementation
        /// </summary>
        public static IMobileDeviceDetection GetDefaultImplementation()
        {
            return new LevenshtienDistanceDeviceDetection();
        }
    }
}