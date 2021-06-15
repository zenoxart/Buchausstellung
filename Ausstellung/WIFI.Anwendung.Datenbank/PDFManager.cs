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
        public PdfSharp.Pdf.PdfDocument GenerierePDFVonHTML(string htmltext, PdfSharp.PageOrientation orientation)
        {
            // Erstellt die Standardkonfiguration für das Format des PDF's
            TheArtOfDev.HtmlRenderer.PdfSharp.PdfGenerateConfig config = new TheArtOfDev.HtmlRenderer.PdfSharp.PdfGenerateConfig();

            config.PageOrientation = orientation;

            config.PageSize = PdfSharp.PageSize.A4;



            // Erstellt ein lesbareres CSS-Objekt umd die Styles im PDF zu übernehmen
            //TheArtOfDev.HtmlRenderer.Core.CssData myCss =
            //    TheArtOfDev.HtmlRenderer.PdfSharp.PdfGenerator.ParseStyleSheet(
            //        stylesheet: css);


            return TheArtOfDev.HtmlRenderer.PdfSharp.PdfGenerator.GeneratePdf(htmltext, config);

        }

        /// <summary>
        /// Ruft einen HTML-Seitenaufbau im Querformat um 2 A3 Blöcke auf eine A4 Seite zu drucken
        /// </summary>
        public string DoppelSeitenStruktur
        {
            get
            {
                return "<html>" +
              "<body >" +
                  "<table style='width: 100%; border-collapse: collapse;'>" +
                      "<tr style='max-height: 21cm !IMPORTANT'>" +
                          "<td style='width: 11cm !IMPORTANT;background:white; text-align:left'>" +
                              "<p style='margin: 0px 0px 0px 15px; font-size:20pt; text-align:left; font-weight: bold '> Buchausstellung " + this.AktuellesJahr + ", $gemeinde</p>" +
                              "<div style='margin: 0px 0px 10px 10px'>" +
                                  "{0}" +
                              "</div>" +
                          "</td>" +
                          "<td style='width: 11cm !IMPORTANT;background:white; text-align:left'>" +
                              "<p style='margin: 0px 0px 0px 15px; font-size:20pt; text-align:left; font-weight: bold'> Buchausstellung " + this.AktuellesJahr + ", $gemeinde</p>" +
                              "<div style='margin: 0px 0px 10px 10px'>" +
                                  "{1}" +
                              "</div>" +
                          "</td>" +
                      "</tr>" +
                  "</table>" +

              "</body>" +
              "</html>";
            }
        }


        /// <summary>
        /// Ruft einen HTML-Seitenaufbau im Hochformat 1 Text-Block auf eine A4 Seite zu drucken
        /// </summary>
        public string EinzelSeitenStruktur
        {
            get
            {
                return "<html>" +
                            "<body >" +
                                "<table style='width: 100%; border-collapse: collapse;'>" +
                                    "<tr style='max-height: 11cm !IMPORTANT'>" +
                                        "<td style='width: 21cm !IMPORTANT;background:white; text-align:left'>" +
                                            "<p style='margin: 0px 0px 0px 15px; font-size:20pt; text-align:left; font-weight: bold '> Buchausstellung " + this.AktuellesJahr + ", $gemeinde</p>" +
                                            "<div style='margin: 0px 0px 10px 10px'>" +
                                                "{0}" +
                                            "</div>" +
                                        "</td>" +
                                    "</tr>" +
                                "</table>" +

                            "</body>" +
                        "</html>";
            }
        }


        /// <summary>
        /// Gibt das aktuelle Jahr zurück
        /// </summary>
        public int AktuellesJahr
        {
            get { return DateTime.Now.Year; }
        }

        public void GeneriereBestellungenübersicht(DTO.Bestellungen bestellungsliste)
        {
            // Ortne nach Kategoriegruppe
            IEnumerable<IGrouping<int?, DTO.Buch>> Bücherliste = OrdneBestellungenNachKategorie(bestellungsliste);


           
            // Für jede Gruppe
            int gruppenanzahl = 1;
            for (int i = 0; i < gruppenanzahl; i++)
            {
                string EinzelBlock = "<p style='font-size:18pt;margin: 15px 0px 10px 5px '>Gruppe $Zahl - $Name</p>" +
                       "<table style='width: 90%; border-collapse: collapse'>" +
                       "<tr style='border-bottom:inset;border-color:black;border-width: 1px'>" +
                           "<td style='font-weight:bold;text-align:center'> Anzahl </td>" +
                           "<td style='font-weight:bold'> Nr </td>" +
                           "<td style='font-weight:bold'> Titel, Autor </td>" +
                           "<td style='font-weight:bold'> Verlag </td>" +
                           "<td style='font-weight:bold;text-align:right;padding: 0px 10px 0px 0px'> Preis </td>" +
                           "<td style='font-weight:bold;text-align:center'> Rab.Gr </td>" +
                       "</tr>";

                // Für jedes Buch der jeweiligen Gruppe
                foreach (var Buch in Bücherliste.Where(l => l.Key == i))
                {
                    EinzelBlock += "<tr>" +
                            "<td style='text-align: center'>" + Buch.Select(p => p.Anzahl).First() + " </td>" +
                            "<td style='text-align: left'> " + Buch.Select(p => p.Buchnummer).First() + " </td>" +
                            "<td style='text-align: left'> " + Buch.Select(p => p.Titel).First() + ", " + Buch.Select(p => p.AutorName).First() + "</td>" +
                            "<td style='text-align: left'> " + Buch.Select(p => p.VerlagName).First() + " </td>" +
                            "<td style='text-align: right; padding: 0px 5px 0px 0px'> " + Buch.Select(p => p.Preis).First() + " </td>" +
                            "<td style='text-align: center'> " + Buch.Select(p => p.Rabattgruppe).First() + " </td>" +
                        "</tr>";
                }

                EinzelBlock += "</table>";


                // Erstellt eine Seite aus den 2 Blöcken
                string darstellung = string.Format(this.EinzelSeitenStruktur, EinzelBlock);
                var dokument = GenerierePDFVonHTML(darstellung, PdfSharp.PageOrientation.Portrait);

                string dokumentname = "{0}_Bestellungenübersicht_{1}";
                dokument.Save(@"C://Temp//Bestellübersichten//" + string.Format(dokumentname, this.AktuellesJahr, i + 1) + ".pdf");

            }

            //string gruppenblock = "<p style='font-size:18pt;margin: 15px 0px 10px 5px '>Gruppe $Zahl - $Name</p>" +
            //            "<table style='width: 90%; border-collapse: collapse'>" +
            //            "<tr style='border-bottom:inset;border-color:black;border-width: 1px'>" +
            //                "<td style='font-weight:bold;text-align:center'> Anzahl </td>" +
            //                "<td style='font-weight:bold'> Nr </td>" +
            //                "<td style='font-weight:bold'> Titel, Autor </td>" +
            //                "<td style='font-weight:bold'> Verlag </td>" +
            //                "<td style='font-weight:bold;text-align:right;padding: 0px 10px 0px 0px'> Preis </td>" +
            //                "<td style='font-weight:bold;text-align:center'> Rab.Gr </td>" +
            //            "</tr>" +
            //            "<tr>" +
            //                "<td style='text-align: center'> 2 </td>" +
            //                "<td style='text-align: left'> 030 </td>" +
            //                "<td style='text-align: left'> Bimbo, der kilometergroße Elefant, Jäger </td>" +
            //                "<td style='text-align: left'> Gaudete </td>" +
            //                "<td style='text-align: right; padding: 0px 5px 0px 0px'> 17,-- </td>" +
            //                "<td style='text-align: center'> 1 </td>" +
            //            "</tr>" +
            //            "<tr>" +
            //                "<td style='text-align: center'> 5 </td>" +
            //                "<td style='text-align: left'> 010 </td>" +
            //                "<td style='text-align: left'> Peter Pan, Einstein </td>" +
            //                "<td style='text-align: left'> Thalia </td>" +
            //                "<td style='text-align: right; padding: 0px 5px 0px 0px'> 7,-- </td>" +
            //                "<td style='text-align: center'> 0 </td>" +
            //            "</tr>";

            //// Erstellt eine Seite aus den 2 Blöcken
            //string darstellung = string.Format(this.EinzelSeitenStruktur, gruppenblock);

            //var dokument = GenerierePDFVonHTML(darstellung,PdfSharp.PageOrientation.Portrait);
            ////PdfSharp.Pdf.PdfPage page = new PdfSharp.Pdf.PdfPage(dokument);


            //////// Fügt die Seite einem neuen PdfDocument an
            ////var newDokument = new PdfSharp.Pdf.PdfDocument();

            ////var a =newDokument.AddPage(page);

            //dokument.Save(@"C://Temp//test.pdf");


        }


        /// <summary>
        /// Gibt alle Bücher nach der Kategorie gruppiert aus der gesammtBestellungenliste zurück
        /// </summary>
        /// <param name="alleBestellungen">Eine ObservableCollection<DTO.Bestellung> </param>
        public IEnumerable<IGrouping<int?, DTO.Buch>> OrdneBestellungenNachKategorie(DTO.Bestellungen alleBestellungen)
        {
            DTO.Bücher Buchliste = new DTO.Bücher();

            foreach (DTO.Bestellung Bestellung in alleBestellungen)
            {
                foreach (DTO.Buch Buch in Bestellung.Buchliste.Keys)
                {

                    Buchliste.Add(Buch);
                }
            }

            var Büchergruppen = Buchliste.GroupBy(l => l.Kategoriegruppe);

            return Büchergruppen;
        }


    }
}
