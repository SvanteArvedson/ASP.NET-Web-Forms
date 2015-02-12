using System.Web.Optimization;

namespace MusicLibrary
{
    public class BundleConfig
    {
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/Scripts/modernizr").Include(
                "~/Scripts/vendor/modernizr.js"
            ));

            bundles.Add(new ScriptBundle("~/Scripts/foundation").Include(
                "~/Scripts/vendor/jquery.js",
                "~/Scripts/vendor/fastclick.js",
                "~/Scripts/foundation.min.js",
                "~/Scripts/siteScript.js"
            ));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                "~/Content/normalize.css",
                "~/Content/foundation.css",
                "~/Content/SiteStyle.css"
            ));

            bundles.Add(new ScriptBundle("~/Scripts/BookletDetailsScript").Include(
                "~/Scripts/siteBookletDetailsScript.js"
            ));
        }
    }
}