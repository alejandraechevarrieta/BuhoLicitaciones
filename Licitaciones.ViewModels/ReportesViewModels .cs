using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Licitaciones.ViewModels
{
    public class ReporteExcelVM
    {
        public List<string> encabezado { get; set; }
        public List<DetalleExcelVM> filas { get; set; }       
        public string filePath { get; set; }
        public string fileName { get; set; }
   
    }
    public class DetalleExcelVM
    {
        public string nombre { get; set; }
        public string expediente { get; set; }
        public string publicacion { get; set; }
        public string organismo { get; set; }
        public string monto { get; set; }
        public string etapa { get; set; }
        public string fechaApertura { get; set; }
        public int? estadoOferta { get; set; }
    }
   
}
