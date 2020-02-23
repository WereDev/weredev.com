using Microsoft.AspNetCore.Mvc;

namespace Weredev.UI.Controllers
{
    public abstract class BaseController : Controller
    {
        public void SetTitle(string title)
        {
            ViewData["Title"] = title;
        }
    }
}
