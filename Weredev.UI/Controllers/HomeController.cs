using Microsoft.AspNetCore.Mvc;

namespace Weredev.UI.Controllers
{
    public class HomeController : BaseController
    {
        public IActionResult Index()
        {
            SetTitle(string.Empty);
            return View();
        }
    }
}
