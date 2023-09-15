using Licitacion.Servicios;
using Licitaciones.ViewModels;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.Entity.Validation;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web.Http;
using System.Security.Cryptography;
using System.Text;


namespace Licitaciones.Areas.Licitacion.Controllers.Api
{
    public class ArchivoController : ApiController
    {

        /// <summary>
        /// Upload archivo de requisitos por parte de la empresa
        /// </summary>
        /// <param name="Upload">Archivo</param>
        /// <param name="idObra">Identificacion Obra</param>
        /// <param name="idEmpresa">Tipo de Archivo</param>
        /// <param name="idRequisito">Tipo de Archivo</param>
        /// <param name="idSobre">id sobre seleccionado</param>
        /// <returns>string con mensaje de exito o error</returns>
        /// <response code="200">Ok devuelve lista</response>
        [Route("api/Archivo/UploadFile")]
        [System.Web.Http.ActionName("UploadFile")]
        [System.Web.Http.HttpPost]
        public string UploadFile()
        {
            try
            {
                if (System.Web.HttpContext.Current.Request.Files.AllKeys.Any())
                {
                    var httpPostedFile = System.Web.HttpContext.Current.Request.Files["Upload"];
                    int idObra = Convert.ToInt32(System.Web.HttpContext.Current.Request["idObra"].ToString());
                    int idEmpresa = Convert.ToInt32(System.Web.HttpContext.Current.Session["idEmpresa"].ToString());
                    int idRequisito = Convert.ToInt32(System.Web.HttpContext.Current.Request["idRequisito"].ToString());
                    int idSobre = Convert.ToInt32(System.Web.HttpContext.Current.Request["idSobre"].ToString());
                    if (httpPostedFile != null)
                    {
                        string ruta = ConfigurationSettings.AppSettings["repositorioFiles"].ToString();
                        string nombreArchivo = DateTime.Now.TimeOfDay.Minutes + DateTime.Now.TimeOfDay.Milliseconds + "_" + httpPostedFile.FileName;
                        string folderName = idObra.ToString();
                        string pathString = System.IO.Path.Combine(ruta, folderName);
                        if (!System.IO.Directory.Exists(pathString))
                        {
                            System.IO.Directory.CreateDirectory(pathString);
                        }
                        ruta = pathString;
                        var filePath = ruta + "\\" + nombreArchivo;
                        ServicioArchivo _servicio = new ServicioArchivo();

                        httpPostedFile.SaveAs(filePath);
                        _servicio.nuevo
                            (ruta, httpPostedFile.FileName, nombreArchivo, httpPostedFile.ContentType, idObra, idEmpresa, idRequisito, idSobre);
                        return "Ok";
                    }
                }
                return "";
            }
            catch (DbEntityValidationException e)
            {
                foreach (var eve in e.EntityValidationErrors)
                {
                    Console.WriteLine("Entity of type \"{0}\" in state \"{1}\" has the following validation errors:",
                        eve.Entry.Entity.GetType().Name, eve.Entry.State);
                    foreach (var ve in eve.ValidationErrors)
                    {
                        Console.WriteLine("- Property: \"{0}\", Error: \"{1}\"",
                            ve.PropertyName, ve.ErrorMessage);
                    }
                }
                throw;
            }
        }

        //[Route("api/Archivo/UploadFile")]
        //[System.Web.Http.ActionName("UploadFile")]
        //[System.Web.Http.HttpPost]
        //public IHttpActionResult UploadFile()
        //{
        //    try
        //    {
        //        if (System.Web.HttpContext.Current.Request.Files.AllKeys.Any())
        //        {
        //            var httpPostedFile = System.Web.HttpContext.Current.Request.Files["Upload"];
        //            int idObra = Convert.ToInt32(System.Web.HttpContext.Current.Request["idObra"].ToString());
        //            int idEmpresa = Convert.ToInt32(System.Web.HttpContext.Current.Session["idEmpresa"].ToString());
        //            int idRequisito = Convert.ToInt32(System.Web.HttpContext.Current.Request["idRequisito"].ToString());
        //            int idSobre = Convert.ToInt32(System.Web.HttpContext.Current.Request["idSobre"].ToString());
        //            if (httpPostedFile != null)
        //            {
        //                string ruta = ConfigurationManager.AppSettings["repositorioFiles"];
        //                string nombreArchivo = DateTime.Now.TimeOfDay.Minutes + DateTime.Now.TimeOfDay.Milliseconds + "_" + httpPostedFile.FileName;
        //                string folderName = idObra.ToString();
        //                string pathString = Path.Combine(ruta, folderName);

        //                if (!Directory.Exists(pathString))
        //                {
        //                    Directory.CreateDirectory(pathString);
        //                }

