using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Optimization;

namespace UI.App_Start
{
    public class BundleConfig
    {
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
            "~/Scripts/jquery-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
           "~/Scripts/bootstrap.min.js"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
           "~/Content/bootstrap.css",
           "~/Content/style.css"));

            bundles.Add(new ScriptBundle("~/scripts/custom").Include(
                "~/Scripts/custom.js"));

            bundles.Add(new StyleBundle("~/Content/custom").Include(
                "~/Content/custom.css"));

            bundles.Add(new StyleBundle("~/Content/card").Include(
                "~/Content/cardStyle.css"));
        }
    }
}