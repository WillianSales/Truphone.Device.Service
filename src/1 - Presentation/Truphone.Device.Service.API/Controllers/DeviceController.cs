using Microsoft.AspNetCore.Mvc;

namespace Truphone.Device.Service.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class DeviceController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}