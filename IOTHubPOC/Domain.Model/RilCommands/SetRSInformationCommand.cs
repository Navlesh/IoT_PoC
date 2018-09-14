using IOTMockDataSerializer.Models.Common;

namespace IOTMockDataSerializer.Models
{
    public class SetRSInformationCommand
    {
        public Command Command { get; set; } = new Command();
        public RSInformation RSInformation { get; set; } = new RSInformation();
    }
}
