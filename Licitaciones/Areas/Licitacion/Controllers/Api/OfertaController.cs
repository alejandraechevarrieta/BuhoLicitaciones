using Licitacion.Servicios;
using Licitaciones.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Licitaciones.Areas.Licitacion.Controllers.Api
{
    public class OfertaController : ApiController
    {
        /// <summary>
        /// Busca las empresas que han hecho oferta en una obra
        /// </summary>
        /// <param name="idObra">identificacion de obra</param>
        /// <returns>Objeto que contiene la informacion</returns>
        /// <response code="200">Ok devuelve lista de Empresas</response>
        [Route("api/Oferta/buscarEmpresas")]
        [System.Web.Http.ActionName("buscarEmpresas")]
        [System.Web.Http.HttpGet]
        public Respuesta buscarEmpresas(int? idObra)
        {
            Respuesta respuesta = new Respuesta();
            try
            {
                ServicioOferta servicio = new ServicioOferta();
                var tmp = servicio.listarEmpresasPorObra(idObra);
                respuesta.codigo = 1;
                respuesta.replay = tmp;
            }
            catch (Exception ex)
            {
                respuesta.codigo = 0;
                respuesta.mensaje = "Ocurrio un error " + ex.Message;
            }
            return respuesta;
        }
    }
}
