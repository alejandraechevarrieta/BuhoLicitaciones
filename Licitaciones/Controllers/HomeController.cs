//using Licitaciones.BaseDato;
using System;
using System.Security.Principal;
using System.Web.Mvc;
using System.Linq;
using Licitaciones.Areas.Licitacion.Controllers;
using System.Threading;
using System.Configuration;


namespace Licitaciones.Areas.Licitacion.Controllers
{
    //public ActionResult Verificar(int id)
    //{
    //    try
    //    {
    //        db_meieEntities db = new db_meieEntities();

    //        var usuario = (db.GralUsuario.Where(x => x.idUsuario == id).ToList().FirstOrDefault());
    //        if (usuario != null)
    //        {

    //            System.Web.HttpContext.Current.Session["nombre"] = usuario.Nombre;   //           


    //        }
    //        return View();
    //    }
    //    catch (UnauthorizedAccessException e)
    //    {
    //        //ViewBag.User = e.Message;
    //    }

    //    return View();
    //}
    //public class HomeController : Controller
    //{
    //    public ActionResult Index()
    //    {
    //        return View("../../Licitacion/Index");
    //    }
    //    public ActionResult Empresa()
    //    {
    //        return View("../../Licitacion/Empresa");
    //    }
    //}
}