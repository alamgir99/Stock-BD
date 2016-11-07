using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Optimization;

namespace StockBDWeb.App_Start
{
    public class BundleConfig
    {
    public static void RegisterBundles(BundleCollection bundles) 
    {
        var scripts = new ScriptBundle("~/Scripts/");
        scripts.IncludeDirectory("~/Scripts/","*.js");
        scripts.IncludeDirectory("~/Scripts/modules/", "*.js");


        var css = new ScriptBundle("~/Styles/");
        css.IncludeDirectory("~/Styles/", "*.css");
        css.IncludeDirectory("~/Styles/images_jq/", "*.png");
        css.IncludeDirectory("~/Styles/images_jq/", "*.gif");

        bundles.Add(scripts);
        bundles.Add(css);
    }
    }
}