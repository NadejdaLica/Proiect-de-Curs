using System.Web.Optimization;
using System;

namespace eUseControl.Web
{
    public static class BundleConfig
    {
        public static void RegisterBundles(BundleCollection bundles)
        {

            //Stiluri
            bundles.Add(new StyleBundle("~/bundles/bootstrap/css").Include("~/Content/bootstrap.min.css"));
            bundles.Add(new StyleBundle("~/bundles/bootstrapicons/css").Include("~/Content/bootstrap-icons.css"));
            bundles.Add(new StyleBundle("~/bundles/boxicons/css").Include("~/Content/boxicons.min.css"));
            bundles.Add(new StyleBundle("~/bundles/glightbox/css").Include("~/Content/glightbox.min.css"));
            bundles.Add(new StyleBundle("~/bundles/remixicon/css").Include("~/Content/remixicon.css"));
            bundles.Add(new StyleBundle("~/bundles/swiper/css").Include("~/Content/swiper-bundle.min.css"));
            bundles.Add(new StyleBundle("~/bundles/style/css").Include("~/Content/style.css"));
              


               //Scripturi
               
               bundles.Add(new ScriptBundle("~/bundles/main/js").Include("~/Scripts/main.js"));
               bundles.Add(new ScriptBundle("~/bundles/validate/js").Include("~/Scripts/validate.js"));
               bundles.Add(new ScriptBundle("~/bundles/swiper/js").Include("~/Scripts/swiper-bundle.min.js"));
               bundles.Add(new ScriptBundle("~/bundles/isotope/js").Include("~/Scripts/isotope.pkgd.min.js"));
               bundles.Add(new ScriptBundle("~/bundles/glightbox/js").Include("~/Scripts/glightbox.min.js"));
               bundles.Add(new ScriptBundle("~/bundles/bootstrap/js").Include("~/Scripts/bootstrap.bundle.min.js"));
               bundles.Add(new ScriptBundle("~/bundles/purecounter/js").Include("~/Scripts/purecounter.js"));



          }
    }
}