using System.Web.Mvc;

namespace Roblox.Configuration.Site
{
    public static class HtmlUtility
    {
        public static string IsActive(this HtmlHelper html,
                                  string control,
                                  string action)
        {
            var routeData = html.ViewContext.RouteData;

            var routeAction = (string)routeData.Values["action"];
            var routeControl = (string)routeData.Values["controller"];

            // must match both
            var returnActive = control == routeControl &&
                               action == routeAction;

            return returnActive ? "active" : "";
        }

        public static string IsActive(this HtmlHelper html,
                                  string control)
        {
            var routeData = html.ViewContext.RouteData;
            
            var routeControl = (string)routeData.Values["controller"];

            var returnActive = control == routeControl;

            return returnActive ? "active" : "";
        }
    }
}