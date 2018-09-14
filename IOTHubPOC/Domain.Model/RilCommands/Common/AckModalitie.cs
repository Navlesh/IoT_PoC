namespace IOTMockDataSerializer.Models.Common
{
    public class AckModalitie
    {
        public AckModalitieProperty Received { get; set; } = new AckModalitieProperty()
        {
            Condition = "ALWAYS",
            Timeout = "PT2H"
        };
        public AckModalitieProperty Validated { get; set; } = new AckModalitieProperty()
        {
            Condition = "NEVER",
            Timeout = "PT0S"
        };
        public AckModalitieProperty Executed { get; set; } = new AckModalitieProperty()
        {
            Condition = "ON_SUCCESS",
            Timeout = "PT2H"
        };
    }

    public class AckModalitieProperty
    {
        public string Condition { get; set; }
        public string Timeout { get; set; }
    }
}
