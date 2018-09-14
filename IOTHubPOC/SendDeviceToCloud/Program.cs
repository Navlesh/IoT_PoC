using System;
using System.Collections.Generic;
using System.Configuration;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Model;
using IOTMockDataSerializer.Models;
using Microsoft.Azure.Devices.Client;
using Newtonsoft.Json;

namespace SendDeviceToCloud
{
    class Program
    {
        private static string IotHubUri; //"rspdeviotreg.azure-devices.net";
        private static string DeviceKey;
        private static string DeviceId;
        private static DeviceClient _deviceClient;

        static void Main(string[] args)
        {
            try
            {
                IotHubUri = ConfigurationManager.AppSettings["IotHubUri"];
                DeviceKey = ConfigurationManager.AppSettings["DeviceKey"];
                DeviceId = ConfigurationManager.AppSettings["DeviceId"];
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Send Device to Cloud : \n");

                Console.ResetColor();
                Console.WriteLine("IotHubUri : " + IotHubUri);
                Console.WriteLine("DeviceId : " + DeviceId);
                Console.WriteLine("DeviceKey : " + DeviceKey);
                Console.WriteLine("\n");

                _deviceClient = DeviceClient.Create(IotHubUri, new DeviceAuthenticationWithRegistrySymmetricKey(DeviceId, DeviceKey), TransportType.Http1);
                _deviceClient.ProductInfo = "Simulated-CSharp";
                do
                {
                    SendDeviceToCloudMessagesAsync();

                } while (Console.ReadLine() != "Y");

                Console.ReadLine();
            }
            catch (Exception ex)
            {
                var exp = JsonConvert.SerializeObject(ex);
                Console.WriteLine(exp);
            }
        }

        private static async void SendDeviceToCloudMessagesAsync()
        {
            double min = 600;

            Random rand = new Random();
            Console.WriteLine("How many messages you want to send ? (ex : enter 1 to send single msg)");
            var times = Convert.ToInt32(Console.ReadLine());
            if (times == 0) times = 1;
            var count = 0;
            while (count < times)
            {
                Console.ForegroundColor = ConsoleColor.Red;

                var registerDevice = new RegisterDeviceCommand();
                var messageString = JsonConvert.SerializeObject(registerDevice);

                var message = new Message(Encoding.ASCII.GetBytes(messageString));

                await _deviceClient.SendEventAsync(message);
                Console.WriteLine("{0} > Sending message: {1}", DateTime.Now, messageString);
                count++;
            }

            Console.ResetColor();
            Console.WriteLine("\nEnter to continue........");

        }
    }
}
