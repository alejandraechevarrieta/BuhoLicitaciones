using Licitacion.Models;
using Licitacion.Servicios.Utiles;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Mvc;


namespace Licitaciones.Areas.Login.Controllers
{
    public class LoginController : Controller
    {
        // GET: Login/Index
        //public ActionResult Index(int? idUsuario, int? tipoEmpresa)
        //{
        //    string entrarPorApi = ConfigurationSettings.AppSettings["validarApi"].ToString();
        //    //string entrarPorApi = "true";
        //    System.Web.HttpContext.Current.Session["validarApi"] = entrarPorApi;
        //    System.Web.HttpContext.Current.Session["idUsuario"] = idUsuario;
        //    System.Web.HttpContext.Current.Session["idEmpresa"] = idUsuario;
        //    System.Web.HttpContext.Current.Session["tipoEmpresa"] = tipoEmpresa;
        //    System.Web.HttpContext.Current.Session["urlRacop"] = ConfigurationSettings.AppSettings["urlRacop"].ToString();
        //    System.Web.HttpContext.Current.Session["urlBuho"] = ConfigurationSettings.AppSettings["urlBuho"].ToString();
        //    System.Web.HttpContext.Current.Session["menuHC"] = ConfigurationSettings.AppSettings["menuHC"].ToString();
        //    System.Web.HttpContext.Current.Session["urlBaseBuhoBlanco"] = ConfigurationSettings.AppSettings["urlBaseBuhoBlanco"].ToString();
        //    System.Web.HttpContext.Current.Session["urlBaseBuhoRacop"] = ConfigurationSettings.AppSettings["urlBaseBuhoRacop"].ToString();
           
        //    //System.Web.HttpContext.Current.Session["idOficina"] = 1;
        //    if (entrarPorApi == "true")
        //    {                
        //        if (idUsuario != null && tipoEmpresa != null)
        //        {
        //            return ValidarIngreso(idUsuario, tipoEmpresa);
                   
        //        }
        //        else
        //        {
        //            return View("~/Areas/Login/Views/Login/Index.cshtml");
        //        }
        //    }
        //    else
        //    {
        //        //sino entro con usuario fijo

        //        System.Web.HttpContext.Current.Session["tipoEmpresa"] = ConfigurationSettings.AppSettings["tipoEmpresaConfig"].ToString();
        //        System.Web.HttpContext.Current.Session["idEmpresa"] = 3;
        //        System.Web.HttpContext.Current.Session["idUsuario"] = 1;
        //        System.Web.HttpContext.Current.Session["idOficina"] = 1;

        //        if (ConfigurationSettings.AppSettings["tipoEmpresaConfig"].ToString() == "2")
        //        {
        //            return RedirectToAction("Empresa", "Licitacion", new { area = "Licitacion" });
        //        }
        //        else
        //        {
        //            return RedirectToAction("Index", "Licitacion", new { area = "Licitacion" });
        //        }
                
        //    }

