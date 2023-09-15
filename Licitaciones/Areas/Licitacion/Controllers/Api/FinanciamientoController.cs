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
    public class FinanciamientoController : ApiController
    {
        /// <summary>
        /// Lista las fuentes de financiamiento disponibles
        /// </summary>
        /// <returns>Lista total de financiamiento sin filtro</returns>
        /// <response code="200">Ok devuelve lista</response>
        [Route("api/Financiamiento/Listar")]
        [System.Web.Http.ActionName("Listar")]
        [System.Web.Http.HttpGet]
        public Respuesta Listar()
        {
            Respuesta respuesta = new Respuesta();
            ServicioFinanciamiento servicio = new ServicioFinanciamiento();
            var lista = servicio.listar();
            respuesta.codigo = 1;
            respuesta.replay = lista;
            return respuesta;
        }
    }
}
