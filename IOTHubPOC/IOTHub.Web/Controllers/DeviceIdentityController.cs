using System;
using System.Threading.Tasks;
using IOTHub.Web.Api.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Devices;
using Microsoft.Azure.Devices.Common.Exceptions;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;

namespace IOTHub.Web.Api.Controllers
{
    [Produces("application/json")]
    [Route("api/DeviceIdentity")]
    public class DeviceIdentityController : Controller
    {
        private static RegistryManager _registryManager;

        static string connectionString;
        static string host;

        IConfiguration _iconfiguration;

        public DeviceIdentityController(IConfiguration iconfiguration)
        {
            _iconfiguration = iconfiguration;
            connectionString = _iconfiguration["ConnectionString"];
            host = _iconfiguration["HostName"];
            _registryManager = RegistryManager.CreateFromConnectionString(connectionString);
        }


        [HttpPost]
        public async Task<IActionResult> Post([FromBody] CreateDeviceRequestModel model)
        {
            try
            {
                if (model == null)
                    return NotFound("CreateDeviceRequestModel is null");

                if (string.IsNullOrEmpty(model.Rudi))
                    return NotFound("CreateDeviceRequestModel, Rudi is null or empty");

                var result = await AddDeviceAsync(model.Rudi);
                result.GwaRegistrationResponseCommand.Rudi = model.Rudi;
                var jsonResponse = JsonConvert.SerializeObject(result);

                return Ok(result);
            }
            catch (Exception exp)
            {
                var expMessage = JsonConvert.SerializeObject(exp);
                return NotFound(expMessage);
            }
        }

        private static async Task<DeviceIdentityResponseModel> AddDeviceAsync(string deviceId)
        {
            //HostName=rspdeviotreg.azure-devices.net;DeviceId=dev1;SharedAccessKey=ErSTGgMRI9uZCTtUn7zMb/yECrgRt8C6BRdIKCwmeag="
            Device device;
            var response = new DeviceIdentityResponseModel();
            try
            {
                device = await _registryManager.AddDeviceAsync(new Device(deviceId));
                var conn = host + "DeviceId=" + deviceId + ";SharedAccessKey=" + device.Authentication.SymmetricKey.PrimaryKey;
                response = new DeviceIdentityResponseModel();
                response.GwaRegistrationResponseCommand.ConnectionString = conn;

            }
            catch (DeviceAlreadyExistsException)
            {
                device = await _registryManager.GetDeviceAsync(deviceId);
                var conn = host + "DeviceId=" + deviceId + ";SharedAccessKey=" + device.Authentication.SymmetricKey.PrimaryKey;
                response = new DeviceIdentityResponseModel();
                response.GwaRegistrationResponseCommand.ConnectionString = conn;
            }
            catch (Exception e)
            {
                var exp = JsonConvert.SerializeObject(e);
                throw e;
            }

            return response;
        }
    }
}