using System.Web;
using System.Web.Optimization;

namespace Portal
{
    public class BundleConfig
    {
        // For more information on bundling, visit http://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            //bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
            //            "~/_Scripts/jquery-{version}.js"));

            //bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
            //            "~/_Scripts/jquery.validate*"));

            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at http://modernizr.com to pick only the tests you need.
            //bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
            //            "~/_Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/jquery.hcaptions").Include(
                        "~/Scripts/jquery.hcaptions.js"));

            bundles.Add(new ScriptBundle("~/bundles/jquery.remodal").Include(
                        "~/Scripts/jquery.remodal.js"));

            
            //bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
            //          "~/_Scripts/bootstrap.js",
            //          "~/_Scripts/respond.js"));

            //bundles.Add(new StyleBundle("~/Content/css").Include(
            //          "~/Content/bootstrap.css",
            //          "~/Content/site.css",
            //          "~/Content/meanstream.css",
            //          "~/Content/jquery.remodal.css"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/meanstream.css",
                      "~/Content/jquery.remodal.css"));
        }
    }
}
