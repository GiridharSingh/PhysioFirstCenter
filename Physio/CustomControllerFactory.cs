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
            dynamic obj = null;
            switch (controllerName)
            {
                case "Services":
                    {
                        obj = new ServicesDAL();
                        break;
                    }
                case "Histories":
                    {
                        obj = new HistoryDAL();
                        break;
                    }
                case "Employees":
                    {
                        obj = new EmployeeDAL();
                        break;
                    }
                case "AboutClinic":
                    {
                        obj = new AboutClinicDAL();
                        break;
                    }
                case "Testimonial":
                    {
                        obj = new TestimonialsDAL();
                        break;
                    } 

            }
            Type controllerType = GetControllerType(requestContext, controllerName);
            if (obj != null)
            {
                controller = Activator.CreateInstance(controllerType, new[] { obj }) as Controller;
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