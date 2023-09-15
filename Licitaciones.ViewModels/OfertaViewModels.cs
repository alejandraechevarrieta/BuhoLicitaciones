using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Licitaciones.ViewModels
{
    public class OfertaViewModels
    {
    }
    public class EmpresaOfertaViewModels
    {
        public int? idObra { get; set; }
        public int? idEmpresa { get; set; }
        public string nombreEmpresa { get; set; }
        public DateTime? fechaOferta { get; set; }
        public string fechaOfertaString => fechaOferta.HasValue ? fechaOferta.Value.ToString("dd/MM/yyyy") : "";
        public int? esGanadora { get; set; }
        public Nullable<bool> mejoraOferta { get; set; }
        public string cuit { get; set; }
    }
    public class DatosEmpresaViewModels
    {        
        public int? idEmpresa { get; set; }
        public string nombreEmpresa { get; set; }
        public string nombreObra { get; set; }
        public string emailEmpresa { get; set; }
        public string emailCopiaEmpresa { get; set; }
        public int? idObra { get; set; }         
    }
    public class EmpresaHabilitadaViewModels
    {
        public int idHabilitacion { get; set; }
        public Nullable<System.DateTime> fecha { get; set; }
        public Nullable<int> idEmpresa { get; set; }
        public string cuit { get; set; }
        public string razonSocial { get; set; }
        public Nullable<int> idObra { get; set; }
        public Nullable<bool> observaciones { get; set; }
        public Nullable<bool> mejoraOferta { get; set; }
        public int? idEstadoArchivo { get; set; }
    }
}
