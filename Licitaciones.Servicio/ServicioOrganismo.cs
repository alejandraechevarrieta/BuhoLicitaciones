using Licitaciones.BaseDato;
using Licitaciones.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Licitaciones.Servicio
{
    public class ServicioOrganismo
    {
        public List<selectViewModels> listar()
        {
            List<selectViewModels> lista = new List<selectViewModels>();
            try
            {
                using (db_meieEntities db = new db_meieEntities())
                {
                    var tmp = from org in db.PryOrganismosEjecutores
                              select new selectViewModels
                              {
                                  nombre = org.NombreOrganismo,
                                  id = org.Id
                              };
                    lista = tmp.ToList();
                }
            }
            catch (Exception ex)
            {

            }
            return lista;
        }
    }
}
