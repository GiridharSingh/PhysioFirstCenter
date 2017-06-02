using System.Web;
using System.Web.Optimization;

namespace Physio
{
    public class BundleConfig
    {
        // For more information on bundling, visit http://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        //"~/Scripts/jquery-{version}.js",
                        //"~/Scripts/jquery-3.1.1.min.js",
                        "~/Scripts/jquery.js",
                        "~/Scripts/jquery-ui.js",
                        "~/Scripts/jquery-migrate-1.2.1.min.js",
                        "~/Scripts/jquery.easing.1.3.js",
                        "~/Scripts/jquery.mobilemenu.js",
                        "~/Scripts/jquery.equalheights.js",
                        "~/Scripts/camera.js",
                        "~/Scripts/TMForm.js"
                        ));  //Removed from list "~/Scripts/device.min.js",

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.validate*"));

            // Use the development version of Modernizr to develop with and learn from. Then, when you're   //jquery-3.1.1.min
            // ready for production, use the build tool at http://modernizr.com to pick only the tests you need.
 
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/footerJSCollect").Include(
                       "~/Scripts/tmstickup.js",
                       "~/Scripts/superfish.js",
                       "~/Scripts/jquery.mousewheel.min.js",
                       "~/Scripts/jquery.simplr.smoothscroll.min.js",
                       "~/Scripts/jquery.stellar.js",
                       "~/Scripts/jquery.ui.totop.js"
                        ));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/Scripts/bootstrap.js",
                      "~/Scripts/bootstrap-filestyle.js",
                      "~/Scripts/respond.js"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/bootstrap.css",                      
                      "~/Content/style.css",
                      "~/Content/animate.css",
                      "~/Content/camera.css",
                      "~/Content/contact-form.css",
                      "~/Content/font-awesome.css"
                      //"~/Content/site.css"
                      ));  //Removed Deleted 

            BundleTable.EnableOptimizations = true;
        }
    }
}
