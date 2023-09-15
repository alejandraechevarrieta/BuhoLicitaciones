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
    public class ServicioOferta
    {
        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        private static string ruta = System.Web.HttpRuntime.AppDomainAppPath + "\\bin\\log4net.config";

        public List<EmpresaOfertaViewModels> listarEmpresasPorObra(int? idObra)
        {
            log4net.Config.XmlConfigurator.Configure(new FileInfo(ruta));
            log.Info("Listar Empresas que han ofertado en la obra");

            List<EmpresaOfertaViewModels> lista = new List<EmpresaOfertaViewModels>();
            try
            {
                using (db_meieEntities db = new db_meieEntities())
                {
                    var query = (from tb1 in db.LicOfertaEmpresa
                                 where tb1.idObra == idObra
                                 join tb2 in db.LicEmpresaHabilitada
                                 on tb1.idEmpresa equals tb2.idEmpresa into empresaHabilitadaGroup
                                 from tb2 in empresaHabilitadaGroup.DefaultIfEmpty()
                                 select new EmpresaOfertaViewModels
                                 {
                                     idObra = tb1.idObra,
                                     idEmpresa = tb1.idEmpresa,
                                     fechaOferta = tb1.fecha,
                                     mejoraOferta = tb2 != null ? tb2.mejoraoferta : false                                     
                                 });
                    lista = query.ToList();
                    
                }
                using (DB_RACOPEntities db1 = new DB_RACOPEntities())
                {
                    foreach (var item in lista)
                    {
                        var unEmpresa = db1.rc_Empresa.Where(x => x.idEmpresa == item.idEmpresa).FirstOrDefault();
                        if (unEmpresa != null)
                        {
                            item.nombreEmpresa = unEmpresa.razonSocial;
                            item.cuit = unEmpresa.cuit;
                        }
                    }
                }
                using (db_meieEntities db = new db_meieEntities())
                {                    

                    var query2 = (from hab in db.LicEmpresaHabilitada
                                  where hab.idObra == idObra && hab.idEmpresa == null
                                  select new EmpresaOfertaViewModels
                                  {
                                      idObra = hab.idObra,
                                      idEmpresa = null,
                                      fechaOferta = null,
                                      mejoraOferta = hab.mejoraoferta,
                                      nombreEmpresa = hab.razonSocial,
                                      cuit = hab.cuit,
                                  });

                    var query2List = query2.ToList();


                    foreach (var item in query2List)
                    {
                        var empresaEnLista = lista.FirstOrDefault(e => e.idEmpresa == item.idEmpresa && e.idObra == idObra);
                        if (empresaEnLista != null)
                        {
                            empresaEnLista.mejoraOferta = item.mejoraOferta;
                        }
                    }
                    
                    lista = lista.Concat(query2List).ToList();

                }
            }
            catch (Exception ex)
            {
                log.Error("Error en listarEmpresasPorObra", ex);
            }
            return lista;
        }       
    }
}
