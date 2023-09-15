using Licitacion.Servicios;
using Licitaciones.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Licitaciones.Controllers.Api
{
    public class DepartamentoController : ApiController
    {
        /// <summary>
        /// Lista los Departamentos disponibles
        /// </summary>
        /// <returns>Lista total de Departamentos sin filtro</returns>
        /// <response code="200">Ok devuelve lista</response>
        [Route("api/Departamento/Listar")]
        [System.Web.Http.ActionName("Listar")]
        [System.Web.Http.HttpGet]
        public Respuesta Listar()
        {
            Respuesta respuesta = new Respuesta();
            ServicioDepartamento servicio = new ServicioDepartamento();
            var lista = servicio.listar();
            respuesta.codigo = 1;
            respuesta.replay = lista;
            return respuesta;
        }

        /// <summary>
        /// Lista de Departamentos cargados a una Obra (se usa para Select2)
        /// </summary>
        /// <param name="idObra">identificacion de obra</param>
        /// <returns>Lista total de Departamentos filtradas por Obra</returns>
        /// <response code="200">Ok devuelve string</response>
        [Route("api/Departamento/listarPorProyectoStr")]
        [System.Web.Http.ActionName("listarPorProyectoStr")]
        [System.Web.Http.HttpGet]
        public Respuesta listarPorProyectoStr(int? idObra)
        {
            Respuesta respuesta = new Respuesta();
            ServicioDepartamento servicio = new ServicioDepartamento();
            var lista = servicio.listarPorProyectoString(idObra);
            respuesta.codigo = 1;
            respuesta.replay = lista;
            return respuesta;
        }
    }
}
