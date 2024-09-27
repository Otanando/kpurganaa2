﻿using System.Web;
using System.Web.Optimization;

namespace kpurganaaAdmin
{
    public class BundleConfig
    {
        // Para obtener más información sobre las uniones, visite https://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new Bundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js"));

            bundles.Add(new Bundle("~/bundles/complementos").Include(
                      "~/Scripts/fontawesome/all.min.js",
                      "~/Scripts/DataTables/jquery.dataTables.js",
                      "~/Scripts/DataTables/dataTables.responsive.js",
                      "~/Scripts/loadingoverlay/loadingoverlay.min.js",
                      "~/Scripts/sweetalert.min.js",
                      "~/Scripts/jquery.validate.js",
                      "~/Scripts/scripts.js"));



            bundles.Add(new Bundle("~/bundles/bootstrap").Include(
                      "~/Scripts/bootstrap.bundle.js"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                "~/Content/Site.css",
                "~/Content/DataTables/css/jquery.datatables.css",
                "~/Content/DataTables/css/responsive.datatables.css",
                "~/Content/sweetalert.css"



                ));


        }
    }
}
