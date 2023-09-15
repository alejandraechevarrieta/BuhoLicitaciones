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
    public class ServicioDepartamento
    {
        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        private static string ruta = System.Web.HttpRuntime.AppDomainAppPath + "\\bin\\log4net.config";
        public List<selectViewModels> listar()
        {
            log4net.Config.XmlConfigurator.Configure(new FileInfo(ruta));
            log.Info("Listar Departamento");

            List<selectViewModels> lista = new List<selectViewModels>();
            try
            {
                using (db_meieEntities db = new db_meieEntities())
                {
                    var tmp = from org in db.GrlDepartaments
                              select new selectViewModels
                              {
                                  nombre = org.Name,
                                  id = org.Id
                              };
                    lista = tmp.ToList();
                }
            }
            catch (Exception ex)
            {
                log.Error("Error en Listar Departamento", ex);
            }
            return lista;
        }
        public string listarPorProyectoString(int? idObra)
        {
            log4net.Config.XmlConfigurator.Configure(new FileInfo(ruta));
            log.Info("Listar Departamento por proyecto para select2");

            string value = "";
            try
            {
                using (db_meieEntities db = new db_meieEntities())
                {
                    var tmp = from pry in db.PryProyectoMunicipios
                              join esp in db.GrlDepartaments
                              on pry.PryMunicipio_Id equals esp.Id
                              where pry.PryProyecto_Id == idObra
                              select new DepartamentoViewModels
                              {
                                  nombre = esp.Name,
                                  idDepartamento = esp.Id,
                                  idObra = pry.Id,
                              };
                    var lista = tmp.ToList();
                    foreach (var item in lista)
                    {
                        value = value + item.idDepartamento + ",";
                    }
                }
            }
            catch (Exception ex)
            {
                log.Error("Error en Listar Departamento por proyecto select2", ex);
            }
            return value;
        }
    }
}
