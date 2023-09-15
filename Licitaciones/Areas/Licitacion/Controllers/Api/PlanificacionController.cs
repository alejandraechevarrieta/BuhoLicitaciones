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
    public class PlanificacionController : ApiController
    {
        /// <summary>
        /// Buscar la planificacion del proyecto
        /// </summary>
        /// <param name="idObra">identificacion de obra</param>
        /// <returns>Planificacion filtrada por proyecto</returns>
        /// <response code="200">Ok devuelve objeto</response>
        [Route("api/Planificacion/buscarUna")]
        [System.Web.Http.ActionName("buscarUna")]
        [System.Web.Http.HttpGet]
        public Respuesta buscarUna(int? idObra)
        {
            Respuesta respuesta = new Respuesta();
            ServicioPlanificacion servicio = new ServicioPlanificacion();
            var planif = servicio.buscarPorProyecto(idObra);
            respuesta.codigo = 1;
            respuesta.replay = planif;
            return respuesta;
        }
    }


}
