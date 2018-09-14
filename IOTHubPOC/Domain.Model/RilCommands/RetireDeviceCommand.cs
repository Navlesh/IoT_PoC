using IOTMockDataSerializer.Models.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IOTMockDataSerializer.Models
{
    public class RetireDeviceCommand
    {
        public CommandMain Command { get; set; } = new CommandMain();
    }
}
