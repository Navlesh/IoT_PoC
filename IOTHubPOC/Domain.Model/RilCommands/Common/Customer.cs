namespace IOTMockDataSerializer.Models.Common
{
    public class Customer
    {
        public string CustomerId { get; set; } = "123456";
        public string CustomerName { get; set; } = "Any Roche Customer";
        public string LaboratoryId { get; set; } = "3245689";
        public string LaboratoryName { get; set; } = "Any Roche Laboratory";
        public string Country { get; set; } = "DK";
        public string City { get; set; } = "Copenhagen";
    }
}
