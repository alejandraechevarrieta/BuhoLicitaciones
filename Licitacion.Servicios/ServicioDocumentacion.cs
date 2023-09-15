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
    public class ServicioDocumentacion
    {
        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        private static string ruta = System.Web.HttpRuntime.AppDomainAppPath + "\\bin\\log4net.config";
        public List<selectViewModels> listarReferencia()
        {
            log4net.Config.XmlConfigurator.Configure(new FileInfo(ruta));
            log.Info("Listar Referencias");

            List<selectViewModels> lista = new List<selectViewModels>();
            try
            {
                using (db_meieEntities db = new db_meieEntities())
                {
                    var tmp = from tbl in db.LicDocGeneral
                              where tbl.esPadre == 1
                              select new selectViewModels
                              {
                                  nombre = tbl.nombre,
                                  id = tbl.idLicDocGeneral
                              };
                    lista = tmp.ToList();
                }
            }
            catch (Exception ex)
            {
                log.Error("Error en Listar Referencias", ex);
            }
            return lista;
        }
        public int nuevaReferencia(ReferenciaViewModels unReferencia)
        {
            log4net.Config.XmlConfigurator.Configure(new FileInfo(ruta));
            log.Info("Nueva Referencia");

            List<selectViewModels> lista = new List<selectViewModels>();
            try
            {
                using (db_meieEntities db = new db_meieEntities())
                {
                    LicDocGeneral docGeneral = new LicDocGeneral();
                    docGeneral.activo = 1;
                    docGeneral.esPadre = 1;
                    docGeneral.nombre = unReferencia.nombre;
                    db.LicDocGeneral.Add(docGeneral);
                    db.SaveChanges();

                }
            }
            catch (Exception ex)
            {
                log.Error("Error en Nueva Referencia", ex);
                return 0;
            }
            return 1;
        }
        public int nuevoRequisito(RequisitoViewModels unRequisito)
        {
            log4net.Config.XmlConfigurator.Configure(new FileInfo(ruta));
            log.Info("Nuevo Requisito");
            try
            {
                using (db_meieEntities db = new db_meieEntities())
                {
                    LicDocGeneral docGeneral = new LicDocGeneral();
                    docGeneral.activo = 1;
                    docGeneral.esPadre = 0;
                    docGeneral.idPadre = unRequisito.idPadre;
                    docGeneral.nombre = unRequisito.nombre;
                    db.LicDocGeneral.Add(docGeneral);
                    db.SaveChanges();

                }
            }
            catch (Exception ex)
            {
                log.Error("Error en Nuevo Requisito", ex);
                return 0;
            }
            return 1;
        }
        public List<RequisitoViewModels> listarRequisitoPorPadre(int? idPadre)
        {
            log4net.Config.XmlConfigurator.Configure(new FileInfo(ruta));
            log.Info("Listar Requisitos Filtrado por padre");

            List<RequisitoViewModels> lista = new List<RequisitoViewModels>();
            try
            {
                using (db_meieEntities db = new db_meieEntities())
                {
                    var query = (from tb1 in db.LicDocGeneral
                                 where tb1.idPadre == idPadre
                                 select new RequisitoViewModels
                                 {
                                     idPadre = tb1.idPadre,
                                     nombre = tb1.nombre,
                                     idRequisito = tb1.idLicDocGeneral
                                 });
                    lista = query.ToList();
                }
            }
            catch (Exception ex)
            {
                log.Error("Error en Listar Requisitos", ex);
            }
            return lista;
        }       
        public List<RequisitoViewModels> listarRequisitoPorObra(int? idPadre, int? idObra, int? idSobre)
        {
            log4net.Config.XmlConfigurator.Configure(new FileInfo(ruta));
            log.Info("Listar Requisitos Filtrado por idObr ay nroSobre");

            List<RequisitoViewModels> lista = new List<RequisitoViewModels>();
            try
            {
                using (db_meieEntities db = new db_meieEntities())
                {
                    var query = (from tb1 in db.LicDocObra
                                 join tb2 in db.LicDocGeneral
                                 on tb1.idDocumentacion equals tb2.idLicDocGeneral
                                 join tb3 in db.LicDocSobres
                                 on tb1.nroSobre equals tb3.numero
                                 //where tb2.idPadre == idPadre && tb1.nroSobre == nroSobre && tb1.idObra == idObra
                                 where tb3.idLicDocSobres == idSobre && tb1.idObra == idObra
                                 select new RequisitoViewModels
                                 {
                                     idPadre = tb2.idPadre,
                                     nombre = tb2.nombre,
                                     idRequisito = tb1.idDocumentacion,
                                     nroSobre = tb1.nroSobre,
                                     id = tb1.idLicDocObra
                                 });
                    lista = query.ToList();
                }
            }
            catch (Exception ex)
            {
                log.Error("Error en Listar Requisitos", ex);
            }
            return lista;
        }       
        public int nuevoRequisitoObra(RequisitoObraViewModels unRequisito)
        {
            log4net.Config.XmlConfigurator.Configure(new FileInfo(ruta));
            log.Info("Nuevo Requisito para una Obra");
            try
            {
                using (db_meieEntities db = new db_meieEntities())
                {
                    var existe = db.LicDocObra
                        .Where(x => x.idDocumentacion == unRequisito.idRequisito.Value && x.idObra == unRequisito.idObra && x.nroSobre == x.nroSobre)
                        .FirstOrDefault();
                    var unSobre = db.LicDocSobres
                        .Where(x => x.idLicDocSobres == unRequisito.idSobre)
                        .FirstOrDefault();
                    if (existe == null)
                    {
                        LicDocObra docGeneral = new LicDocObra();
                        docGeneral.idDocumentacion = unRequisito.idRequisito.Value;
                        docGeneral.idObra = unRequisito.idObra;
                        docGeneral.nroSobre = unSobre.numero;
                        db.LicDocObra.Add(docGeneral);
                        db.SaveChanges();
                        log.Info("Se grabo el requisito en la obra");
                    }
                    else
                    {
                        log.Info("No se carga el requisito porque ya existe");
                    }
                }
            }
            catch (Exception ex)
            {
                log.Error("Error en Nuevo Requisito para una Obra", ex);
                return 0;
            }
            return 1;
        }
        public int eliminarRequisitoObra(int? idRequisito)
        {
            log4net.Config.XmlConfigurator.Configure(new FileInfo(ruta));
            log.Info("Eliminar Requisito para una Obra");
            try
            {
                using (db_meieEntities db = new db_meieEntities())
                {
                    var existe = db.LicDocObra.Where(x => x.idLicDocObra == idRequisito).FirstOrDefault();
                    if (existe != null)
                    {
                        db.LicDocObra.Remove(existe);
                        db.SaveChanges();
                    }
                }
            }
            catch (Exception ex)
            {
                log.Error("Error en Eliminar Requisito para una Obra", ex);
                return 0;
            }
            return 1;
        }
        public List<selectViewModels> listarRequisitoPorObra(int? idObra, int? idSobre)
        {
            log4net.Config.XmlConfigurator.Configure(new FileInfo(ruta));
            log.Info("Listar Requisitos Filtrado por padre");

            List<selectViewModels> lista = new List<selectViewModels>();
            try
            {               
                using (db_meieEntities db = new db_meieEntities())
                {
                    var query = (from tb1 in db.LicDocObra
                                 join tb2 in db.LicDocGeneral
                                    on tb1.idDocumentacion equals tb2.idLicDocGeneral
                                 join tb3 in db.LicDocSobres
                                on tb1.nroSobre equals tb3.numero
                                 where tb3.idLicDocSobres == idSobre && tb1.idObra == idObra
                                 select new selectViewModels
                                 {
                                     nombre = tb2.nombre,
                                     id = tb1.idLicDocObra
                                 });
                    lista = query.ToList();
                }
            }
            catch (Exception ex)
            {
                log.Error("Error en Listar Requisitos", ex);
            }
            return lista;
        }
        public List<RequisitoViewModels> listarRequisitoPorObraEmpresa(int? idEmpresa, int? idObra, int? idSobre, int? idEstado)
        {
            log4net.Config.XmlConfigurator.Configure(new FileInfo(ruta));
            log.Info("Listar Requisitos Filtrado por idObra, nroSobre y Empresa");

            List<RequisitoViewModels> lista = new List<RequisitoViewModels>();
            List<RequisitoViewModels> tmp = new List<RequisitoViewModels>();
            try
            {
                using (db_meieEntities db = new db_meieEntities())
                {
                    var query = (from tb1 in db.LicDocObra
                                 join tb2 in db.LicDocGeneral
                                    on tb1.idDocumentacion equals tb2.idLicDocGeneral
                                 join tb3 in db.LicDocSobres
                                 on tb1.nroSobre equals tb3.numero
                                 where tb3.idLicDocSobres == idSobre && tb1.idObra == idObra
                                 // //where tb2.idPadre == idPadre && tb1.nroSobre == nroSobre && tb1.idObra == idObra
                                 // where tb1.nroSobre == nroSobre && tb1.idObra == idObra
                                 select new RequisitoViewModels
                                 {
                                     idPadre = tb2.idPadre,
                                     nombre = tb2.nombre,
                                     idRequisito = tb1.idDocumentacion,
                                     nroSobre = tb1.nroSobre,
                                     id = tb1.idLicDocObra
                                 });
                    tmp = query.ToList();
                    foreach (var item in tmp)
                    {
                        var existe = db.LicArchivoEmpresa
                            .Where(x => x.idRequisito == item.id && x.idEmpresa == idEmpresa && x.nroSobre == item.nroSobre)
                            .FirstOrDefault();
                        if (existe != null)
                        {
                            item.completo = 1;
                            item.idArchivo = existe.idArchivo;
                        }
                        else
                        {
                            item.completo = 0;
                        }
                        if(idEstado == 0)
                        {
                            lista.Add(item);
                        }
                        else
                        {
                            if(existe != null)
                            {
                                if (existe.idEstadoArchivo == idEstado)
                                {
                                    lista.Add(item);
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                log.Error("Error en listarRequisitoPorObraEmpresa", ex);
            }
            return lista;
        }
        public List<RequisitoViewModels> listarArchivoPorObraEmpresa(int? idEmpresa, int? idObra, int? idSobre)
        {
            log4net.Config.XmlConfigurator.Configure(new FileInfo(ruta));
            log.Info("Listar Archivos Filtrado por idObra, nroSobre y Empresa");

            List<RequisitoViewModels> lista = new List<RequisitoViewModels>();
            try
            {
                using (db_meieEntities db = new db_meieEntities())
                {
                    var query = (from tb1 in db.LicDocObra
                                 join tb2 in db.LicDocGeneral
                                    on tb1.idDocumentacion equals tb2.idLicDocGeneral
                                 join tb3 in db.LicDocSobres
                                    on tb1.nroSobre equals tb3.numero
                                 where tb3.idLicDocSobres == idSobre && tb1.idObra == idObra
                                 select new RequisitoViewModels
                                 {
                                     idPadre = tb2.idPadre,
                                     nombre = tb2.nombre,
                                     idRequisito = tb1.idDocumentacion,
                                     nroSobre = tb1.nroSobre,
                                     id = tb1.idLicDocObra,
                                     horaArchivo="",
                                     idArchivo = 0,
                                     tieneArchivo = true
                                 });
                    lista = query.ToList();
                    foreach (var item in lista)
                    {
                        var tieneArchivo = db.LicArchivoEmpresa
                                              .Where(x => x.idEmpresa == idEmpresa && x.nroSobre == item.nroSobre && x.idObra == idObra && x.idRequisito == item.id)
                                              .FirstOrDefault();
                        if (tieneArchivo != null)
                        {
                            item.idArchivo = tieneArchivo.idArchivo;
                            item.idEstado = tieneArchivo.idEstadoArchivo;
                            item.observacion = string.IsNullOrEmpty(tieneArchivo.observaciones) ? "" : tieneArchivo.observaciones;
                            item.horaArchivo = string.IsNullOrEmpty(tieneArchivo.hora)?"":tieneArchivo.hora;
                            if (string.IsNullOrEmpty(tieneArchivo.nombreArchivo))
                            {
                                item.tieneArchivo = false;
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                log.Error("Error en listarArchivoPorObraEmpresa", ex);
            }
            return lista;
        }
        public int cambiarEstadoArchivo(int? idArchivo, string observaciones, int? idEstado)
        {
            log4net.Config.XmlConfigurator.Configure(new FileInfo(ruta));
            log.Info("Cambia Estado Archivos de la Oferta");

            try
            {
                using (db_meieEntities db = new db_meieEntities())
                {
                    var unArchivo = db.LicArchivoEmpresa.Where(x => x.idArchivo == idArchivo).FirstOrDefault();
                    if (unArchivo != null)
                    {
                        unArchivo.idEstadoArchivo = idEstado;
                        unArchivo.observaciones = observaciones;
                        db.SaveChanges();
                    }
                }
                return 1;
            }
            catch (Exception ex)
            {
                log.Error("Error en listarArchivoPorObraEmpresa", ex);
            }
            return 0;
        }
        public List<RequisitoViewModels> listarRequisitoPorObraEmpresa(int? idEmpresa, int? idObra)
        {
            log4net.Config.XmlConfigurator.Configure(new FileInfo(ruta));
            log.Info("Listar Requisitos Filtrado por idObra y Empresa");

            List<RequisitoViewModels> lista = new List<RequisitoViewModels>();
            List<RequisitoViewModels> tmp = new List<RequisitoViewModels>();
            try
            {
                using (db_meieEntities db = new db_meieEntities())
                {
                    var query = (from tb1 in db.LicDocObra
                                 join tb2 in db.LicDocGeneral
                                    on tb1.idDocumentacion equals tb2.idLicDocGeneral
                                 where tb1.idObra == idObra
                                 select new RequisitoViewModels
                                 {
                                     idPadre = tb2.idPadre,
                                     nombre = tb2.nombre,
                                     idRequisito = tb1.idDocumentacion,
                                     nroSobre = tb1.nroSobre,
                                     id = tb1.idLicDocObra
                                 });
                    tmp = query.ToList();
                    foreach (var item in tmp)
                    {
                        var existe = db.LicArchivoEmpresa
                            .Where(x => x.idRequisito == item.id && x.idEmpresa == idEmpresa)
                            .FirstOrDefault();
                        if (existe != null)
                        {
                            item.completo = 1;
                            item.idArchivo = existe.idArchivo;
                            item.nombreArchivo = existe.nombreArchivo;
                        }
                        else
                        {
                            item.completo = 0;
                            item.nombreArchivo = "No se subió ningún archivo";
                        }
                        lista.Add(item);
                    }
                }
            }
            catch (Exception ex)
            {
                log.Error("Error en listarRequisitoPorObraEmpresa", ex);
            }
            return lista;
        }
        public List<SobresViewModels> listarSobres(int? idObra)
        {
            log4net.Config.XmlConfigurator.Configure(new FileInfo(ruta));
            log.Info("Listar Sobres");

            List<SobresViewModels> lista = new List<SobresViewModels>();
            try
            {
                agregarSobres(idObra);
                using (db_meieEntities db = new db_meieEntities())
                {
                    var tmp = from s in db.LicDocSobres
                              where s.activo == true && s.idObra == idObra
                              select new SobresViewModels
                              {
                                  idLicDocSobres = s.idLicDocSobres,
                                  numero = s.numero,
                                  nombre = "Nro " + s.numero + (s.nombre != null ? " - " + s.nombre : ""),
                                  idObra = s.idObra,
                              };
                    lista = tmp.ToList();
                }
            }
            catch (Exception ex)
            {
                log.Error("Error en Listar Referencias", ex);
            }
            return lista;
        }
        public int agregarSobres(int? idObra)
        {
            log4net.Config.XmlConfigurator.Configure(new FileInfo(ruta));
            log.Info("Nuevos Sobres");

            try
            {
                using (db_meieEntities db = new db_meieEntities())
                {
                    var existe = db.LicDocSobres
                           .Where(x => x.idObra == idObra && x.activo == true)
                           .FirstOrDefault();
                    LicDocSobres docSobres = new LicDocSobres();
                    if (existe == null)
                    {
                        int i = 1;
                        for (i = 1; i <= 3; i++)
                        {
                            docSobres.numero = i;
                            docSobres.idObra = idObra;
                            docSobres.activo = true;
                            db.LicDocSobres.Add(docSobres);
                            db.SaveChanges();
                        }
                    }
                    db.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                log.Error("Error en Nuevas Sobres", ex);
                return 0;
            }
            return 1;
        }
        public int? ultimoSobre(int? idObra)
        {
            log4net.Config.XmlConfigurator.Configure(new FileInfo(ruta));
            log.Info("ultimo");
            int? numeroUltimoSobre = 0;
            try
            {
                using (db_meieEntities db = new db_meieEntities())
                {
                    var ultimoSobre = db.LicDocSobres
                         .Where(x => x.idObra == idObra && x.activo == true)
                         .OrderByDescending(x => x.numero)
                         .FirstOrDefault();

                    if (ultimoSobre != null)
                    {
                        numeroUltimoSobre = ultimoSobre.numero;
                    }
                }
            }
            catch (Exception ex)
            {
                log.Error("Error en Carga ultimo Sobres", ex);
            }
            return numeroUltimoSobre + 1;
        }

        public int nuevoSobre(SobresViewModels unSobre)
        {
            log4net.Config.XmlConfigurator.Configure(new FileInfo(ruta));
            log.Info("Nuevo Sobre");
            try
            {
                using (db_meieEntities db = new db_meieEntities())
                {
                    var ultimoSobre = db.LicDocSobres
                            .Where(x => x.idObra == unSobre.idObra && x.activo == true)
                            .OrderByDescending(x => x.numero)
                            .FirstOrDefault();

                    LicDocSobres docSobre = new LicDocSobres();
                   
                    docSobre.nombre = string.IsNullOrWhiteSpace(unSobre.nombre) ? null : unSobre.nombre;
                    docSobre.idObra = unSobre.idObra;
                    docSobre.activo = true;
                    if (ultimoSobre != null)  //pongo esto por las dudas 
                    {
                        docSobre.numero = ultimoSobre.numero + 1;
                    } else
                        {
                            docSobre.numero = 1;
                        }
                    db.LicDocSobres.Add(docSobre);
                    db.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                log.Error("Error en Nuevo Sobre", ex);
                return 0;
            }
            return 1;
        }
        public SobresViewModels buscarSobre(int? idObra, int? idSobre)
        {
            log4net.Config.XmlConfigurator.Configure(new FileInfo(ruta));
            log.Info("buscar Un Sobre");
            SobresViewModels unSobre = new SobresViewModels();
            try
            {
                using (db_meieEntities db = new db_meieEntities())
                {
                    var tmp = from s in db.LicDocSobres
                              where s.activo == true && s.idLicDocSobres == idSobre
                              select new SobresViewModels
                              {
                                  idLicDocSobres = s.idLicDocSobres,
                                  numero = s.numero,
                                  nombre = s.nombre != null ? s.nombre : "",
                                  idObra = s.idObra,
                              };
                    unSobre = tmp.FirstOrDefault();

                }
            }
            catch (Exception ex)
            {
                log.Error("Error buscar Una Licitacion", ex);
            }

            return unSobre;
        }
        public int editarSobre(SobresViewModels unSobre)
        {
            log4net.Config.XmlConfigurator.Configure(new FileInfo(ruta));
            log.Info("Editar Sobre");
            try
            {
                using (db_meieEntities db = new db_meieEntities())
                {
                    var sobre = db.LicDocSobres
                            .Where(x => x.idLicDocSobres == unSobre.idLicDocSobres)                            
                            .FirstOrDefault();

                    sobre.nombre = unSobre.nombre;

                    db.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                log.Error("Error en Nuevo Sobre", ex);
                return 0;
            }
            return 1;
        }
        public int eliminarSobre(SobresViewModels unSobre)
        {
            log4net.Config.XmlConfigurator.Configure(new FileInfo(ruta));
            log.Info("Editar Sobre");
            try
            {
                using (db_meieEntities db = new db_meieEntities())
                {
                    var sobre = db.LicDocSobres
                            .Where(x => x.idLicDocSobres == unSobre.idLicDocSobres)                            
                            .FirstOrDefault();
                    var requisitosSobre = db.LicDocObra 
                           .Where(x => x.idObra == unSobre.idObra && x.nroSobre == sobre.numero)
                           .ToList();
                    foreach(var item in requisitosSobre) //elimino los requisitos
                    {
                        db.LicDocObra.Remove(item);
                    }
                    db.LicDocSobres.Remove(sobre);      //elimino el sobre
                    db.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                log.Error("Error en Nuevo Sobre", ex);
                return 0;
            }
            return 1;
        }
        /// <summary>
        /// lista requisitos de observaciones
        /// </summary>
        /// <param name="idEmpresa"></param>
        /// <param name="idObra"></param>
        /// <param name="idSobre"></param>
        /// <param name="idEstado"></param>
        /// <returns></returns>
        public List<RequisitoViewModels> listarRequisitoPorObraEmpresaObs(int? idEmpresa, int? idObra, int? idSobre, int? idEstado)
        {
            log4net.Config.XmlConfigurator.Configure(new FileInfo(ruta));
            log.Info("Listar Requisitos Filtrado por idObra, nroSobre y Empresa");

            List<RequisitoViewModels> lista = new List<RequisitoViewModels>();
            List<RequisitoViewModels> tmp = new List<RequisitoViewModels>();
            try
            {
                using (db_meieEntities db = new db_meieEntities())
                {
                    var query = (from tb1 in db.LicDocObra
                                 join tb2 in db.LicDocGeneral
                                    on tb1.idDocumentacion equals tb2.idLicDocGeneral
                                 join tb3 in db.LicDocSobres
                                 on tb1.nroSobre equals tb3.numero
                                 where tb3.idLicDocSobres == idSobre && tb1.idObra == idObra                                 
                                 select new RequisitoViewModels
                                 {
                                     idPadre = tb2.idPadre,
                                     nombre = tb2.nombre,
                                     idRequisito = tb1.idDocumentacion,
                                     nroSobre = tb1.nroSobre,
                                     id = tb1.idLicDocObra
                                 });
                    tmp = query.ToList();
                    foreach (var item in tmp)
                    {
                        var existe = db.LicArchivoEmpresa
                            .Where(x => x.idRequisito == item.id && x.idEmpresa == idEmpresa && x.nroSobre == item.nroSobre && x.ruta.Contains("observaciones"))
                            .FirstOrDefault();
                        if (existe != null)
                        {
                            item.completo = 1;
                            item.idArchivo = existe.idArchivo;
                        }
                        else
                        {
                            item.completo = 0;
                        }
                        if (idEstado == 0)
                        {
                            lista.Add(item);
                        }
                        else
                        {
                            if (existe != null)
                            {
                                if (existe.idEstadoArchivo == idEstado)
                                {
                                    lista.Add(item);
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                log.Error("Error en listarRequisitoPorObraEmpresaObs", ex);
            }
            return lista;
        }
    }
}