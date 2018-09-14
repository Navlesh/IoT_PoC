using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Microsoft.ServiceBus.Messaging;
using System.Threading;
using System.Configuration;
using Newtonsoft.Json;
using RestSharp;

namespace SimulatedDeviceD2C
{
    class Program
    {
        private static string connectionString = "HostName=masiot.azure-devices.net;SharedAccessKeyName=iothubowner;SharedAccessKey=p7TG4a6XPSL61PO7Ih/NFVJToYdJpcN5fOde8cE5ZqY=";
        private static string iotHubD2CEndpoint = "messages/events";
        private static EventHubClient eventHubClient;

        static void Main(string[] args)
        {
            try
            {


                connectionString = ConfigurationManager.AppSettings["connectionString"];
                iotHubD2CEndpoint = ConfigurationManager.AppSettings["iotHubD2CEndpoint"];

                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("Receive messages D2C : Ctrl-C to exit.\n");
                Console.ResetColor();
                Console.WriteLine("connectionString :" + connectionString);
                Console.WriteLine("iotHubD2CEndpoint :" + iotHubD2CEndpoint);

                eventHubClient = EventHubClient.CreateFromConnectionString(connectionString, iotHubD2CEndpoint);

                var d2cPartitions = eventHubClient.GetRuntimeInformation().PartitionIds;

                CancellationTokenSource cts = new CancellationTokenSource();

                Console.CancelKeyPress += (s, e) =>
                {
                    e.Cancel = true;
                    cts.Cancel();
                    Console.WriteLine("Exiting...");
                };

                var tasks = new List<Task>();
                foreach (string partition in d2cPartitions)
                {
                    tasks.Add(ReceiveMessagesFromDeviceAsync(partition, cts.Token));
                }
                Task.WaitAll(tasks.ToArray());
            }
            catch (Exception ex)
            {
                Console.WriteLine(JsonConvert.SerializeObject(ex));
            }
        }



        private static async Task ReceiveMessagesFromDeviceAsync(string partition, CancellationToken ct)
        {
            var min = Convert.ToInt32(ConfigurationManager.AppSettings["minutes"]);
            var dte = DateTime.UtcNow.AddMinutes(min);
            var eventHubReceiver = eventHubClient.GetDefaultConsumerGroup().CreateReceiver(partition, dte);
            while (true)
            {
                if (ct.IsCancellationRequested) break;
                EventData eventData = await eventHubReceiver.ReceiveAsync();
                if (eventData == null) continue;

                string data = Encoding.UTF8.GetString(eventData.GetBytes());
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("Message received => Partition: {0}, Data: '{1}'", partition, data);
                Console.ResetColor();
                SendMessagesToRestApi(data);
            }
        }

        private static void SendMessagesToRestApi(string message, string deviceId = "dev1")
        {
            try
            {
                var hostUrl = ConfigurationManager.AppSettings["apiUrl"];
                var relativeUrl = ConfigurationManager.AppSettings["relativeUrl"];

                var client = new RestClient(hostUrl);
                var request = new RestRequest(relativeUrl, Method.POST);
                request.RequestFormat = DataFormat.Json;
                request.AddBody(message);
                Console.WriteLine("Sending to Rest Api :");
                var result = client.Execute(request);

                Console.WriteLine("Response : \n" + result.StatusCode + "\t" + result.Content);
            }
            catch (Exception exp)
            {
                var ex = JsonConvert.SerializeObject(exp);
                Console.WriteLine(ex);
            }
        }

        private static void SendMessagesToRestApiLocal(string message, string deviceId = "dev1")
        {
            try
            {
                var client = new RestClient("http://localhost:51073");
                var request = new RestRequest("api/CloudToDevice/", Method.POST);
                request.RequestFormat = DataFormat.Json;

                request.AddBody(new MessageModel
                {
                    DeviceId = deviceId,
                    Message = message
                });
                client.Execute(request);
            }
            catch (Exception exp)
            {
                var ex = JsonConvert.SerializeObject(exp);
                Console.WriteLine(ex);
            }
        }
    }

    public class MessageModel
    {
        public string DeviceId { get; set; }
        public string Message { get; set; }
    }
}
