namespace IOTHub.Web.Api.Model
{
    public class CreateDeviceRequestModel
    {
        public int MessageID { get; set; }
        public string ApplicationID { get; set; }
        public string Rudi { get; set; }
        public string Thumbprint { get; set; }
        public string Certificate { get; set; }
        public string TimeStamp { get; set; }
    }
}
