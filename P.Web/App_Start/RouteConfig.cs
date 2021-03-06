﻿using System.Web.Mvc;
using System.Web.Routing;

namespace P.Web
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { area = "Common", controller = "Login", action = "Index", id = UrlParameter.Optional },
                namespaces: new string[] { "P.Web.Areas.Common.Controllers" }
            ).DataTokens.Add("Area", "Common");

            routes.MapRoute(
                           name: "comm_Default",
                           url: "Common/{controller}/{action}/{id}",
                           defaults: new { area = "Common", controller = "Login", action = "Index", id = UrlParameter.Optional },
                           namespaces: new string[] { "P.Web.Areas.Common.Controllers" }
                       ).DataTokens.Add("Area", "Common");
        }
    }
}