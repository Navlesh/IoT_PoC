using Microsoft.Azure.Devices;
using Microsoft.Azure.Devices.Client;
using Microsoft.ServiceBus.Messaging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace IOT_Sample.Controllers
{
    public class HomeController : Controller
    {
        private string IotConnectionString = ConfigurationManager.AppSettings["IotConnectionString"];
        static string iotHubUri = ConfigurationManager.AppSettings["IotHubUri"];
        static string deviceKey = ConfigurationManager.AppSettings["DeviceKey"];
        string iotHubD2cEndpoint = "messages/events";
        EventHubClient eventHubClient;
        DeviceClient deviceClient;
        public ActionResult Index()
        {
            ViewBag.deviceList = GetDevicesList();
            return View();
        }

        private List<DeviceInfo> GetDevicesList()
        {
            List<DeviceInfo> lst = new List<DeviceInfo>();
            RegistryManager registryManager = RegistryManager.CreateFromConnectionString(IotConnectionString);

            var devices = registryManager.GetDevicesAsync(1000).Result;
            foreach (var dev in devices)
            {
                lst.Add(new DeviceInfo() { Name = dev.Id, Status = dev.Status.ToString(), LastActivityTime = dev.LastActivityTime, CloudToDeviceMessageCount = dev.CloudToDeviceMessageCount });
            }
            return lst;
        }

        [HttpPost]
        public ActionResult Index(FormCollection fc)
        {
            Device device;
            RegistryManager registryManager = RegistryManager.CreateFromConnectionString(IotConnectionString);
            try
            {
                if (!String.IsNullOrEmpty(fc["DeviceId"]))
                {
                    device = registryManager.AddDeviceAsync(new Device(fc["DeviceId"])).Result;
                }
                else
                {
                    ViewBag.error = "DeviceId can not be blank";
                    ViewBag.deviceList = GetDevicesList();
                    return View();
                }
            }
            catch (Exception ex)
            {
                ViewBag.error = "Device is already registered";
                ViewBag.deviceList = GetDevicesList();
                return View();
            }

            return RedirectToAction("Index");
        }
        [HttpPost]
        public ActionResult SendMessages(FormCollection fc)
        {
            try
            {
                int count = 0;
                double minTemperature = 20;
                double minHumidity = 60;
                Random rand = new Random();
                while (count < 10)
                {
                    double currentTemperature = minTemperature + rand.NextDouble() * 15;
                    double currentHumidity = minHumidity + rand.NextDouble() * 20;

                    ServiceClient serviceClient = ServiceClient.CreateFromConnectionString(IotConnectionString);

                    var telemetryDataPoint = new
                    {
                        temperature = currentTemperature,
                        humidity = currentHumidity
                    };
                    var messageString = JsonConvert.SerializeObject(telemetryDataPoint);
                    var message = new Microsoft.Azure.Devices.Message(Encoding.ASCII.GetBytes(messageString));

                    message.Properties.Add("temperatureAlert", (currentTemperature > 30) ? "true" : "false");

                    serviceClient.SendAsync(fc["Message"], message);
                    count++;
                }
            }
            catch (Exception ex)
            {
                ViewBag.senderror = "There is an issue sending message";
                ViewBag.deviceList = GetDevicesList();
                return View();
            }
            return RedirectToAction("Index");

        }
        public ActionResult About()
        {
           List<string> lst= ReceiveC2dAsync().Result;
            if(lst.Count>0)
            {
                ViewBag.getMessages = lst;
            }
            return View();
        }
        private async Task<List<string>> ReceiveC2dAsync()
        {
            List<string> lst = new List<string>();
            deviceClient = DeviceClient.Create(iotHubUri, new DeviceAuthenticationWithRegistrySymmetricKey(Request.QueryString["deviceid"], deviceKey), Microsoft.Azure.Devices.Client.TransportType.Http1);

            //deviceClient.ProductInfo = "Simulated-DotNet-CSharp";
            //Console.WriteLine("\nReceiving cloud to device messages from service");
            try
            {
                while (true)
                {
                    Microsoft.Azure.Devices.Client.Message receivedMessage = await deviceClient.ReceiveAsync();
                    if (receivedMessage == null) break;

                    //Console.ForegroundColor = ConsoleColor.Yellow;
                    //Console.WriteLine("Received message: {0}", Encoding.ASCII.GetString(receivedMessage.GetBytes()));
                    //Console.ResetColor();
                    lst.Add(Encoding.ASCII.GetString(receivedMessage.GetBytes()));
                    await deviceClient.CompleteAsync(receivedMessage);
                }
            }catch(Exception ex)
            {
                lst.Add(JsonConvert.SerializeObject(ex));
            }
            return lst;
        }


        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
    public class DeviceInfo
    {
        public string Name { get; set; }
        public string Status { get; set; }
        public DateTime LastActivityTime { get; set; }
        public int CloudToDeviceMessageCount { get; set; }
    }
}