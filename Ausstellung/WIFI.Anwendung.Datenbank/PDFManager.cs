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
        public PdfSharp.Pdf.PdfDocument GenerierePDFVonHTML(string htmltext)
        {
            // Erstellt die Standardkonfiguration für das Format des PDF's
            TheArtOfDev.HtmlRenderer.PdfSharp.PdfGenerateConfig config = new TheArtOfDev.HtmlRenderer.PdfSharp.PdfGenerateConfig();

            config.PageOrientation = PdfSharp.PageOrientation.Landscape;

            config.PageSize = PdfSharp.PageSize.A4;
            


            // Erstellt ein lesbareres CSS-Objekt umd die Styles im PDF zu übernehmen
            //TheArtOfDev.HtmlRenderer.Core.CssData myCss =
            //    TheArtOfDev.HtmlRenderer.PdfSharp.PdfGenerator.ParseStyleSheet(
            //        stylesheet: css);


            return TheArtOfDev.HtmlRenderer.PdfSharp.PdfGenerator.GeneratePdf(htmltext, config);

        }

        public void GeneriereBestellungenübersicht()
        {
            // Ortne nach Kategoriegruppe

            string htmltext =
                "<html>" +
                "<body >" +
                    "<table style='width: 100%; border-collapse: collapse;'>" +
                        "<tr style='max-height: 21cm !IMPORTANT'>" +
                            "<td style='width: 11cm !IMPORTANT;background:white; text-align:left'>" +
                                "<p style='margin: 0px 0px 0px 15px; font-size:20pt; text-align:left; font-weight: bold '> Buchausstellung xxxx, $gemeinde</p>" +
                                "<div style='margin: 0px 0px 10px 10px'>" +
                                    "{0}" +
                                "</div>" +
                            "</td>" +
                            "<td style='width: 11cm !IMPORTANT;background:white; text-align:left'>" +
                                "<p style='margin: 0px 0px 0px 15px; font-size:20pt; text-align:left; font-weight: bold'> Buchausstellung xxxx, $gemeinde</p>" +
                                "<div style='margin: 0px 0px 10px 10px'>" +
                                    "{1}" +
                                "</div>" +
                            "</td>" +
                        "</tr>" +
                    "</table>" +

                "</body>" +
                "</html>";


            string gruppenblock = "<p style='font-size:18pt;margin: 15px 0px 10px 5px '>Gruppe $Zahl - $Name</p>" +
                        "<table style='width: 90%; border-collapse: collapse'>" +
                        "<tr style='border-bottom:inset;border-color:black;border-width: 1px'>" +
                            "<td style='font-weight:bold;text-align:center'> Anzahl </td>" +
                            "<td style='font-weight:bold'> Nr </td>" +
                            "<td style='font-weight:bold'> Titel, Autor </td>" +
                            "<td style='font-weight:bold'> Verlag </td>" +
                            "<td style='font-weight:bold;text-align:right;padding: 0px 10px 0px 0px'> Preis </td>" +
                            "<td style='font-weight:bold;text-align:center'> Rab.Gr </td>" +
                        "</tr>" +
                        "<tr>" +
                            "<td style='text-align: center'> 2 </td>" +
                            "<td style='text-align: left'> 030 </td>" +
                            "<td style='text-align: left'> Bimbo, der kilometergroße Elefant, Jäger </td>" +
                            "<td style='text-align: left'> Gaudete </td>" +
                            "<td style='text-align: right; padding: 0px 5px 0px 0px'> 17,-- </td>" +
                            "<td style='text-align: center'> 1 </td>" +
                        "</tr>" +
                        "<tr>" +
                            "<td style='text-align: center'> 5 </td>" +
                            "<td style='text-align: left'> 010 </td>" +
                            "<td style='text-align: left'> Peter Pan, Einstein </td>" +
                            "<td style='text-align: left'> Thalia </td>" +
                            "<td style='text-align: right; padding: 0px 5px 0px 0px'> 7,-- </td>" +
                            "<td style='text-align: center'> 0 </td>" +
                        "</tr>";

            // Erstellt eine Seite aus den 2 Blöcken
            string darstellung = string.Format(htmltext, gruppenblock, gruppenblock);
            var dokument = GenerierePDFVonHTML(darstellung);
            //PdfSharp.Pdf.PdfPage page = new PdfSharp.Pdf.PdfPage(dokument);


            ////// Fügt die Seite einem neuen PdfDocument an
            //var newDokument = new PdfSharp.Pdf.PdfDocument();

            //var a =newDokument.AddPage(page);

            dokument.Save(@"C://Temp//test.pdf");


        }
    }
}
