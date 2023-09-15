using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Licitaciones.ViewModels
{
    public class EmailViewModels
    {
        public string asunto { get; set; }
        public string cuerpo { get; set; }
        public string destino { get; set; }
        public string origen { get; set; }
        public string resumen { get; set; }
        public string cuit { get; set; }
        public string copiaMail { get; set; }
        public DateTime? fechaAlta { get; set; }
        public string fechaAltaString => fechaAlta.HasValue ? fechaAlta.Value.ToShortDateString() : "";
    }

}
