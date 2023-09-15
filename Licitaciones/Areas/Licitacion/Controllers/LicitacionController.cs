using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Licitaciones.Areas.Licitacion.Controllers
{
    public class LicitacionController : Controller
    {
        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        private static string ruta = System.Web.HttpRuntime.AppDomainAppPath + "\\bin\\log4net.config";
        // GET: Licitacion
        public ActionResult Index()
        {
            try
            {
                log4net.Config.XmlConfigurator.Configure(new FileInfo(ruta));
                log.Info("Controller Licitacion");
                if (System.Web.HttpContext.Current.Session["tipoEmpresa"].ToString() == "2")
                {
                    return RedirectToAction("AccesoDenegado", "Login", new { area = "Login" });
                }
                return View("~/Areas/Licitacion/Views/Licitacion/Index.cshtml");
            }
            catch(Exception e)
            {
                return RedirectToAction("CierreSesion", "Login", new { area = "Login" });
            }
           
        }
        public ActionResult Index2()
        {
            return View();
        }
        public ActionResult Empresa()
        {
            try
            {
                if (System.Web.HttpContext.Current.Session["tipoEmpresa"].ToString() == "1")
                {
                    return RedirectToAction("AccesoDenegado", "Login", new { area = "Login" });
                }
                return View();
            }
            catch(Exception e)
            {
                return RedirectToAction("CierreSesion", "Login", new { area = "Login" });
            }
            
        }
    }
}