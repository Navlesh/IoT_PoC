using IOTMockDataSerializer.Models.Common;

namespace IOTMockDataSerializer.Models
{
    public class StopApplicationTunnelingCommand
    {
        public string Application { get; set; } = "RDP";
        public string Host { get; set; } = "IP v4 or IP v6";
        public string Port { get; set; } = "TCP port";
        public Parameter Parameter { get; set; } = new Parameter();

        public CommandMain Command { get; set; } = new CommandMain();
    }
}
