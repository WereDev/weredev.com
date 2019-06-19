using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace Weredev.UI.Controllers
{

    [RequireHttps]
    public abstract class BaseController : Controller
    {
        public void SetTitle(string title)
        {
            ViewData["Title"] = title;
        }
    }
}