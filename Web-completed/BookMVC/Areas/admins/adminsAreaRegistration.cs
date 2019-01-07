using System.Web.Mvc;

namespace BookMVC.Areas.admins
{
    public class adminsAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "admins";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute(
                "admins_default",
                "admins/{controller}/{action}/{id}",
                new {controller ="Home", action = "Index", id = UrlParameter.Optional },
                new[] { "BookMVC.Areas.admins.Controllers" } // khai bao them namespace de khong bi xung dot giua Area va main (cung voi file RouteConfig.cs)
            );
        }
    }
}