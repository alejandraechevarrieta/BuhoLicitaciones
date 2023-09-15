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
    public class ServicioEspecialidad
    {
        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        private static string ruta = System.Web.HttpRuntime.AppDomainAppPath + "\\bin\\log4net.config";
        public List<selectViewModels> listar()
        {
            log4net.Config.XmlConfigurator.Configure(new FileInfo(ruta));
            log.Info("Listar Especialidad");

            List<selectViewModels> lista = new List<selectViewModels>();
            try
            {
                using (db_meieEntities db = new db_meieEntities())
                {
                    var tmp = from org in db.PryEspecialidads
                              select new selectViewModels
                              {
                                  nombre = org.especialidad,
                                  id = org.id
                              };
                    lista = tmp.ToList();
                }
            }
            catch (Exception ex)
            {
                log.Error("Error en Listar Especialidad", ex);
            }
            return lista;
        }
        public List<selectViewModels> ListarSubEspecialidad()
        {
            log4net.Config.XmlConfigurator.Configure(new FileInfo(ruta));
            log.Info("Listar SubEspecialidad");

            List<selectViewModels> lista = new List<selectViewModels>();
            try
            {
                using (db_meieEntities db = new db_meieEntities())
                {
                    var tmp = from org in db.PrySubEspecialidad
                              select new selectViewModels
                              {
                                  nombre = org.NombreSubEspecialidad,
                                  id = org.idPrySubEspecialidad
                              };
                    lista = tmp.ToList();
                }
            }
            catch (Exception ex)
            {
                log.Error("Error en Listar SubEspecialidad", ex);
            }
            return lista;
        }
        public List<EspecialidadViewModels> listarPorProyecto(int? idObra)
        {
            log4net.Config.XmlConfigurator.Configure(new FileInfo(ruta));
            log.Info("Listar Especialidad por proyecto");

            List<EspecialidadViewModels> lista = new List<EspecialidadViewModels>();
            try
            {
                using (db_meieEntities db = new db_meieEntities())
                {
                    var tmp = from pry in db.PryProyectoEspecialidads
                              join esp in db.PryEspecialidads 
                              on pry.idPryEspecialidad equals esp.id
                              where pry.idPryProyecto == idObra
                              select new EspecialidadViewModels
                              {
                                  nombre = esp.especialidad,
                                  idEspecialidad = esp.id,
                                  idObra = pry.id,
                                  porcentaje100 = pry.porcentaje100to400,
                                  porcentaje500 = pry.porcentaje500
                              };
                    lista = tmp.ToList();
                }
            }
            catch (Exception ex)
            {
                log.Error("Error en Listar Especialidad por proyecto", ex);
            }
            return lista;
        }
        public string listarPorProyectoString(int? idObra)
        {
            log4net.Config.XmlConfigurator.Configure(new FileInfo(ruta));
            log.Info("Listar Especialidad por proyecto para select2");

            string value = "";
            try
            {
                using (db_meieEntities db = new db_meieEntities())
                {
                    var tmp = from pry in db.PryProyectoEspecialidads
                              join esp in db.PryEspecialidads
                              on pry.idPryEspecialidad equals esp.id
                              where pry.idPryProyecto == idObra
                              select new EspecialidadViewModels
                              {
                                  nombre = esp.especialidad,
                                  idEspecialidad = esp.id,
                                  idObra = pry.id,
                                  porcentaje100 = pry.porcentaje100to400,
                                  porcentaje500 = pry.porcentaje500
                              };
                    var lista = tmp.ToList();
                    foreach(var item in lista)
                    {
                        //value = value + "'" +item.idEspecialidad +"'" + ",";
                        value = value + item.idEspecialidad + ",";
                    }
                    //value = value.Remove(value.Length-1, 1);
                    //value = "[" + value + "]";
                }
            }
            catch (Exception ex)
            {
                log.Error("Error en Listar Especialidad por proyecto select2", ex);
            }
            return value;
        }
        public string listarSubEspPorEspecialidadString(string especialidad)
        {
            log4net.Config.XmlConfigurator.Configure(new FileInfo(ruta));
            log.Info("Listar SubEspecialidad por Especialidad para select2");

            string value = "";
            try
            {
                using (db_meieEntities db = new db_meieEntities())
                {
                    var splitIdSt = especialidad.Split(',');
                    if(splitIdSt.Length > 0)
                    {
                        foreach (var item in splitIdSt)
                        {
                            var splitIdInt = Convert.ToInt32(item);

                            //var tmp = from subEsp in db.PryProyectoSubEspecialidad
                            //          join sen in db.PrySubEspecialidad on subEsp.idSubEspecialidad equals sen.idPrySubEspecialidad
                            //          where subEsp.idSubEspecialidadProyecto == splitIdInt
                            //          select new SubEspecialidadViewModels
                            //          {
                            //              nombre = sen.NombreSubEspecialidad,
                            //              idEspecialidad = sen.idEspecialidad,
                            //              idSubEspecialidad = sen.idPrySubEspecialidad

                            //          };
                            var tmp = from subEsp in db.PrySubEspecialidad
                                      where subEsp.idEspecialidad == splitIdInt
                                      select new SubEspecialidadViewModels
                                      {
                                          nombre = subEsp.NombreSubEspecialidad,
                                          idEspecialidad = subEsp.idEspecialidad,
                                          idSubEspecialidad = subEsp.idPrySubEspecialidad

                                                };
                            var lista = tmp.ToList().Where(x => x.idSubEspecialidad > 0).Distinct();
                            foreach (var item2 in lista)
                            {
                                //value = value + "'" +item.idEspecialidad +"'" + ",";
                                value = value + item2.idSubEspecialidad + ",";
                            }
                        }
                            
                    }
                   
                    //value = value.Remove(value.Length-1, 1);
                    //value = "[" + value + "]";
                }
            }
            catch (Exception ex)
            {
                log.Error("Error en Listar SubEspecialidad por Especialidad select2", ex);
            }
            return value;
        }
    }
}
