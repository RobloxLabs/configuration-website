using System.Web;
using System.Web.Optimization;

namespace Roblox.Settings.Site
{
    public class BundleConfig
    {
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new StyleBundle("~/Content/css").Include("~/Content/site.css"));
            bundles.Add(new StyleBundle("~/Content/login").Include("~/Content/Login.css"));
            bundles.Add(new ScriptBundle("~/bundles/base").Include("~/Scripts/Base.js", "~/Scripts/Select2Helpers.js"));
            bundles.Add(new ScriptBundle("~/bundles/testing").Include("~/Scripts/TestingSelect2.js"));
        }
    }
}
