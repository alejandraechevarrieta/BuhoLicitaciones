using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Licitaciones.ViewModels
{
    public class EspecialidadViewModels
    {
        public string nombre { get; set; }
        public int? idEspecialidad { get; set; }
        public int? idObra { get; set; }
        public decimal? porcentaje100 { get; set; }
        public decimal? porcentaje500 { get; set; }

    }
    public class SubEspecialidadViewModels
    {
        public string nombre { get; set; }
        public int? idEspecialidad { get; set; }
        public int? idSubEspecialidad { get; set; }


    }
}
