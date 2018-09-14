using System;

namespace IOTMockDataSerializer.Models.Common
{
    public class CommandMain
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string[] TargetExecutorId { get; set; } = new string[] { "REF^07269234001^SCL75549" };
        public AckModalitie AckModalities { get; set; } = new AckModalitie();
    }

    public class CommandMain2
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string[] TargetExecutorId { get; set; } = new string[] { "REF^07269234001^SCL75549" };
        public AckModalitie DerivedCommandAckModalities { get; set; } = new AckModalitie();
    }

    public class Command : CommandMain
    {
        public string Timestamp { get; set; } = "2016-10-06T15:32:05.257809Z";
        public string CreatorId { get; set; } = "BA-REGCONF";
        public string ProcessingMode { get; set; } = "PRODUCTION";
        public string Token { get; set; } = "eyJ0eXAiOiJKV1QiLCJhbGciOiJIUzI1NiJ9.eyJpc3MiOiJSb2NoZSIsImlhdCI6MTUxMDAxNjc3OSwiZXhwIjoxNTQxNTUyNzc5LCJhdWQiOiJyaWwucm9jaGVhcGlzLmNvbSIsInN1YiI6IlJleGlzIiwiUlVESSI6IlJFWElTLVBST0QtMSJ9.wy2GSbceSzi6Vhj81kkMMFZItfjPRW359ZPethMY6Lo";
    }
}
