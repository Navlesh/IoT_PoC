using Microsoft.AspNetCore.Mvc;

namespace IOTHub.Web.Api.Controllers
{
    
    [Route("api/DeviceToCloud")]
    public class DeviceToCloudController : Controller
    {
        // GET api/DeviceToCloud
        [HttpGet]
        public string Get()
        {
            return "Welcome to DeviceToCloud";
        }

        // POST api/DeviceToCloud
        [HttpPost]
        public IActionResult Post([FromBody]string message)
        {
            return Ok(message);
        }
    }
}