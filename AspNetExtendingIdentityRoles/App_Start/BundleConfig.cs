using System.Web;
using System.Web.Optimization;

namespace PageWebMic
{
    public class BundleConfig
    {
        // For more information on bundling, visit http://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {

            bundles.Add(new ScriptBundle("~/scripts/jQuery")
                .Include("~/scripts/jquery-{version}.js")
                .Include("~/scripts/jqueryui/jquery-ui.js")
                .Include("~/scripts/jquery.validate.js")
                );

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.validate*"));

            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at http://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/Scripts/bootstrap.js",
                       "~/Scripts/tree/jquery.treetable.js",
                         "~/Scripts/bootstrap-treeview.js",
                      "~/Scripts/respond.js",
                        "~/Scripts/jquery.maskMoney.js"
                        ));
            bundles.Add(new ScriptBundle("~/bundles/checkeditor").Include(
                      "~/Scripts/ckeditor/ckeditor.js"
                     ));

            bundles.Add(new ScriptBundle("~/bundles/jspage").Include(
                  "~/Scripts/jquery.colorbox-min.js",
                  "~/Scripts/image-picker/image-picker.js",
                 "~/Scripts/inputmask/dist/jquery.inputmask.bundle.js",
                  "~/Scripts/inputosaurus.js",
                 "~/Scripts/admin.js"));


            bundles.Add(new StyleBundle("~/Content/bootstrap").Include(
                      "~/Content/bootstrap.css",
                       "~/Content/nvar.css"
                        ));
            bundles.Add(new StyleBundle("~/Content/FileUploadd").Include("~/Content/FileUpload/jquery.fileupload-ui.css"
                ));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/bootstrap.css",
                      "~/Content/bootstrap-treeview.css",
                       "~/Content/nvar.css",
                      "~/Content/site.css",
                      "~/Content/tree/jquery.treetable.css",
                       "~/Content/tree/jquery.treetable.theme.default.css",
                       "~/Content/colorbox.css",
                       "~/Content/inputosaurus.css"
                        ));

            bundles.Add(new ScriptBundle("~/bundles/FileUpload")
             .Include("~/scripts/FileUpload/vendor/jquery.ui.widget.js")
             .Include("~/scripts/FileUpload/tmpl.js")
             .Include("~/scripts/FileUpload/load-image.js")
             .Include("~/scripts/FileUpload/canvas-to-blob.js")

             .Include("~/scripts/FileUpload/jquery.iframe-transport.js")
             .Include("~/scripts/FileUpload/jquery.fileupload.js")
             .Include("~/scripts/FileUpload/jquery.fileupload-fp.js")
             .Include("~/scripts/FileUpload/jquery.fileupload-ui.js")
             //.Include("~/scripts/FileUpload/locale.js")
             .Include("~/scripts/FileUpload/main.js")
             );

          

        }
    }
}
