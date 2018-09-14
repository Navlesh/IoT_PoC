using System.Collections.Generic;

namespace IOTMockDataSerializer.Models.Common
{
    public class Device
    {
        public string MaterialNo { get; set; } = "0123456789";
        public string Type { get; set; } = "c600x";
        public string RoutingType { get; set; } = "c600x";
        public string GTIN { get; set; } = "0401560938629";
        public string Revision { get; set; } = "A";
        public string SerialNo { get; set; } = "1234-12";
        public string UDI { get; set; } = "00875197004885";
        public string Manufacturer { get; set; } = "Roche Diagnostics GmbH, Mannheim, Germany";
        public string LotNo { get; set; } = "23b";

        public List<Module> Module { get; set; } = new List<Module>() { new Module() };
    }
}
