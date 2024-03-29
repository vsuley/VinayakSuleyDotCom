﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace VinayakSuleyDotCom
{
    // Note: For instructions on enabling IIS6 or IIS7 classic mode, 
    // visit http://go.microsoft.com/?LinkId=9394801

    public class MvcApplication : System.Web.HttpApplication
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                "Tagged photos",
                "Portfolio/{tag}",
                new { controller = "Portfolio", action = "TaggedPhotos"}
            );

            routes.MapRoute(
                "Recent photos",
                "Portfolio",
                new { controller = "Portfolio", action = "RecentPhotos" }
            );
            
            routes.MapRoute(
                "Default", // Route name
                "{controller}/{action}/{id}", // URL with parameters
                new { controller = "TextPages", action = "Index", id = UrlParameter.Optional } // Parameter defaults
            );
            
            

        }

        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();

            RegisterRoutes(RouteTable.Routes);
        }
    }
}