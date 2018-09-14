using IOTMockDataSerializer.Models.Common;

namespace IOTMockDataSerializer.Models
{
    public class RegisterDeviceCommand
    {
        public Command Command { get; set; } = new Command();
        public string Country { get; set; } = "US";
    }
}
