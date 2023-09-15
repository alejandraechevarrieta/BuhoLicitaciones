using System.Web.Mvc;

namespace Licitaciones.Areas.Licitacion
{
    public class LicitacionAreaRegistration : AreaRegistration
    {
        public override string AreaName
        {
            get
            {
                return "Licitacion";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context)
        {
            context.MapRoute(
                "Licitacion_default",
                "Licitacion/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}