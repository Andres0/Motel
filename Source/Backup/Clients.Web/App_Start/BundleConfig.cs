using System.Web;
using System.Web.Optimization;

namespace DS.Motel.Clients.Web
{
    public class BundleConfig
    {
        // Para obtener más información sobre las uniones, visite https://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js",
                        "~/Scripts/jquery.unobtrusive-ajax.min.js",
                        "~/Scripts/notify.min.js",
                        "~/Scripts/Motel.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.validate*"));

            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/Scripts/bootstrap.min.js"));

            bundles.Add(new ScriptBundle("~/bundles/kendo").Include(
                      "~/Scripts/kendo/2018.3.1017/jszip.min.js",
                      "~/Scripts/kendo/2018.3.1017/kendo.all.min.js",
                      "~/Scripts/kendo/2018.3.1017/kendo.aspnetmvc.min.js",
                      "~/Scripts/kendo/2018.3.1017/cultures/kendo.culture.es-BO.min.js"));


            bundles.Add(new StyleBundle("~/Content/bootstrapcss").Include(
                      "~/Content/bootstrap.min.css"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                      //"~/Content/bootstrap.min.css",
                      "~/Content/site.css"));

            bundles.Add(new StyleBundle("~/Content/kendocss").Include(
                      "~/Content/kendo/2018.3.1017/kendo.common-bootstrap.min.css",
                      "~/Content/kendo/2018.3.1017/kendo.mobile.all.min.css",
                      "~/Content/kendo/2018.3.1017/kendo.bootstrap.min.css"));
        }
    }
}
