﻿using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace pivotalbankmvctest
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {



            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }

        protected void Application_Error(object sener, EventArgs e)
        {
            var le = Server.GetLastError();
            Console.WriteLine("Unhandled Exception " + le.Message + " " + le.StackTrace);
        }
    }
}
