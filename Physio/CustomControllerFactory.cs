using DAL;
using DAL.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using System.Web.SessionState;
//IControllerFactory
namespace Physio
{
    public class CustomControllerFactory: DefaultControllerFactory 
    {
        
        public override IController CreateController(System.Web.Routing.RequestContext requestContext, string controllerName)  
        {
            IController controller = null;
            dynamic service = null;
            switch (controllerName)
            {
                case "Services":
                    {
                        service = new ServicesDAL();
                        break;
                    }
                case "Histories":
                    {
                        service = new HistoryDAL();
                        break;
                    }
                case "Employees":
                    {
                        service = new EmployeeDAL();
                        break;
                    }                

            }
            Type controllerType = GetControllerType(requestContext, controllerName);
            if (service != null)
            {
                controller = Activator.CreateInstance(controllerType, new[] { service }) as Controller;
            }
            else {
                controller = Activator.CreateInstance(controllerType) as Controller;
            }
                               
            return controller;                        
        }
        
        public SessionStateBehavior GetControllerSessionBehavior(RequestContext requestContext, string controllerName)
        {
            return SessionStateBehavior.Default;
        }
        public override void ReleaseController(IController controller)
        {
            IDisposable disposable = controller as IDisposable;
            if (disposable != null)
                disposable.Dispose();
        }
    }
}