        //                ruta = pathString;
        //                var filePath = Path.Combine(ruta, nombreArchivo);
        //                ServicioArchivo _servicio = new ServicioArchivo();

        //                httpPostedFile.SaveAs(filePath);

        //                // Encripta
        //                string claveEncriptacion = "12345678912345678912345678912345"; //me pide de 32 caracteres verlo
        //                byte[] claveBytes = Encoding.UTF8.GetBytes(claveEncriptacion);

        //                using (Aes aesAlg = Aes.Create())
        //                {
        //                    aesAlg.Key = claveBytes;

        //                    ICryptoTransform encryptor = aesAlg.CreateEncryptor(aesAlg.Key, aesAlg.IV);

        //                    using (FileStream fsInput = new FileStream(filePath, FileMode.Open))
        //                    using (FileStream fsOutput = new FileStream(filePath + ".encrypted", FileMode.Create))
        //                    using (CryptoStream csEncrypt = new CryptoStream(fsOutput, encryptor, CryptoStreamMode.Write))
        //                    {
        //                        fsOutput.Write(aesAlg.IV, 0, aesAlg.IV.Length);

        //                        fsInput.CopyTo(csEncrypt);
        //                    }
        //                }

        //                _servicio.nuevo
        //                    (ruta, httpPostedFile.FileName, nombreArchivo, httpPostedFile.ContentType, idObra, idEmpresa, idRequisito, idSobre);

        //                return Ok("Archivo subido y encriptado exitosamente.");
        //            }
        //        }
        //        return BadRequest("No se ha subido ningún archivo.");
        //    }
        //    catch (Exception ex)
        //    {
        //        // Manejo de excepciones y registro de errores
        //        return InternalServerError(ex);
        //    }
        //}
        ////adaptar 
        //private void DesencriptarArchivo(string archivoEncriptado, string claveEncriptacion, string rutaDesencriptado)
        //{
        //    using (Aes aesAlg = Aes.Create())
        //    {
        //        byte[] claveBytes = Encoding.UTF8.GetBytes(claveEncriptacion);
        //        aesAlg.Key = claveBytes;

        //        byte[] iv = new byte[aesAlg.IV.Length];
        //        using (FileStream fsInput = new FileStream(archivoEncriptado, FileMode.Open))
        //        {
        //            fsInput.Read(iv, 0, iv.Length);

        //            ICryptoTransform decryptor = aesAlg.CreateDecryptor(aesAlg.Key, iv);

        //            using (FileStream fsOutput = new FileStream(rutaDesencriptado, FileMode.Create))
        //            using (CryptoStream csDecrypt = new CryptoStream(fsOutput, decryptor, CryptoStreamMode.Write))
        //            {
        //                byte[] buffer = new byte[1024];
        //                int bytesRead;
        //                while ((bytesRead = fsInput.Read(buffer, 0, buffer.Length)) > 0)
        //                {
        //                    csDecrypt.Write(buffer, 0, bytesRead);
        //                }
        //            }
        //        }
        //    }
        //}




        /// <summary>
        /// Elimina archivo de requisitos por parte de la empresa
        /// </summary>
        /// <param name="idArchivo">Identificacion Obra</param>
        /// <returns>string con mensaje de exito o error</returns>
        /// <response code="200">Ok devuelve lista</response>
        [Route("api/Archivo/EliminarArchivo")]
        [System.Web.Http.ActionName("EliminarArchivo")]
        [System.Web.Http.HttpGet]
        public string EliminarArchivo(int? id)
        {
            ServicioArchivo servicio = new ServicioArchivo();
            servicio.eliminarArchivo(id);
            return "Ok";
        }

        [Route("api/Archivo/DescargarArchivo")]
        [System.Web.Http.ActionName("DescargarArchivo")]
        [HttpGet]
        public IHttpActionResult DescargarArchivo(int id)
        {
            HttpResponseMessage result = null;
            ServicioArchivo servicio = new ServicioArchivo();
            var unArchivo = servicio.buscarArchivo(id);
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
                responseMsg.Content.Headers.ContentDisposition.FileName = unArchivo.nombre;
                response = ResponseMessage(responseMsg);
                return response;
            }
        }

