namespace IOTHub.Web.Api.Model
{
    public class DeviceIdentityResponseModel
    {
        public Cmd GwaRegistrationResponseCommand { get; set; } = new Cmd();
    }

    public class Cmd
    {
        public int MessageID { get; set; } = 101;
        public string ApplicationID { get; set; } = "ap001";
        public string Rudi { get; set; }
        public string AuthCode { get; set; } = "12GH-76YU-UI34";
        public string Status { get; set; } = "Registred";
        public string TimeStamp { get; set; } = "2018-02-15T23:42:03.522Z";
        public string ConnectionString { get; set; }
    }
}
