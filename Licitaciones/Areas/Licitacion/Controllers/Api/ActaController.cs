using Licitacion.Servicios;
using Licitaciones.ViewModels;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web.Http;

namespace Licitaciones.Areas.Licitacion.Controllers.Api
{
    public class ActaController : ApiController
    {
        /// <summary>
        /// Buscar Acta Generica
        /// </summary>
        /// <param name="idActa">Identificador Acta Generica</param>
        /// <returns>string con mensaje de exito o error</returns>
        /// <response code="200">Ok devuelve Objeto</response>
        [Route("api/Acta/BuscarGenerica")]
        [System.Web.Http.ActionName("BuscarGenerica")]
        [System.Web.Http.HttpGet]
        public ActaViewModels BuscarGenerica(int? idActa, int? idObra)
        {
            ActaViewModels unActa = new ActaViewModels();
            try
            {
                ServicioActa servicio = new ServicioActa();
                var tmp = servicio.buscarActaGenerica(idActa, idObra);
                unActa = tmp;
                return unActa;
            }
            catch (Exception ex)
            {
            }
            return unActa;
        }

        /// <summary>
        /// Editar Acta
        /// </summary>
        /// <param name="idActa">Identificador Acta Generica</param>
        /// <returns>string con mensaje de exito o error</returns>
        /// <response code="200">Ok devuelve Objeto</response>
        [Route("api/Acta/EditarActa")]
        [System.Web.Http.ActionName("EditarActa")]
        [System.Web.Http.HttpGet]
        public ActaViewModels EditarActa(int? idObra)
        {
            ActaViewModels unActa = new ActaViewModels();
            try
            {
                ServicioActa servicio = new ServicioActa();
                var tmp = servicio.buscarActaObra(idObra);
                unActa = tmp;
                return unActa;
            }
            catch (Exception ex)
            {
            }
            return unActa;
        }

        /// <summary>
        /// Buscar Acta Generica
        /// </summary>
        /// <param name="idActa">Identificador Acta Generica</param>
        /// <returns>string con mensaje de exito o error</returns>
        /// <response code="200">Ok devuelve Objeto</response>
        [Route("api/Acta/BuscarFormal")]
        [System.Web.Http.ActionName("BuscarFormal")]
        [System.Web.Http.HttpGet]
        public ActaViewModels BuscarFormal(int? idActa, int? idObra)
        {
            ActaViewModels unActa = new ActaViewModels();
            try
            {
                ServicioActa servicio = new ServicioActa();
                var tmp = servicio.buscarActaFormal(idActa, idObra);
                unActa = tmp;
                return unActa;
            }
            catch (Exception ex)
            {
            }
            return unActa;
        }

        /// <summary>
        /// Grabar Acta Obra
        /// </summary>
        /// <param name="unActa">datos Acta Obra</param>
        /// <returns>string con mensaje de exito o error</returns>
        /// <response code="200">Ok</response>
        [Route("api/Acta/GrabarActaObra")]
        [System.Web.Http.ActionName("GrabarActaObra")]
        [System.Web.Http.HttpPost]
        public int GrabarActaObra(ActaViewModels actaView)
        {
            try
            {
                ServicioActa _servicio = new ServicioActa();
                var tmp = _servicio.grabarActaObra(actaView);
                ServicioPDF servicio = new ServicioPDF();
                var unActa = _servicio.buscarActaObra(actaView.idObra);
                string filePath = ConfigurationSettings.AppSettings["repositorioFiles"].ToString();
                filePath = filePath + "ActaApertura_" + actaView.idObra + ".pdf";
                servicio.generarActaApertura(filePath, unActa);
                _servicio.grabarArchivo(actaView.idObra, 1, "ActaApertura_" + actaView.idObra, filePath);

                return tmp;
            }
            catch (Exception ex)
            {
            }
            return 0;
        }

        [Route("api/Acta/ImprimirActa")]
        [System.Web.Http.ActionName("ImprimirActa")]
        [System.Web.Http.HttpGet]
        public IHttpActionResult ImprimirActa(int? idObra)
        {
            ServicioPDF servicio = new ServicioPDF();
            ServicioActa _servicio = new ServicioActa();
            var unActa = _servicio.buscarActaObra(idObra);
            string filePath = ConfigurationSettings.AppSettings["repositorioFiles"].ToString();
            filePath = filePath + "ActaApertura_" + idObra + ".pdf";
            servicio.generarActaApertura(filePath, unActa);
            _servicio.grabarArchivo(idObra, 1, "ActaApertura_" + idObra, filePath);
            return Ok();
        }

        [Route("api/Acta/DescargarArchivo")]
        [System.Web.Http.ActionName("DescargarArchivo")]
        [HttpGet]
        public IHttpActionResult DescargarArchivo(int? idObra, int? idCategoria)
        {
            HttpResponseMessage result = null;
            ServicioActa servicio = new ServicioActa();
            var unArchivo = servicio.buscarArchivo(idObra, idCategoria);
            if (string.IsNullOrEmpty(unArchivo.ruta))
            {
                return BadRequest();
            }
            else
            {
                string filePath = unArchivo.ruta;
                IHttpActionResult response;
                HttpResponseMessage responseMsg = new HttpResponseMessage(HttpStatusCode.OK);
                var fileStream = new FileStream(filePath, FileMode.Open);
                responseMsg.Content = new StreamContent(fileStream);
                responseMsg.Content.Headers.ContentType = new MediaTypeHeaderValue("application/octet-stream");
                responseMsg.Content.Headers.ContentDisposition = new ContentDispositionHeaderValue("attachment");
                responseMsg.Content.Headers.ContentDisposition.FileName = unArchivo.nombre + ".pdf";
                response = ResponseMessage(responseMsg);
                return response;
            }
        }

        [Route("api/Acta/buscarTieneActa")]
        [System.Web.Http.ActionName("buscarTieneActa")]
        [System.Web.Http.HttpGet]
        public int buscarTieneActa(int? idObra)
        {
            ServicioActa _servicio = new ServicioActa();
            var value = _servicio.buscarTieneActa(idObra);
            return value;
        }
    }
}