        /// <summary>
        /// Upload archivo de requisitos por parte de la empresa
        /// </summary>
        /// <param name="Upload">Archivo</param>
        /// <param name="idObra">Identificacion Obra</param>
        /// <param name="idCategoria">Tipo de Archivo</param>
        /// <returns>string con mensaje de exito o error</returns>
        /// <response code="200">Ok devuelve lista</response>
        [Route("api/Archivo/UploadFileObra")]
        [System.Web.Http.ActionName("UploadFileObra")]
        [System.Web.Http.HttpPost]
        public string UploadFileObra()
        {
            try
            {
                if (System.Web.HttpContext.Current.Request.Files.AllKeys.Any())
                {
                    var httpPostedFile = System.Web.HttpContext.Current.Request.Files["Upload"];
                    int idObra = Convert.ToInt32(System.Web.HttpContext.Current.Request["idObra"].ToString());
                    int idCategoria = Convert.ToInt32(System.Web.HttpContext.Current.Request["idCategoria"].ToString());

                    if (httpPostedFile != null)
                    {
                        string ruta = ConfigurationSettings.AppSettings["repositorioFiles"].ToString();
                        string nombreArchivo = DateTime.Now.TimeOfDay.Minutes + DateTime.Now.TimeOfDay.Milliseconds + "_" + httpPostedFile.FileName;
                        string folderName = idObra.ToString();
                        string pathString = System.IO.Path.Combine(ruta, folderName);
                        if (!System.IO.Directory.Exists(pathString))
                        {
                            System.IO.Directory.CreateDirectory(pathString);
                        }
                        ruta = pathString;
                        var filePath = ruta + "\\" + nombreArchivo;
                        ServicioArchivo _servicio = new ServicioArchivo();

                        httpPostedFile.SaveAs(filePath);
                        _servicio.nuevoObra(filePath, nombreArchivo, idObra, idCategoria);
                        return "Ok";
                    }
                }
                return "";
            }
            catch (DbEntityValidationException e)
            {
                foreach (var eve in e.EntityValidationErrors)
                {
                    Console.WriteLine("Entity of type \"{0}\" in state \"{1}\" has the following validation errors:",
                        eve.Entry.Entity.GetType().Name, eve.Entry.State);
                    foreach (var ve in eve.ValidationErrors)
                    {
                        Console.WriteLine("- Property: \"{0}\", Error: \"{1}\"",
                            ve.PropertyName, ve.ErrorMessage);
                    }
                }
                throw;
            }
        }

        [Route("api/Archivo/listarArchivoObra")]
        [System.Web.Http.ActionName("listarArchivoObra")]
        [HttpGet]
        public List<ArchivoObraViewModels> listarArchivoObra(int idObra)
        {
            ServicioArchivo servicio = new ServicioArchivo();
            var lista = servicio.listarArchivoObra(idObra);
            return lista;
        }

