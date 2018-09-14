using IOTMockDataSerializer.Models.Common;
using System;

namespace IOTMockDataSerializer.Models
{
    public class GetDataCommand
    {
        public Guid DataID { get; set; } = Guid.NewGuid();
        public CommandMain Command { get; set; } = new CommandMain();
    }
}
