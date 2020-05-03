using Microsoft.AspNetCore.Mvc;

namespace Weredev.UI.Controllers
{
    public class HomeController : BaseController
    {
        [HttpGet]
        public IActionResult Index()
        {
            SetDescription("Rolling dice, writing code, and wandering the world.");
            SetKeywords("Geek",
                        "Developer",
                        "World Traveler",
                        "Gaming",
                        "D&D",
                        "Dungeons and Dragons",
                        "Pathfinder",
                        "Board Games",
                        "C#",
                        "WPF",
                        "MVC",
                        "GitHub",
                        "Azure",
                        "Wu10Man",
                        "Photos",
                        "Photography",
                        "Travel",
                        "Flickr");
            return View();
        }
    }
}
