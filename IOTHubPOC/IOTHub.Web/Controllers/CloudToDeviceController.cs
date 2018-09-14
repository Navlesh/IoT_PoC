using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IOTHub.Web.Api.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Devices;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;

namespace IOTHub.Web.Controllers
{
    [Route("api/[controller]")]
    public class CloudToDeviceController : Controller
    {


        IConfiguration _iconfiguration;

        static ServiceClient serviceClient;
        static string connectionString;
        private static string deviceId;
        static int i = 1;


        public CloudToDeviceController(IConfiguration iconfiguration)
        {
            _iconfiguration = iconfiguration;
            connectionString = _iconfiguration["ConnectionString"];
        }

        // GET api/CloudToDevice
        [HttpGet]
        public string Get()
        {
            return "Welcome to CloudToDevice";
        }

        //POST api/CloudToDevice
        [HttpPost]
        public async Task<IActionResult> Post([FromBody]MessageModel message)
        {
            try
            {
                if (message == null)
                {
                    return NotFound("input parms null");
                }

                if (message != null)
                {
                    if (string.IsNullOrEmpty(message.DeviceId))
                        message.DeviceId = "dev1";
                }

                serviceClient = ServiceClient.CreateFromConnectionString(connectionString);
                await SendCloudToDeviceMessageAsync(message);
                var sentMsg = JsonConvert.SerializeObject(message);
                return Ok(sentMsg);
            }
            catch (Exception exp)
            {
                var err = JsonConvert.SerializeObject(exp);
                return Ok(err);
            }

        }

        private async static Task SendCloudToDeviceMessageAsync(MessageModel message)
        {
            var commandMessage = new Message(Encoding.ASCII.GetBytes(message.Message));
            await serviceClient.SendAsync(message.DeviceId, commandMessage);
        }
    }
}
