using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WIFI.Anwendung
{
    /// <summary>
    /// Stellt einen Dienst aufgrund des Nuget-Paketes PDF-Sharp 
    /// zum Konvertieren von HTML-Seiten in PDF-Dateien
    /// </summary>
    public class PDFManager : ViewModelAppObjekt
    {
        public PdfSharp.Pdf.PdfDocument GenerierePDFVonHTML(string html_text)
        {
            TheArtOfDev.HtmlRenderer.PdfSharp.PdfGenerateConfig config = new TheArtOfDev.HtmlRenderer.PdfSharp.PdfGenerateConfig();
            config.PageOrientation = PdfSharp.PageOrientation.Landscape;
            config.PageSize = PdfSharp.PageSize.A4;
            return TheArtOfDev.HtmlRenderer.PdfSharp.PdfGenerator.GeneratePdf(html_text, config);
        }
    }
}
