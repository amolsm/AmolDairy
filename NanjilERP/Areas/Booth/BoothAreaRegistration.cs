using System.Web.Mvc;

namespace NanjilERP.Areas.Booth
{
    public class BoothAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "Booth";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute(
                "Booth_default",
                "Booth/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}