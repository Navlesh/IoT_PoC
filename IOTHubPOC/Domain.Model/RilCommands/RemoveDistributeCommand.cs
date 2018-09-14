using IOTMockDataSerializer.Models.Common;
using System;

namespace IOTMockDataSerializer.Models
{
    public class RemoveDistributeCommand
    {
        public Guid SourceSystemPackageID { get; set; } = Guid.NewGuid();
        public string ReleaseMetadata { get; set; } = string.Empty;
        public CommandMain2 Command { get; set; } = new CommandMain2();
    }
}
