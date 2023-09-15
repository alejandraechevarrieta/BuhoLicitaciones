using Licitacion.Servicios;
using Licitaciones.ViewModels;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Web.Http;

namespace Licitaciones.Areas.Licitacion.Controllers.Api
{
    public class LicitacionController : ApiController
    {
        /// <summary>
        /// Completa la grilla de Licitaciones desde la vista del Administrador 
        /// </summary>
        /// <param name="form"></param>
        /// <param name="nroExpediente"></param>
        /// <param name="nombreObra"></param>
        /// <param name="fechaPub"></param>
        /// <param name="idOrganismo"></param>
        /// <param name="idEstado"></param>
        /// <returns>Lista de Obras filtradas en etapa de Licitaciones</returns>
        /// <response code="200">Ok devuelve lista</response>
        [Route("api/Licitacion/listarObra")]
        [System.Web.Http.ActionName("listarObra")]
        [System.Web.Http.HttpPost]
        public HttpResponseMessage listarObra(FormDataCollection form,
            string nroExpediente, string nombreObra, DateTime? fechaPub, int? idOrganismo, int? idEstado, int? nroObra)
        {
            var draw = form.Get("draw");
            var start = form.Get("start");
            var length = form.Get("length");
            var sortColumn = (form.Get("columns[" + form.Get("order[0][column]").FirstOrDefault() + "][data]").ToString()).ToString();
            var sortColumnDir = form.Get("order[0][dir]").ToString();
            var searchValue = form.Get("search[value]").ToString();
            string qObra = ConfigurationSettings.AppSettings["qObraOficina"].ToString();

            int pageSize = length != null ? Convert.ToInt32(length) : 0;
            int skip = start != null ? Convert.ToInt32(start) : 0;
            int recordsTotal = 0;
            ServicioLicitacion servicio = new ServicioLicitacion();

            var lst = servicio.listaLicitaciones(ref recordsTotal, sortColumn, sortColumnDir, null,
                nroExpediente, nombreObra, qObra, idEstado, fechaPub, idOrganismo,nroObra, pageSize, skip);

            var json = Json(new { draw = draw, recordsFiltered = recordsTotal, recordsTotal = recordsTotal, data = lst });

            return Request.CreateResponse(HttpStatusCode.OK, new { draw = draw, recordsFiltered = recordsTotal, recordsTotal = recordsTotal, data = lst }, Configuration.Formatters.JsonFormatter);

        }

        /// <summary>
        /// Totaliza las obras en etapa de licitacion por SubEtapa
        /// </summary>
        /// <returns>Objeto que contiene las cantidades totales</returns>
        /// <response code="200">Ok devuelve objeto</response>
        [Route("api/Licitacion/calcularTarjeta")]
        [System.Web.Http.ActionName("calcularTarjeta")]
        [System.Web.Http.HttpGet]
        public Respuesta calcularTarjeta
            (string nroExpediente, string nombreObra, DateTime? fechaPub, int? idOrganismo, int? nroObra)
        {
            Respuesta respuesta = new Respuesta();
            tarjetaLicViewModels tarjeta = new tarjetaLicViewModels();
            try
            {
                string qObra = ConfigurationSettings.AppSettings["qObraOficina"].ToString();
                ServicioLicitacion servicio = new ServicioLicitacion();
                tarjeta = servicio.calcularTarjeta(nroExpediente, nombreObra, qObra, fechaPub, idOrganismo, nroObra);
                respuesta.codigo = 1;
                respuesta.replay = tarjeta;
            }
            catch (Exception ex)
            {
                respuesta.codigo = 0;
                respuesta.mensaje = "Ocurrio un error " + ex.Message;
            }
            return respuesta;
        }

