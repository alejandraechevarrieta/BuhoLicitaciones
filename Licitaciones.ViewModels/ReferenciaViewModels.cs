using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Licitaciones.ViewModels
{
    public class ReferenciaViewModels
    {
        public string nombre { get; set; }
    }
    public class RequisitoViewModels
    {
        public string nombre { get; set; }
        public int? idPadre { get; set; }
        public int? idRequisito { get; set; }
        public int? nroSobre { get; set; }
        public int? id { get; set; }
        public int? idArchivo { get; set; }
        public int? completo { get; set; }
        public int? idDocEmpresa { get; set; }
        public int? idEstado { get; set; }
        public string observacion { get; set; }
        public string nombreArchivo { get; set; }
        public string horaArchivo { get; set; }
        public bool? tieneArchivo { get; set; }
    }
    public class RequisitoObraViewModels
    {
        public string nombre { get; set; }
        public int? idPadre { get; set; }
        public int? idRequisito { get; set; }
        public int? idObra { get; set; }
        public int? nroSobre { get; set; }
        public int? idSobre { get; set; }
    }
    public class ArchivoViewModels
    {
        public string nombre { get; set; }
        public string ruta { get; set; }
    }
    public class ArchivoObraViewModels
    {
        public string nombre { get; set; }
        public string ruta { get; set; }
        public int? idDetalle { get; set; }
        public int? idObra { get; set; }
        public int? idCategoria { get; set; }
        public string nombreCategoria { get; set; }
        public string nombreArchivo { get; set; }
        public DateTime? fechaArchivo { get; set; }
        public string fechaArchivoString => fechaArchivo.HasValue ? fechaArchivo.Value.ToShortDateString() : "";
    }
    public class estadoArchivoViewModels
    {
        public int? idArchivo { get; set; }
        public int? idEstado { get; set; }
        public string observaciones { get; set; }
    }
    public class SobresViewModels
    {
        public int? idLicDocSobres { get; set; }
        public int? numero { get; set; }
        public string nombre { get; set; }
        public int? idObra { get; set; }
        public bool? activo { get; set; }
    }
}