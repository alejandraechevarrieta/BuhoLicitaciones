//------------------------------------------------------------------------------
// <auto-generated>
//     Este código se generó a partir de una plantilla.
//
//     Los cambios manuales en este archivo pueden causar un comportamiento inesperado de la aplicación.
//     Los cambios manuales en este archivo se sobrescribirán si se regenera el código.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Licitaciones.BaseDato
{
    using System;
    using System.Collections.Generic;
    
    public partial class LicOfertaEmpresa
    {
        public int idOferta { get; set; }
        public Nullable<int> idEmpresa { get; set; }
        public Nullable<int> idObra { get; set; }
        public Nullable<System.DateTime> fecha { get; set; }
        public Nullable<int> esGanadora { get; set; }
        public string rutaComprobante { get; set; }
    }
}
