using System.Web;
using System.Web.Optimization;

namespace DashboardB2B
{
    public class BundleConfig
    {
        // For more information on bundling, visit https://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js"));

            //bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
            //            "~/Scripts/jquery-2.2.3.min.js"));

            bundles.Add(new ScriptBundle("~/bundles/backstretch").Include(
                        "~/Scripts/jquery.backstretch.min.js"));
            bundles.Add(new ScriptBundle("~/bundles/ScriptLogin").Include(
                        "~/Scripts/GlobalScripts/scriptLogin.js"));
            bundles.Add(new ScriptBundle("~/bundles/ScriptSalesB2B").Include(
                        "~/Scripts/GlobalScripts/scriptSalesB2B.js"));
            bundles.Add(new ScriptBundle("~/bundles/scriptSalesTraditional").Include(
                        "~/Scripts/GlobalScripts/scriptSalesTraditional.js"));
            bundles.Add(new ScriptBundle("~/bundles/ScriptHome").Include(
                        "~/Scripts/GlobalScripts/ScriptHome.js"));
            bundles.Add(new ScriptBundle("~/bundles/scriptUsers").Include(
                       "~/Scripts/GlobalScripts/UsersScript.js"));
            // Utilice la versión de desarrollo de Modernizr para desarrollar y obtener información. De este modo, estará
            // ready for production, use the build tool at https://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/Scripts/bootstrap.js",
                      "~/Scripts/respond.js"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/bootstrap.css",
                      "~/Content/site.css"));


        }
    }
}