        /// <summary>
        /// Busca los datos generales de una obra
        /// </summary>
        /// <param name="idObra">identificacion de obra</param>
        /// <returns>Objeto que contiene la informacion</returns>
        /// <response code="200">Ok devuelve objeto</response>
        [Route("api/Licitacion/buscarUna")]
        [System.Web.Http.ActionName("buscarUna")]
        [System.Web.Http.HttpGet]
        public Respuesta buscarUna(int? idObra)
        {
            Respuesta respuesta = new Respuesta();
            unLicitacionViewModels tmp = new unLicitacionViewModels();
            try
            {
                ServicioLicitacion servicio = new ServicioLicitacion();
                tmp = servicio.buscarUna(idObra);
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

        /// <summary>
        /// Grabar los detalles en la tabla de Licitaciones
        /// </summary>
        /// <param name="unLicitacion">detalle de obra</param>
        /// <returns>numero entero</returns>
        /// <response code="200">Ok devuelve objeto</response>
        [Route("api/Licitacion/GrabarDatos")]
        [System.Web.Http.ActionName("GrabarDatos")]
        [System.Web.Http.HttpPost]
        public async Task<int> GrabarDatos(unLicitacionViewModels unLicitacion)
        {
            var tmp = 0;
            try
            {
                ServicioLicitacion servicio = new ServicioLicitacion();
                tmp = await servicio.grabarDetalleLicitacion(unLicitacion);
                tmp = await servicio.grabarDetalleProyecto(unLicitacion);
                tmp = await servicio.grabarLicitacion(unLicitacion);
            }
            catch (Exception ex)
            {
            }
            return tmp;
        }

        /// <summary>
        /// Grabar los detalles en la tabla de Licitaciones
        /// </summary>
        /// <param name="unLicitacion">detalle de obra</param>
        /// <returns>numero entero</returns>
        /// <response code="200">Ok devuelve objeto</response>
        [Route("api/Licitacion/GrabarEspecialidad")]
        [System.Web.Http.ActionName("GrabarEspecialidad")]
        [System.Web.Http.HttpPost]
        public async Task<int> GrabarEspecialidad(especialidadViewModels unEspecialidad)
        {
            var tmp = 0;
            try
            {
                ServicioLicitacion servicio = new ServicioLicitacion();
                tmp = await servicio.grabarEspecialidad(unEspecialidad);
            }
            catch (Exception ex)
            {
            }
            return tmp;
        }

        /// <summary>
        /// Grabar las fechas en la tabla de Licitaciones
        /// </summary>
        /// <param name="detalleFecha">detalle de fechas</param>
        /// <returns>numero entero</returns>
        /// <response code="200">Ok devuelve objeto</response>
        [Route("api/Licitacion/GrabarFechas")]
        [System.Web.Http.ActionName("GrabarFechas")]
        [System.Web.Http.HttpPost]
        public async Task<int> GrabarFechas(fechaViewModels detalleFecha)
        {
            var tmp = 0;
            try
            {
                ServicioLicitacion servicio = new ServicioLicitacion();
                tmp = await servicio.grabarFechas(detalleFecha);
            }
            catch (Exception ex)
            {
            }
            return tmp;
        }

        /// <summary>
        /// Lista las fechas de la Licitacion
        /// </summary>
        /// <param name="idObra">identificador d eobra</param>
        /// <returns>lista de Fechas</returns>
        /// <response code="200">Ok devuelve objeto</response>
        [Route("api/Licitacion/ListarFechas")]
        [System.Web.Http.ActionName("ListarFechas")]
        [System.Web.Http.HttpGet]
        public List<fechaViewModels> ListarFechas(int? idObra)
        {
            var tmp = new List<fechaViewModels>();
            try
            {
                ServicioLicitacion servicio = new ServicioLicitacion();
                tmp = servicio.buscarListaFechas(idObra);
            }
            catch (Exception ex)
            {
            }
            return tmp;
        }

        [Route("api/Licitacion/ImprimirFicha")]
        [System.Web.Http.ActionName("ImprimirFicha")]
        [System.Web.Http.HttpGet]
        public IHttpActionResult ImprimirFicha(int? idObra)
        {
            ServicioPDF servicio = new ServicioPDF();
            string filePath = ConfigurationSettings.AppSettings["repositorioFiles"].ToString();
            //var filePath = "D:\\Archivos\\";
            //var filePath = "C:\\inetpub\\Licitaciones\\Tmp\\";
            filePath = filePath + "FichaObra_" + idObra + ".pdf";
            servicio.generarFichaObra(filePath, idObra);
            //var filePath = System.Web.HttpContext.Current.Server.MapPath("~/Files/Ingenieria/" + unFile.fileRuta + "/" + unFile.fileNombre);
            // sendo file to client

            IHttpActionResult response;
            HttpResponseMessage responseMsg = new HttpResponseMessage(HttpStatusCode.OK);
            var fileStream = new FileStream(filePath, FileMode.Open);
            responseMsg.Content = new StreamContent(fileStream);
            responseMsg.Content.Headers.ContentType = new MediaTypeHeaderValue("application/octet-stream");
            responseMsg.Content.Headers.ContentDisposition = new ContentDispositionHeaderValue("attachment");
            responseMsg.Content.Headers.ContentDisposition.FileName = "FichaObra_" + idObra + ".pdf";
            response = ResponseMessage(responseMsg);
            return response;
        }
        private HttpResponseMessage DownloadFile(string downloadFilePath, string fileName)
        {
            try
            {               
                if (!System.IO.File.Exists(downloadFilePath))
                {
                    throw new HttpResponseException(HttpStatusCode.NotFound);
                }

                MemoryStream responseStream = new MemoryStream();
                Stream fileStream = System.IO.File.Open(downloadFilePath, FileMode.Open);

                fileStream.CopyTo(responseStream);
                fileStream.Close();
                responseStream.Position = 0;

                HttpResponseMessage response = new HttpResponseMessage();
                response.StatusCode = HttpStatusCode.OK;
              
                response.Content = new StreamContent(responseStream);
                string contentDisposition = string.Concat("attachment; filename=", fileName);
                response.Content.Headers.ContentDisposition =
                              ContentDispositionHeaderValue.Parse(contentDisposition);
                return response;
            }
            catch
            {
                throw new HttpResponseException(HttpStatusCode.InternalServerError);
            }
        }

        [Route("api/Licitacion/listarLicitaciones")]
        [System.Web.Http.ActionName("listarLicitaciones")]
        [System.Web.Http.HttpGet]
        public HttpResponseMessage listarLicitaciones(
            string nroExpediente, string nombreObra, DateTime? fechaPub, int? idOrganismo, int? idEstado, 
            bool? nombre, bool? expediente, bool? publicacion, bool? organismo, bool? monto, bool? etapa)
        {

            int recordsTotal = 0;
            ServicioLicitacion servicio = new ServicioLicitacion();
            string qObra = ConfigurationSettings.AppSettings["qObraOficina"].ToString();
            var lst = servicio.listarLicitaciones(ref recordsTotal, "", "", null,
                nroExpediente, nombreObra,qObra, idEstado, fechaPub, idOrganismo, 500, 500);
            
            var excel = servicio.generarExcelGrilla(lst, nombre, expediente, publicacion, organismo, monto, etapa);


            return DownloadFile(excel.filePath, excel.fileName);
          

        }

        [Route("api/Licitacion/buscarTipoContratacion")]
        [System.Web.Http.ActionName("buscarTipoContratacion")]
        [System.Web.Http.HttpGet]
        public string buscarTipoContratacion(int? idTipoContratacion)
        {
            ServicioLicitacion servicio = new ServicioLicitacion();
            var value = servicio.buscarTipoContratacion(idTipoContratacion);           
            return value;
        }

        /// <summary>
        /// Cambio de Etapa
        /// </summary>
        /// <param name="unLicitacion">detalle de obra</param>
        /// <returns>numero entero</returns>
        /// <response code="200">Ok devuelve objeto</response>
        [Route("api/Licitacion/CambioEtapa")]
        [System.Web.Http.ActionName("CambioEtapa")]
        [System.Web.Http.HttpPost]
        public async Task<int> CambioEtapa(cambioEtapaViewModels unLicitacion)
        {
            var tmp = 0;
            try
            {
                ServicioLicitacion servicio = new ServicioLicitacion();
                tmp = await servicio.cambioEtapa(unLicitacion);
            }
            catch (Exception ex)
            {
            }
            return tmp;
        }

        /// <summary>
        /// Vuelve a la Etapa Anterior
        /// </summary>
        /// <param name="unLicitacion">detalle de obra</param>
        /// <returns>numero entero</returns>
        /// <response code="200">Ok devuelve objeto</response>
        [Route("api/Licitacion/EtapaAnterior")]
        [System.Web.Http.ActionName("EtapaAnterior")]
        [System.Web.Http.HttpPost]
        public async Task<int> EtapaAnterior(cambioEtapaViewModels unLicitacion)
        {
            var tmp = 0;
            try
            {
                ServicioLicitacion servicio = new ServicioLicitacion();
                tmp = await servicio.cambioEtapaAnterior(unLicitacion);
            }
            catch (Exception ex)
            {
            }
            return tmp;
        }

        [Route("api/Licitacion/EnviarMailAdjunto")]
        [System.Web.Http.ActionName("EnviarMailAdjunto")]
        [System.Web.Http.HttpGet]
        public async Task<int> EnviarMailAdjunto(int? idObra, int? tipoMail, int? idCategoria)
        {
            var tmp = 0;
            try
            {
                ServicioLicitacion servicio = new ServicioLicitacion();
                await servicio.enviarNotificacionAdjunto(idObra, tipoMail, idCategoria);
            }
            catch (Exception ex)
            {
            }
            return tmp;
        }
        [Route("api/Licitacion/GrabarVencimientoObs")]
        [System.Web.Http.ActionName("GrabarVencimientoObs")]
        [System.Web.Http.HttpPost]
        public async Task<int> GrabarVencimientoObs(unLicitacionViewModels unLicitacion)
        {
            var tmp = 0;
            try
            {
                ServicioLicitacion servicio = new ServicioLicitacion();
                tmp = await servicio.grabarVencimientoObs(unLicitacion);
            }
            catch (Exception ex)
            {
            }
            return tmp;
        }
    }
}