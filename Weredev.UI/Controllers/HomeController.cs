using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
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