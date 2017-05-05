using Admin.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace Physio
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
         {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            RegisterCustomControllerFactory();
        }
        
        protected void Application_Error(object sender, EventArgs e)
        {
            Server.ClearError();
        }
        private void RegisterCustomControllerFactory()
        {
            //DefaultControllerFactory DefaultFactory = new DefaultControllerFactory();
            //IControllerFactory factory = new CustomControllerFactory("Admin.Controllers");
            IControllerFactory factory = new CustomControllerFactory();
            ControllerBuilder.Current.SetControllerFactory(factory);
        }
    }
}
