using System.Web;
using System.Web.Optimization;

namespace AuctionHouse
{
    public class BundleConfig
    {
        // For more information on bundling, visit https://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        /*"~/Scripts/jquery-{version}.min.js"));*/
                        "~/Scripts/jquery-3.3.1.min.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.validate*"));

            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at https://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/Scripts/bootstrap.min.js"));

            bundles.Add(new ScriptBundle("~/bundles/popper").Include(
                      "~/Scripts/popper.min.js"));

            bundles.Add(new StyleBundle("~/bundles/ajax").Include(
                      "~/Scripts/jquery.unobtrusive-ajax.js",
                      "~/Scripts/MyScripts/ajax.js"));

            bundles.Add(new StyleBundle("~/bundles/index").Include(
                      "~/Scripts/MyScripts/jquery.fittext.js",
                      "~/Scripts/MyScripts/items.js",
                      "~/Scripts/MyScripts/pagination.js",
                      "~/Scripts/MyScripts/update.js",
                      "~/Scripts/MyScripts/load.js",
                      //
                      "~/Scripts/MyScripts/signalr.js"
                      ));

            bundles.Add(new ScriptBundle("~/bundles/auction").Include(
                      "~/Scripts/MyScripts/auction-details.js"));

            bundles.Add(new ScriptBundle("~/bundles/account").Include(
                      "~/Scripts/MyScripts/account-details.js"));

            bundles.Add(new StyleBundle("~/bundles/modal").Include(
                      "~/Scripts/MyScripts/modal.js"));

            bundles.Add(new StyleBundle("~/bundles/signalr").Include(
                      "~/Scripts/jquery.signalR-2.2.2.min.js"));

            bundles.Add(new StyleBundle("~/Content/bootstrap").Include(
                      "~/Content/bootstrap.min.css"));

            bundles.Add(new StyleBundle("~/Content/myContent").Include(
                      "~/Content/MyContent/modal.css",
                      "~/Content/MyContent/details.css",
                      "~/Content/MyContent/index.css"));
        }
    }
}
