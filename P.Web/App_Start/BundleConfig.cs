using System.Web.Optimization;

namespace P.Web
{
    public class BundleConfig
    {
        // 有关 Bundling 的详细信息，请访问 http://go.microsoft.com/fwlink/?LinkId=254725
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                    //"~/Scripts/jquery-{version}.js",
                    "~/Scripts/AdminLTE2.3.0/plugins/jQuery/jQuery-2.1.4.min.js"
                        ));

            bundles.Add(new ScriptBundle("~/bundles/jqueryui").Include(
                        "~/Scripts/jquery-ui-{version}.min.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.unobtrusive*",
                        "~/Scripts/jquery.validate*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                    "~/Scripts/AdminLTE2.3.0/bootstrap/js/bootstrap.min.js",
                    "~/Scripts/AdminLTE2.3.0/dist/js/app.min.js",
                    "~/Scripts/AdminLTE2.3.0/plugins/slimScroll/jquery.slimscroll.min.js",
                    "~/Scripts/AdminLTE2.3.0/plugins/fastclick/fastclick.min.js",
                    "~/Scripts/AdminLTE2.3.0/plugins/iCheck/icheck.min.js",
                   "~/Scripts/bootstraptable/dist/bootstrap-table.min.js",
                   "~/Scripts/bootstraptable/dist/locale/bootstrap-table-zh-CN.min.js",
                    "~/Scripts/Custom/WinMsg.js"));
            // 使用要用于开发和学习的 Modernizr 的开发版本。然后，当你做好
            // 生产准备时，请使用 http://modernizr.com 上的生成工具来仅选择所需的测试。
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new StyleBundle("~/Content/css").Include("~/Content/site.css",
                        "~/Scripts/AdminLTE2.3.0/bootstrap/css/bootstrap.min.css",
                       "~/Scripts/AdminLTE2.3.0/dist/css/AdminLTE.min.css",
                       "~/Scripts/AdminLTE2.3.0/dist/css/skins/skin-blue.min.css",
                       "~/Scripts/AdminLTE2.3.0/plugins/iCheck/flat/blue.css",
                       "~/Scripts/Toastr/toastr.min.css",
                       "~/Scripts/bootstraptable/dist/bootstrap-table.css"));
            bundles.Add(new StyleBundle("~/Content/themes/base/css").Include(
                        "~/Content/themes/base/minified/jquery.ui.core.min.css",
                        "~/Content/themes/base/minified/jquery.ui.resizable.min.css",
                        "~/Content/themes/base/minified/jquery.ui.selectable.min.css",
                        "~/Content/themes/base/minified/jquery.ui.accordion.min.css",
                        "~/Content/themes/base/minified/jquery.ui.autocomplete.min.css",
                        "~/Content/themes/base/minified/jquery.ui.button.min.css",
                        "~/Content/themes/base/minified/jquery.ui.dialog.min.css",
                        "~/Content/themes/base/minified/jquery.ui.slider.min.css",
                        "~/Content/themes/base/minified/jquery.ui.tabs.min.css",
                        "~/Content/themes/base/minified/jquery.ui.datepicker.min.css",
                        "~/Content/themes/base/minified/jquery.ui.progressbar.min.css",
                        "~/Content/themes/base/minified/jquery.ui.theme.min.css"));
        }
    }
}