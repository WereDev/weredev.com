using System;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Weredev.UI.Helpers
{
    public static class MenuHelper
    {
        public static string SetActive(this IHtmlHelper html, string controller = null, string action = null)
        {
            const string cssClass = "active";
            var currentAction = (string)html.ViewContext.RouteData.Values["action"];
            var currentController = (string)html.ViewContext.RouteData.Values["controller"];
 
            if (string.IsNullOrEmpty(controller))
                controller = currentController;
 
            if (string.IsNullOrEmpty(action))
                action = currentAction;
 
            return (controller.Equals(currentController, StringComparison.CurrentCultureIgnoreCase)
                    && action.Equals(currentAction, StringComparison.CurrentCultureIgnoreCase))
                    ? cssClass
                    : string.Empty;
        }
    }
}
