using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Licitaciones.ViewModels
{
    public class licitacionGrillaViewModels
    {
        public string nombreObra { get; set; }
        public int? idObra { get; set; }
        public int? idEtapa { get; set; }
        public string nombreEtapa { get; set; }
        public string nroLicitacion { get; set; }
        public int? nombreSubEtapa { get; set; }
        public int? idSubEtapa { get; set; }
        public int? idFavorito { get; set; }
        public DateTime? fechaPublicacion { get; set; }
        public int? plazo { get; set; }
        public DateTime? fechaApertura { get; set; }
        public string tipoContratacion { get; set; }
        public decimal? montoObra { get; set; }
        public string nroExpediente { get; set; }
        public int? idOrganismo { get; set; }
        public int? idContratacion { get; set; }
        public string nombreContratacion { get; set; }
        public string nombreOrganismo { get; set; }
        public string nombreContratacionString => string.IsNullOrEmpty(nombreContratacion) ? "" : nombreContratacion;
        public string nombreOrganismoString => string.IsNullOrEmpty(nombreOrganismo) ? "" : nombreOrganismo;
        public string nroExpedienteString => string.IsNullOrEmpty(nroExpediente) ? "" : nroExpediente;
        public string nombreTipoContratacion { get; set; }
        public string accion { get; set; }
        //public string color
        //{
        //    get
        //    {
        //        string value = "#00c0ef";
        //        if(idEtapa == 6)
        //        {
        //            value = "#00a65a";
        //        }
        //        else
        //        {
        //            if(idEtapa == 7)
        //            {
        //                value = "#5f2571";
        //            }
        //            else
        //            {

        //            }
        //        }
        //        return value;
        //    }
        //}
        public string montoObraString => montoObra.HasValue ? string.Format("{0:c}", montoObra) : "$ 0";
        public string fechaPublicacionString => fechaPublicacion.HasValue ? fechaPublicacion.Value.ToString("dd/MM/yyyy") : "";
        public string fechaAperturaString => fechaApertura.HasValue ? fechaApertura.Value.ToString("dd/MM/yyyy") : "";
        public DateTime? fechaOferta { get; set; }
        public int? tipoPlazo { get; set; }
        public int? estadoOferta => fechaOferta.HasValue ? 1 : 0;
        public bool? Eliminado { get; set; }
        public int? idOrgano { get; set; }
        public int? idOffice { get; set; }
        public int? idOrganoReceptor { get; set; }
    }
    public class tarjetaLicViewModels
    {
        public int? qAPublicar { get; set; }
        public int? qPublicada { get; set; }
        public int? qAbierta { get; set; }
        public int? qDesierta { get; set; }
        public int? qFracasada { get; set; }
        public int? qPreAdjudicada { get; set; }
        public int? qAdjudicada { get; set; }
        public int? qFirmada { get; set; }
    }
    public class unLicitacionViewModels
    {
        public string nombreObra { get; set; }
        public string nombreFuenteFinanciamiento { get; set; }
        public int? idObra { get; set; }
        public int? idEtapa { get; set; }
        public int? idTipoLicitacion { get; set; }
        public int? idTipoContratacion { get; set; }
        public string nombreEtapa { get; set; }
        public int? nombreSubEtapa { get; set; }
        public int? unidadPlazo { get; set; }
        public int? idSubEtapa { get; set; }
        public int? idFinanciamiento { get; set; }
        public DateTime? fechaPublicacion { get; set; }
        public DateTime? fechaApertura { get; set; }
        public string horaApertura { get; set; }
        public string tipoContratacion { get; set; }
        public decimal? montoObra { get; set; }
        public decimal? valorPliego { get; set; }
        public string nroExpediente { get; set; }
        public string caratula { get; set; }
        public int? idOrganismo { get; set; }
        public string nombreOrganismo { get; set; }
        public string rutaVirtual { get; set; }
        public decimal? latitud { get; set; }
        public decimal? longitud { get; set; }
        public string domicilio { get; set; }
        public string domicilioApertura { get; set; }
        public string domicilioPresentacion { get; set; }
        public string urlPliego { get; set; }
        public DateTime? fechaConsulta { get; set; }
        public DateTime? fechaCierre { get; set; }
        public DateTime? fechaVisita { get; set; }
        public int? idMoneda { get; set; }
        public string nombreMoneda { get; set; }
        public string lugarVisita { get; set; }
        public string descripcion { get; set; }
        public string mailConsulta { get; set; }
        public string nroLicitacion { get; set; }
        public int? plazo { get; set; }
        public string textoFinanciamiento { get; set; }
        public string nombreOrganismoString => string.IsNullOrEmpty(nombreOrganismo) ? "" : nombreOrganismo;
        public string nombreTipoContratacionString => string.IsNullOrEmpty(nombreTipoContratacion) ? "" : nombreTipoContratacion;
        public string nroExpedienteString => string.IsNullOrEmpty(nroExpediente) ? "" : nroExpediente;
        public string nombreTipoContratacion { get; set; }
        public string fechaConsultaString => fechaConsulta.HasValue ? fechaConsulta.Value.ToShortDateString() : "";
        public string montoObraString => montoObra.HasValue ? string.Format("{0:c}", montoObra) : "$ 0";
        public string fechaPublicacionString => fechaPublicacion.HasValue ? fechaPublicacion.Value.ToString("dd/MM/yyyy") : "";
        public string fechaAperturaString => fechaApertura.HasValue ? fechaApertura.Value.ToString("dd/MM/yyyy") : "";
        public string departamento { get; set; }
        public string fechaVisitaString => fechaVisita.HasValue ? fechaVisita.Value.ToString("dd/MM/yyyy") : "";
        public string fechaCierreString => fechaCierre.HasValue ? fechaCierre.Value.ToString("dd/MM/yyyy") : "";
        public DateTime? fechaVtoObs { get; set; }
        public string horaVtoObs { get; set; }
    }
    public class fechaViewModels
    {
        public int? idObra { get; set; }
        public int? idDetalle { get; set; }
        public DateTime? fechaPublicacion { get; set; }
        public DateTime? fechaApertura { get; set; }
        public DateTime? fechaConsulta { get; set; }
        public DateTime? fechaCierre { get; set; }
        public string fechaConsultaString => fechaConsulta.HasValue ? fechaConsulta.Value.ToString("dd/MM/yyyy") : "";
        public string fechaCierreString => fechaCierre.HasValue ? fechaCierre.Value.ToString("dd/MM/yyyy") : "";
        public string fechaPublicacionString => fechaPublicacion.HasValue ? fechaPublicacion.Value.ToString("dd/MM/yyyy") : "";
        public string fechaAperturaString => fechaApertura.HasValue ? fechaApertura.Value.ToString("dd/MM/yyyy") : "";
        public string horaApertura { get; set; }
    }
    public class cambioEtapaViewModels
    {
        public int? idObra { get; set; }
        public int? idEtapa { get; set; }
    }
    public class especialidadViewModels
    {
        public int? idObra { get; set; }
        public int? idEspecialidad { get; set; }
        public int? porcentaje100 { get; set; }
        public int? porcentaje400 { get; set; }
        public List<string> listaEspecialidad { get; set; }
    }
}
