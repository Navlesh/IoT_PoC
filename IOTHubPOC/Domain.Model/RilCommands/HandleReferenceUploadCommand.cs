using IOTMockDataSerializer.Models.Common;

namespace IOTMockDataSerializer.Models
{
    public class HandleReferenceUploadCommand
    {
        public CommandMain Command { get; set; } = new CommandMain();
        public string PackageLocation { get; set; } = string.Empty;
        public string PackageMetadata { get; set; } = string.Empty;
        public string New_PackageLocation { get; set; } = string.Empty;
    }
}
