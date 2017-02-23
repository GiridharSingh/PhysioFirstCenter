using DAL;
using DAL.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using System.Web.SessionState;

namespace Admin.Controllers
{
    public class CustomControllerFactory: IControllerFactory
    {
        private readonly string _controllerNamespace;
        public CustomControllerFactory(string controllerNamespace)
        {
            _controllerNamespace = controllerNamespace;
        }
        public IController CreateController(System.Web.Routing.RequestContext requestContext, string controllerName)
        {
            Type controllerType = Type.GetType(string.Concat(_controllerNamespace, ".", controllerName, "Controller"));
            dynamic service = new ServicesDAL();
            switch (controllerName)
            {
                case "Services" :
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
            IController controller = Activator.CreateInstance(controllerType, new[] { service }) as Controller;
            return controller;

            
            
        }
        public SessionStateBehavior GetControllerSessionBehavior(RequestContext requestContext, string controllerName)
        {
            return SessionStateBehavior.Default;
        }
        public void ReleaseController(IController controller)
        {
            IDisposable disposable = controller as IDisposable;
            if (disposable != null)
                disposable.Dispose();
        }
    }
}