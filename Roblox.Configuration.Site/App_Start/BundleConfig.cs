using System.Web;
using System.Web.Optimization;

namespace Roblox.Configuration.Site
{
    public class BundleConfig
    {
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new StyleBundle("~/Content/css").Include(
                "~/Content/site.css",
                "~/themes/aristo/Aristo.css"
            ));
            bundles.Add(new StyleBundle("~/Content/login").Include("~/Content/Login.css"));
            // Lifted from DevOps scripts bundle
            bundles.Add(new ScriptBundle("~/bundles/base").Include(
                "~/Scripts/jquery-3.1.1.min.js",
                "~/Scripts/bootstrap.min.js",
                "~/Scripts/bootbox.min.js",
                "~/Scripts/jquery.tablesorter.min.js",
                "~/Scripts/select2.full.min.js",
                "~/Scripts/BootstrapDropdownHover.js",
                "~/Scripts/Select2Helpers.js",
                // Config site additions
                "~/Scripts/TestingSelect2.js",
                "~/Scripts/jquery-ui.js" // For setting/connection configuration
            ));

            /* Pages */

            // ~/Config
            bundles.Add(new ScriptBundle("~/bundles/config").Include(
                "~/Scripts/Settings.js"
            ));
            bundles.Add(new StyleBundle("~/bundles/config-styles").Include(
                "~/Content/Config.css"
            ));


            //BundleTable.EnableOptimizations = true;
        }
    }
}
