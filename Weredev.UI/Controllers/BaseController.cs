using Microsoft.AspNetCore.Mvc;

namespace Weredev.UI.Controllers
{
    public abstract class BaseController : Controller
    {
        public const string MetaTitle = "title";
        public const string MetaDescription = "description";
        public const string MetaKeywords = "keywords";

        protected virtual void SetTitle(string title)
        {
            ViewData[MetaTitle] = title;
        }

        protected virtual void SetDescription(string description)
        {
            ViewData[MetaDescription] = description;
        }

        protected virtual void SetKeywords(params string[] keywords)
        {
            ViewData[MetaKeywords] = string.Join(", ", keywords ?? new string[0]);
        }
    }
}
