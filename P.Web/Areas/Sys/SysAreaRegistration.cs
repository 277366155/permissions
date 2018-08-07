using System.Web.Mvc;

namespace P.Web.Areas.Sys
{
    public class SysAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "Sys";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute(
                "Sys_default",
                "Sys/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional },
                new string[] { "P.Web.Areas.Sys.Controllers" }
            );
        }
    }
}