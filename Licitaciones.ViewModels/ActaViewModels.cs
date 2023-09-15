using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Licitaciones.ViewModels
{
    public class ActaViewModels
    {
        public int? idActa { get; set; }
        public int? idObra { get; set; }
        public string textoEncabezado { get; set; }
        public string textoFormal { get; set; }
        public string textoCierre { get; set; }
        public string textoOferta { get; set; }
        public string nroExpediente { get; set; }
        public string nombreObra { get; set; }
        public string nroLicitacion { get; set; }
        public string plazoEjecucion { get; set; }
        public decimal? montoObra { get; set; }
        public string montoObraString => montoObra.HasValue ? string.Format("{0:c}", montoObra) : "$ 0";
    }
    public class ActaFormalViewModels
    {
        public int? idObra { get; set; }
        public int? idFecha { get; set; }
        public string domicilioApertura { get; set; }
        public DateTime? fechaApertura { get; set; }
        public string horaApertura { get; set; }
    }
}
