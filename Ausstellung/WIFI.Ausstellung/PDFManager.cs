using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WIFI.Ausstellung
{
    /// <summary>
    /// Stellt einen Dienst aufgrund des Nuget-Paketes PDF-Sharp 
    /// zum Konvertieren von HTML-Seiten in PDF-Dateien
    /// </summary>
    public class PdfManager : WIFI.Anwendung.ViewModelAppObjekt
    {
        #region Basis
        public PdfSharp.Pdf.PdfDocument GenerierePDFVonHTML(string htmltext, PdfSharp.PageOrientation orientation)
        {
            // Erstellt die Standardkonfiguration für das Format des PDF's
            TheArtOfDev.HtmlRenderer.PdfSharp.PdfGenerateConfig config = new TheArtOfDev.HtmlRenderer.PdfSharp.PdfGenerateConfig
            {
                PageOrientation = orientation,

                PageSize = PdfSharp.PageSize.A4
            };

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
                              "<p style='margin: 0px 0px 0px 15px; font-size:20pt; text-align:left; font-weight: bold '> Buchausstellung " + this.AktuellesJahr + ", " + this.Gemeinde + "</p>" +
                              "<div style='margin: 0px 0px 10px 10px'>" +
                                  "{0}" +
                              "</div>" +
                          "</td>" +
                          "<td style='width: 11cm !IMPORTANT;background:white; text-align:left'>" +
                              "<p style='margin: 0px 0px 0px 15px; font-size:20pt; text-align:left; font-weight: bold'> Buchausstellung " + this.AktuellesJahr + ", " + this.Gemeinde + "</p>" +
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
        /// Gibt das Trennzeichen eines Dateipfades an
        /// </summary>
        private const string PathDelimiter = "\\";


        /// <summary>
        /// Internes Feld für die Eigenschaft
        /// </summary>
        private string _Gemeinde = string.Empty;
        /// <summary>
        /// Ruft den Ort der Veranstaltung ab
        /// </summary>
        public string Gemeinde
        {
            get
            {
                if (this._Gemeinde == string.Empty)
                {
                    async void Load()
                    {
                        this.Gemeinde = await WIFI.Ausstellung.DBControllerManager.VeranstaltungsController.HoleGemeinde();

                    }
                    Load();
                }


                return this._Gemeinde;
            }
            set
            {
                this._Gemeinde = value;
                this.OnPropertyChanged();
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
                                            "<p style='margin: 0px 0px 0px 15px; font-size:20pt; text-align:left; font-weight: bold '> Buchausstellung " + this.AktuellesJahr + ", " + this.Gemeinde + "</p>" +
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

        #endregion

        #region PDFs generieren
        /// <summary>
        /// Erstellt eine Sammlung an PDFs aus allen Bestellten büchern nach der gruppe sortiert
        /// </summary>
        /// <param name="bestellungsliste"></param>
        public void GeneriereBestellungenübersicht(WIFI.Gateway.DTO.Bestellungen bestellungsliste)
        {
            // Ortne nach Kategoriegruppe
            var Bücherliste = OrdneBestellungsBücherNachKategorie(bestellungsliste);

            Gateway.DTO.Buchgruppen buchgruppes = new Gateway.DTO.Buchgruppen();
            // Hole Alle Kategorien
            async void LoadGruppen()
            {
                buchgruppes = await
                WIFI.Ausstellung.DBControllerManager.BuchgruppenController.HoleBuchgruppen();
            }
            LoadGruppen();

            // Funktioniert nicht zu 100%
            try
            {

                // Für jede Gruppe
                if (Bücherliste.Count > 0)
                {
                    for (int i = 0; i < Bücherliste.Count; i++)
                    {
                        var strB = new StringBuilder("<p style='font-size:18pt;margin: 15px 0px 10px 5px '>Gruppe " +
                                buchgruppes.FirstOrDefault(x => x.ID == i).Gruppennummer + " - " +
                                buchgruppes.FirstOrDefault(x => x.ID == i).Beschreibung + "</p>" +
                               "<table style='width: 90%; border-collapse: collapse'>" +
                               "<tr style='border-bottom:inset;border-color:black;border-width: 1px'>" +
                                   "<td style='font-weight:bold;text-align:center'> Anzahl </td>" +
                                   "<td style='font-weight:bold'> Nr </td>" +
                                   "<td style='font-weight:bold'> Titel, Autor </td>" +
                                   "<td style='font-weight:bold'> Verlag </td>" +
                                   "<td style='font-weight:bold;text-align:right;padding: 0px 10px 0px 0px'> Preis </td>" +
                                   "<td style='font-weight:bold;text-align:center'> Rab.Gr </td>" +
                               "</tr>");

                        // Für jedes Buch der jeweiligen Gruppe
                        foreach (var Buch in Bücherliste[i])
                        {
                            strB.Append( "<tr>" +
                                    "<td style='text-align: center'>" +
                                        Buch.Anzahl + " </td>" +
                                    "<td style='text-align: left'> " + Buch.Buchnummer + " </td>" +
                                    "<td style='text-align: left'> " + Buch.Titel + ", " + Buch.AutorName + "</td>" +
                                    "<td style='text-align: left'> " + Buch.VerlagName + " </td>" +
                                    "<td style='text-align: right; padding: 0px 5px 0px 0px'> " + Buch.Preis + " </td>" +
                                    "<td style='text-align: center'> " + Buch.Rabattgruppe + " </td>" +
                                "</tr>");
                        }

                        strB.Append( "</table>");

                        // Erstellt eine Seite aus den 2 Blöcken
                        string darstellung = string.Format(this.EinzelSeitenStruktur, strB.ToString());
                        PdfSharp.Pdf.PdfDocument dokument = GenerierePDFVonHTML(darstellung, PdfSharp.PageOrientation.Portrait);

                        string dokumentname = "{0}_Bestellungenübersicht_{1}";
                        string path = Properties.Settings.Default.Gesamtbestelllistenpfad + PathDelimiter;
                        dokument.Save(
                             $"{path}{string.Format(dokumentname, AktuellesJahr, i + 1)}.pdf");
                    }
                }
            }
            catch (Exception e)
            {
                this.AppKontext.Protokoll.Eintragen("Fehler aufgetaucht beim generieren der PDFs" + e.Message);
            }
        }

        /// <summary>
        /// Erstellt PDFs an Bestellisten von einzelnen Besuchern.
        /// Einzel A4-Seite soll Vertikal 2 Bestellungen darstellen können
        /// </summary>
        public void GeneriereBesucherBestellungen(WIFI.Gateway.DTO.Bestellungen bestellungsliste)
        {
            List<BesucherZuBestellung> OrdneNachReferenz()
            {
                List<BesucherZuBestellung> reff = new List<BesucherZuBestellung>();
                var alleBestellungen = OrdneBestellungNachBesucher(bestellungsliste).ToList();

                WIFI.Gateway.DTO.Besucher zuvorigerBesucher = new WIFI.Gateway.DTO.Besucher();

                for (int i = 0; i < alleBestellungen.Count; i++)
                {
                    if (zuvorigerBesucher != alleBestellungen[i].ZugehörigerBesucher)
                    {
                        zuvorigerBesucher = alleBestellungen[i].ZugehörigerBesucher;
                        reff.Add(new BesucherZuBestellung { ZugehörigerBesucher = zuvorigerBesucher });
                    }

                    var eintrag = reff.Last(x => x.ZugehörigerBesucher == zuvorigerBesucher);

                    eintrag.Liste = new Gateway.DTO.Bücher();
                    foreach (var alleBücher in alleBestellungen[i].Buchliste.Keys.ToList())
                    {
                        eintrag.Liste.Add(alleBücher);
                    }


                }

                return reff;
            }

            var referenzen = OrdneNachReferenz();




            int counter = 0;
            string[] arr = new string[2];
            int secCounter = 0;
            foreach (var besucherBuch in referenzen)
            {
                // Nur 2 Blöcke immer nebeneinander


                string besucher = string.Format("{0},{1}", besucherBuch.ZugehörigerBesucher.Vorname, besucherBuch.ZugehörigerBesucher.Nachname);


                var strB = new StringBuilder("<p style='margin: 10px'> Besucher: " + besucher + " </p>" +
                            "<table style='width: 90%; border-collapse: collapse'>" +
                            "<tr style='border-bottom:inset;border-color:black;border-width: 1px'>" +
                                "<td style='font-weight:bold;text-align:center'> Anzahl </td>" +
                                "<td style='font-weight:bold'> Nr </td>" +
                                "<td style='font-weight:bold'> Titel, Autor </td>" +
                                "<td style='font-weight:bold'> Verlag </td>" +
                                "<td style='font-weight:bold;text-align:left;padding: 0px 10px 0px 0px'>  </td>" +
                                "<td style='font-weight:bold;text-align:right'> </td>" +
                            "</tr>");

                foreach (var buch in besucherBuch.Liste)
                {
                    strB.Append("<tr>" +
                                        "<td style='text-align: center'> " + buch.Anzahl + " </td>" +
                                        "<td style='text-align: left'> " + buch.Buchnummer + " </td>" +
                                        "<td style='text-align: left'> " + buch.Titel + " , " + buch.AutorName + "</td>" +
                                        "<td style='text-align: left'> " + buch.VerlagName + "</td>" +
                                        "<td style='text-align: left; padding: 0px 5px 0px 0px'>  </td>" +
                                        "<td style='text-align: right'>  </td>" +
                                   "</tr>");
                }

                strB.Append("</table>");

                arr[counter] = strB.ToString();



                counter++;
                if (counter % 2 == 0)
                {
                    counter = 0;

                    string druckdarstellung = string.Format(this.DoppelSeitenStruktur, arr[0], arr[1]);

                    PdfSharp.Pdf.PdfDocument dokument = GenerierePDFVonHTML(druckdarstellung, PdfSharp.PageOrientation.Landscape);
                    // 
                    string dokumentname = "{0}_Bestellung_{1}";
                    string path = $"{Properties.Settings.Default.Bestellbestätigungenpfad}"+ PathDelimiter;

                    dokument.Save(
                         path + string.Format(
                             dokumentname,
                             this.AktuellesJahr,
                             secCounter +
                             ".pdf"));


                    arr = new string[2] { string.Empty, string.Empty };
                }
                secCounter++;

            }

            // Wenn noch 1 Person übrig bleibt
            if (counter > 0)
            {
                var druckdarstellung = string.Format(this.DoppelSeitenStruktur, arr[0], "");
                var dokument = GenerierePDFVonHTML(druckdarstellung, PdfSharp.PageOrientation.Landscape);

                string dokumentname = "{0}_Bestellung_{1}";
                dokument.Save(
                        Properties.Settings.Default.Gesamtbestelllistenpfad + PathDelimiter + string.Format(
                            dokumentname,
                            this.AktuellesJahr,
                            secCounter +
                            ".pdf"));

            }
        }
        #endregion

        #region Ordnen
        /// <summary>
        /// Sortiert die Bestellungen nach dem Besucher
        /// </summary>
        public IOrderedEnumerable<WIFI.Gateway.DTO.Bestellung> OrdneBestellungNachBesucher(WIFI.Gateway.DTO.Bestellungen alleBestellungen)
        {
            IOrderedEnumerable<WIFI.Gateway.DTO.Bestellung> sortiert = alleBestellungen.OrderBy(k => k.ZugehörigerBesucher.Id);

            return sortiert;
        }


        /// <summary>
        /// Gibt alle Bücher nach der Kategorie gruppiert aus der gesammtBestellungenliste zurück
        /// </summary>
        /// <param name="alleBestellungen">Eine ObservableCollection<DTO.Bestellung> </param>
        public Dictionary<int, Gateway.DTO.Bücher> OrdneBestellungsBücherNachKategorie(WIFI.Gateway.DTO.Bestellungen alleBestellungen)
        {
            WIFI.Gateway.DTO.Bücher Buchliste = new WIFI.Gateway.DTO.Bücher();
            foreach (Gateway.DTO.Buch Buch in from WIFI.Gateway.DTO.Bestellung Bestellung in alleBestellungen
                                 from WIFI.Gateway.DTO.Buch Buch in Bestellung.Buchliste.Keys
                                 select Buch)
            {
                Buchliste.Add(Buch);
            }

            // KategorieId, Bücher der Kategorie
            Dictionary<int, Gateway.DTO.Bücher> outva = new Dictionary<int, Gateway.DTO.Bücher>();


            for (int i = 0; i < Buchliste.Max(x => x.Kategoriegruppe); i++)
            {

                Gateway.DTO.Bücher liste = new Gateway.DTO.Bücher();
                if (Buchliste.Count((x => x.Kategoriegruppe == i)) > 0)
                {
                    foreach (var item in Buchliste.Where(x => x.Kategoriegruppe == i))
                    {
                        liste.Add(item);
                    }

                    if (liste.Count > 0)
                    {
                        outva.Add(i, liste);
                    }

                }
            }




            return outva;

        }
        #endregion

    }

    /// <summary>
    /// Stellt eine Verbindung eines Besuchers und seinen Bestellungen
    /// </summary>
    public class BesucherZuBestellung : WIFI.Anwendung.Daten.DatenBasis
    {
        /// <summary>
        /// Internes Feld für die Eigenschaft
        /// </summary>
        private WIFI.Gateway.DTO.Besucher _ZugehörigerBesucher = null;

        /// <summary>
        /// Ruft den Besucher zu der Bücherliste ab oder legt diesen fest
        /// </summary>
        public WIFI.Gateway.DTO.Besucher ZugehörigerBesucher
        {
            get { return this._ZugehörigerBesucher; }
            set
            {
                this._ZugehörigerBesucher = value;
                this.OnPropertyChanged();
            }
        }

        /// <summary>
        /// Internes Feld für die Eigenschaft
        /// </summary>
        private WIFI.Gateway.DTO.Bücher _Liste = null;

        /// <summary>
        /// Ruft dendie Bücherliste zu dem zugehörigen Besucher ab oder legt diesen fest
        /// </summary>
        public WIFI.Gateway.DTO.Bücher Liste
        {
            get { return this._Liste; }
            set
            {
                this._Liste = value;
                this.OnPropertyChanged();
            }
        }

    }
}
