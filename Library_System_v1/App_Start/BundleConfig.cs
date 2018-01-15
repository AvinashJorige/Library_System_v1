using System.Web;
using System.Web.Optimization;

namespace Library_System_v1
{
    public class BundleConfig
    {
        // For more information on bundling, visit http://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Content/assets/plugins/jQuery/jQuery-2.2.0.min.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.validate*"));

            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at http://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/Scripts/respond.js",
                      "~/Content/assets/bootstrap/js/bootstrap.min.js",
                      "~/Content/assets/plugins/slimScroll/jquery.slimscroll.min.js",
                      "~/Content/assets/plugins/fastclick/fastclick.js",
                      "~/Content/assets/dist/js/app.min.js",
                      "~/Content/assets/dist/js/demo.js"
                      ));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/bootstrap.min.css",
                      "~/Content/font-awesome.min.css",
                      "~/Content/assets/dist/css/AdminLTE.min.css",
                      "~/Content/assets/dist/css/skins/_all-skins.css",
                      "~/Content/Site.css"));
        }
    }
}
