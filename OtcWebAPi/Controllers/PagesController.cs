using Microsoft.AspNetCore.Mvc;

namespace OtcWebAPi.Controllers
{
    public class PagesController : Controller
    {

        [HttpGet("index")]
        public IActionResult Index()
        {
            return View();
        }
    }
}
