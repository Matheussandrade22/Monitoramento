using System.Web;
using System.Web.Optimization;
using System.IO;

namespace MonitoramentoAPI
{
    public class BundleConfig
    {
        // Para obter mais informações sobre o agrupamento, visite https://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            string jqueryPath = HttpContext.Current.Server.MapPath("~/Scripts/jquery-3.6.0.js");
            if (File.Exists(jqueryPath))
            {
                bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                            "~/Scripts/jquery-3.6.0.js"));
            }

            string modernizrPath = HttpContext.Current.Server.MapPath("~/Scripts/modernizr-2.8.3.js");
            if (File.Exists(modernizrPath))
            {
                bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                            "~/Scripts/modernizr-2.8.3.js"));
            }

            string bootstrapScriptPath = HttpContext.Current.Server.MapPath("~/Scripts/bootstrap.js");
            if (File.Exists(bootstrapScriptPath))
            {
                bundles.Add(new Bundle("~/bundles/bootstrap").Include(
                          "~/Scripts/bootstrap.js"));
            }

            string bootstrapCssPath = HttpContext.Current.Server.MapPath("~/Content/bootstrap.css");
            string siteCssPath = HttpContext.Current.Server.MapPath("~/Content/site.css");
            if (File.Exists(bootstrapCssPath) && File.Exists(siteCssPath))
            {
                bundles.Add(new StyleBundle("~/Content/css").Include(
                          "~/Content/bootstrap.css",
                          "~/Content/site.css"));
            }
        }
    }
}
