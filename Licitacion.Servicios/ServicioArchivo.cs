using Licitaciones.BaseDato;
using Licitaciones.ViewModels;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Licitacion.Servicios
{
    public class ServicioArchivo
    {
        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        private static string ruta = System.Web.HttpRuntime.AppDomainAppPath + "\\bin\\log4net.config";
        public void nuevo
            (string ruta, string nombre, string nombreVirtual, string tipo, int? idObra,
            int? idEmpresa, int? idRequisito, int? idSobre)
        {
            log4net.Config.XmlConfigurator.Configure(new FileInfo(ruta));
            ServicioEmpresa servicioEmpresa = new ServicioEmpresa();
            if (servicioEmpresa.puedeSubirOferta(idObra))
            {
                log.Info("Graba Archivo de Empresa");
                try
                {
                    using (db_meieEntities db = new db_meieEntities())
                    {
                        var existe = db.LicArchivoEmpresa
                            .Where(x => x.idEmpresa == idEmpresa && x.idObra == idObra && x.idRequisito == idRequisito)
                            .FirstOrDefault();
                        var sobre = db.LicDocSobres
                            .Where(x => x.idLicDocSobres == idSobre)
                            .FirstOrDefault();
                        if (existe == null)
                        {
                            //// GRABO ARCHIVO
                            LicArchivoEmpresa unArchivo = new LicArchivoEmpresa();
                            unArchivo.nombreArchivo = nombre;
                            unArchivo.idEmpresa = idEmpresa;
                            unArchivo.ruta = ruta;
                            unArchivo.idEstadoArchivo = 1;
                            unArchivo.idObra = idObra;
                            unArchivo.idRequisito = idRequisito;
                            unArchivo.nombreArchivoEnc = nombreVirtual;
                            unArchivo.fecha = DateTime.Now.Date;
                            unArchivo.hora = DateTime.Now.ToString("hh:mm tt");
                            unArchivo.nroSobre = sobre.numero;
                            unArchivo.tipoArchivo = tipo;
                            unArchivo.observaciones = string.Empty;
                            db.LicArchivoEmpresa.Add(unArchivo);
                            db.SaveChanges();
                        }
                        else
                        {
                            existe.nombreArchivo = nombre;
                            existe.idEmpresa = idEmpresa;
                            existe.ruta = ruta;
                            existe.idEstadoArchivo = 1;
                            existe.idObra = idObra;
                            existe.idRequisito = idRequisito;
                            existe.nombreArchivoEnc = nombreVirtual;
                            existe.fecha = DateTime.Now.Date;
                            existe.nroSobre = sobre.numero;
                            existe.tipoArchivo = tipo;
                            existe.hora = DateTime.Now.ToString("hh:mm tt");
                            existe.observaciones = string.Empty;
                            db.SaveChanges();
                        }
                    }
                }
                catch (Exception ex)
                {
                    log.Error("Error Graba Archivo de Empresa " + ex.Message);
                }
            }
            else
            {
                log.Info("No puede subir Archivo de Empresa Fecha de cierre vencida");
            }
        }
        public void nuevoObs
           (string ruta, string nombre, string nombreVirtual, string tipo, int? idObra,
           int? idEmpresa, int? idRequisito, int? idSobre)
        {
            log4net.Config.XmlConfigurator.Configure(new FileInfo(ruta));
            ServicioEmpresa servicioEmpresa = new ServicioEmpresa();
            if (servicioEmpresa.puedeSubirOferta(idObra))
            {
                log.Info("Graba Archivo de Empresa");
                try
                {
                    using (db_meieEntities db = new db_meieEntities())
                    {
                        var existe = db.LicArchivoEmpresa
                            .Where(x => x.idEmpresa == idEmpresa && x.idObra == idObra && x.idRequisito == idRequisito && x.ruta.Contains("observaciones"))
                            .FirstOrDefault();
                        var sobre = db.LicDocSobres
                            .Where(x => x.idLicDocSobres == idSobre)
                            .FirstOrDefault();
                        if (existe == null)
                        {
                            //// GRABO ARCHIVO
                            LicArchivoEmpresa unArchivo = new LicArchivoEmpresa();
                            unArchivo.nombreArchivo = nombre;
                            unArchivo.idEmpresa = idEmpresa;
                            unArchivo.ruta = ruta;
                            unArchivo.idEstadoArchivo = 1;
                            unArchivo.idObra = idObra;
                            unArchivo.idRequisito = idRequisito;
                            unArchivo.nombreArchivoEnc = nombreVirtual;
                            unArchivo.fecha = DateTime.Now.Date;
                            unArchivo.hora = DateTime.Now.ToString("hh:mm tt");
                            unArchivo.nroSobre = sobre.numero;
                            unArchivo.tipoArchivo = tipo;
                            unArchivo.observaciones = string.Empty;
                            db.LicArchivoEmpresa.Add(unArchivo);
                            db.SaveChanges();
                        }
                        else
                        {
                            existe.nombreArchivo = nombre;
                            existe.idEmpresa = idEmpresa;
                            existe.ruta = ruta;
                            existe.idEstadoArchivo = 1;
                            existe.idObra = idObra;
                            existe.idRequisito = idRequisito;
                            existe.nombreArchivoEnc = nombreVirtual;
                            existe.fecha = DateTime.Now.Date;
                            existe.nroSobre = sobre.numero;
                            existe.tipoArchivo = tipo;
                            existe.hora = DateTime.Now.ToString("hh:mm tt");
                            existe.observaciones = string.Empty;
                            db.SaveChanges();
                        }
                    }
                }
                catch (Exception ex)
                {
                    log.Error("Error Graba Archivo de Empresa " + ex.Message);
                }
            }
            else
            {
                log.Info("No puede subir Archivo de Empresa Fecha de cierre vencida");
            }
        }
        public void eliminarArchivo(int? idArchivo)
        {
            log4net.Config.XmlConfigurator.Configure(new FileInfo(ruta));
            log.Info("Elimina Archivo de Empresa");
            try
            {
                using (db_meieEntities db = new db_meieEntities())
                {
                    var existe = db.LicArchivoEmpresa
                        .Where(x => x.idArchivo == idArchivo)
                        .FirstOrDefault();
                    if (existe != null)
                    {
                        var ruta = existe.ruta + '\\' + existe.nombreArchivoEnc;
                        if (System.IO.File.Exists(ruta))
                        {
                            System.IO.File.Delete(ruta);
                        }
                        db.LicArchivoEmpresa.Remove(existe);
                        db.SaveChanges();
                    }
                }
            }
            catch (Exception ex)
            {
                log.Error("Error Elimina Archivo de Empresa " + ex.Message);
            }
        }
        public ArchivoViewModels buscarArchivo(int? idArchivo)
        {
            ArchivoViewModels unArchivo = new ArchivoViewModels();
            log4net.Config.XmlConfigurator.Configure(new FileInfo(ruta));
            log.Info("Buscar Archivo de Empresa");
            try
            {
                using (db_meieEntities db = new db_meieEntities())
                {
                    var existe = db.LicArchivoEmpresa
                        .Where(x => x.idArchivo == idArchivo)
                        .FirstOrDefault();
                    if (existe != null)
                    {
                        unArchivo.ruta = existe.ruta + '\\' + existe.nombreArchivoEnc;
                        unArchivo.nombre = existe.nombreArchivo;
                    }
                }
            }
            catch (Exception ex)
            {
                log.Error("Error buscar Archivo de Empresa " + ex.Message);
            }
            return unArchivo;
        }
        public void nuevoObra
            (string ruta, string nombre, int? idObra, int? idCategoria)
        {
            log4net.Config.XmlConfigurator.Configure(new FileInfo(ruta));
            log.Info("Graba Archivo de Obra");
            try
            {
                using (db_meieEntities db = new db_meieEntities())
                {
                    var existe = db.LicArchivoObra
                        .Where(x => x.idObra == idObra && x.idObra == idObra && x.idCategoria == idCategoria)
                        .FirstOrDefault();
                    if (existe == null)
                    {
                        //// GRABO ARCHIVO
                        LicArchivoObra unArchivo = new LicArchivoObra();
                        unArchivo.nombreArchivo = nombre;
                        unArchivo.idCategoria = idCategoria;
                        unArchivo.rutaArchivo = ruta;
                        unArchivo.idObra = idObra;
                        unArchivo.nombreArchivo = nombre;
                        unArchivo.fecha = DateTime.Now;
                        db.LicArchivoObra.Add(unArchivo);
                        db.SaveChanges();
                    }
                    else
                    {
                        existe.nombreArchivo = nombre;
                        existe.idCategoria = idCategoria;
                        existe.rutaArchivo = ruta;
                        existe.idObra = idObra;
                        existe.nombreArchivo = nombre;
                        existe.fecha = DateTime.Now;
                        db.SaveChanges();
                    }
                }
            }
            catch (Exception ex)
            {
                log.Error("Error Graba Archivo de Obra " + ex.Message);
            }
        }
        public List<ArchivoObraViewModels> listarArchivoObra(int? idObra)
        {
            List<ArchivoObraViewModels> lista = new List<ArchivoObraViewModels>();
            try
            {
                using (db_meieEntities db = new db_meieEntities())
                {
                    var query = from tl in db.LicArchivoObra
                                join tl2 in db.LicArchivoCategoria on tl.idCategoria equals tl2.idCategoria
                                where tl.idObra == idObra
                                select new ArchivoObraViewModels
                                {
                                    nombre = tl.nombreArchivo,
                                    nombreCategoria = tl2.nombreCategoria,
                                    idDetalle = tl.idLicArchivoObra,
                                    idCategoria = tl.idCategoria,
                                    idObra = tl.idObra,
                                    fechaArchivo = tl.fecha
                                };
                    lista = query.ToList();
                }
            }
            catch (Exception ex)
            {

            }
            return lista;
        }
        public List<ArchivoObraViewModels> listarArchivoObraEmpresa(int? idObra, int? idEmpresa)
        {
            List<ArchivoObraViewModels> lista = new List<ArchivoObraViewModels>();
            try
            {
                using (db_meieEntities db = new db_meieEntities())
                {
                    var query = from tl in db.LicArchivoEmpresa
                                where tl.idObra == idObra && tl.idEmpresa == idEmpresa && tl.nombreArchivo != null && !tl.ruta.Contains("observaciones")
                                select new ArchivoObraViewModels
                                {
                                    nombre = tl.nombreArchivoEnc,
                                    ruta = tl.ruta + "\\" + tl.nombreArchivoEnc,
                                    nombreArchivo = tl.nombreArchivoEnc
                                };
                    lista = query.ToList();
                }
            }
            catch (Exception ex)
            {

            }
            return lista;
        }
        public List<ArchivoObraViewModels> listarArchivoObraEmpresaObs(int? idObra, int? idEmpresa)
        {
            List<ArchivoObraViewModels> lista = new List<ArchivoObraViewModels>();
            try
            {
                using (db_meieEntities db = new db_meieEntities())
                {
                    var query = from tl in db.LicArchivoEmpresa
                                where tl.idObra == idObra && tl.idEmpresa == idEmpresa && tl.nombreArchivo != null &&
                                      (tl.ruta.Contains("observaciones"))
                                select new ArchivoObraViewModels
                                {
                                    nombre = tl.nombreArchivoEnc,
                                    ruta = tl.ruta + "\\" + tl.nombreArchivoEnc,
                                    nombreArchivo = tl.nombreArchivoEnc
                                };
                    lista = query.ToList();
                }
            }
            catch (Exception ex)
            {
                
            }
            return lista;
        }
    }
}
