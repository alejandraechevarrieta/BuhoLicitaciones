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
    public class ServicioTipoContratacion
    {
        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        private static string ruta = System.Web.HttpRuntime.AppDomainAppPath + "\\bin\\log4net.config";
        public List<selectViewModels> listar()
        {
            log4net.Config.XmlConfigurator.Configure(new FileInfo(ruta));
            log.Info("Listar Tipo Contratacion");

            List<selectViewModels> lista = new List<selectViewModels>();
            try
            {
                using (db_meieEntities db = new db_meieEntities())
                {
                    var tmp = from tb in db.GrlTypes
                              where tb.TypeConfig_Id == 23
                              select new selectViewModels
                              {
                                  nombre = tb.Name,
                                  id = tb.Id
                              };
                    lista = tmp.ToList();
                }
            }
            catch (Exception ex)
            {
                log.Error("Error en Listar Tipo Contratacion", ex);
            }
            return lista;
        }
    }
}
