using IOTMockDataSerializer.Models.Common;

namespace IOTMockDataSerializer.Models
{
    public class StoragePathRequestCommand
    {
        public string PackageMetaData { get; set; }
        public CommandMain Command { get; set; } = new CommandMain();
    }
}
