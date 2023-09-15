using Licitacion.Servicios;
using Licitaciones.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using System.Web.Services;

namespace Licitaciones.Areas.Licitacion.Controllers.Api
{
    public class LoginController : ApiController
    {
        [Route("api/Login/Validar")]
        // GET: api/Hora
        [System.Web.Http.ActionName("Validar")]
        [System.Web.Http.HttpGet]
        [WebMethod(EnableSession = true)]
        public empresaLoginViewModels Validar(string cuit, string password)
    {
        ServicioLogin _servicio = new ServicioLogin();
            List<empresaLoginViewModels> list = new List<empresaLoginViewModels>();
            var tmp = _servicio.Validar(cuit, password);
            if(tmp != null)
            {
                //HttpContext.Session.SetString("tipoEmpresa", "2");
                System.Web.HttpContext.Current.Session["tipoEmpresa"] = "2";
                System.Web.HttpContext.Current.Session["inicioSesion"] = "true";
                System.Web.HttpContext.Current.Session["nombreUsuario"] = tmp.razonSocial;
                System.Web.HttpContext.Current.Session["cuit"] = tmp.cuit;
                System.Web.HttpContext.Current.Session["idUsuario"] = tmp.idEmpresa;
            }
          
            //((IDisposable)_servicio).Dispose();
            return tmp;
    }

        [Route("api/Login/ObtenerMenu")]
        // GET: api/Hora
        [System.Web.Http.ActionName("ObtenerMenu")]
        [System.Web.Http.HttpGet]
        [WebMethod(EnableSession = true)]
        public List<AccessViewModel> ObtenerMenu(int idUsuario)
        {
            ServicioLogin _servicio = new ServicioLogin();
           
            var tmp = _servicio.ObtenerMenu(idUsuario);
            if (tmp != null)
            {
              
            }

            //((IDisposable)_servicio).Dispose();
            return tmp;
        }

    }
   
}
