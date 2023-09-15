using Licitacion.Servicios;
using Licitaciones.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Threading.Tasks;
using System.Web.Http;
using System.IO;
using System.Net.Http.Headers;
using System.Configuration;

namespace Licitaciones.Areas.Licitacion.Controllers.Api 
{
    public class EmpresaController : ApiController
    {
        /// <summary>
        /// Completa la grilla de Licitaciones desde la vista de la Empresa 
        /// </summary>
        /// <param name="form"></param>
        /// <param name="nroExpediente"></param>
        /// <param name="nombreObra"></param>
        /// <param name="fechaPub"></param>
        /// <param name="idOrganismo"></param>
        /// <param name="idFavoritos"></param>
        /// <returns>Lista de Obras filtradas en etapa de Publicadas</returns>
        /// <response code="200">Ok devuelve lista</response>
        [Route("api/Empresa/listarObraEmpresa")]
        [System.Web.Http.ActionName("listarObraEmpresa")]
        [System.Web.Http.HttpPost]
        public HttpResponseMessage listarObraEmpresa(FormDataCollection form, 
            string nroExpediente, string nombreObra, DateTime? fechaPub, int? idOrganismo, int? idEmpresa, int? idFavoritos, int? idEtapa)
        {
            var draw = form.Get("draw");
            var start = form.Get("start");
            var length = form.Get("length");
            var sortColumn = (form.Get("columns[" + form.Get("order[0][column]").FirstOrDefault() + "][data]").ToString()).ToString();
            var sortColumnDir = form.Get("order[0][dir]").ToString();
            var searchValue = form.Get("search[value]").ToString();

            int pageSize = length != null ? Convert.ToInt32(length) : 0;
            int skip = start != null ? Convert.ToInt32(start) : 0;
            int recordsTotal = 0;
            ServicioEmpresa servicio = new ServicioEmpresa();
            string qObra = ConfigurationSettings.AppSettings["qObra"].ToString();

            var lst = servicio.listaLicitaciones(ref recordsTotal, sortColumn, sortColumnDir, null,
                nroExpediente, nombreObra, qObra, idEmpresa, idFavoritos, idEtapa, fechaPub, idOrganismo, pageSize, skip);

            var json = Json(new { draw = draw, recordsFiltered = recordsTotal, recordsTotal = recordsTotal, data = lst });

            return Request.CreateResponse(HttpStatusCode.OK, new { draw = draw, recordsFiltered = recordsTotal, recordsTotal = recordsTotal, data = lst }, Configuration.Formatters.JsonFormatter);

        }

        /// <summary>
        /// Busca los datos generales de una obra 
        /// </summary>
        /// <param name="idObra">identificacion de obra</param>
        /// <returns>Objeto que contiene la informacion</returns>
        /// <response code="200">Ok devuelve objeto</response>
        [Route("api/Empresa/buscarUnaEmpresa")]
        [System.Web.Http.ActionName("buscarUnaEmpresa")]
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
        [Route("api/Empresa/GrabarDatosEmpresa")]
        [System.Web.Http.ActionName("GrabarDatosEmpresa")]
        [System.Web.Http.HttpPost]
        public async Task<int> GrabarDatosEmpresa(unLicitacionViewModels unLicitacion)
        {
            var tmp = 0;
            try
            {
                ServicioLicitacion servicio = new ServicioLicitacion();
                tmp = await servicio.grabarDetalleLicitacion(unLicitacion);
                tmp = await servicio.grabarDetalleProyecto(unLicitacion);
                tmp = await servicio .grabarLicitacion(unLicitacion);
            }
            catch (Exception ex)
            {
            }
            return tmp;
        }

