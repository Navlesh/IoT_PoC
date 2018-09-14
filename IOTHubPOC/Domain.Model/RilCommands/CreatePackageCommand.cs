using IOTMockDataSerializer.Models.Common;
using System;

namespace IOTMockDataSerializer.Models
{
    public class CreatePackageCommand
    {
        public string PackageMetaData { get; set; } = string.Empty;
        public Guid SourceSystemPackageID { get; set; } = Guid.NewGuid();
        public string URI { get; set; } = string.Empty;
        public string NEW_URI { get; set; } = string.Empty;
        public CommandMain Command { get; set; } = new CommandMain();
    }
}
