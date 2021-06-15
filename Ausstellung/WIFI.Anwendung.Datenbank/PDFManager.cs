﻿using System;
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

        /// <summary>
        /// Erstellt eine Sammlung an PDFs aus allen Bestellten büchern nach der gruppe sortiert
        /// </summary>
        /// <param name="bestellungsliste"></param>
        public void GeneriereBestellungenübersicht(DTO.Bestellungen bestellungsliste)
        {
            // Ortne nach Kategoriegruppe
            IEnumerable<IGrouping<int?, DTO.Buch>> Bücherliste = OrdneBestellungsBücherNachKategorie(bestellungsliste);



            // Für jede Gruppe
            if (Bücherliste.Count() > 0)
            {
                for (int i = 0; i < Bücherliste.Count(); i++)
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
                                "<td style='text-align: center'>" +
                                    Buch.Select(
                                        p => p.Anzahl).First() + " </td>" +
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
        /// Erstellt PDFs an Bestellisten von einzelnen Besuchern.
        /// Einzel A4-Seite soll Vertikal 2 Bestellungen darstellen können
        /// </summary>
        public void GeneriereBesucherBestellungen(DTO.Bestellungen bestellungsliste)
        {
            List<BesucherZuBestellung> OrdneNachReferenz()
            {
                List<BesucherZuBestellung> reff = new List<BesucherZuBestellung>();
                var alleBestellungen = OrdneBestellungNachBesucher(bestellungsliste).ToList();

                DTO.Besucher zuvorigerBesucher = new DTO.Besucher();

                for (int i = 0; i < alleBestellungen.Count(); i++)
                {
                    if (zuvorigerBesucher != alleBestellungen[i].ZugehörigerBesucher)
                    {
                        zuvorigerBesucher = alleBestellungen[i].ZugehörigerBesucher;
                        reff.Add(new BesucherZuBestellung { ZugehörigerBesucher = zuvorigerBesucher });
                    }

                    var eintrag = reff.Last(x => x.ZugehörigerBesucher == zuvorigerBesucher);
                    foreach (var alleBücher in alleBestellungen[i].Buchliste.Keys.ToList())
                    {
                        eintrag.Liste.Add(alleBücher);
                    }


                }

                return reff;
            }

            var referenzen = OrdneNachReferenz();


            // TODO: Nico fragen
            // Für jeden Besucher

            foreach (var besucherBuch in referenzen)
            {
                // Nur 2 Blöcke immer nebeneinander
                //string.Format(DoppelSeitenStruktur, block1, block2);

                string besucher = string.Format("{0},{1}", besucherBuch.ZugehörigerBesucher.Vorname, besucherBuch.ZugehörigerBesucher.Nachname);



                string gruppenblock = "<p style='font-size:18pt;margin: 15px 0px 10px 5px '>Gruppe $Zahl - $Name</p>" +
                            "<table style='width: 90%; border-collapse: collapse'>" +
                            "<tr style='border-bottom:inset;border-color:black;border-width: 1px'>" +
                                "<td style='font-weight:bold;text-align:center'> Anzahl </td>" +
                                "<td style='font-weight:bold'> Nr </td>" +
                                "<td style='font-weight:bold'> Titel, Autor </td>" +
                                "<td style='font-weight:bold'> Verlag </td>" +
                                "<td style='font-weight:bold;text-align:right;padding: 0px 10px 0px 0px'> Preis </td>" +
                                "<td style='font-weight:bold;text-align:center'> Rab.Gr </td>" +
                            "</tr>";

                foreach (var buch in besucherBuch.Liste)
                {
                    gruppenblock += "<tr>" +
                                        "<td style='text-align: center'> " + buch.Anzahl + " </td>" +
                                        "<td style='text-align: left'> " + buch.Buchnummer + " </td>" +
                                        "<td style='text-align: left'> " + buch.Titel + " , " + buch.AutorName + "</td>" +
                                        "<td style='text-align: left'> " + buch.VerlagName + "</td>" +
                                        "<td style='text-align: right; padding: 0px 5px 0px 0px'> " + buch.Preis + " </td>" +
                                        "<td style='text-align: center'> " + buch.Rabattgruppe + " </td>" +
                                   "</tr>";
                }

                gruppenblock += "</table>";



                //if (counter % 2 == 0)
                //{
                //    counter = 1;
                //}
                //else
                //{
                //    counter++;
                //}


            }
            // Für jedes Buch




        }

        public IOrderedEnumerable<DTO.Bestellung> OrdneBestellungNachBesucher(DTO.Bestellungen alleBestellungen)
        {
            IOrderedEnumerable<DTO.Bestellung> sortiert = alleBestellungen.OrderBy(k => k.ZugehörigerBesucher.Id);

            return sortiert;
        }


        /// <summary>
        /// Gibt alle Bücher nach der Kategorie gruppiert aus der gesammtBestellungenliste zurück
        /// </summary>
        /// <param name="alleBestellungen">Eine ObservableCollection<DTO.Bestellung> </param>
        public IEnumerable<IGrouping<int?, DTO.Buch>> OrdneBestellungsBücherNachKategorie(DTO.Bestellungen alleBestellungen)
        {
            DTO.Bücher Buchliste = new DTO.Bücher();

            foreach (DTO.Bestellung Bestellung in alleBestellungen)
            {
                foreach (DTO.Buch Buch in Bestellung.Buchliste.Keys)
                {

                    Buchliste.Add(Buch);
                }
            }




            //var Büchergruppen = Buchliste.GroupBy(l => l.Kategoriegruppe);

            return (from d in Buchliste group d by d.Kategoriegruppe);

        }


    }

    /// <summary>
    /// Stellt eine Verbindung eines Besuchers und seinen Bestellungen
    /// </summary>
    public class BesucherZuBestellung : WIFI.Anwendung.Daten.DatenBasis
    {

        private DTO.Besucher _ZugehörigerBesucher;

        public DTO.Besucher ZugehörigerBesucher
        {
            get { return _ZugehörigerBesucher; }
            set
            {
                _ZugehörigerBesucher = value;
                this.OnPropertyChanged();
            }
        }



        private DTO.Bücher _Liste;

        public DTO.Bücher Liste
        {
            get { return _Liste; }
            set
            {
                _Liste = value;
                this.OnPropertyChanged();
            }
        }

    }
}
