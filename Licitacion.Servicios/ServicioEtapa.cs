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
    public class ServicioEtapa
    {
        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        private static string ruta = System.Web.HttpRuntime.AppDomainAppPath + "\\bin\\log4net.config";
        public List<selectViewModels> listar()
        {
            log4net.Config.XmlConfigurator.Configure(new FileInfo(ruta));
            log.Info("Listar Etapas");

            List<selectViewModels> lista = new List<selectViewModels>();
            try
            {
                using (db_meieEntities db = new db_meieEntities())
                {
                    var tmp = from tbl in db.PryEstadoEtapas
                              where tbl.PryStage_Id == 49
                              select new selectViewModels
                              {
                                  nombre = tbl.Name,
                                  id = tbl.Id
                              };
                    lista = tmp.ToList();
                }
            }
            catch (Exception ex)
            {
                log.Error("Error en Listar Etapas", ex);
            }
            return lista;
        }
        public List<selectViewModels> listarEtapaEmpresa()
        {
            log4net.Config.XmlConfigurator.Configure(new FileInfo(ruta));
            log.Info("Listar Etapas Empresas");

            List<selectViewModels> lista = new List<selectViewModels>();
            try
            {
                using (db_meieEntities db = new db_meieEntities())
                {
                    var tmp = from tbl in db.PryEstadoEtapas
                              where tbl.PryStage_Id == 49 && tbl.Id < 9 && tbl.Id > 5
                              select new selectViewModels
                              {
                                  nombre = tbl.Name,
                                  id = tbl.Id
                              };
                    lista = tmp.ToList();
                }
            }
            catch (Exception ex)
            {
                log.Error("Error en Listar Etapas", ex);
            }
            return lista;
        }
    }
}