        /// <summary>
        /// Marca un Obra como favorita para una Empresa
        /// </summary>
        /// <param name="unLicitacion">detalle de obra favorita</param>
        /// <returns>numero entero</returns>
        /// <response code="200">Ok devuelve objeto</response>
        [Route("api/Empresa/marcarFavorita")]
        [System.Web.Http.ActionName("marcarFavorita")]
        [System.Web.Http.HttpPost]
        public async Task<int> marcarFavorita(obraFavoritaViewModels unLicitacion)
        {
            var tmp = 0;
            try
            {
                ServicioEmpresa servicio = new ServicioEmpresa();
                tmp = await servicio.marcarFavoritas(unLicitacion);
            }
            catch (Exception ex)
            {
            }
            return tmp;
        }

        /// <summary>
        /// Envia la Oferta de la empresa
        /// </summary>
        /// <param name="oferta">detalle de oferta</param>
        /// <returns>numero entero</returns>
        /// <response code="200">Ok devuelve objeto</response>
        [Route("api/Empresa/enviarOferta")]
        [System.Web.Http.ActionName("enviarOferta")]
        [System.Web.Http.HttpPost]
        public async Task<int> enviarOferta(empresaOfertaViewModels oferta)
        {
            var tmp = 0;
            ServicioEmpresa servicio = new ServicioEmpresa();
            tmp = await servicio.enviarOferta(oferta);
            return tmp;
        }

