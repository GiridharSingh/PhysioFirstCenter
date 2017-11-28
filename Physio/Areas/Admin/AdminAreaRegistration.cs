using System.Web.Mvc;

namespace Physio.Areas.Admin
{
    public class AdminAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "Admin";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {            
            context.MapRoute(
                "Admin_default",
                "Admin/{controller}/{action}/{id}",
                defaults: new { action = "Index", controller = "AboutClinic", id = UrlParameter.Optional },
                namespaces: new[] { "Physio.Areas.Admin.Controllers" }
                    );
        }
    }
}