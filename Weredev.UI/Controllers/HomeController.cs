using Microsoft.AspNetCore.Mvc;

namespace Weredev.UI.Controllers
{
    public class HomeController : BaseController
    {
        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }
    }
}
