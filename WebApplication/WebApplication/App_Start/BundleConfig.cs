using System.Web;
using System.Web.Optimization;

namespace WebApplication
{
    public class BundleConfig
    {
        // For more information on bundling, visit https://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include("~/Scripts/jquery-1.10.2.min.js", "~/Scripts/fancybox2.js"));
            
            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include("~/Scripts/jquery.validate*"));

            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at https://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include("~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include("~/Scripts/bootstrap.js","~/Scripts/respond.js", "~/Scripts/bootstrap-datepicker.js"));

            //new bundle of scripts to add the custom scripts
            //bundles.Add(new ScriptBundle("~/bundles/myscripts").Include("~/Scripts/departments.js", "~/Scripts/employees.js", "~/Scripts/departmentEdit.js", "~/Scripts/employeeEdit.js"));
            bundles.Add(new ScriptBundle("~/bundles/Pages").Include("~/Scripts/departments.js", "~/Scripts/employees.js"));
            bundles.Add(new ScriptBundle("~/bundles/EditPages").Include("~/Scripts/departmentEdit.js", "~/Scripts/employeeEdit.js"));

            //new bundle of styles to add the custom css
            bundles.Add(new StyleBundle("~/Content/css").Include("~/Content/bootstrap.css", "~/Content/site.css", "~/Content/fancybox.css", "~/Content/datepicker.css"));
        }
    }
}