        //}
        public ActionResult Index(string hash)
        {
            try
            {
                string entrarPorApi = ConfigurationSettings.AppSettings["validarApi"].ToString();
                int? idUsuario = null;
                int? tipoEmpresa = null;
                int? idEmpresa = null;
                if (hash != null && hash != "")
                {
                    SecurityMd5Models encriptador = new SecurityMd5Models();
                    var hashEncripted = encriptador.DecriptMd5(hash);
                    var detalleHash = hashEncripted.Split(',');
                    var detalleTiempo = detalleHash[2].Split('-');
                    string formato = "ddMMyyyy-HH-mm";
                    DateTime fecha = DateTime.ParseExact(detalleHash[2], formato, System.Globalization.CultureInfo.InvariantCulture);
                    //var tiempo = DateTime.Parse(detalleTiempo[0]);
                    var tmp = (DateTime.Now - fecha).TotalMinutes;
                    if (tmp >= 30)
                    {
                        //return se le expiró la sesión
                        return View("~/Areas/Login/Views/Login/SesionExpirada.cshtml");
                    }
                    idUsuario = Convert.ToInt32(detalleHash[0]);
                    tipoEmpresa = Convert.ToInt32(detalleHash[1]);

                    if (detalleHash.Length > 3) // Se verifica si hay un cuarto parámetro
                    {
                        idEmpresa = Convert.ToInt32(detalleHash[3]); // Se asigna el cuarto parámetro a idEmpresa
                    }
                }


                //string entrarPorApi = "true";
                System.Web.HttpContext.Current.Session["validarApi"] = entrarPorApi;
                System.Web.HttpContext.Current.Session["idUsuario"] = tipoEmpresa == 2 ? idEmpresa : idUsuario; // en RACOP envia el usuario en el id de Empresa
                System.Web.HttpContext.Current.Session["idEmpresa"] = tipoEmpresa == 2? idUsuario: idEmpresa; // en RACOP envia el usuario en el id de Empresa
                System.Web.HttpContext.Current.Session["tipoEmpresa"] = tipoEmpresa;
                System.Web.HttpContext.Current.Session["urlRacop"] = ConfigurationSettings.AppSettings["urlRacop"].ToString();
                System.Web.HttpContext.Current.Session["urlBuho"] = ConfigurationSettings.AppSettings["urlBuho"].ToString();
                System.Web.HttpContext.Current.Session["menuHC"] = ConfigurationSettings.AppSettings["menuHC"].ToString();
                System.Web.HttpContext.Current.Session["urlBaseBuhoBlanco"] = ConfigurationSettings.AppSettings["urlBaseBuhoBlanco"].ToString();
                System.Web.HttpContext.Current.Session["urlBaseBuhoRacop"] = ConfigurationSettings.AppSettings["urlBaseBuhoRacop"].ToString();

                //System.Web.HttpContext.Current.Session["idOficina"] = 1;
                if (entrarPorApi == "true")
                {
                    if (idUsuario != null && tipoEmpresa != null)
                    {
                        return ValidarIngreso(idUsuario, tipoEmpresa);

                    }
                    else
                    {
                        return View("~/Areas/Login/Views/Login/Index.cshtml");
                    }
                }
                else
                {
                    //sino entro con usuario fijo

                    System.Web.HttpContext.Current.Session["tipoEmpresa"] = ConfigurationSettings.AppSettings["tipoEmpresaConfig"].ToString();
                    System.Web.HttpContext.Current.Session["idEmpresa"] = 1015;
                    System.Web.HttpContext.Current.Session["idUsuario"] = 1;
                    System.Web.HttpContext.Current.Session["idOficina"] = 1;

                    if (ConfigurationSettings.AppSettings["tipoEmpresaConfig"].ToString() == "2")
                    {
                        return RedirectToAction("Empresa", "Licitacion", new { area = "Licitacion" });
                    }
                    else
                    {
                        return RedirectToAction("Index", "Licitacion", new { area = "Licitacion" });
                    }

                }
            }
            catch(Exception e)
            {
                return View("~/Areas/Login/Views/Login/Index.cshtml");
            }
            

        }
        public ActionResult ValidarIngreso(int? idUsuario, int? tipoEmpresa)
        {          
            
            return View("~/Areas/Login/Views/Login/ValidarIngreso.cshtml");
        }
        public ActionResult CierreSesion()
        {
            System.Web.HttpContext.Current.Session["idUsuario"] = null;
            System.Web.HttpContext.Current.Session["tipoEmpresa"] = null;
            System.Web.HttpContext.Current.Session["urlRacop"] = null;
            System.Web.HttpContext.Current.Session["urlBuho"] = null;

            return View("~/Areas/Login/Views/Login/CierreSesion.cshtml");
        }
        public ActionResult AccesoDenegado()
        {
           
            return View("~/Areas/Login/Views/Login/AccesoDenegado.cshtml");
        }

    }

}