        [Route("api/Archivo/DescargarOferta")]
        [System.Web.Http.ActionName("DescargarOferta")]
        [HttpGet]
        public IHttpActionResult DescargarOferta(int? idObra, int? idEmpresa)
        {
            HttpResponseMessage result = null;
            ServicioEmpresa servicio = new ServicioEmpresa();
            var unArchivo = servicio.buscarOferta(idEmpresa, idObra);
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
                responseMsg.Content.Headers.ContentDisposition.FileName = "Comprobante_Oferta.pdf";
                response = ResponseMessage(responseMsg);
                return response;
            }
        }

        [Route("api/Archivo/DescargarArchivosEmpresa")]
        [System.Web.Http.ActionName("DescargarArchivosEmpresa")]
        [System.Web.Http.HttpGet]
        public IHttpActionResult DescargarArchivosEmpresa(int? idObra, int? idEmpresa)
        {
            HttpResponseMessage result = null;
            IHttpActionResult response;
            HttpResponseMessage responseMsg = new HttpResponseMessage(HttpStatusCode.OK);
            response = ResponseMessage(responseMsg);

            string filePath = ConfigurationSettings.AppSettings["repositorioFiles"].ToString();
            string nombreZip = "DescargaDocumentacion_" + DateTime.Now.Day + DateTime.Now.Month + DateTime.Now.Year + "_" + DateTime.Now.Hour + DateTime.Now.Minute + "_" + DateTime.Now.Millisecond + ".zip";
            string filePath2 = ConfigurationSettings.AppSettings["repositorioFiles"].ToString();
            ServicioArchivo servicioArchivo = new ServicioArchivo();
            var lista = servicioArchivo.listarArchivoObraEmpresa(idObra,idEmpresa);
            using (ZipArchive archive = ZipFile.Open(filePath + nombreZip, ZipArchiveMode.Create))
            {
                foreach (var item in lista)
                {
                    try
                    {
                        archive.CreateEntryFromFile(item.ruta, item.nombreArchivo);
                    }

                    catch (Exception ex)
                    {
                        return BadRequest();
                    }
                }
            }
            var fileStream = new FileStream(filePath + nombreZip, FileMode.Open, FileAccess.Read);
            responseMsg.Content = new StreamContent(fileStream);
            responseMsg.Content.Headers.ContentType = new MediaTypeHeaderValue("application/zip");
            responseMsg.Content.Headers.ContentDisposition = new ContentDispositionHeaderValue("attachment");
            responseMsg.Content.Headers.ContentDisposition.FileName = nombreZip;
            response = ResponseMessage(responseMsg);
            return response;
        }
        [Route("api/Archivo/UploadFileObs")]
        [System.Web.Http.ActionName("UploadFileObs")]
        [System.Web.Http.HttpPost]
        public string UploadFileObservaciones()
        {
            try
            {
                if (System.Web.HttpContext.Current.Request.Files.AllKeys.Any())
                {
                    var httpPostedFile = System.Web.HttpContext.Current.Request.Files["Upload"];
                    int idObra = Convert.ToInt32(System.Web.HttpContext.Current.Request["idObra"].ToString());
                    int idEmpresa = Convert.ToInt32(System.Web.HttpContext.Current.Session["idEmpresa"].ToString());
                    int idRequisito = Convert.ToInt32(System.Web.HttpContext.Current.Request["idRequisito"].ToString());
                    int idSobre = Convert.ToInt32(System.Web.HttpContext.Current.Request["idSobre"].ToString());
                    if (httpPostedFile != null)
                    {
                        string ruta = ConfigurationSettings.AppSettings["repositorioFiles"].ToString() + "observaciones" + "\\";
                        string nombreArchivo = DateTime.Now.TimeOfDay.Minutes + DateTime.Now.TimeOfDay.Milliseconds + "_" + httpPostedFile.FileName;
                        string folderName = idObra.ToString();
                        string pathString = System.IO.Path.Combine(ruta, folderName);
                        if (!System.IO.Directory.Exists(pathString))
                        {
                            System.IO.Directory.CreateDirectory(pathString);
                        }
                        ruta = pathString;
                        var filePath = ruta + "\\" + nombreArchivo;
                        ServicioArchivo _servicio = new ServicioArchivo();

                        httpPostedFile.SaveAs(filePath);
                        _servicio.nuevoObs
                            (ruta, httpPostedFile.FileName, nombreArchivo, httpPostedFile.ContentType, idObra, idEmpresa, idRequisito, idSobre);
                        return "Ok";
                    }
                }
                return "";
            }
            catch (DbEntityValidationException e)
            {
                foreach (var eve in e.EntityValidationErrors)
                {
                    Console.WriteLine("Entity of type \"{0}\" in state \"{1}\" has the following validation errors:",
                        eve.Entry.Entity.GetType().Name, eve.Entry.State);
                    foreach (var ve in eve.ValidationErrors)
                    {
                        Console.WriteLine("- Property: \"{0}\", Error: \"{1}\"",
                            ve.PropertyName, ve.ErrorMessage);
                    }
                }
                throw;
            }
        }
        [Route("api/Archivo/DescargarArchivosEmpresaObs")]
        [System.Web.Http.ActionName("DescargarArchivosEmpresaObs")]
        [System.Web.Http.HttpGet]
        public IHttpActionResult DescargarArchivosEmpresaObs(int? idObra, int? idEmpresa)
        {
            HttpResponseMessage result = null;
            IHttpActionResult response;
            HttpResponseMessage responseMsg = new HttpResponseMessage(HttpStatusCode.OK);
            response = ResponseMessage(responseMsg);

            string filePath = ConfigurationSettings.AppSettings["repositorioFiles"].ToString();
            string nombreZip = "DescargaDocumentacion_" + DateTime.Now.Day + DateTime.Now.Month + DateTime.Now.Year + "_" + DateTime.Now.Hour + DateTime.Now.Minute + "_" + DateTime.Now.Millisecond + ".zip";
            string filePath2 = ConfigurationSettings.AppSettings["repositorioFiles"].ToString();
            ServicioArchivo servicioArchivo = new ServicioArchivo();
            var lista = servicioArchivo.listarArchivoObraEmpresaObs(idObra, idEmpresa);
            using (ZipArchive archive = ZipFile.Open(filePath + nombreZip, ZipArchiveMode.Create))
            {
                foreach (var item in lista)
                {
                    try
                    {
                        archive.CreateEntryFromFile(item.ruta, item.nombreArchivo);
                    }

                    catch (Exception ex)
                    {
                        return BadRequest();
                    }
                }
            }
            var fileStream = new FileStream(filePath + nombreZip, FileMode.Open, FileAccess.Read);
            responseMsg.Content = new StreamContent(fileStream);
            responseMsg.Content.Headers.ContentType = new MediaTypeHeaderValue("application/zip");
            responseMsg.Content.Headers.ContentDisposition = new ContentDispositionHeaderValue("attachment");
            responseMsg.Content.Headers.ContentDisposition.FileName = nombreZip;
            response = ResponseMessage(responseMsg);
            return response;
        }
    }
}