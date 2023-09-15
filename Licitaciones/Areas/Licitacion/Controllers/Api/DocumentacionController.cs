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
    public class DocumentacionController : ApiController
    {
        /// <summary>
        /// Nueva Documentación (Padre)
        /// </summary>
        /// <returns>Entero que indica el exito de la operacion</returns>
        /// <response code="200">Ok</response>
        [Route("api/Documentacion/nuevaReferencia")]
        [System.Web.Http.ActionName("nuevaReferencia")]
        [System.Web.Http.HttpPost]
        public int nuevaReferencia(ReferenciaViewModels unReferencia)
        {
            ServicioDocumentacion servicio = new ServicioDocumentacion();
            var value = servicio.nuevaReferencia(unReferencia);
            return value;
        }

        /// <summary>
        /// Lista las Refencias disponibles (Requisito Padre)
        /// </summary>
        /// <returns>Lista total de Refencias Padres</returns>
        /// <response code="200">Ok devuelve lista</response>
        [Route("api/Documentacion/ListarReferencia")]
        [System.Web.Http.ActionName("ListarReferencia")]
        [System.Web.Http.HttpGet]
        public Respuesta ListarReferencia()
        {
            Respuesta respuesta = new Respuesta();
            ServicioDocumentacion servicio = new ServicioDocumentacion();
            var lista = servicio.listarReferencia();
            respuesta.codigo = 1;
            respuesta.replay = lista;
            return respuesta;
        }

        /// <summary>
        /// Nueva Documentación (Hijo)
        /// </summary>
        /// <returns>Entero que indica el exito de la operacion</returns>
        /// <response code="200">Ok</response>
        [Route("api/Documentacion/nuevoRequisito")]
        [System.Web.Http.ActionName("nuevoRequisito")]
        [System.Web.Http.HttpPost]
        public int nuevoRequisito(RequisitoViewModels unRequisito)
        {
            ServicioDocumentacion servicio = new ServicioDocumentacion();
            var value = servicio.nuevoRequisito(unRequisito);
            return value;
        }

        /// <summary>
        /// Nueva Documentación en una Obra determinada
        /// </summary>
        /// <returns>Entero que indica el exito de la operacion</returns>
        /// <response code="200">Ok</response>
        [Route("api/Documentacion/nuevoRequisitoObra")]
        [System.Web.Http.ActionName("nuevoRequisitoObra")]
        [System.Web.Http.HttpPost]
        public int nuevoRequisitoObra(RequisitoObraViewModels unRequisito)
        {
            ServicioDocumentacion servicio = new ServicioDocumentacion();
            var value = servicio.nuevoRequisitoObra(unRequisito);
            return value;
        }

        /// <summary>
        /// Eliminar una Documentación en una Obra determinada
        /// </summary>
        /// <returns>Entero que indica el exito de la operacion</returns>
        /// <response code="200">Ok</response>
        [Route("api/Documentacion/eliminarRequisitoObra")]
        [System.Web.Http.ActionName("eliminarRequisitoObra")]
        [System.Web.Http.HttpPost]
        public int eliminarRequisitoObra(RequisitoObraViewModels unRequisito)
        {
            ServicioDocumentacion servicio = new ServicioDocumentacion();
            var value = servicio.eliminarRequisitoObra(unRequisito.idRequisito);
            return value;
        }

        /// <summary>
        /// Lista las Requisitos disponibles por Padre
        /// </summary>
        /// <param name="idPadre">Identificacion Padre</param>
        /// <returns>Lista total de Requisitos</returns>
        /// <response code="200">Ok devuelve lista</response>
        [Route("api/Documentacion/listarRequisitoPorPadre")]
        [System.Web.Http.ActionName("listarRequisitoPorPadre")]
        [System.Web.Http.HttpGet]
        public Respuesta listarRequisitoPorPadre(int? idPadre)
        {
            Respuesta respuesta = new Respuesta();
            ServicioDocumentacion servicio = new ServicioDocumentacion();
            var lista = servicio.listarRequisitoPorPadre(idPadre);
            respuesta.codigo = 1;
            respuesta.replay = lista;
            return respuesta;
        }      

        /// <summary>
        /// Lista las Requisitos disponibles por Obra
        /// </summary>
        /// <param name="idObra">Identificacion Obra</param>
        /// <param name="idPadre">Identificacion Referencia Padre</param>
        /// <param name="nroSobre">Nro de Sobre</param>
        /// <returns>Lista total de Requisitos</returns>
        /// <response code="200">Ok devuelve lista</response>
        [Route("api/Documentacion/listarRequisitoPorObra")]
        [System.Web.Http.ActionName("listarRequisitoPorObra")]
        [System.Web.Http.HttpGet]
        public Respuesta listarRequisitoPorObra(int? idPadre, int? idObra, int? idSobre)
        {
            Respuesta respuesta = new Respuesta();
            ServicioDocumentacion servicio = new ServicioDocumentacion();
            var lista = servicio.listarRequisitoPorObra(idPadre, idObra, idSobre);
            respuesta.codigo = 1;
            respuesta.replay = lista;
            return respuesta;
        }

        /// <summary>
        /// Lista las Requisitos disponibles por Obra
        /// </summary>
        /// <param name="idObra">Identificacion Obra</param>
        /// <param name="nroSobre">Nro de Sobre</param>
        /// <returns>Lista total de Requisitos</returns>
        /// <response code="200">Ok devuelve lista</response>
        [Route("api/Documentacion/listarRequisitoPorObra")]
        [System.Web.Http.ActionName("listarRequisitoPorObra")]
        [System.Web.Http.HttpGet]
        public Respuesta listarRequisitoPorObra(int? idObra, int? idSobre)
        {
            Respuesta respuesta = new Respuesta();
            ServicioDocumentacion servicio = new ServicioDocumentacion();
            var lista = servicio.listarRequisitoPorObra(idObra, idSobre);
            respuesta.codigo = 1;
            respuesta.replay = lista;
            return respuesta;
        }

        /// <summary>
        /// Lista las Requisitos disponibles por Obra y Empresa
        /// </summary>
        /// <param name="idObra">Identificacion Obra</param>
        /// <param name="nroSobre">Nro de Sobre</param>
        /// <param name="idEmpresa">Identificacion de Empresa</param>
        /// <returns>Lista total de Requisitos</returns>
        /// <response code="200">Ok devuelve lista</response>
        [Route("api/Documentacion/listarRequisitoPorObraEmpresa")]
        [System.Web.Http.ActionName("listarRequisitoPorObraEmpresa")]
        [System.Web.Http.HttpGet]
        public Respuesta listarRequisitoPorObraEmpresa(int? idObra, int? idSobre, int? idEmpresa, int? idEstado)
        {
            Respuesta respuesta = new Respuesta();
            ServicioDocumentacion servicio = new ServicioDocumentacion();
            var lista = servicio.listarRequisitoPorObraEmpresa(idEmpresa, idObra, idSobre, idEstado);
            respuesta.codigo = 1;
            respuesta.replay = lista;
            return respuesta;
        }

        /// <summary>
        /// Lista los archivos subidos por Obra, Empresa y sobre
        /// </summary>
        /// <param name="idObra">Identificacion Obra</param>
        /// <param name="nroSobre">Nro de Sobre</param>
        /// <param name="idEmpresa">Identificacion de Empresa</param>
        /// <returns>Lista total de Requisitos</returns>
        /// <response code="200">Ok devuelve lista</response>
        [Route("api/Documentacion/listarArchivoPorObraEmpresa")]
        [System.Web.Http.ActionName("listarArchivoPorObraEmpresa")]
        [System.Web.Http.HttpGet]
        public Respuesta listarArchivoPorObraEmpresa(int? idObra, int? idSobre, int? idEmpresa)
        {
            Respuesta respuesta = new Respuesta();
            ServicioDocumentacion servicio = new ServicioDocumentacion();
            var lista = servicio.listarArchivoPorObraEmpresa(idEmpresa, idObra, idSobre);
            respuesta.codigo = 1;
            respuesta.replay = lista;
            return respuesta;
        }

        /// <summary>
        /// Cambia los Estados de un Archivo correspondiente a una oferta
        /// </summary>
        /// <returns>Entero que indica el exito de la operacion</returns>
        /// <response code="200">Ok</response>
        [Route("api/Documentacion/cambiarEstadoArchivo")]
        [System.Web.Http.ActionName("cambiarEstadoArchivo")]
        [System.Web.Http.HttpPost]
        public int cambiarEstadoArchivo(estadoArchivoViewModels estadoArchivo)
        {
            ServicioDocumentacion servicio = new ServicioDocumentacion();
            var value = servicio.cambiarEstadoArchivo(estadoArchivo.idArchivo, estadoArchivo.observaciones, estadoArchivo.idEstado);
            return value;
        }
        /// <summary>
        /// Lista sobres y agregar 3 sobres si no tiene
        /// </summary>
        /// <returns>Lista total de sobres</returns>
        /// <response code="200">Ok devuelve lista</response>
        [Route("api/Documentacion/listarSobres")]
        [System.Web.Http.ActionName("listarSobres")]
        [System.Web.Http.HttpGet]
        public Respuesta listarSobres(int? idObra)
        {
            Respuesta respuesta = new Respuesta();
            ServicioDocumentacion servicio = new ServicioDocumentacion();
            var lista = servicio.listarSobres(idObra);
            respuesta.codigo = 1;
            respuesta.replay = lista;
            return respuesta;
        }
        /// <summary>
        /// Trae el próximo sobre
        /// </summary>
        /// <returns>ultimo sobre + 1</returns>
        /// <response code="200">Ok devuelve lista</response>
        [Route("api/Documentacion/ultimoSobre")]
        [System.Web.Http.ActionName("ultimoSobre")]
        [System.Web.Http.HttpGet]
        public int ultimoSobre(int? idObra)
        {
            ServicioDocumentacion servicio = new ServicioDocumentacion();
            var value = servicio.ultimoSobre(idObra);
            return value ?? 0;
        }        
        /// <summary>
        /// Agrega un sobre 
        /// </summary>
        /// <returns>Entero que indica el exito de la operacion</returns>
        /// <response code="200">Ok</response>
        [Route("api/Documentacion/nuevoSobre")]
        [System.Web.Http.ActionName("nuevoSobre")]
        [System.Web.Http.HttpPost]
        public int nuevoSobre(SobresViewModels unSobre)
        {
            ServicioDocumentacion servicio = new ServicioDocumentacion();
            var value = servicio.nuevoSobre(unSobre);
            return value;
        }
        /// <summary>
        /// Busca un sobre
        /// </summary>
        /// <param name="idObra">identificacion de obra</param>
        /// <returns>Objeto que contiene la informacion</returns>
        /// <response code="200">Ok devuelve objeto</response>
        [Route("api/Documentacion/buscarSobre")]
        [System.Web.Http.ActionName("buscarSobre")]
        [System.Web.Http.HttpGet]
        public Respuesta buscarSobre(int? idObra, int? idSobre)
        {
            Respuesta respuesta = new Respuesta();
            SobresViewModels tmp = new SobresViewModels();
            try
            {
                ServicioDocumentacion servicio = new ServicioDocumentacion();
                tmp = servicio.buscarSobre(idObra, idSobre);
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
        /// Edita el sobre seleccionado
        /// </summary>
        /// <returns>Entero que indica el exito de la operacion</returns>
        /// <response code="200">Ok</response>
        [Route("api/Documentacion/editarSobre")]
        [System.Web.Http.ActionName("editarSobre")]
        [System.Web.Http.HttpPost]
        public int editarSobre(SobresViewModels unSobre)
        {
            ServicioDocumentacion servicio = new ServicioDocumentacion();
            var value = servicio.editarSobre(unSobre);
            return value;
        }
        /// <summary>
        /// elimina el sobre seleccionado
        /// </summary>
        /// <returns>Entero que indica el exito de la operacion</returns>
        /// <response code="200">Ok</response>
        [Route("api/Documentacion/eliminarSobre")]
        [System.Web.Http.ActionName("eliminarSobre")]
        [System.Web.Http.HttpPost]
        public int eliminarSobre(SobresViewModels unSobre)
        {
            ServicioDocumentacion servicio = new ServicioDocumentacion();
            var value = servicio.eliminarSobre(unSobre);
            return value;
        }
        /// <summary>
        /// Lista los requisitos observaciones
        /// </summary>
        /// <param name="idObra">Identificacion Obra</param>
        /// <param name="nroSobre">Nro de Sobre</param>
        /// <param name="idEmpresa">Identificacion de Empresa</param>
        /// <returns>Lista total de Requisitos</returns>
        /// <response code="200">Ok devuelve lista</response>
        [Route("api/Documentacion/listarRequisitoPorObraEmpresaObs")]
        [System.Web.Http.ActionName("listarRequisitoPorObraEmpresaObs")]
        [System.Web.Http.HttpGet]
        public Respuesta listarRequisitoPorObraEmpresaObs(int? idObra, int? idSobre, int? idEmpresa, int? idEstado)
        {
            Respuesta respuesta = new Respuesta();
            ServicioDocumentacion servicio = new ServicioDocumentacion();
            var lista = servicio.listarRequisitoPorObraEmpresaObs(idEmpresa, idObra, idSobre, idEstado);
            respuesta.codigo = 1;
            respuesta.replay = lista;
            return respuesta;
        }
    }
}