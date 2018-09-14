using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Azure.Devices.Client;
using Newtonsoft.Json;

namespace SimulatedDevice
{
    class Program
    {
        static DeviceClient deviceClient;
        static string iotHubUri = "masiot.azure-devices.net";
        static string deviceKey = "p7TG4a6XPSL61PO7Ih/NFVJToYdJpcN5fOde8cE5ZqY=";
        private static string deviceId = "dev1";
        
        static void Main(string[] args)
        {
            try
            {
                iotHubUri = ConfigurationManager.AppSettings["iotHubUri"];
                deviceKey = ConfigurationManager.AppSettings["deviceKey"];
                deviceId = ConfigurationManager.AppSettings["deviceId"];

                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("Receive messages C2D \n");
                Console.ResetColor();

                Console.WriteLine("iotHubUri : " + iotHubUri);
                Console.WriteLine("deviceKey : " + deviceKey);
                Console.WriteLine("deviceId : " + deviceId);

                deviceClient = DeviceClient.Create(iotHubUri, new DeviceAuthenticationWithRegistrySymmetricKey(deviceId, deviceKey), TransportType.Http1);
                deviceClient.ProductInfo = "Simulated-DotNet-CSharp";
                ReceiveC2dAsync();
                Console.ReadLine();
            }
            catch (Exception exp)
            {
                Console.WriteLine(exp.Message);
            }

        }

        private static async void ReceiveC2dAsync()
        {
            Console.WriteLine("\nReceiving cloud to device messages from service");
            while (true)
            {
                Message receivedMessage = await deviceClient.ReceiveAsync();
                if (receivedMessage == null) continue;

                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("Received message: {0}", Encoding.ASCII.GetString(receivedMessage.GetBytes()));
                Console.ResetColor();

                await deviceClient.CompleteAsync(receivedMessage);
            }
        }
    }
}
