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
    public class ServicioFinanciamiento
    {
        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        private static string ruta = System.Web.HttpRuntime.AppDomainAppPath + "\\bin\\log4net.config";
        public List<selectViewModels> listar()
        {
            log4net.Config.XmlConfigurator.Configure(new FileInfo(ruta));
            log.Info("Listar Financiamiento");

            List<selectViewModels> lista = new List<selectViewModels>();
            try
            {
                using (db_meieEntities db = new db_meieEntities())
                {
                    var tmp = from org in db.PryFuentesFinanciamientoes
                              select new selectViewModels
                              {
                                  nombre = org.NombreFuente,
                                  id = org.Id
                              };
                    lista = tmp.ToList();
                }
            }
            catch (Exception ex)
            {
                log.Error("Error en Listar Financiamiento", ex);
            }
            return lista;
        }
    }
}
