using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace StockBDWeb
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
            name: "MinuteChart",
            url: "MinuteChart/Show/{selectTicker}/{selectDate}",
            defaults: new { controller = "MinuteChart", action = "Show" }
            );

            routes.MapRoute(
                name: "DailyChart",
                url: "DailyChart/Show/{selectTicker}/{selectDuration}",
                defaults: new  { controller="DailyChart", action="Show" }
                );
            
            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}