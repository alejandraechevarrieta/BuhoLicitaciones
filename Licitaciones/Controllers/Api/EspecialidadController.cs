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
    public class EspecialidadController : ApiController
    {
        /// <summary>
        /// Lista de Especialidades disponibles
        /// </summary>
        /// <returns>Lista total de Especialidades sin filtro</returns>
        /// <response code="200">Ok devuelve lista</response>
        [Route("api/Especialidad/Listar")]
        [System.Web.Http.ActionName("Listar")]
        [System.Web.Http.HttpGet]
        public Respuesta Listar()
        {
            Respuesta respuesta = new Respuesta();
            ServicioEspecialidad servicio = new ServicioEspecialidad();
            var lista = servicio.listar();
            respuesta.codigo = 1;
            respuesta.replay = lista;
            return respuesta;
        }
        [Route("api/Especialidad/ListarSubEspecialidad")]
        [System.Web.Http.ActionName("ListarSubEspecialidad")]
        [System.Web.Http.HttpGet]
        public Respuesta LiListarSubEspecialidadstar()
        {
            Respuesta respuesta = new Respuesta();
            ServicioEspecialidad servicio = new ServicioEspecialidad();
            var lista = servicio.ListarSubEspecialidad();
            respuesta.codigo = 1;
            respuesta.replay = lista;
            return respuesta;
        }

        /// <summary>
        /// Lista de Especialidades cargadas a una Obra
        /// </summary>
        /// <param name="idObra">identificacion de obra</param>
        /// <returns>Lista total de Especialidades filtradas por Obra</returns>
        /// <response code="200">Ok devuelve lista</response>
        [Route("api/Especialidad/listarPorProyecto")]
        [System.Web.Http.ActionName("listarPorProyecto")]
        [System.Web.Http.HttpGet]
        public Respuesta listarPorProyecto(int? idObra)
        {
            Respuesta respuesta = new Respuesta();
            ServicioEspecialidad servicio = new ServicioEspecialidad();
            var lista = servicio.listarPorProyecto(idObra);
            if(lista.Count > 0)
            {
                respuesta.codigo = 1;
            }
            else
            {
                respuesta.codigo = 0;
            }
            respuesta.replay = lista;
            return respuesta;
        }

        /// <summary>
        /// Lista de Especialidades cargadas a una Obra (se usa para Select2)
        /// </summary>
        /// <param name="idObra">identificacion de obra</param>
        /// <returns>Lista total de Especialidades filtradas por Obra</returns>
        /// <response code="200">Ok devuelve string</response>
        [Route("api/Especialidad/listarPorProyectoStr")]
        [System.Web.Http.ActionName("listarPorProyectoStr")]
        [System.Web.Http.HttpGet]
        public Respuesta listarPorProyectoStr(int? idObra)
        {
            Respuesta respuesta = new Respuesta();
            ServicioEspecialidad servicio = new ServicioEspecialidad();
            var lista = servicio.listarPorProyectoString(idObra);
            if(lista.Length > 2)
            {
                respuesta.codigo = 1;
            }
            else
            {
                respuesta.codigo = 0;
            }
            
            respuesta.replay = lista;
            return respuesta;
        }

        [Route("api/Especialidad/listarSubEspPorEspecialidadString")]
        [System.Web.Http.ActionName("listarSubEspPorEspecialidadString")]
        [System.Web.Http.HttpGet]
        public Respuesta listarSubEspPorEspecialidadString(string especialidad)
        {
            Respuesta respuesta = new Respuesta();
            ServicioEspecialidad servicio = new ServicioEspecialidad();
            var lista = servicio.listarSubEspPorEspecialidadString(especialidad);
            if (lista.Length > 2)
            {
                respuesta.codigo = 1;
            }
            else
            {
                respuesta.codigo = 0;
            }

            respuesta.replay = lista;
            return respuesta;
        }
    }
}