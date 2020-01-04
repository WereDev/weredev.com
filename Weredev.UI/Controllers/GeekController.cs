using Microsoft.AspNetCore.Mvc;

namespace Weredev.UI.Controllers
{
    public class GeekController : BaseController
    {
        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }
    }
}
