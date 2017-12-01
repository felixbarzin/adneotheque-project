using System.Web;
using System.Web.Optimization;

namespace adneotheque_solution
{
    public class BundleConfig
    {
        // For more information on bundling, visit https://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new Bundle("~/bundles/adneotheque").Include(
                "~/Scripts/jquery-{version}.js",
                "~/Scripts/jquery-ui.js",
                "~/Scripts/jquery.unobtrusive*",
                "~/Scripts/jquery.validate*",
                "~/Scripts/Javascript/adneotheque.js"));

            bundles.Add(new Bundle("~/bundles/jquery").Include(
                "~/Scripts/jquery-{version}.js"));

            bundles.Add(new Bundle("~/bundles/jqueryval").Include(
                "~/Scripts/jquery.validate*"));

            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at http://modernizr.com to pick only the tests you need.
            bundles.Add(new Bundle("~/bundles/modernizr").Include(
                "~/Scripts/modernizr-*"));

            bundles.Add(new Bundle("~/bundles/bootstrap").Include(
                "~/Scripts/bootstrap.js",
                "~/Scripts/respond.js"));

            bundles.Add(new Bundle("~/Content/css").Include(
                "~/Content/bootstrap.css",
                "~/Content/site.css",
                "~/Content/jquery-ui.css",
                "~/Content/jquery-ui.min.css",
                "~/Content/jquery-ui.structure.css",
                "~/Content/jquery-ui.structure.min.css",
                "~/Content/jquery-ui.theme.css",
                "~/Content/jquery-ui.theme.min.css"));
        }
    }
}
