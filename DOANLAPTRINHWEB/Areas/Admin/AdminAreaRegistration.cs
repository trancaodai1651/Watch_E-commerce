using System.Web.Mvc;

namespace DOANLAPTRINHWEB.Areas.Admin
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
                new { controller= "Product", action = "Dashbroad", id = UrlParameter.Optional },
                new[] { "DOANLAPTRINHWEB.Areas.Admin.Controllers" }
            );
        }
    }
}