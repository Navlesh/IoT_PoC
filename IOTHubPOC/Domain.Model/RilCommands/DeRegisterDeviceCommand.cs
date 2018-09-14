using IOTMockDataSerializer.Models.Common;

namespace IOTMockDataSerializer.Models
{
    public class DeRegisterDeviceCommand
    {
        public Command Command { get; set; }
        public string Country { get; set; }
    }
}
