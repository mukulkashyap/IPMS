using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Optimization;
using System.Web.UI;

namespace IPMS
{
    public class BundleConfig
    {
        // For more information on Bundling, visit http://go.microsoft.com/fwlink/?LinkID=303951
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/WebFormsJs").Include(
                            "~/Scripts/WebForms/WebForms.js",
                            "~/Scripts/WebForms/WebUIValidation.js",
                            "~/Scripts/WebForms/MenuStandards.js",
                            "~/Scripts/WebForms/Focus.js",
                            "~/Scripts/WebForms/GridView.js",
                            "~/Scripts/WebForms/DetailsView.js",
                            "~/Scripts/WebForms/TreeView.js",
                            "~/Scripts/WebForms/WebParts.js"));

            // Order is very important for these files to work, they have explicit dependencies
            bundles.Add(new ScriptBundle("~/bundles/MsAjaxJs").Include(
                    "~/Scripts/WebForms/MsAjax/MicrosoftAjax.js",
                    "~/Scripts/WebForms/MsAjax/MicrosoftAjaxApplicationServices.js",
                    "~/Scripts/WebForms/MsAjax/MicrosoftAjaxTimer.js",
                    "~/Scripts/WebForms/MsAjax/MicrosoftAjaxWebForms.js"));

            //Bundle For Zoom Defined Custom Stylesheets
            bundles.Add(new StyleBundle("~/bundles/custom/css").Include(
            "~/Content/css/bootstrap/bootstrap.min.css",
            "~/Content/css/libs/nanoscroller.css",
            "~/Content/css/compiled/theme_styles.css",
            "~/Content/css/libs/datepicker.css",
            "~/Content/css/libs/select2.css",
            "~/Content/css/Custom.css"));

            //Bundle For Telerik Kendo Stylesheets 
            bundles.Add(new StyleBundle("~/Content/kendo/css").Include(
            "~/Content/kendo/kendo.common.min.css",
            "~/Content/kendo/kendo.silver.min.css"));

            //Bundle For Scripts 
            bundles.Add(new ScriptBundle("~/bundles/jquerybootstrap/js").Include(
            "~/Scripts/jquery-1.10.2.min.js",
            "~/Scripts/jquery-ui.min.js",
            "~/Scripts/jquery.validate.min.js",
            "~/Scripts/jquery.validate.bootstrap.popover.js",
            "~/Scripts/bootstrap.min.js"        
           ));

            bundles.Add(new ScriptBundle("~/bundles/custom/js").Include(
            "~/Scripts/js/jquery.nanoscroller.min.js",
            "~/Scripts/js/bootstrap-datepicker.js",
            "~/Scripts/js/moment.min.js",
            "~/Scripts/js/pace.min.js",
            "~/Scripts/js/scripts.js",
            "~/Scripts/common.js",
            "~/Scripts/jszip.min.js"
           ));
           


            //Bundle For Telerik Kendo Javascript files
            bundles.Add(new ScriptBundle("~/bundles/kendo/js").Include(
            "~/Scripts/kendo/kendo.all.min.js",
            "~/Scripts/kendo/kendo.aspnetmvc.min.js"));

            // Use the Development version of Modernizr to develop with and learn from. Then, when you’re
            // ready for production, use the build tool at http://modernizr.com to pick only the tests you need
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                            "~/Scripts/modernizr-*"));

            ScriptManager.ScriptResourceMapping.AddDefinition(
                "respond",
                new ScriptResourceDefinition
                {
                    Path = "~/Scripts/respond.min.js",
                    DebugPath = "~/Scripts/respond.js",
                });

            BundleTable.EnableOptimizations = true;
        }
    }
}