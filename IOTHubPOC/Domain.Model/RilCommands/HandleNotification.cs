using System;

namespace IOTMockDataSerializer.Models
{
    public class HandleNotification
    {
        public Guid MessageID { get; set; } = Guid.NewGuid();
        public string TimeStamp { get; set; } = "2017-12-20T14:05:08.373236Z";
        public string CreatorID { get; set; } = "Creator Application ID";
        public string ReceiverID { get; set; } = "Subscriber Application ID";
        public string MessageCode { get; set; } = "Error Message code or Warning code";
        public string Message { get; set; } = "Error Message or Warning Messages";
        public Parameter Supplementary { get; set; } = new Parameter();
    }
}
