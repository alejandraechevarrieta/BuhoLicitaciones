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
    public class ServicioPlanificacion
    {
        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        private static string ruta = System.Web.HttpRuntime.AppDomainAppPath + "\\bin\\log4net.config";
        public PlanificacionViewModels buscarPorProyecto(int? idObra)
        {
            log4net.Config.XmlConfigurator.Configure(new FileInfo(ruta));
            log.Info("Planificacion por proyecto");

            PlanificacionViewModels value = new PlanificacionViewModels();
            try
            {
                using (db_meieEntities db = new db_meieEntities())
                {
                    var tmp = from pry in db.PryProyectoPlanificacion
                              where pry.PryProyecto_Id == idObra
                              select new PlanificacionViewModels
                              {
                                  idObra = pry.Id,
                                  presupuesto = pry.MontoOficial
                              };
                    var lista = tmp.FirstOrDefault();
                }
            }
            catch (Exception ex)
            {
                log.Error("Error en buscar Planificacion por proyecto", ex);
            }
            return value;
        }
    }
}
