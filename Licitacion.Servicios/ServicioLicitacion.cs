
using Licitaciones.BaseDato;
using Licitaciones.ViewModels;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Configuration;
using System;
using System.Security.Principal;
using System.Text.RegularExpressions;
using static iTextSharp.text.pdf.AcroFields;

namespace Licitacion.Servicios
{
    public class ServicioLicitacion
    {
        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        private static string ruta = System.Web.HttpRuntime.AppDomainAppPath + "\\bin\\log4net.config";
        public List<licitacionGrillaViewModels> listaLicitaciones
        (ref int recordsTotal, string sortColumn, string sortColumnDir, string searchValue,
            string nroExpediente, string nombreObra, string qObra, int? idEstado, DateTime? fechaPub, int? idOrganismo, int? nroObra,
            int pageSize = 0, int skip = 0)
        {
            log4net.Config.XmlConfigurator.Configure(new FileInfo(ruta));
            log.Info("Grilla Licitaciones Administrador");
            List<licitacionGrillaViewModels> lista = new List<licitacionGrillaViewModels>();
            try
            {
                using (db_meieEntities db = new db_meieEntities())
                {

                    var query = from lic in db.PryProyectoes
                                join edo in db.PryEstadoEtapas
                                on lic.GrlTypeState_Id equals edo.Id
                                join pyl in db.PryLicitacions
                                on lic.Id equals pyl.IdPryProyecto
                                    into pylo
                                from pyj in pylo.DefaultIfEmpty()
                                join org in db.PryOrganismosEjecutores
                                on lic.PryOrganismoEjecutor_Id equals org.Id
                                    into orgo
                                from orgj in orgo.DefaultIfEmpty()
                                where edo.PryStage_Id == 49 && lic.Eliminado == false
                                select new licitacionGrillaViewModels
                                {
                                    nombreEtapa = edo.Name,
                                    idEtapa = edo.Id,
                                    nombreObra = lic.Nombre,
                                    idObra = lic.Id,
                                    fechaApertura = pyj.FechaApertura,
                                    fechaPublicacion = pyj.FechaPublicacionDesde,
                                    montoObra = pyj.MontoOficial,
                                    nroExpediente = lic.Expediente,
                                    nombreOrganismo = orgj.NombreOrganismo,
                                    idOrganismo = lic.PryOrganismoEjecutor_Id,
                                    Eliminado = lic.Eliminado,
                                    idOrgano = lic.ufiOrgano_Id,
                                    idOffice = lic.oficinaCarga,
                                    idOrganoReceptor = lic.ufiOrganoReceptor_Id
                                };

                    if (!string.IsNullOrEmpty(qObra))
                    {
                        if(qObra != "*")
                        {
                            var array = qObra.Split(',');
                            List<int?> listaObra = new List<int?>();
                            foreach (var item in array)
                            {
                                listaObra.Add(Convert.ToInt32(item));
                            }
                            query = query.Where(x => listaObra.Contains(x.idObra));
                        }
                    }
                    if (idOrganismo != 0)
                    {
                        query = query.Where(x => x.idOrganismo == idOrganismo);
                    }
                    if (idEstado != 0)
                    {
                        query = query.Where(x => x.idEtapa == idEstado);
                    }
                    if (!string.IsNullOrEmpty(nombreObra))
                    {
                        query = query.Where(x => x.nombreObra.Contains(nombreObra));
                    }
                    if (nroObra.HasValue)
                    {
                        if(nroObra != 0)
                        {
                            query = query.Where(x => x.idObra == nroObra);
                        }
                    }
                    if (!string.IsNullOrEmpty(nroExpediente))
                    {
                        query = query.Where(x => x.nroExpediente.Contains(nroExpediente));
                    }
                    if (fechaPub.HasValue)
                    {
                        query = query.Where(x => x.fechaPublicacion == fechaPub);
                    }
                    if (!(string.IsNullOrEmpty(sortColumn) && string.IsNullOrEmpty(sortColumnDir)))
                    {
                        if ((sortColumn == "nombreObra") && (sortColumnDir == "desc"))
                        {
                            query = query.OrderByDescending(x => x.nombreObra);
                        }
                        if ((sortColumn == "nombreObra") && (sortColumnDir == "asc"))
                        {
                            query = query.OrderBy(x => x.nombreObra);
                        }
                        if ((sortColumn == "nroExpedienteString") && (sortColumnDir == "desc"))
                        {
                            query = query.OrderByDescending(x => x.nroExpediente);
                        }
                        if ((sortColumn == "nroExpedienteString") && (sortColumnDir == "asc"))
                        {
                            query = query.OrderBy(x => x.nroExpediente);
                        }
                        if ((sortColumn == "fechaPublicacionString") && (sortColumnDir == "desc"))
                        {
                            query = query.OrderByDescending(x => x.fechaPublicacion);
                        }
                        if ((sortColumn == "fechaPublicacionString") && (sortColumnDir == "asc"))
                        {
                            query = query.OrderBy(x => x.fechaPublicacion);
                        }
                        if ((sortColumn == "nombreOrganismoString") && (sortColumnDir == "asc"))
                        {
                            query = query.OrderBy(x => x.nombreOrganismo);
                        }
                        if ((sortColumn == "nombreOrganismoString") && (sortColumnDir == "desc"))
                        {
                            query = query.OrderByDescending(x => x.nombreOrganismo);
                        }
                        if ((sortColumn == "montoObraString") && (sortColumnDir == "asc"))
                        {
                            query = query.OrderBy(x => x.montoObra);
                        }
                        if ((sortColumn == "montoObraString") && (sortColumnDir == "desc"))
                        {
                            query = query.OrderByDescending(x => x.montoObra);
                        }
                        if ((sortColumn == "nombreEtapa") && (sortColumnDir == "asc"))
                        {
                            query = query.OrderBy(x => x.nombreEtapa);
                        }
                        if ((sortColumn == "nombreEtapa") && (sortColumnDir == "desc"))
                        {
                            query = query.OrderByDescending(x => x.nombreEtapa);
                        }
                        if ((sortColumn == "accion") && (sortColumnDir == "asc"))
                        {
                            query = query.OrderBy(x => x.nombreObra);
                        }
                        if ((sortColumn == "accion") && (sortColumnDir == "desc"))
                        {
                            query = query.OrderByDescending(x => x.nombreObra);
                        }
                        if ((sortColumn == "idObra") && (sortColumnDir == "asc"))
                        {
                            query = query.OrderBy(x => x.idObra);
                        }
                        if ((sortColumn == "idObra") && (sortColumnDir == "desc"))
                        {
                            query = query.OrderByDescending(x => x.idObra);
                        }

                    }
                    else
                    {
                        query = query = query.OrderBy(x => x.nombreObra);
                    }

                    query = query.Take(500);
                    
                    var oficina = System.Web.HttpContext.Current.Session["idOficina"].ToString();
                    if (Convert.ToInt32(oficina) > 1)
                    {
                        var usuario = System.Web.HttpContext.Current.Session["idUsuario"].ToString();
                        query = ProyectFilterByOffice(Convert.ToInt32(oficina), Convert.ToInt32(usuario), query);
                        var lst = query.Skip(skip).Take(pageSize);
                        lista = lst.ToList();
                    }
                    else if (Convert.ToInt32(oficina) == 1)
                    {
                        var lst = query.Skip(skip).Take(pageSize);
                        lista = lst.ToList();
                    }
                    recordsTotal = query.Count();

                    return lista;
                }
            }
            catch (Exception ex)
            {
                var a = ex.InnerException;
                var e = ex.Data;
                log.Error("Error en Grilla Licitaciones Administrador", ex);
            }
            return lista;
        }
        public tarjetaLicViewModels calcularTarjeta
        (string nroExpediente, string nombreObra, string qObra, DateTime? fechaPub, int? idOrganismo, int? nroObra)
        {
            log4net.Config.XmlConfigurator.Configure(new FileInfo(ruta));
            log.Info("Tarjeta Licitaciones");
            tarjetaLicViewModels tarjeta = new tarjetaLicViewModels();
            tarjeta.qAbierta = 0;
            tarjeta.qAdjudicada = 0;
            tarjeta.qAPublicar = 0;
            tarjeta.qDesierta = 0;
            tarjeta.qFirmada = 0;
            tarjeta.qFracasada = 0;
            tarjeta.qPreAdjudicada = 0;
            tarjeta.qPublicada = 0;
            try
            {
                using (db_meieEntities db = new db_meieEntities())
                {
                    var query = from lic in db.PryProyectoes
                                join edo in db.PryEstadoEtapas
                                on lic.GrlTypeState_Id equals edo.Id
                                join pyl in db.PryLicitacions
                                on lic.Id equals pyl.IdPryProyecto
                                    into pylo
                                from pyj in pylo.DefaultIfEmpty()
                                join org in db.PryOrganismosEjecutores
                                on lic.PryOrganismoEjecutor_Id equals org.Id
                                    into orgo
                                from orgj in orgo.DefaultIfEmpty()
                                where edo.PryStage_Id == 49 && lic.Eliminado == false
                                select new licitacionGrillaViewModels
                                {
                                    nombreEtapa = edo.Name,
                                    idEtapa = edo.Id,
                                    nombreObra = lic.Nombre,
                                    idObra = lic.Id,
                                    fechaApertura = pyj.FechaApertura,
                                    fechaPublicacion = pyj.FechaPublicacionDesde,
                                    montoObra = pyj.MontoOficial,
                                    nroExpediente = lic.Expediente,
                                    nombreOrganismo = orgj.NombreOrganismo,
                                    idOrganismo = lic.PryOrganismoEjecutor_Id,
                                    Eliminado = lic.Eliminado,
                                    idOrgano = lic.ufiOrgano_Id,
                                    idOffice = lic.oficinaCarga,
                                    idOrganoReceptor = lic.ufiOrganoReceptor_Id
                                };

                    if (!string.IsNullOrEmpty(qObra))
                    {
                        if (qObra != "*")
                        {
                            var array = qObra.Split(',');
                            List<int?> listaObra = new List<int?>();
                            foreach (var item in array)
                            {
                                listaObra.Add(Convert.ToInt32(item));
                            }
                            query = query.Where(x => listaObra.Contains(x.idObra));
                        }
                    }
                    if (idOrganismo != 0)
                    {
                        query = query.Where(x => x.idOrganismo == idOrganismo);
                    }
                    if (!string.IsNullOrEmpty(nombreObra))
                    {
                        query = query.Where(x => x.nombreObra.Contains(nombreObra));
                    }
                    if (!string.IsNullOrEmpty(nroExpediente))
                    {
                        query = query.Where(x => x.nroExpediente.Contains(nroExpediente));
                    }
                    if (fechaPub.HasValue)
                    {
                        query = query.Where(x => x.fechaPublicacion == fechaPub);
                    }
                    if (nroObra.HasValue)
                    {
                        if (nroObra != 0)
                        {
                            query = query.Where(x => x.idObra == nroObra);
                        }
                    }
                    var oficina = System.Web.HttpContext.Current.Session["idOficina"].ToString();
                    if (Convert.ToInt32(oficina) > 1)
                    {
                        var usuario = System.Web.HttpContext.Current.Session["idUsuario"].ToString();
                        query = ProyectFilterByOffice(Convert.ToInt32(oficina), Convert.ToInt32(usuario), query);
                    }
                        tarjeta.qAPublicar = query.Where(x => x.idEtapa == 5).Count();
                        tarjeta.qPublicada = query.Where(x => x.idEtapa == 6).Count();
                        tarjeta.qAbierta = query.Where(x => x.idEtapa == 7).Count();
                        tarjeta.qFirmada = query.Where(x => x.idEtapa == 8).Count();
                        tarjeta.qAdjudicada = query.Where(x => x.idEtapa == 14).Count();
                        tarjeta.qPreAdjudicada = query.Where(x => x.idEtapa == 15).Count();
                        tarjeta.qFracasada = query.Where(x => x.idEtapa == 16).Count();
                        tarjeta.qDesierta = query.Where(x => x.idEtapa == 17).Count();
                    
                                       
                }
            }
            catch (Exception ex)
            {
                log.Error("Error en Tarjeta Licitaciones", ex);
            }
            return tarjeta;
        }
        public unLicitacionViewModels buscarUna(int? idObra)
        {
            log4net.Config.XmlConfigurator.Configure(new FileInfo(ruta));
            log.Info("buscar Una Licitacion");
            unLicitacionViewModels unLicitacion = new unLicitacionViewModels();
            try
            {
                using (db_meieEntities db = new db_meieEntities())
                {
                    var query = from lic in db.PryProyectoes
                                join edo in db.PryEstadoEtapas
                                on lic.GrlTypeState_Id equals edo.Id

                                join dp in db.GrlDepartaments
                                on lic.GrlDepartament_Id equals dp.Id
                                    into dpo
                                from dpj in dpo.DefaultIfEmpty()

                                join pyl in db.PryLicitacions
                                on lic.Id equals pyl.IdPryProyecto
                                    into pylo
                                from pyj in pylo.DefaultIfEmpty()

                                join ff in db.PryFinanciacion
                                    on lic.PryFinanciacion_Id equals ff.IdPryFinanciacion
                                        into ffo
                                from ffj in ffo.DefaultIfEmpty()

                                join org in db.PryOrganismosEjecutores
                                on lic.PryOrganismoEjecutor_Id equals org.Id
                                    into orgo
                                from orgj in orgo.DefaultIfEmpty()
                                join licpry in db.LicProyecto
                                on lic.Id equals licpry.idProyecto                                 
                                   into licpryo
                                from licpryj in licpryo.DefaultIfEmpty()
                                where lic.Id == idObra && lic.Eliminado == false
                                select new unLicitacionViewModels
                                {
                                    nombreEtapa = edo.Name,
                                    idEtapa = edo.Id,
                                    nombreObra = lic.Nombre,
                                    idObra = lic.Id,
                                    fechaApertura = pyj.FechaApertura,
                                    fechaPublicacion = pyj.FechaPublicacionDesde,
                                    montoObra = pyj.MontoOficial,
                                    nroExpediente = lic.Expediente,
                                    nombreOrganismo = orgj.NombreOrganismo,
                                    idOrganismo = lic.PryOrganismoEjecutor_Id,
                                    caratula = lic.CaratulaExpediente,
                                    descripcion = lic.descripcion,
                                    plazo = pyj.Plazo,
                                    latitud = lic.Latitud,
                                    longitud = lic.Longitud,
                                    domicilio = lic.Dirección,
                                    valorPliego = pyj.ValorPliego,
                                    horaApertura = pyj.Hora,
                                    idTipoContratacion = pyj.TipoDeContratacion_Id,
                                    idMoneda = licpryj.moneda,
                                    domicilioApertura = licpryj.domicilioApertura,
                                    domicilioPresentacion = licpryj.domicilioPresentacion,
                                    urlPliego = licpryj.urlPliego,
                                    mailConsulta = licpryj.mailConsulta,
                                    fechaCierre = licpryj.fechaCierre,
                                    lugarVisita = licpryj.lugarVisita,
                                    fechaVisita = licpryj.fechaVisita,
                                    fechaConsulta = licpryj.limiteConsulta,
                                    departamento = dpj.Name,
                                    idFinanciamiento = lic.PryFinanciacion_Id,
                                    nombreFuenteFinanciamiento = ffj.nombrePryFinanciacion,
                                    idTipoLicitacion = pyj.Id,
                                    unidadPlazo = pyj.TipoPlazo,
                                    rutaVirtual= licpryj.domicilioVirtual,
                                    textoFinanciamiento = lic.financiamiento,
                                    nroLicitacion = lic.numeroLicitacion,
                                    fechaVtoObs = licpryj.fechaVtoObs,
                                    horaVtoObs = licpryj.horaVtoObs, 
                                };
                    unLicitacion = query.FirstOrDefault();
                    var listaFecha = buscarListaFechas(idObra);
                    if ((listaFecha != null)&&(listaFecha.Count > 0)){
                        var unFecha = listaFecha.OrderByDescending(x => x.idDetalle).FirstOrDefault();
                        if (unFecha.fechaApertura.HasValue)
                        {
                            unLicitacion.fechaApertura = unFecha.fechaApertura;
                        }
                        if (unFecha.fechaCierre.HasValue)
                        {
                            unLicitacion.fechaCierre = unFecha.fechaCierre;
                        }
                        if (unFecha.fechaConsulta.HasValue)
                        {
                            unLicitacion.fechaConsulta = unFecha.fechaConsulta;
                        }
                        if (unLicitacion.fechaPublicacion.HasValue)
                        {
                            unLicitacion.fechaPublicacion = unFecha.fechaPublicacion;
                        }
                        if (!string.IsNullOrEmpty(unFecha.horaApertura))
                        {
                            unLicitacion.horaApertura = unFecha.horaApertura;
                        }                        
                    }
                }
            }
            catch (Exception ex)
            {
                log.Error("Error buscar Una Licitacion", ex);
            }
            if(unLicitacion != null) 
            {
                if(unLicitacion.idMoneda == 1)
                {
                    unLicitacion.nombreMoneda = "ARG";
                }
                else
                {
                    if (unLicitacion.idMoneda == 2)
                    {
                        unLicitacion.nombreMoneda = "USD";
                    }
                }               
            }
            return unLicitacion;
        }
        public async Task<int> grabarDetalleLicitacion(unLicitacionViewModels unLicitacion)
        {
            int value = 0;
            unLicitacion = validarDetalleLicitacion(unLicitacion);
            try
            {
                using (db_meieEntities db = new db_meieEntities())
                {
                    var existe = db.LicProyecto.Where(x => x.idProyecto == unLicitacion.idObra).FirstOrDefault();

                    if (existe != null)
                    {
                        existe.domicilioApertura = unLicitacion.domicilioApertura;
                        existe.domicilioPresentacion = unLicitacion.domicilioPresentacion;
                        existe.limiteConsulta = unLicitacion.fechaConsulta;
                        existe.idProyecto = unLicitacion.idObra.Value;
                        existe.mailConsulta = unLicitacion.mailConsulta;
                        existe.urlPliego = unLicitacion.urlPliego;
                        existe.moneda = unLicitacion.idMoneda;
                        existe.lugarVisita = unLicitacion.lugarVisita;
                        existe.fechaCierre = unLicitacion.fechaCierre;
                        existe.fechaVisita = unLicitacion.fechaVisita;
                        existe.fechaModif = DateTime.Now;
                        existe.domicilioVirtual = unLicitacion.rutaVirtual;
                        await db.SaveChangesAsync();
                    }
                    else
                    {
                        LicProyecto licProyecto = new LicProyecto();
                        licProyecto.domicilioApertura = unLicitacion.domicilioApertura;
                        licProyecto.domicilioPresentacion = unLicitacion.domicilioPresentacion;
                        licProyecto.limiteConsulta = unLicitacion.fechaConsulta;
                        licProyecto.idProyecto = unLicitacion.idObra.Value;
                        licProyecto.mailConsulta = unLicitacion.mailConsulta;
                        licProyecto.urlPliego = unLicitacion.urlPliego;
                        licProyecto.moneda = unLicitacion.idMoneda;
                        licProyecto.lugarVisita = unLicitacion.lugarVisita;
                        licProyecto.fechaCierre = unLicitacion.fechaCierre;
                        licProyecto.fechaVisita = unLicitacion.fechaVisita;
                        licProyecto.fechaModif = DateTime.Now;
                        licProyecto.domicilioVirtual = unLicitacion.rutaVirtual;
                        db.LicProyecto.Add(licProyecto);
                        await db.SaveChangesAsync();
                    }
                }
            }
            catch (Exception ex)
            {
                log.Error("Error grabar Detalle Licitacion", ex);
            }
            return value;
        }
        private unLicitacionViewModels validarDetalleLicitacion(unLicitacionViewModels unLicitacion)
        {
            try
            {
                if (string.IsNullOrEmpty(unLicitacion.domicilioApertura))
                {
                    unLicitacion.domicilioApertura = string.Empty;
                }
                if (string.IsNullOrEmpty(unLicitacion.domicilioPresentacion))
                {
                    unLicitacion.domicilioPresentacion = string.Empty;
                }
                if (string.IsNullOrEmpty(unLicitacion.mailConsulta))
                {
                    unLicitacion.mailConsulta = string.Empty;
                }
            }
            catch (Exception ex)
            {
                log.Error("Error validar Detalle Licitacion", ex);
            }
            return unLicitacion;
        }
        public async Task<int> grabarDetalleProyecto(unLicitacionViewModels unLicitacion)
        {
            int value = 0;
            try
            {
                using (db_meieEntities db = new db_meieEntities())
                {
                    var existe = db.PryProyectoes.Where(x => x.Id == unLicitacion.idObra).FirstOrDefault();

                    if (existe != null)
                    {
                        //existe.GrlTypeState_Id = unLicitacion.idEtapa;
                        existe.Plazo = unLicitacion.unidadPlazo;
                        existe.Dias = unLicitacion.plazo;
                        existe.financiamiento = unLicitacion.textoFinanciamiento;
                        existe.numeroLicitacion = unLicitacion.nroLicitacion;
                        await db.SaveChangesAsync();
                    }
                    var existe2 = db.PryLicitacions.Where(x => x.IdPryProyecto == unLicitacion.idObra).FirstOrDefault();

                    if (existe2 != null)
                    {
                        existe2.Plazo = unLicitacion.plazo;
                        existe2.TipoPlazo = unLicitacion.unidadPlazo;
                        await db.SaveChangesAsync();
                    }
                }
            }
            catch (Exception ex)
            {
                log.Error("Error grabar Detalle Proyecto", ex);
            }
            return value;
        }
        public async Task<int> grabarLicitacion(unLicitacionViewModels unLicitacion)
        {
            log4net.Config.XmlConfigurator.Configure(new FileInfo(ruta));
            log.Info("Modifica una Licitacion ");
            int value = 0;
            unLicitacion = validarDetalleLicitacion(unLicitacion);
            try
            {
                using (db_meieEntities db = new db_meieEntities())
                {
                    var existe = db.PryLicitacions.Where(x => x.IdPryProyecto == unLicitacion.idObra).FirstOrDefault();

                    if (existe != null)
                    {
                        existe.MontoOficial = unLicitacion.montoObra;
                        existe.FechaApertura = unLicitacion.fechaApertura;
                        existe.Hora = unLicitacion.horaApertura;
                        existe.Plazo = unLicitacion.plazo;
                        existe.ValorPliego = unLicitacion.valorPliego;
                        existe.FechaPublicacionDesde = unLicitacion.fechaPublicacion;
                        existe.TipoDeContratacion_Id = unLicitacion.idTipoContratacion;
                        await db.SaveChangesAsync();
                    }
                    else
                    {
                        PryLicitacion pry = new PryLicitacion();
                        pry.MontoOficial = unLicitacion.montoObra;
                        pry.FechaApertura = unLicitacion.fechaApertura;
                        pry.Hora = unLicitacion.horaApertura;
                        pry.FechaPublicacionDesde = unLicitacion.fechaPublicacion;
                        pry.TipoDeContratacion_Id = unLicitacion.idTipoContratacion;
                        pry.Plazo = unLicitacion.plazo;
                        pry.ValorPliego = unLicitacion.valorPliego;
                        db.PryLicitacions.Add(pry);
                        await db.SaveChangesAsync();
                    }

                    var existe2 = db.PryProyectoPlanificacion.Where(x => x.PryProyecto_Id == unLicitacion.idObra).FirstOrDefault();
                    if(existe2 != null)
                    {
                        existe2.MontoOficial = unLicitacion.montoObra;
                        //existe2.MontoContratado = unLicitacion.montoObra;
                        await db.SaveChangesAsync();
                    }
                    //var existe3 = db.PryProyectoExpedientes
                    //    .Where(x => x.idPryProyecto == unLicitacion.idObra && x.nroExpediente == unLicitacion.nroExpediente && unLicitacion.nroExpediente == unLicitacion.nroExpediente && x.caratulaExpediente == unLicitacion.caratula)
                    //    .FirstOrDefault();
                    //if(existe3 == null)
                    //{
                    //    PryProyectoExpediente proyectoExpediente = new PryProyectoExpediente();
                    //    proyectoExpediente.idPryProyecto = unLicitacion.idObra.Value;
                    //    proyectoExpediente.idPryTipoLicitacion = 2;
                    //    proyectoExpediente.nroExpediente = unLicitacion.nroExpediente;
                    //    proyectoExpediente.caratulaExpediente = unLicitacion.caratula;
                    //    db.PryProyectoExpedientes.Add(proyectoExpediente);
                    //    await db.SaveChangesAsync();
                    //}
                }
            }
            catch (Exception ex)
            {
                log.Error("Error grabar Licitacion", ex);
            }
            return value;
        }
        public async Task<int> grabarFechas(fechaViewModels fechaLicitacion)
        {
            log4net.Config.XmlConfigurator.Configure(new FileInfo(ruta));
            log.Info("Modifica una fecha de la licitacion ");
            int value = 0;
            try
            {
                using (db_meieEntities db = new db_meieEntities())
                {
                    var existe = db.LicProyectoFecha
                        .Where(x => x.idProyecto == fechaLicitacion.idObra && x.fechaApertura == fechaLicitacion.fechaApertura && x.fechaCierre == fechaLicitacion.fechaCierre && x.fechaPublicacion == fechaLicitacion.fechaPublicacion && x.horaApertura == fechaLicitacion.horaApertura)
                        .FirstOrDefault();
                    if(existe == null)
                    {
                        LicProyectoFecha proyectoFecha = new LicProyectoFecha();
                        proyectoFecha.fechaApertura = fechaLicitacion.fechaApertura;
                        proyectoFecha.fechaCierre = fechaLicitacion.fechaCierre;
                        proyectoFecha.fechaPublicacion = fechaLicitacion.fechaPublicacion;
                        proyectoFecha.horaApertura = fechaLicitacion.horaApertura;
                        proyectoFecha.idProyecto = fechaLicitacion.idObra;
                        db.LicProyectoFecha.Add(proyectoFecha);
                        await db.SaveChangesAsync();
                    }
                }
            }
            catch (Exception ex)
            {
                log.Error("Error grabar Fechas Licitacion", ex);
            }
            return value;
        }
        public async Task<int> grabarEspecialidad(especialidadViewModels unEspecialidad)
        {
            log4net.Config.XmlConfigurator.Configure(new FileInfo(ruta));
            log.Info("Grabar Especialidad");
            int value = 0;
            try
            {
                using (db_meieEntities db = new db_meieEntities())
                {
                    List<int> lista = new List<int>();
                    foreach(var item in unEspecialidad.listaEspecialidad)
                    {
                        if (!string.IsNullOrEmpty(item))
                        {
                            var id = Convert.ToInt32(item);
                            lista.Add(id);
                        }
                    }
                    
                    if(lista.Count > 0)
                    {
                        var listaEspecialidad = db.PryProyectoEspecialidads.Where(x => x.idPryProyecto == unEspecialidad.idObra).ToList();
                        foreach (var item in listaEspecialidad)
                        {
                            db.PryProyectoEspecialidads.Remove(item);
                            db.SaveChanges();
                        }
                        foreach(var item in lista)
                        {
                            PryProyectoEspecialidad proyectoEspecialidad = new PryProyectoEspecialidad();
                            proyectoEspecialidad.idPryProyecto = unEspecialidad.idObra.Value;
                            proyectoEspecialidad.idPryEspecialidad = item;
                            proyectoEspecialidad.porcentaje100to400 = unEspecialidad.porcentaje100;
                            proyectoEspecialidad.porcentaje500 = unEspecialidad.porcentaje400;
                            db.PryProyectoEspecialidads.Add(proyectoEspecialidad);
                            await db.SaveChangesAsync();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                log.Error("Error grabar Especialidad", ex);
            }
            return value;
        }
        public List<fechaViewModels> buscarListaFechas(int? idObra)
        {
            log4net.Config.XmlConfigurator.Configure(new FileInfo(ruta));
            log.Info("buscar Lista de Fechas");
            List<fechaViewModels> lista = new List<fechaViewModels>();
            try
            {
                using (db_meieEntities db = new db_meieEntities())
                {
                    var query = from lic in db.LicProyectoFecha
                                where lic.idProyecto == idObra
                                select new fechaViewModels
                                {
                                    idObra = lic.idProyecto,
                                    fechaApertura = lic.fechaApertura,
                                    fechaPublicacion = lic.fechaPublicacion,
                                    horaApertura = lic.horaApertura,
                                    fechaCierre = lic.fechaCierre,
                                    idDetalle = lic.idLicProyectoFecha
                                };
                    lista = query.OrderByDescending(x => x.idDetalle).ToList();
                }
            }
            catch (Exception ex)
            {
                log.Error("Error buscar Lista de Fechas", ex);
            }
            return lista;
        }
        public List<licitacionGrillaViewModels> listarLicitaciones
        (ref int recordsTotal, string sortColumn, string sortColumnDir, string searchValue,
            string nroExpediente, string nombreObra,string qObra, int? idEstado, DateTime? fechaPub, int? idOrganismo,
            int pageSize = 0, int skip = 0)
        {
            var idObras = "";
            log4net.Config.XmlConfigurator.Configure(new FileInfo(ruta));
            log.Info("Grilla Licitaciones Administrador");
            var listaFiltrada = new List<licitacionGrillaViewModels>();
            List<licitacionGrillaViewModels> lista = new List<licitacionGrillaViewModels>();
            try
            {
                using (db_meieEntities db = new db_meieEntities())
                {

                    var query = from lic in db.PryProyectoes
                                join edo in db.PryEstadoEtapas
                                on lic.GrlTypeState_Id equals edo.Id
                                join pyl in db.PryLicitacions
                                on lic.Id equals pyl.IdPryProyecto
                                    into pylo
                                from pyj in pylo.DefaultIfEmpty()
                                join org in db.PryOrganismosEjecutores
                                on lic.PryOrganismoEjecutor_Id equals org.Id
                                    into orgo
                                from orgj in orgo.DefaultIfEmpty()
                                where edo.PryStage_Id == 49 && lic.Eliminado == false
                                select new licitacionGrillaViewModels
                                {
                                    nombreEtapa = edo.Name,
                                    idEtapa = edo.Id,
                                    nombreObra = lic.Nombre,
                                    idObra = lic.Id,
                                    fechaApertura = pyj.FechaApertura,
                                    fechaPublicacion = pyj.FechaPublicacionDesde,
                                    montoObra = pyj.MontoOficial,
                                    nroExpediente = lic.Expediente,
                                    nombreOrganismo = orgj.NombreOrganismo,
                                    idOrganismo = lic.PryOrganismoEjecutor_Id,

                                };

                    if (!string.IsNullOrEmpty(qObra))
                    {
                        if (qObra != "*")
                        {
                            var array = qObra.Split(',');
                            List<int?> listaObra = new List<int?>();
                            foreach (var item in array)
                            {
                                listaObra.Add(Convert.ToInt32(item));
                            }
                            query = query.Where(x => listaObra.Contains(x.idObra));
                        }
                    }
                    if (idOrganismo != 0)
                    {
                        query = query.Where(x => x.idOrganismo == idOrganismo);
                    }
                    if (idEstado != 0)
                    {
                        query = query.Where(x => x.idEtapa == idEstado);
                    }
                    if (!string.IsNullOrEmpty(nombreObra))
                    {
                        query = query.Where(x => x.nombreObra.Contains(nombreObra));
                    }
                    if (!string.IsNullOrEmpty(nroExpediente))
                    {
                        query = query.Where(x => x.nroExpediente.Contains(nroExpediente));
                    }
                    if (fechaPub.HasValue)
                    {
                        query = query.Where(x => x.fechaPublicacion == fechaPub);
                    }
                    if (!(string.IsNullOrEmpty(sortColumn) && string.IsNullOrEmpty(sortColumnDir)))
                    {
                        if ((sortColumn == "nombreObra") && (sortColumnDir == "desc"))
                        {
                            query = query.OrderByDescending(x => x.nombreObra);
                        }
                        if ((sortColumn == "nombreObra") && (sortColumnDir == "asc"))
                        {
                            query = query.OrderBy(x => x.nombreObra);
                        }
                        if ((sortColumn == "nroExpedienteString") && (sortColumnDir == "desc"))
                        {
                            query = query.OrderByDescending(x => x.nroExpediente);
                        }
                        if ((sortColumn == "nroExpedienteString") && (sortColumnDir == "asc"))
                        {
                            query = query.OrderBy(x => x.nroExpediente);
                        }
                        if ((sortColumn == "fechaPublicacionString") && (sortColumnDir == "desc"))
                        {
                            query = query.OrderByDescending(x => x.fechaPublicacion);
                        }
                        if ((sortColumn == "fechaPublicacionString") && (sortColumnDir == "asc"))
                        {
                            query = query.OrderBy(x => x.fechaPublicacion);
                        }
                        if ((sortColumn == "nombreOrganismoString") && (sortColumnDir == "asc"))
                        {
                            query = query.OrderBy(x => x.nombreOrganismo);
                        }
                        if ((sortColumn == "nombreOrganismoString") && (sortColumnDir == "desc"))
                        {
                            query = query.OrderByDescending(x => x.nombreOrganismo);
                        }
                        if ((sortColumn == "montoObraString") && (sortColumnDir == "asc"))
                        {
                            query = query.OrderBy(x => x.montoObra);
                        }
                        if ((sortColumn == "montoObraString") && (sortColumnDir == "desc"))
                        {
                            query = query.OrderByDescending(x => x.montoObra);
                        }
                        if ((sortColumn == "nombreEtapa") && (sortColumnDir == "asc"))
                        {
                            query = query.OrderBy(x => x.nombreEtapa);
                        }
                        if ((sortColumn == "nombreEtapa") && (sortColumnDir == "desc"))
                        {
                            query = query.OrderByDescending(x => x.nombreEtapa);
                        }
                        if ((sortColumn == "accion") && (sortColumnDir == "asc"))
                        {
                            query = query.OrderBy(x => x.nombreObra);
                        }
                        if ((sortColumn == "accion") && (sortColumnDir == "desc"))
                        {
                            query = query.OrderByDescending(x => x.nombreObra);
                        }

                    }
                    else
                    {
                        query = query = query.OrderBy(x => x.nombreObra);
                    }

                    query = query.Take(500);
                    recordsTotal = query.Count();

                    var lst = query.Skip(skip).Take(pageSize);
                    lista = lst.ToList();
                   
                    
                    listaFiltrada = query.ToList();
                   
                    return listaFiltrada;
                }
                
            }
           
            catch (Exception ex)
            {
                log.Error("Error en Grilla Licitaciones Administrador", ex);
            }

            return listaFiltrada;
            
        }
        public ReporteExcelVM generarExcelGrilla(List<licitacionGrillaViewModels> obras, bool? nombre, bool? expediente, bool? publicacion, bool? organismo, bool? monto, bool? etapa)
        {
            ReporteExcelVM excel = new ReporteExcelVM();
            excel.filas = new List<DetalleExcelVM>();
           
            using (db_meieEntities db = new db_meieEntities())
            {
                foreach (var item in obras)
                {
                    excel.encabezado = new List<string>();                    
                    if (obras != null)
                    {                        
                        DetalleExcelVM detalle = new DetalleExcelVM();
                        excel.encabezado.Add("Grilla Obras");   
                        detalle.nombre = item.nombreObra;
                        detalle.expediente = item.nroExpedienteString;
                        detalle.publicacion = item.fechaPublicacionString;   
                        detalle.organismo = item.nombreOrganismo;
                        detalle.monto = item.montoObraString;                
                        detalle.etapa = item.nombreEtapa; 
                        excel.filas.Add(detalle);
                    }
                }
            }
            ExcelUtility excelUtility = new ExcelUtility();
            excel = excelUtility.GenerarExcelGrilla(excel, nombre, expediente, publicacion, organismo, monto, etapa);
            return excel;
        }
        public IQueryable<licitacionGrillaViewModels> ProyectFilterByOffice(int currentIdOffice, int currentIdUser, IQueryable<licitacionGrillaViewModels> listaDatos)
        {
            
            var oficinaUsuario = new SegUserVewModels();
            using (var userData = new db_meieEntities())
            {
                oficinaUsuario = (from u in userData.SegUsers
                                  where u.Id == currentIdUser
                                  select new SegUserVewModels
                                  {
                                      idOffice = (int)u.SegOffice_Id,
                                      segvisualiza_id = (int)u.SegVisualiza_Id

                                  }).FirstOrDefault();
            }
            var oficina = new SegUserVewModels();
            using (var userData = new db_meieEntities())
            {
                oficina = (from o in userData.SegOffices
                           where o.Id == currentIdOffice
                           select new SegUserVewModels
                           {
                               idVM = o.Id,
                               idOfficePad = (int)o.idOfficePadre
                           }).FirstOrDefault();
            }
            var organo = new SegUserVewModels();
            using (var userData = new db_meieEntities())
            {
                organo = (from o in userData.UfiOrgano
                          where o.idUfiOrgano == oficina.idOfficePad
                          select new SegUserVewModels
                          {
                              idUfiOrg = o.idUfiOrgano
                          }).FirstOrDefault();
            }

            //
            var hijos = new List<SegUserVewModels>();
            var cargarHijos = new List<SegUserVewModels>();
            using (var userData = new db_meieEntities())
            {
                hijos = (from o in userData.SegOffices
                             //where o.Eliminado == 0
                         select new SegUserVewModels
                         {
                             idVM = o.Id,

                             idOfficePad = o.idOfficePadre ?? 0
                         }).ToList();
            }
            int?[] num1 = new int?[100000];
            int i = 0;
            foreach (var item in hijos)
            {
                if (item.idOfficePad == oficina.idOfficePad)
                {
                    num1[i++] = item.idVM;
                    cargarHijos.Add(item);
                }
            }

            //
            var cargar = new List<SegUserVewModels>();
            using (var userData = new db_meieEntities())
            {
                cargar = (from o in userData.SegOffices
                          where o.idOfficePadre == oficina.idOfficePad
                          select new SegUserVewModels
                          {
                              idVM = o.Id,

                              idOfficePad = o.idOfficePadre ?? 0
                          }).ToList();
            }
           
            if (oficinaUsuario.segvisualiza_id == 1)
            {
                listaDatos = listaDatos.Where(x => x.Eliminado == false);

            }
            else
            if (oficinaUsuario.segvisualiza_id == 2)
            {
                listaDatos = listaDatos.Where(x => x.Eliminado == false
                         && (x.idOffice == currentIdOffice || x.idOrgano == oficina.idOfficePad || x.idOrganoReceptor == oficina.idOfficePad));
            }
            else if (oficinaUsuario.segvisualiza_id == 3)
            {
                listaDatos = listaDatos.Where(x => x.Eliminado == false &&
                 (x.idOrgano == oficina.idOfficePad || x.idOrganoReceptor == oficina.idOfficePad
                 || (x.idOffice == currentIdOffice && x.fechaPublicacion != null)));

            }
            //var s = listaDatos.ToList();
                return listaDatos;
        }
        public int cambiarEtapa(int? idObra, int? idEtapa)
        {
            var value = 0;
            try
            {
                if (validarCambiarEtapa(idObra, idEtapa))
                {
                    using (db_meieEntities db = new db_meieEntities())
                    {
                        var existe = db.PryProyectoes.Where(x => x.Id == idObra).FirstOrDefault();

                        if (existe != null)
                        {
                            existe.GrlTypeState_Id = idEtapa;
                            db.SaveChanges();
                            value = 1;
                        }
                    }
                }
            }
            catch (Exception ex)
            {

            }
            return value;
        }
        public bool validarCambiarEtapa(int? idObra, int? idEtapa)
        {
            var value = false;
            try
            {
                using (db_meieEntities db = new db_meieEntities())
                {
                    var existe = db.LicArchivoCategoria.Where(x => x.idEtapa == idEtapa).FirstOrDefault();

                    if (existe != null)
                    {
                        var existeArchivo = db.LicArchivoObra.Where(x => x.idCategoria == existe.idCategoria && x.idObra == idObra).FirstOrDefault();
                        if (existeArchivo != null)
                        {
                            return true;
                        }
                    }
                    else
                    {
                        value = true;
                    }
                }
            }
            catch (Exception ex)
            {

            }
            return value;
        }
        public string buscarTipoContratacion(int? idTipoContratacion)
        {
            string value = string.Empty;
            try
            {
                using (db_meieEntities db= new db_meieEntities())
                {
                    var tmp = db.GrlTypes.Where(x => x.Id == idTipoContratacion).FirstOrDefault();
                    if(tmp != null)
                    {
                        value = tmp.Name;
                    }
                }
            }
            catch(Exception ex)
            {

            }
            return value;
        }
        public async Task<int> cambioEtapa(cambioEtapaViewModels unLicitacion)
        {
            int value = 1;
            try
            {
                using (db_meieEntities db = new db_meieEntities())
                {
                    var existe = db.PryProyectoes.Where(x => x.Id == unLicitacion.idObra).FirstOrDefault();

                    if (existe != null)
                    {
                        if(unLicitacion.idEtapa == 7)
                        {
                            var unFecha = db.PryLicitacions.Where(x => x.IdPryProyecto == unLicitacion.idObra).FirstOrDefault();
                            if(unFecha != null)
                            {
                                var fechaControl = DateTime.Now.Date;
                                var horaControl = DateTime.Now.TimeOfDay;
                                if (unFecha.FechaApertura <= fechaControl)
                                {
                                    if (!string.IsNullOrEmpty(unFecha.Hora))
                                    {
                                        var horaApertura = TimeSpan.Parse(unFecha.Hora); 
                                        if (horaApertura <= horaControl)
                                        {
                                            existe.GrlTypeState_Id = unLicitacion.idEtapa;
                                            await db.SaveChangesAsync();
                                        }
                                    }
                                    else
                                    {
                                        return 0;
                                    }
                                }
                                else
                                {
                                    return 0;
                                }
                            }
                        }
                        else
                        {
                            existe.GrlTypeState_Id = unLicitacion.idEtapa;
                            await db.SaveChangesAsync();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                log.Error("Error grabar cambioEtapa", ex);
            }
            return value;
        }
        public async Task<int> cambioEtapaAnterior(cambioEtapaViewModels unLicitacion)
        {
            int value = 1;
            try
            {
                using (db_meieEntities db = new db_meieEntities())
                {
                    var existe = db.PryProyectoes.Where(x => x.Id == unLicitacion.idObra).FirstOrDefault();
                    int? idEtapaAnterior = 5;
                    if (existe != null)
                    {
                        if(unLicitacion.idEtapa == 6)
                        {
                            idEtapaAnterior = 5;
                        }
                        if (unLicitacion.idEtapa == 7)
                        {
                            var unFecha = db.PryLicitacions.Where(x => x.IdPryProyecto == unLicitacion.idObra).FirstOrDefault();
                            if (unFecha != null)
                            {
                                var fechaControl = DateTime.Now.Date;
                                var horaControl = DateTime.Now.TimeOfDay;
                                if (unFecha.FechaApertura <= fechaControl)
                                {
                                    if (!string.IsNullOrEmpty(unFecha.Hora))
                                    {
                                        var horaApertura = TimeSpan.Parse(unFecha.Hora);
                                        if (horaApertura <= horaControl)
                                        {

                                            idEtapaAnterior = 6;
                                        }
                                    }
                                }
                            }
                        }
                        if (unLicitacion.idEtapa == 8)
                        {
                            idEtapaAnterior = 14;
                        }
                        if (unLicitacion.idEtapa == 14)
                        {
                            idEtapaAnterior = 15;
                        }
                        if (unLicitacion.idEtapa == 15)
                        {
                            idEtapaAnterior = 7;
                        }
                        if (unLicitacion.idEtapa == 16)
                        {
                            idEtapaAnterior = 5;
                        }
                        if (unLicitacion.idEtapa == 17)
                        {
                            idEtapaAnterior = 5;
                        }
                        existe.GrlTypeState_Id = idEtapaAnterior;
                        await db.SaveChangesAsync();
                    }
                }
            }
            catch (Exception ex)
            {
                log.Error("Error grabar cambioEtapa", ex);
            }
            return value;
        }
        public async Task enviarNotificacionAdjunto(int? idObra, int? idTipoMail, int? idCategoria)
        {
            log4net.Config.XmlConfigurator.Configure(new FileInfo(ruta));
            log.Info("Envia mail Notificacion con Adjunto");
            var nombreObra = "";
            int? idOrganismo = 0;
            var mailEmpresa = "";
            var mailOrganismo = "";
            var archivoAdjunto = "";
            using (db_meieEntities db = new db_meieEntities())
            {
                var unObra = db.PryProyectoes.Where(x => x.Id == idObra).FirstOrDefault();
                if(unObra != null)
                {
                    nombreObra = unObra.Nombre;
                    idOrganismo = unObra.PryOrganismoEjecutor_Id;

                    var listaEmpresa = db.LicOfertaEmpresa.Where(x => x.idObra == idObra).ToList();
                    var unOrganismo = db.PryOrganismosEjecutores.Where(x => x.Id == idOrganismo).FirstOrDefault();
                    if(unOrganismo != null)
                    {
                        if (!string.IsNullOrEmpty(unOrganismo.mailOrganisimo))
                        {
                            mailOrganismo = unOrganismo.mailOrganisimo;
                            log.Info("Envia mail Notificacion con Adjunto a Organismo: " + mailOrganismo);
                        }
                    }
                    foreach(var item in listaEmpresa)
                    {
                        using (DB_RACOPEntities db2 = new DB_RACOPEntities())
                        {
                            var unEmpresa = db2.rc_Empresa.Where(x => x.idEmpresa == item.idEmpresa).FirstOrDefault();
                            if(unEmpresa != null)
                            {
                                if (!string.IsNullOrEmpty(unEmpresa.mailEspecial))
                                {
                                    mailEmpresa = unEmpresa.mailEspecial + ";";
                                    log.Info("Envia mail Notificacion con Adjunto a: " + mailEmpresa);
                                }
                            }
                        }
                    }
                }
                var unAdjunto = db.LicArchivoObra.Where(x => x.idCategoria == idCategoria && x.idObra == idObra).FirstOrDefault();
                if(unAdjunto != null)
                {
                    archivoAdjunto = unAdjunto.rutaArchivo;
                }
            }
            
            ServicioEmail servicioEmail = new ServicioEmail();
            var emailViewModels = servicioEmail.buscarMail(5);
            emailViewModels.copiaMail = mailOrganismo;
            if (!string.IsNullOrEmpty(mailEmpresa))
            {
                emailViewModels.destino = mailEmpresa;
            }
            else
            {
                emailViewModels.destino = mailOrganismo;
            }

            emailViewModels.cuerpo = emailViewModels.cuerpo.Replace("#NombreObra", nombreObra);
            // Enviar Mail
            await servicioEmail.enviarEmailAdjunto(emailViewModels, archivoAdjunto);
        }
        public async Task<int> grabarVencimientoObs(unLicitacionViewModels unLicitacion)
        {
            log4net.Config.XmlConfigurator.Configure(new FileInfo(ruta));
            log.Info("Grabar Especialidad");
            int value = 0;
            try
            {
                using (db_meieEntities db = new db_meieEntities())
                {
                    var existe = db.LicProyecto.Where(x => x.idProyecto == unLicitacion.idObra).FirstOrDefault();
                    if (existe != null)
                    {
                        existe.fechaVtoObs = unLicitacion.fechaVtoObs;
                        existe.horaVtoObs = unLicitacion.horaVtoObs;
                    }
                   
                    await db.SaveChangesAsync();
                }
            }
            catch (Exception ex)
            {
                log.Error("Error grabar vencimiento observaciones", ex);
            }
            return value;
        }
        /// <summary>
        /// busca si la empresa esta habilitada para observaciones y mejora si las fechas estan ok
        /// </summary>
        /// <param name="idObra"></param>
        /// <param name="idEmpresa"></param>
        /// <returns></returns>
        public EmpresaHabilitadaViewModels buscarEmpresaHabilitada(int? idObra, int? idEmpresa)
        {
            log4net.Config.XmlConfigurator.Configure(new FileInfo(ruta));
            log.Info("buscar Lista de Fechas");
            var cuit = "";
            using (DB_RACOPEntities db2 = new DB_RACOPEntities())
            {
                var unaEmpresa = db2.rc_Empresa.Where(x => x.idEmpresa == idEmpresa).FirstOrDefault();
                cuit = unaEmpresa.cuit;
            }
            
                EmpresaHabilitadaViewModels unaEmpresaHabilitada = new EmpresaHabilitadaViewModels();
            try
            {
                using (db_meieEntities db = new db_meieEntities())
                {
                    var query = from hab in db.LicEmpresaHabilitada                 //busca en tabla empresa habilitada y carga observaciones y mejora
                                where hab.idObra == idObra && (hab.idEmpresa == idEmpresa || hab.cuit == cuit)
                                select new EmpresaHabilitadaViewModels
                                {
                                    idObra = hab.idObra,
                                    cuit = hab.cuit,
                                    idEmpresa = hab.idEmpresa,
                                    razonSocial = hab.razonSocial,
                                    observaciones = hab.observaciones,
                                    mejoraOferta = hab.mejoraoferta,
                                };
                    unaEmpresaHabilitada = query.FirstOrDefault();
                    if(unaEmpresaHabilitada != null)                         //si esta habilidada verifica si tiene archivos observados y mejora
                    {
                        bool tieneEstado4 = db.LicArchivoEmpresa.Any(x => x.idEmpresa == unaEmpresaHabilitada.idEmpresa && x.idObra == idObra && x.idEstadoArchivo == 4);

                        if (tieneEstado4)
                        {
                            unaEmpresaHabilitada.observaciones = true;
                        }
                    }
                    else
                    {
                        var query2 = from hab in db.LicArchivoEmpresa           //si la empresa no está en la tabla verifica si tiene archivos observados
                                     where hab.idObra == idObra && hab.idEmpresa == idEmpresa
                                    select new EmpresaHabilitadaViewModels
                                    {
                                        idObra = hab.idObra,
                                        cuit = cuit,
                                        idEmpresa = hab.idEmpresa,
                                        razonSocial = "",
                                        observaciones = false,
                                        mejoraOferta = false,
                                        idEstadoArchivo = hab.idEstadoArchivo,
                                    };
                        unaEmpresaHabilitada = query2.FirstOrDefault();
                        var tmp = query2.ToList();
                        if(unaEmpresaHabilitada != null)
                        {
                            foreach (var item in tmp)
                            {
                                if(item.idEstadoArchivo == 4)
                                {
                                    unaEmpresaHabilitada.observaciones = true;
                                }

                            }
                        }
                    }
                    
                }
            }
            catch (Exception ex)
            {
                log.Error("Error buscar empresa habilitada", ex);
            }
            return unaEmpresaHabilitada;
        }
    }
}

