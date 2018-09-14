using System;
using System.Configuration;
using System.Text;
using System.Threading.Tasks;
using Domain.Model;
using Microsoft.Azure.Devices;
using Newtonsoft.Json;

namespace SendCloudToDevice
{
    class Program
    {
        static ServiceClient serviceClient;
        static string connectionString = "HostName=masiot.azure-devices.net;SharedAccessKeyName=iothubowner;SharedAccessKey=p7TG4a6XPSL61PO7Ih/NFVJToYdJpcN5fOde8cE5ZqY=";
        private static string deviceId = "dev1";
        static int i = 1;

        static void Main(string[] args)
        {
            try
            {
                connectionString = ConfigurationManager.AppSettings["connStr"];
                deviceId = ConfigurationManager.AppSettings["deviceId"];
                Console.WriteLine("Sending Cloud-to-Device message\n");
                Console.WriteLine("Conn Str : " + connectionString);
                Console.WriteLine("Device Id : " + deviceId);
                serviceClient = ServiceClient.CreateFromConnectionString(connectionString);

                while (true)
                {
                    SendCloudToDeviceMessageAsync().Wait();
                }
            }
            catch (Exception exp)
            {
                var ex = JsonConvert.SerializeObject(exp);
                Console.WriteLine(ex);
            }
        }

        private async static Task SendCloudToDeviceMessageAsync()
        {
            double min = 600;
            Random rand = new Random();
            double rudiRandom = Convert.ToInt32(min + rand.NextDouble() * 10000);

            var telemetryDataPoint = new
            {
                GetPackageList = new SampleMessage() { RUDI = "A12Z" + rudiRandom }
            };
            var messageString = JsonConvert.SerializeObject(telemetryDataPoint);

            var commandMessage = new Message(Encoding.ASCII.GetBytes(messageString));
            Console.WriteLine("Sending msg : " + messageString);
            await serviceClient.SendAsync(deviceId, commandMessage);

            var telemetryDataPoint2 = new
            {
                GWAHeartBeat = new SampleMessage() { RUDI = "A12Z" + rudiRandom }
            };
            messageString = JsonConvert.SerializeObject(telemetryDataPoint2);

            commandMessage = new Message(Encoding.ASCII.GetBytes(messageString));
            Console.WriteLine("Sending msg : " + messageString);
            await serviceClient.SendAsync(deviceId, commandMessage);
        }
    }
}
