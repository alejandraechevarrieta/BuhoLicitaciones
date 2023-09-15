using iTextSharp.text;
using iTextSharp.text.html.simpleparser;
using iTextSharp.text.pdf;
using Licitaciones.ViewModels;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Licitacion.Servicios
{
    public class ServicioPDF
    {
        public void generarFichaObra(string rutaCompleta, int? idObra)
        {
            ServicioLicitacion servicioLicitacion = new ServicioLicitacion();
            var unLicitacion = servicioLicitacion.buscarUna(idObra);           
            //directorio = HttpContext.Current.Server.MapPath("~/TempFiles/");
            // Creamos el documento con el tamaño de página tradicional
            Document doc = new Document();
            // Indicamos donde vamos a guardar el documento
            PdfWriter writer = PdfWriter.GetInstance(doc,
                                        new FileStream(rutaCompleta, FileMode.Create));
            // Abrimos el archivo
            doc.Open();

            #region Font 
            // Creamos el tipo de Font que vamos utilizar
            iTextSharp.text.Font _standardFont = new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 10, iTextSharp.text.Font.NORMAL, BaseColor.BLACK);

            iTextSharp.text.Font _standardFontChica = new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 9, iTextSharp.text.Font.NORMAL, BaseColor.BLACK);

            iTextSharp.text.Font _headerFont = new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 11, iTextSharp.text.Font.BOLD, BaseColor.BLACK);

            iTextSharp.text.Font _titleFont = new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 10, iTextSharp.text.Font.BOLD, BaseColor.BLACK);

            iTextSharp.text.Font _emptyFont = new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 2, iTextSharp.text.Font.BOLD, BaseColor.BLACK);

            iTextSharp.text.Font _title = new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 10, iTextSharp.text.Font.BOLD, BaseColor.BLACK);

            iTextSharp.text.Font _headerItem = new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 11, iTextSharp.text.Font.BOLD, BaseColor.BLACK);
            #endregion

            #region Imagen
            
            string imageUrl = System.Configuration.ConfigurationSettings.AppSettings["urlLogo"].ToString();
            iTextSharp.text.Image imgPdf = Image.GetInstance(imageUrl);           
            PdfPCell celImage = new PdfPCell();
         
            celImage.AddElement(new Chunk(imgPdf, 0, 0));           
            celImage.BorderWidthBottom = 0;
            celImage.BorderWidthLeft = 0;
            celImage.VerticalAlignment = Element.ALIGN_MIDDLE;
            celImage.BorderWidthTop = 0;
            celImage.BorderWidthRight = 0;
            celImage.FixedHeight = 60f;

            imgPdf.ScaleAbsoluteWidth(200);
            imgPdf.ScaleAbsoluteHeight(50);
            #endregion

            #region Encabezado
            PdfPTable tblHeader = new PdfPTable(1);
            tblHeader.WidthPercentage = 100;

            PdfPCell clNombre2 = new PdfPCell(new Phrase("FICHA DE OBRA DE: " + unLicitacion.nombreObra, _headerFont));
            clNombre2.HorizontalAlignment = Element.ALIGN_CENTER;
            clNombre2.BackgroundColor = new iTextSharp.text.BaseColor(224, 224, 224);
            clNombre2.BorderWidthBottom = 0;
            clNombre2.VerticalAlignment = Element.ALIGN_MIDDLE;
            clNombre2.FixedHeight = 30f;
            clNombre2.BorderWidthLeft = 1;
            clNombre2.BorderWidthTop = 1;
            clNombre2.BorderWidthRight = 1;
            clNombre2.Rowspan = 3;


            //tblHeader.AddCell(celImage);////
            tblHeader.AddCell(clNombre2);


            /////////////////////////////////////////////////////////
            #endregion

            PdfPTable tblCuerpo = new PdfPTable(3);
            tblCuerpo.WidthPercentage = 100;

            PdfPCell clNro1 = new PdfPCell(new Phrase("Expediente: " + unLicitacion.nroExpedienteString, _standardFont));
            clNro1.HorizontalAlignment = Element.ALIGN_LEFT;
            clNro1.BackgroundColor = new iTextSharp.text.BaseColor(224, 224, 224);
            //cltitulo.BorderWidthBottom = 0.75f;
            clNro1.BorderWidthBottom = 1;
            clNro1.BorderWidthLeft = 0;
            clNro1.VerticalAlignment = Element.ALIGN_MIDDLE;
            clNro1.FixedHeight = 30f;
            clNro1.BorderWidthTop = 0;
            clNro1.Colspan = 3;
            clNro1.BorderWidthRight = 0;

            PdfPCell clNro10 = new PdfPCell(new Phrase(" ", _standardFont));
            clNro10.HorizontalAlignment = Element.ALIGN_CENTER;
            //clNro10.BackgroundColor = new iTextSharp.text.BaseColor(224, 224, 224);
            //cltitulo.BorderWidthBottom = 0.75f;
            clNro10.BorderWidthBottom = 0;
            clNro10.BorderWidthLeft = 0;
            clNro10.BorderWidthTop = 0;
            clNro10.BorderWidthRight = 0;

            PdfPCell clNro11 = new PdfPCell(new Phrase("  ", _standardFont));
            clNro11.HorizontalAlignment = Element.ALIGN_LEFT;
            clNro11.BackgroundColor = new iTextSharp.text.BaseColor(224, 224, 224);
            //cltitulo.BorderWidthBottom = 0.75f;
            clNro11.BorderWidthBottom = 0;
            clNro11.BorderWidthLeft = 0;
            clNro11.BorderWidthTop = 0;
            clNro11.BorderWidthRight = 0;

            tblCuerpo.AddCell(clNro1);

            PdfPCell clLabelOrden = new PdfPCell(new Phrase("Descripcion: " + unLicitacion.descripcion, _standardFont));
            clLabelOrden.HorizontalAlignment = Element.ALIGN_LEFT;
            clLabelOrden.VerticalAlignment = Element.ALIGN_MIDDLE;
            //clLabelOrden.BackgroundColor = new iTextSharp.text.BaseColor(224, 224, 224);
            //cltitulo.BorderWidthBottom = 0.75f;
            clLabelOrden.BorderWidthBottom = 0;
            clLabelOrden.BorderWidthLeft = 0;
            clLabelOrden.BorderWidthTop = 0;
            clLabelOrden.Colspan = 3;
            clLabelOrden.BorderWidthRight = 0;
            clLabelOrden.FixedHeight = 30f;

            PdfPCell clLabelNombre = new PdfPCell(new Phrase("Estado: " + unLicitacion.nombreEtapa, _standardFont));
            clLabelNombre.HorizontalAlignment = Element.ALIGN_LEFT;
            clLabelNombre.VerticalAlignment = Element.ALIGN_MIDDLE;
            //clLabelNombre.BackgroundColor = new iTextSharp.text.BaseColor(224, 224, 224);
            //cltitulo.BorderWidthBottom = 0.75f;
            clLabelNombre.BorderWidthBottom = 0;
            clLabelNombre.BorderWidthLeft = 0;
            clLabelNombre.BorderWidthTop = 0;
            clLabelNombre.BorderWidthRight = 0;
            clLabelNombre.FixedHeight = 30f;

            PdfPCell clLabelFirma = new PdfPCell(new Phrase("Organismo: " + unLicitacion.nombreOrganismoString, _standardFont));
            clLabelFirma.HorizontalAlignment = Element.ALIGN_LEFT;
            clLabelFirma.VerticalAlignment = Element.ALIGN_MIDDLE;
            //clLabelFirma.BackgroundColor = new iTextSharp.text.BaseColor(224, 224, 224);
            //cltitulo.BorderWidthBottom = 0.75f;
            clLabelFirma.BorderWidthBottom = 0;
            clLabelFirma.BorderWidthLeft = 0;
            clLabelFirma.BorderWidthTop = 0;
            clLabelFirma.BorderWidthRight = 0;
            clLabelFirma.FixedHeight = 30f;
            clLabelFirma.Colspan = 2;

            PdfPCell clLabelAclaracion = new PdfPCell(new Phrase("Presupuesto Oficial: " + unLicitacion.montoObraString, _standardFont));
            clLabelAclaracion.HorizontalAlignment = Element.ALIGN_LEFT;
            clLabelAclaracion.VerticalAlignment = Element.ALIGN_MIDDLE;
            //clLabelAclaracion.BackgroundColor = new iTextSharp.text.BaseColor(224, 224, 224);
            //cltitulo.BorderWidthBottom = 0.75f;
            clLabelAclaracion.BorderWidthBottom = 0;
            clLabelAclaracion.BorderWidthLeft = 0;
            clLabelAclaracion.BorderWidthTop = 0;
            clLabelAclaracion.BorderWidthRight = 0;
            clLabelAclaracion.FixedHeight = 30f;

            PdfPCell clMoneda = new PdfPCell(new Phrase("Moneda: " + unLicitacion.nombreMoneda, _standardFont));
            clMoneda.HorizontalAlignment = Element.ALIGN_LEFT;
            clMoneda.VerticalAlignment = Element.ALIGN_MIDDLE;
            clMoneda.BorderWidthBottom = 0;
            clMoneda.BorderWidthLeft = 0;
            clMoneda.BorderWidthTop = 0;
            clMoneda.BorderWidthRight = 0;
            clMoneda.FixedHeight = 30f;

            PdfPCell clContratacion = new PdfPCell(new Phrase("Tipo Contratacion: " + unLicitacion.nombreTipoContratacion, _standardFont));
            clContratacion.HorizontalAlignment = Element.ALIGN_LEFT;
            clContratacion.VerticalAlignment = Element.ALIGN_MIDDLE;
            clContratacion.BorderWidthBottom = 0;
            clContratacion.BorderWidthLeft = 0;
            clContratacion.BorderWidthTop = 0;
            clContratacion.BorderWidthRight = 0;
            clContratacion.FixedHeight = 30f;

            PdfPCell clEspecialidad = new PdfPCell(new Phrase("Especialidad: " + "", _standardFont));
            clEspecialidad.HorizontalAlignment = Element.ALIGN_LEFT;
            clEspecialidad.VerticalAlignment = Element.ALIGN_MIDDLE;
            clEspecialidad.BorderWidthBottom = 0;
            clEspecialidad.BorderWidthLeft = 0;
            clEspecialidad.BorderWidthTop = 0;
            clEspecialidad.BorderWidthRight = 0;
            clEspecialidad.Colspan = 1;
            clEspecialidad.FixedHeight = 30f;

            PdfPCell clSubEspecialidad = new PdfPCell(new Phrase("Subespecialidad: " + "", _standardFont));
            clSubEspecialidad.HorizontalAlignment = Element.ALIGN_LEFT;
            clSubEspecialidad.VerticalAlignment = Element.ALIGN_MIDDLE;
            clSubEspecialidad.BorderWidthBottom = 0;
            clSubEspecialidad.BorderWidthLeft = 0;
            clSubEspecialidad.BorderWidthTop = 0;
            clSubEspecialidad.BorderWidthRight = 0;
            clSubEspecialidad.FixedHeight = 30f;
            clSubEspecialidad.Colspan = 2;

            PdfPCell clEjecucion = new PdfPCell(new Phrase("Plazo Ejecución: " + unLicitacion.plazo, _standardFont));
            clEjecucion.HorizontalAlignment = Element.ALIGN_LEFT;
            clEjecucion.VerticalAlignment = Element.ALIGN_MIDDLE;
            clEjecucion.BorderWidthBottom = 0;
            clEjecucion.BorderWidthLeft = 0;
            clEjecucion.BorderWidthTop = 0;
            clEjecucion.BorderWidthRight = 0;
            clEjecucion.FixedHeight = 30f;

            PdfPCell clDomicilio = new PdfPCell(new Phrase("Domicilio: " + unLicitacion.domicilio, _standardFont));
            clDomicilio.HorizontalAlignment = Element.ALIGN_LEFT;
            clDomicilio.VerticalAlignment = Element.ALIGN_MIDDLE;
            clDomicilio.BorderWidthBottom = 0;
            clDomicilio.BorderWidthLeft = 0;
            clDomicilio.BorderWidthTop = 0;
            clDomicilio.BorderWidthRight = 0;
            clDomicilio.Colspan = 2;
            clDomicilio.FixedHeight = 30f;

            PdfPCell clDepartamento = new PdfPCell(new Phrase("Departamento: " + unLicitacion.departamento, _standardFont)); 
            clDepartamento.HorizontalAlignment = Element.ALIGN_LEFT;
            clDepartamento.VerticalAlignment = Element.ALIGN_MIDDLE;
            clDepartamento.BorderWidthBottom = 0;
            clDepartamento.BorderWidthLeft = 0;
            clDepartamento.BorderWidthTop = 0;
            clDepartamento.BorderWidthRight = 0;
            clDepartamento.FixedHeight = 30f;

            PdfPCell clUrlPliego = new PdfPCell(new Phrase("Pliego: " + unLicitacion.urlPliego, _standardFont));
            clUrlPliego.HorizontalAlignment = Element.ALIGN_LEFT;
            clUrlPliego.VerticalAlignment = Element.ALIGN_MIDDLE;
            clUrlPliego.BorderWidthBottom = 0;
            clUrlPliego.BorderWidthLeft = 0;
            clUrlPliego.BorderWidthTop = 0;
            clUrlPliego.BorderWidthRight = 0;
            clUrlPliego.FixedHeight = 30f;
            clUrlPliego.Colspan = 2;

            PdfPCell clValorPliego = new PdfPCell(new Phrase("Valor Pliego: " + unLicitacion.valorPliego, _standardFont)); 
            clValorPliego.HorizontalAlignment = Element.ALIGN_LEFT;
            clValorPliego.VerticalAlignment = Element.ALIGN_MIDDLE;
            clValorPliego.BorderWidthBottom = 0;
            clValorPliego.BorderWidthLeft = 0;
            clValorPliego.BorderWidthTop = 0;
            clValorPliego.BorderWidthRight = 0;
            clValorPliego.FixedHeight = 30f;

            PdfPCell clFechaVisita = new PdfPCell(new Phrase("Fecha Visita: " + unLicitacion.fechaVisitaString, _standardFont)); 
            clFechaVisita.HorizontalAlignment = Element.ALIGN_LEFT;
            clFechaVisita.VerticalAlignment = Element.ALIGN_MIDDLE;
            clFechaVisita.BorderWidthBottom = 0;
            clFechaVisita.BorderWidthLeft = 0;
            clFechaVisita.BorderWidthTop = 0;
            clFechaVisita.BorderWidthRight = 0;
            clFechaVisita.FixedHeight = 30f;


            PdfPCell clLugarVisita = new PdfPCell(new Phrase("Lugar Visita: " + unLicitacion.lugarVisita, _standardFont));
            clLugarVisita.HorizontalAlignment = Element.ALIGN_LEFT;
            clLugarVisita.VerticalAlignment = Element.ALIGN_MIDDLE;
            clLugarVisita.BorderWidthBottom = 0;
            clLugarVisita.BorderWidthLeft = 0;
            clLugarVisita.BorderWidthTop = 0;
            clLugarVisita.BorderWidthRight = 0;
            clLugarVisita.Colspan = 2;
            clLugarVisita.FixedHeight = 30f;


            PdfPCell clFechaCierre = new PdfPCell(new Phrase("Fecha Cierre: " + unLicitacion.fechaCierreString, _standardFont));
            clFechaCierre.HorizontalAlignment = Element.ALIGN_LEFT;
            clFechaCierre.VerticalAlignment = Element.ALIGN_MIDDLE;
            clFechaCierre.BorderWidthBottom = 0;
            clFechaCierre.BorderWidthLeft = 0;
            clFechaCierre.BorderWidthTop = 0;
            clFechaCierre.BorderWidthRight = 0;
            clFechaCierre.FixedHeight = 30f;

            var fecha = unLicitacion.fechaVisita;

            PdfPCell clFechaApertura = new PdfPCell(new Phrase("Fecha Apertura: " + unLicitacion.fechaAperturaString, _standardFont));
            clFechaApertura.HorizontalAlignment = Element.ALIGN_LEFT;
            clFechaApertura.VerticalAlignment = Element.ALIGN_MIDDLE;
            clFechaApertura.BorderWidthBottom = 0;
            clFechaApertura.BorderWidthLeft = 0;
            clFechaApertura.BorderWidthTop = 0;
            clFechaApertura.BorderWidthRight = 0;
            clFechaApertura.FixedHeight = 30f;

            PdfPCell clHoraApertura = new PdfPCell(new Phrase("Hora Apertura: " + unLicitacion.horaApertura, _standardFont));
            clHoraApertura.HorizontalAlignment = Element.ALIGN_LEFT;
            clHoraApertura.VerticalAlignment = Element.ALIGN_MIDDLE;
            clHoraApertura.BorderWidthBottom = 0;
            clHoraApertura.BorderWidthLeft = 0;
            clHoraApertura.BorderWidthTop = 0;
            clHoraApertura.BorderWidthRight = 0;
            clHoraApertura.FixedHeight = 30f;

            PdfPCell clDomPres = new PdfPCell(new Phrase("Domicilio Presentación: " + unLicitacion.domicilioPresentacion, _standardFont));
            clDomPres.HorizontalAlignment = Element.ALIGN_LEFT;
            clDomPres.VerticalAlignment = Element.ALIGN_MIDDLE;
            clDomPres.BorderWidthBottom = 0;
            clDomPres.BorderWidthLeft = 0;
            clDomPres.BorderWidthTop = 0;
            clDomPres.BorderWidthRight = 0;
            clDomPres.Colspan = 3;
            clDomPres.FixedHeight = 30f;

            PdfPCell clDomAper = new PdfPCell(new Phrase("Domicilio Apertura: " + unLicitacion.domicilioApertura, _standardFont));
            clDomAper.HorizontalAlignment = Element.ALIGN_LEFT;
            clDomAper.VerticalAlignment = Element.ALIGN_MIDDLE;
            clDomAper.BorderWidthBottom = 0;
            clDomAper.BorderWidthLeft = 0;
            clDomAper.BorderWidthTop = 0;
            clDomAper.BorderWidthRight = 0;
            clDomAper.Colspan = 3;
            clDomAper.FixedHeight = 30f;

            PdfPCell clMailConsulta = new PdfPCell(new Phrase("Mail Consultas: " + unLicitacion.mailConsulta, _standardFont));
            clMailConsulta.HorizontalAlignment = Element.ALIGN_LEFT;
            clMailConsulta.VerticalAlignment = Element.ALIGN_MIDDLE;
            clMailConsulta.BorderWidthBottom = 0;
            clMailConsulta.BorderWidthLeft = 0;
            clMailConsulta.BorderWidthTop = 0;
            clMailConsulta.BorderWidthRight = 0;
            clMailConsulta.FixedHeight = 30f;
            clMailConsulta.Colspan = 3;
            ///////////////////
            tblCuerpo.AddCell(clLabelOrden);

            tblCuerpo.AddCell(clLabelNombre);
            tblCuerpo.AddCell(clLabelFirma);

            tblCuerpo.AddCell(clLabelAclaracion);
            tblCuerpo.AddCell(clMoneda);
            tblCuerpo.AddCell(clNro10);

            tblCuerpo.AddCell(clContratacion);
            tblCuerpo.AddCell(clEjecucion);
            tblCuerpo.AddCell(clNro10);

            tblCuerpo.AddCell(clEspecialidad);
            tblCuerpo.AddCell(clSubEspecialidad);

            tblCuerpo.AddCell(clDomicilio);
            tblCuerpo.AddCell(clDepartamento);

            tblCuerpo.AddCell(clUrlPliego);
            tblCuerpo.AddCell(clValorPliego);
            

            tblCuerpo.AddCell(clFechaVisita);
            tblCuerpo.AddCell(clLugarVisita);

            tblCuerpo.AddCell(clFechaCierre);
            tblCuerpo.AddCell(clFechaApertura);
            tblCuerpo.AddCell(clHoraApertura);

            tblCuerpo.AddCell(clDomPres);
            tblCuerpo.AddCell(clDomAper);
            tblCuerpo.AddCell(clMailConsulta);

            PdfPTable tblHeader2 = new PdfPTable(1);
            //tblHeader2.SetWidths(new float[] { 12f });
            tblHeader2.WidthPercentage = 100;

            tblHeader2.AddCell(tblCuerpo);
            // Finalmente, añadimos la tabla al documento PDF y cerramos el documento
            doc.Add(imgPdf);
            doc.Add(tblHeader);
            doc.Add(tblHeader2);
            doc.Close();
            writer.Close();

        }
        public void generarActaAperturaOld(string rutaCompleta, ActaViewModels unActa)
        {
            // Creamos el documento con el tamaño de página tradicional
            Document doc = new Document();
            // Indicamos donde vamos a guardar el documento
            PdfWriter writer = PdfWriter.GetInstance(doc,
                                        new FileStream(rutaCompleta, FileMode.Create));
            // Abrimos el archivo
            doc.Open();

            #region Font 
            // Creamos el tipo de Font que vamos utilizar
            iTextSharp.text.Font _standardFont = new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 10, iTextSharp.text.Font.NORMAL, BaseColor.BLACK);

            iTextSharp.text.Font _standardFontChica = new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 9, iTextSharp.text.Font.NORMAL, BaseColor.BLACK);

            iTextSharp.text.Font _headerFont = new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 11, iTextSharp.text.Font.BOLD, BaseColor.BLACK);

            iTextSharp.text.Font _titleFont = new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 10, iTextSharp.text.Font.BOLD, BaseColor.BLACK);

            iTextSharp.text.Font _emptyFont = new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 2, iTextSharp.text.Font.BOLD, BaseColor.BLACK);

            iTextSharp.text.Font _title = new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 10, iTextSharp.text.Font.BOLD, BaseColor.BLACK);

            iTextSharp.text.Font _headerItem = new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 11, iTextSharp.text.Font.BOLD, BaseColor.BLACK);
            #endregion

            #region Imagen

            string imageUrl = System.Configuration.ConfigurationSettings.AppSettings["urlLogo"].ToString();
            iTextSharp.text.Image imgPdf = Image.GetInstance(imageUrl);
            PdfPCell celImage = new PdfPCell();

            celImage.AddElement(new Chunk(imgPdf, 0, 0));
            celImage.BorderWidthBottom = 0;
            celImage.BorderWidthLeft = 0;
            celImage.VerticalAlignment = Element.ALIGN_MIDDLE;
            celImage.BorderWidthTop = 0;
            celImage.BorderWidthRight = 0;
            celImage.FixedHeight = 60f;

            imgPdf.ScaleAbsoluteWidth(200);
            imgPdf.ScaleAbsoluteHeight(50);
            #endregion

            #region Encabezado
            PdfPTable tblHeader = new PdfPTable(1);
            tblHeader.WidthPercentage = 100;
            var textEncabezado = unActa.textoEncabezado;
            textEncabezado = textEncabezado.Replace("<p>", "");
            textEncabezado = textEncabezado.Replace("</p>", "");
            textEncabezado = textEncabezado.Replace("<n>", "");
            textEncabezado = textEncabezado.Replace("</n>", "");
            textEncabezado = textEncabezado.Replace("<br>", "{");

            var listaEncabezado = textEncabezado.Split('{');

            PdfPCell clNombre2 = new PdfPCell(new Phrase("ACTA DE APERTURA SIMULTANEA SOBRE N° 1, N° 2 Y N° 3", _headerFont));
            clNombre2.HorizontalAlignment = Element.ALIGN_CENTER;
            //clNombre2.BackgroundColor = new iTextSharp.text.BaseColor(224, 224, 224);
            clNombre2.BorderWidthBottom = 0;
            clNombre2.VerticalAlignment = Element.ALIGN_MIDDLE;
            clNombre2.FixedHeight = 30f;
            //clNombre2.Rowspan = 3;

            //tblHeader.AddCell(celImage);
            tblHeader.AddCell(clNombre2);

            foreach (var item in listaEncabezado)
            {
                var texto2 = HtmlToPlainText(item);
                PdfPCell clNombre3 = new PdfPCell(new Phrase(texto2, _headerFont));
                clNombre3.HorizontalAlignment = Element.ALIGN_CENTER;
                //clNombre3.BackgroundColor = new iTextSharp.text.BaseColor(224, 224, 224);
                clNombre3.BorderWidthBottom = 0;
                clNombre3.BorderWidthTop = 0;
                clNombre3.VerticalAlignment = Element.ALIGN_MIDDLE;
                clNombre3.FixedHeight = 30f;
                //clNombre3.Rowspan = 3;
                tblHeader.AddCell(clNombre3);
            }
            #endregion

            #region Cuerpo
            PdfPTable tblCuerpo = new PdfPTable(1);
            tblCuerpo.WidthPercentage = 100;

            PdfPCell clNro1 = new PdfPCell(new Phrase(unActa.textoFormal, _standardFont));
            clNro1.HorizontalAlignment = Element.ALIGN_LEFT;
            //clNro1.BackgroundColor = new iTextSharp.text.BaseColor(224, 224, 224);
            //cltitulo.BorderWidthBottom = 0.75f;
            clNro1.BorderWidthBottom = 0;
            clNro1.BorderWidthLeft = 0;
            clNro1.VerticalAlignment = Element.ALIGN_MIDDLE;
            clNro1.FixedHeight = 30f;
            clNro1.BorderWidthTop = 0;
            clNro1.Colspan = 3;
            clNro1.BorderWidthRight = 0;

            tblCuerpo.AddCell(clNro1);

            var textCuerpo = unActa.textoOferta;
            textCuerpo = textCuerpo.Replace("<p>", "");
            textCuerpo = textCuerpo.Replace("</p>", "");
            textCuerpo = textCuerpo.Replace("<n>", "");
            textCuerpo = textCuerpo.Replace("</n>", "");
            textCuerpo = textCuerpo.Replace("<b>", "");
            textCuerpo = textCuerpo.Replace("</b>", "");
            textCuerpo = textCuerpo.Replace("<br>", "{");
            var listaCuerpo = textCuerpo.Split('{');
            foreach (var item in listaCuerpo)
            {
                var texto3 = HtmlToPlainText(item);
                PdfPCell clNombre3 = new PdfPCell(new Phrase(texto3, _standardFont));
                clNombre3.HorizontalAlignment = Element.ALIGN_LEFT;
                //clNombre3.BackgroundColor = new iTextSharp.text.BaseColor(224, 224, 224);
                clNombre3.BorderWidthBottom = 0;
                clNombre3.BorderWidthTop = 0;
                clNombre3.VerticalAlignment = Element.ALIGN_MIDDLE;
                //clNombre3.FixedHeight = 30f; // Ver TAMAÑO CORTA TEXTO
                //clNombre3.Rowspan = 3;
                tblHeader.AddCell(clNombre3);
            }
            #endregion

            #region Cierre
            var textCierre = unActa.textoCierre;
            textCierre = textCierre.Replace("<p>", "");
            textCierre = textCierre.Replace("</p>", "");
            textCierre = textCierre.Replace("<n>", "");
            textCierre = textCierre.Replace("</n>", "");
            textCierre = textCierre.Replace("<br>", "{");
            textCierre = textCierre.Replace("<br>", "{");
            textCierre = textCierre.Replace("{", "");
            textCierre = textCierre.Replace("{", "");
            textCierre = textCierre.Replace("{", "");
            textCierre = textCierre.Replace("{", "");
            textCierre = textCierre.Replace("{", "");
            textCierre = textCierre.Replace("{", "");
            textCierre = textCierre.Replace("{", "");
            textCierre = textCierre.Replace("{", "");
            textCierre = textCierre.Replace("{", "");
            var texto = HtmlToPlainText(textCierre);
            PdfPCell clNombre10 = new PdfPCell(new Phrase(texto, _standardFont));
            clNombre10.HorizontalAlignment = Element.ALIGN_LEFT;
            //clNombre2.BackgroundColor = new iTextSharp.text.BaseColor(224, 224, 224);
            clNombre10.BorderWidthTop = 0;
            clNombre10.BorderWidthBottom = 1;
            clNombre10.VerticalAlignment = Element.ALIGN_MIDDLE;
            clNombre10.FixedHeight = 30f;
            tblHeader.AddCell(clNombre10);
            #endregion

            // Finalmente, añadimos la tabla al documento PDF y cerramos el documento
            doc.Add(imgPdf);
            doc.Add(tblHeader);
            //doc.Add(tblHeader2);
            doc.Close();
            writer.Close();

        }
        public void generarComprobanteOferta(int? idEmpresa, int? idObra, string rutaCompleta, string nombreObra)
        {
            try
            {
                ServicioDocumentacion servicioDocumentacion = new ServicioDocumentacion();
                List<RequisitoViewModels> listaArchivo = new List<RequisitoViewModels>();
                listaArchivo = servicioDocumentacion.listarRequisitoPorObraEmpresa(idEmpresa, idObra);

                //directorio = HttpContext.Current.Server.MapPath("~/TempFiles/");
                // Creamos el documento con el tamaño de página tradicional
                Document doc = new Document();
                // Indicamos donde vamos a guardar el documento
                PdfWriter writer = PdfWriter.GetInstance(doc,
                                            new FileStream(rutaCompleta, FileMode.Create));
                // Abrimos el archivo
                doc.Open();

                #region Font 
                // Creamos el tipo de Font que vamos utilizar
                iTextSharp.text.Font _standardFont = new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 10, iTextSharp.text.Font.NORMAL, BaseColor.BLACK);

                iTextSharp.text.Font _standardFontChica = new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 9, iTextSharp.text.Font.NORMAL, BaseColor.BLACK);

                iTextSharp.text.Font _headerFont = new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 11, iTextSharp.text.Font.BOLD, BaseColor.BLACK);

                iTextSharp.text.Font _titleFont = new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 10, iTextSharp.text.Font.BOLD, BaseColor.BLACK);

                iTextSharp.text.Font _emptyFont = new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 2, iTextSharp.text.Font.BOLD, BaseColor.BLACK);

                iTextSharp.text.Font _title = new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 10, iTextSharp.text.Font.BOLD, BaseColor.BLACK);

                iTextSharp.text.Font _headerItem = new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 11, iTextSharp.text.Font.BOLD, BaseColor.BLACK);
                #endregion

                #region Imagen

                string imageUrl = System.Configuration.ConfigurationSettings.AppSettings["urlLogo"].ToString();
                iTextSharp.text.Image imgPdf = Image.GetInstance(imageUrl);
                PdfPCell celImage = new PdfPCell();

                celImage.AddElement(new Chunk(imgPdf, 0, 0));
                celImage.BorderWidthBottom = 0;
                celImage.BorderWidthLeft = 0;
                celImage.VerticalAlignment = Element.ALIGN_MIDDLE;
                celImage.BorderWidthTop = 0;
                celImage.BorderWidthRight = 0;
                celImage.FixedHeight = 60f;

                imgPdf.ScaleAbsoluteWidth(200);
                imgPdf.ScaleAbsoluteHeight(50);
                #endregion

                #region Encabezado
                PdfPTable tblHeader = new PdfPTable(1);
                tblHeader.WidthPercentage = 100;

                PdfPCell clNombre2 = new PdfPCell(new Phrase("DETALLE DE OFERTA PRESENTADA POR LA OBRA: " + nombreObra + " EL "+ DateTime.Now.Date.ToString("dd/MM/yyyy"), _headerFont));
                clNombre2.HorizontalAlignment = Element.ALIGN_CENTER;
                clNombre2.BackgroundColor = new iTextSharp.text.BaseColor(224, 224, 224);
                clNombre2.BorderWidthBottom = 1;
                clNombre2.VerticalAlignment = Element.ALIGN_MIDDLE;
                clNombre2.FixedHeight = 30f;
                clNombre2.BorderWidthLeft = 1;
                clNombre2.BorderWidthTop = 1;
                clNombre2.BorderWidthRight = 1;
                clNombre2.Rowspan = 3;


                //tblHeader.AddCell(celImage);////
                tblHeader.AddCell(clNombre2);
                #endregion

                PdfPTable tblCuerpo = new PdfPTable(2);
                tblCuerpo.WidthPercentage = 100;
                foreach(var item in listaArchivo)
                {
                    PdfPCell clRequisito = new PdfPCell(new Phrase("Requisito: " + item.nombre, _standardFont));
                    clRequisito.HorizontalAlignment = Element.ALIGN_LEFT;
                    //clRequisito.BackgroundColor = new iTextSharp.text.BaseColor(224, 224, 224);
                    //cltitulo.BorderWidthBottom = 0.75f;
                    clRequisito.BorderWidthBottom = 0;
                    clRequisito.BorderWidthLeft = 0;
                    clRequisito.VerticalAlignment = Element.ALIGN_MIDDLE;
                    clRequisito.FixedHeight = 30f;
                    clRequisito.BorderWidthTop = 0;
                    clRequisito.BorderWidthRight = 0;

                    var archivo = string.Empty;
                    if (string.IsNullOrEmpty((item.nombreArchivo))){
                        archivo = "No presenta archivo";
                    }
                    else
                    {
                        archivo = "Archivo " + item.nombreArchivo;
                    }
                    PdfPCell clFile = new PdfPCell(new Phrase(archivo, _standardFont));
                    clFile.HorizontalAlignment = Element.ALIGN_LEFT;
                    //cltitulo.BorderWidthBottom = 0.75f;
                    clFile.BorderWidthBottom = 0;
                    clFile.BorderWidthLeft = 0;
                    clFile.VerticalAlignment = Element.ALIGN_MIDDLE;
                    clFile.FixedHeight = 30f;
                    clFile.BorderWidthTop = 0;
                    clFile.BorderWidthRight = 0;

                    tblCuerpo.AddCell(clRequisito);
                    tblCuerpo.AddCell(clFile);
                }

                PdfPTable tblHeader2 = new PdfPTable(1);
                //tblHeader2.SetWidths(new float[] { 12f });
                tblHeader2.WidthPercentage = 100;

                tblHeader2.AddCell(tblCuerpo);
                // Finalmente, añadimos la tabla al documento PDF y cerramos el documento
                doc.Add(imgPdf);
                doc.Add(tblHeader);
                doc.Add(tblHeader2);
                doc.Close();
                writer.Close();
            }
            catch(Exception ex)
            {

            }
        }
        private static string HtmlToPlainText(string html)
        {
            const string tagWhiteSpace = @"(>|$)(\W|\n|\r)+<";//matches one or more (white space or line breaks) between '>' and '<'
            const string stripFormatting = @"<[^>]*(>|$)";//match any character between '<' and '>', even when end tag is missing
            const string lineBreak = @"<(br|BR)\s{0,1}\/{0,1}>";//matches: <br>,<br/>,<br />,<BR>,<BR/>,<BR />
            var lineBreakRegex = new Regex(lineBreak, RegexOptions.Multiline);
            var stripFormattingRegex = new Regex(stripFormatting, RegexOptions.Multiline);
            var tagWhiteSpaceRegex = new Regex(tagWhiteSpace, RegexOptions.Multiline);

            var text = html;
            //Decode html specific characters
            text = System.Net.WebUtility.HtmlDecode(text);
            //Remove tag whitespace/line breaks
            text = tagWhiteSpaceRegex.Replace(text, "><");
            //Replace <br /> with line breaks
            text = lineBreakRegex.Replace(text, Environment.NewLine);
            //Strip formatting
            text = stripFormattingRegex.Replace(text, string.Empty);

            return text;
        }
        public void generarActaApertura(string rutaCompleta, ActaViewModels unActa)
        {
            try
            {
                var texto = unActa.textoEncabezado;
                texto = texto + unActa.textoOferta;
                texto = texto + unActa.textoCierre;

                Byte[] bytes;
                using (var ms = new MemoryStream())
                {
                    using (var doc = new Document())
                    {
                        using (var writer = PdfWriter.GetInstance(doc, ms))
                        {

                            doc.Open();

                            #region Imagen

                            string imageUrl = System.Configuration.ConfigurationSettings.AppSettings["urlLogo"].ToString();
                            iTextSharp.text.Image imgPdf = Image.GetInstance(imageUrl);
                            PdfPCell celImage = new PdfPCell();

                            celImage.AddElement(new Chunk(imgPdf, 0, 0));
                            celImage.BorderWidthBottom = 0;
                            celImage.BorderWidthLeft = 0;
                            celImage.VerticalAlignment = Element.ALIGN_MIDDLE;
                            celImage.BorderWidthTop = 0;
                            celImage.BorderWidthRight = 0;
                            celImage.FixedHeight = 60f;

                            imgPdf.ScaleAbsoluteWidth(200);
                            imgPdf.ScaleAbsoluteHeight(50);
                            doc.Add(imgPdf);
                            #endregion

                            using (var htmlWorker = new iTextSharp.text.html.simpleparser.HTMLWorker(doc))
                            {
                                using (var sr = new StringReader(texto))
                                {
                                    htmlWorker.Parse(sr);
                                }
                            }

                            doc.Close();
                        }
                    }
                    bytes = ms.ToArray();
                }
                //var testFile = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Desktop), rutaCompleta);
                System.IO.File.WriteAllBytes(rutaCompleta, bytes);
            }
            catch(Exception ex)
            {
                var a = ex.Message;
            }
        }     
    }
}