        /// <summary>
        /// Busca el nombre de la Empresa
        /// </summary>
        /// <param name="idEmpresa">identificacion de Empresa</param>
        /// <returns>string con el nombre d ela empresa</returns>
        /// <response code="200">Ok devuelve objeto</response>
        [Route("api/Empresa/buscarNombreEmpresa")]
        [System.Web.Http.ActionName("buscarNombreEmpresa")]
        [System.Web.Http.HttpGet]
        public Respuesta buscarNombreEmpresa(int? idEmpresa)
        {
            Respuesta respuesta = new Respuesta();
            try
            {
                ServicioEmpresa servicio = new ServicioEmpresa();
                var tmp = servicio.buscarNombreEmpresa(idEmpresa);
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
       
        [Route("api/Empresa/listarObraEmpresaExpotar")]
        [System.Web.Http.ActionName("listarObraEmpresaExpotar")]
        [System.Web.Http.HttpGet]
        public HttpResponseMessage listarObraEmpresaExpotar(
            string nroExpediente, string nombreObra, DateTime? fechaPub, int? idOrganismo, int? idEmpresa, int? idFavoritos,
            bool? nombre, bool? expediente, bool? publicacion, bool? organismo, bool? monto, bool? etapa, bool? apertura, bool? oferta)
        {

            int recordsTotal = 0;
            ServicioEmpresa servicio = new ServicioEmpresa();
            string qObra = ConfigurationSettings.AppSettings["qObra"].ToString();
            var lst = servicio.listarObraEmpresaExpotar(ref recordsTotal, "", "", null,
              nroExpediente, nombreObra,qObra, idEmpresa, idFavoritos, fechaPub, idOrganismo, 500, 500);

            var excel = servicio.generarExcelGrilla(lst, nombre, expediente, publicacion, organismo, monto, etapa, apertura, oferta);


            return DownloadFile(excel.filePath, excel.fileName);


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
        
        /// <summary>
        /// Busca las Empresas que ofertaron
        /// </summary>
        /// <param name="idEmpresa">identificacion de Obra</param>
        /// <returns>string con el nombre d ela empresa</returns>
        /// <response code="200">Ok devuelve objeto</response>
        [Route("api/Empresa/buscarEmpresaPorObra")]
        [System.Web.Http.ActionName("buscarEmpresaPorObra")]
        [System.Web.Http.HttpGet]
        public Respuesta buscarEmpresaPorObra(int? idObra)
        {
            Respuesta respuesta = new Respuesta();
            try
            {
                ServicioEmpresa servicio = new ServicioEmpresa();
                var tmp = servicio.listarEmpresaOferta(idObra);
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
        /// Busca las Empresas que ofertaron
        /// </summary>
        /// <param name="idEmpresa">identificacion de Obra</param>
        /// <returns>string con el nombre d ela empresa</returns>
        /// <response code="200">Ok devuelve objeto</response>
        [Route("api/Empresa/marcarGanadora")]
        [System.Web.Http.ActionName("marcarGanadora")]
        [System.Web.Http.HttpGet]
        public Respuesta marcarGanadora(int? idObra, int? idEmpresa)
        {
            Respuesta respuesta = new Respuesta();
            try
            {
                ServicioEmpresa servicio = new ServicioEmpresa();
                var valor = servicio.marcarGanadora(idObra, idEmpresa);
                respuesta.codigo = 1;
                respuesta.replay = valor;
            }
            catch (Exception ex)
            {
                respuesta.codigo = 0;
                respuesta.mensaje = "Ocurrio un error " + ex.Message;
            }
            return respuesta;
        }

        /// <summary>
        /// Busca los Datos de las Empresas 
        /// </summary>
        /// <param name="idEmpresa">identificacion de Obra</param>
        /// <returns>objeto con los datos de la empresa</returns>
        /// <response code="200">Ok devuelve objeto</response>
        [Route("api/Empresa/buscarDatosEmpresa")]
        [System.Web.Http.ActionName("buscarDatosEmpresa")]
        [System.Web.Http.HttpGet]
        public Respuesta buscarDatosEmpresa(int? idEmpresa)
        {
            Respuesta respuesta = new Respuesta();
            try
            {
                ServicioEmpresa servicio = new ServicioEmpresa();
                var tmp = servicio.buscarDatosEmpresa(idEmpresa);
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
        /// Ratifica el mail de la empresa
        /// </summary>
        /// <param name="datosEmpresa">detalle de empresa</param>
        /// <returns>numero entero</returns>
        /// <response code="200">Ok devuelve objeto</response>
        [Route("api/Empresa/grabarEmail")]
        [System.Web.Http.ActionName("grabarEmail")]
        [System.Web.Http.HttpPost]
        public async Task<string> grabarEmail(DatosEmpresaViewModels datosEmpresa)
        {
            var tmp = "";
            ServicioEmpresa servicio = new ServicioEmpresa();
            tmp = await servicio.grabarEmailEmpresa(datosEmpresa);
            return tmp;
        }
        /// <summary>
        /// Grabar empresa habilitada
        /// </summary>
        /// <param name="unLicitacion"></param>
        /// <returns></returns>
        [Route("api/Empresa/grabarEmpresaHabilitada")]
        [System.Web.Http.ActionName("grabarEmpresaHabilitada")]
        [System.Web.Http.HttpPost]
        public async Task<string> grabarEmpresaHabilitada(EmpresaHabilitadaViewModels datosEmpresa) //cambiar vm
        {
            var tmp = "";
            try
            {
                ServicioEmpresa servicio = new ServicioEmpresa();
                tmp = await servicio.grabarEmpresaHabilitada(datosEmpresa);
            }
            catch (Exception ex)
            {
            }
            return tmp;
        }
        [Route("api/Empresa/habilitarSubaMejora")]
        [System.Web.Http.ActionName("habilitarSubaMejora")]
        [System.Web.Http.HttpPost]
        public async Task<string> habilitarSubaMejora(EmpresaHabilitadaViewModels datosEmpresa) //cambiar vm
        {
            var tmp = "";
            try
            {
                ServicioEmpresa servicio = new ServicioEmpresa();
                tmp = await servicio.habilitarSubaMejora(datosEmpresa);
            }
            catch (Exception ex)
            {
            }
            return tmp;
        }
        [Route("api/Empresa/buscarEmpresaHabilitada")]
        [System.Web.Http.ActionName("buscarEmpresaHabilitada")]
        [System.Web.Http.HttpGet]
        public Respuesta buscarEmpresaHabilitada(int? idObra, int? idEmpresa)
        {
            Respuesta respuesta = new Respuesta();
            EmpresaHabilitadaViewModels tmp = new EmpresaHabilitadaViewModels();
            try
            {
                ServicioLicitacion servicio = new ServicioLicitacion();
                tmp = servicio.buscarEmpresaHabilitada(idObra, idEmpresa);
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