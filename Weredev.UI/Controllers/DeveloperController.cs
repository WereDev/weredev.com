using Microsoft.AspNetCore.Mvc;

namespace Weredev.UI.Controllers
{
    public class DeveloperController : BaseController
    {
        [HttpGet]
        public IActionResult Index()
        {
            SetTitle("developer");
            return View();
        }

        [HttpGet]
        public IActionResult Weredev()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Wu10Man()
        {
            return View();
        }
    }
}
