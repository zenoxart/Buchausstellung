using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WIFI.Ausstellung.ViewModels
{
    /// <summary>
    /// Stellt einen Dienst zum Verwalten
    /// der Buch-Ausstellung bereit
    /// </summary>
    public class AusstellungsManager : WIFI.Anwendung.ViewModelAppObjekt
    {

        #region AusstellungView
        /// <summary>
        /// Internes Feld für die Eigenschaft
        /// </summary>
        private static WIFI.Anwendung.DTO.Bücher _Liste = null;

        /// <summary>
        /// Ruft eine Auflistung aller Bücher, welche bei der Veranstaltung erhältlich sind ab oder legt diese fest
        /// </summary>
        public WIFI.Anwendung.DTO.Bücher Buchausstellungsliste
        {
            get
            {
                if (AusstellungsManager._Liste == null)
                {

                    Buchausstellungsliste = new WIFI.Anwendung.DTO.Bücher
                    {
                        new WIFI.Anwendung.DTO.Buch{
                            Buchnummer = "0",
                            Titel="Buchtitel werden geladen...",
                            AutorName="Bitte warten.",
                            ID = 0,
                            Kategoriegruppe = 0,
                            Rabattgruppe= 0,
                            Preis = 0,
                            VerlagName = ""
                        }
                    };

                    //  Liste muss Asyncon Initialisiert werden
                    InitialisiereAusstellungAsync();

                }

                return AusstellungsManager._Liste;

            }
            set
            {
                AusstellungsManager._Liste = value;
                this.OnPropertyChanged();
            }
        }


        /// <summary>
        /// Ruft einen Wahrheitswert ab,
        /// ob das Initialisieren der Ausstelltung 
        /// aktuell läuft oder nicht
        /// </summary>
        private bool InitialisiereAusstellungLäuft { get; set; }

        /// <summary>
        /// Füllt die Liste mit den Aufgaben
        /// asynchron in einem eigenen Thread
        /// </summary>
        protected virtual async void InitialisiereAusstellungAsync()
        {
            // Damit die Thread-Methode nicht mehrmals gestartet wird
            if (this.InitialisiereAusstellungLäuft)
            {
                return;
            }

            await System.Threading.Tasks.Task.Run(
                () =>
                {
                    this.InitialisiereAusstellungLäuft = true;




                    // SO NIE, nicht mit dem Feld!!!
                    //this._Liste = this.Controller.HoleAusRessourcen();
                    // a) Damit WPF mitbekommt, dass sich die Liste
                    //    geändert hat, wird PropertyChanged benötigt
                    // b) Weil kein Thread in die Daten von einem
                    //    anderen Thread greifen darf, nicht hier
                    //    das PropertyChanged. Das muss in der Eigenschaft sein!

                    this.StartProtokollieren();

                    System.Threading.Thread.Sleep(7000);
                    //this.Buchausstellungsliste = this.Controller.HoleAusRessourcen();

                    this.Buchausstellungsliste = this.AppKontext.DBControllerManager.BücherController.HoleBücher();


                    //this.Buchausstellungsliste = new WIFI.Anwendung.DTO.Bücher();

                    //this.Buchausstellungsliste.Add(
                    //    new WIFI.Anwendung.DTO.Buch { ID = 1, Titel = "Harry Potter", AutorName = "J. K. Rowling", VerlagName = "Thalia", Kategoriegruppe = 0, Preis = 15.30, Rabattgruppe = 1 }

                    //);


                    this.EndeProtokollieren();
                    this.InitialisiereAusstellungLäuft = false;
                }
                );

        }
        #endregion





        /// <summary>
        /// Internes Feld für die Eigenschaft
        /// </summary>
        private static WIFI.Anwendung.DTO.Bücher _AktuelleBücherbestellung = null;

        /// <summary>
        /// Ruft die aktuell bestellten Bücher ab oder legt diese fest
        /// </summary>
        public static WIFI.Anwendung.DTO.Bücher AktuelleBücherbestellung
        {
            get
            {
                if (AusstellungsManager._AktuelleBücherbestellung == null)
                {
                    AusstellungsManager._AktuelleBücherbestellung = new WIFI.Anwendung.DTO.Bücher();
                }
                return AusstellungsManager._AktuelleBücherbestellung;
            }
            set
            {
                if (AusstellungsManager._AktuelleBücherbestellung != value)
                {

                    AusstellungsManager._AktuelleBücherbestellung = value;

                }

                // Da statische Eigenschaften nicht OnPropertyChanged aufrufen können, wird das gesammte Layout geupdatet


            }
        }

        /// <summary>
        /// Internes Feld für die Eigenschaft
        /// </summary>
        private static WIFI.Anwendung.DTO.Bestellung _AktuelleBestellung = null;

        /// <summary>
        /// Ruft die Aktuelle Bestellung mit den zugehörigen Benutzerdaten 
        /// und der Bestellnummer sowie dessen Bücher jeweilige Anzahl 
        /// ab oder setzt diese
        /// </summary>
        public static WIFI.Anwendung.DTO.Bestellung AktuelleBestellung
        {
            get
            {
                if (AusstellungsManager._AktuelleBestellung == null)
                {
                    AusstellungsManager._AktuelleBestellung = new WIFI.Anwendung.DTO.Bestellung();
                }
                return AusstellungsManager._AktuelleBestellung;
            }
            set
            {

                if (AusstellungsManager._AktuelleBestellung != value)
                {

                    AusstellungsManager._AktuelleBestellung = value;

                }
            }
        }

        /// <summary>
        /// Internes Feld für die Eigenschaft
        /// </summary>
        private WIFI.Anwendung.DTO.Bestellungen _BestellungenListe = null;

        /// <summary>
        /// Ruft eine Auflistung aller Bestellungen ab oder legt diese fest
        /// </summary>
        public WIFI.Anwendung.DTO.Bestellungen BestellungenListe
        {
            get
            {

                if (this._BestellungenListe == null)
                {

                    this._BestellungenListe = new WIFI.Anwendung.DTO.Bestellungen();


                    // Aus der Datenbank laden
                    InitialisiereBestellungenListe();
                }
                return this._BestellungenListe;
            }
            set
            {
                if (this._BestellungenListe != value)
                {
                    this._BestellungenListe = value;

                    this.OnPropertyChanged();
                }
            }
        }

        private void InitialisiereBestellungenListe()
        {
            this.BestellungenListe =
                this.AppKontext.DBControllerManager.BestellungController.HoleBestellungen();
        }


        /// <summary>
        /// Internes Feld für die Eigenschaft
        /// </summary>
        private WIFI.Anwendung.Befehl _BestellungHinzufügen = null;

        /// <summary>
        /// Fügt die AktuelleBücherbestellungsliste mit der AktuellenBestellung zusammen
        /// </summary>
        public WIFI.Anwendung.Befehl BestellungHinzufügen
        {
            get
            {
                if (this._BestellungHinzufügen == null)
                {
                    // Den Befehl mit anoymen Methoden initialisieren

                    this._BestellungHinzufügen = new WIFI.Anwendung.Befehl(

                        // TODO: Werte des Buches in eine Bestellliste hinzufügen
                        p =>
                        {

                            if (AusstellungsManager.AktuelleBestellung == null)
                            {

                                AusstellungsManager.AktuelleBestellung = new WIFI.Anwendung.DTO.Bestellung();
                            }


                            AusstellungsManager.AktuelleBestellung.Buchliste = new Dictionary<WIFI.Anwendung.DTO.Buch, int>();
                            // Fügt jedes Buch mit der jeweiligen Anzahl der Bestellung hinzu
                            foreach (var Buch in AusstellungsManager.AktuelleBücherbestellung)
                            {
                                AusstellungsManager.AktuelleBestellung.Buchliste.Add(Buch, Buch.Anzahl);
                            }

                            if (this.BestellBesucher.Nachname != string.Empty
                                && this.BestellBesucher.Vorname != string.Empty
                                && BestellBesucher.Telefon != string.Empty
                                && BestellBesucher.Postleitzahl != 0
                                && BestellBesucher.Ort != string.Empty
                                && BestellBesucher.Straßenname != string.Empty)
                            {

                                // Erstellt einen Zugehörigen Besucher (TODO: Noch OHNE ID)
                                AusstellungsManager.AktuelleBestellung.ZugehörigerBesucher =
                                    new WIFI.Anwendung.DTO.Besucher
                                    {
                                        Hausnummer = this.BestellBesucher.Hausnummer,
                                        Vorname = this.BestellBesucher.Vorname,
                                        Nachname = this.BestellBesucher.Nachname,
                                        Ort = this.BestellBesucher.Ort,
                                        Postleitzahl = this.BestellBesucher.Postleitzahl,
                                        Straßenname = this.BestellBesucher.Straßenname,
                                        Telefon = this.BestellBesucher.Telefon
                                    };

                                // Erstelle neuen Besucher oder lade dessen ID von der Datenbank

                                AusstellungsManager.AktuelleBestellung.ZugehörigerBesucher =
                                    this.AppKontext.DBControllerManager.BesucherController.ErstelleBesucher(
                                        AusstellungsManager.AktuelleBestellung.ZugehörigerBesucher);


                                int AktuelleBestellNr =
                                    this.AppKontext.DBControllerManager.BestellungController.ErstelleBestellung(
                                        AusstellungsManager.AktuelleBestellung.ZugehörigerBesucher);

                                if (AktuelleBestellNr != -1)
                                {
                                    AusstellungsManager.AktuelleBestellung.BestellNr = AktuelleBestellNr;

                                    // Alles von der Aktuellen Bestellung auf die Datenbank schieben
                                    this.AppKontext.DBControllerManager.BestellungController.AlleBuchbestellungenHinzufügen(
                                        AusstellungsManager.AktuelleBestellung);

                                    // 20210514 -> Kasper, bei den Bestellungen wird beim Aufruf die Liste von der Datenbank abgefragt,
                                    // desshalb muss hier nichtmehr die Bestellung der BestellungenListe hinzugefügt werden
                                    // Aktuelle Bestellung zu der Bestellungsliste hinzufügen
                                    //AusstellungsManager.BestellungenListe.Add(AusstellungsManager.AktuelleBestellung);


                                    // Aktuelle Bestellung bereinigen
                                    AusstellungsManager.AktuelleBestellung = null;
                                    AusstellungsManager.AktuelleBücherbestellung = null;

                                    this.BestellBesucher = null;

                                    this.BestellBesucher = new WIFI.Anwendung.DTO.Besucher();



                                }

                            }
                        }
                        );
                }

                return this._BestellungHinzufügen;
            }

            set { this._BestellungHinzufügen = value; }
        }



        /// <summary>
        /// Internes Feld für die Eigenschaft
        /// </summary>
        private WIFI.Anwendung.DTO.Besucher _BestellBesucher = null;

        /// <summary>
        /// Ruft den aktuell Bestellenden Besucher ab oder legt diesen fest
        /// </summary>
        public WIFI.Anwendung.DTO.Besucher BestellBesucher
        {
            get {

                if (this._BestellBesucher == null)
                {
                    this._BestellBesucher = new WIFI.Anwendung.DTO.Besucher();
                }
                return this._BestellBesucher; }
            set
            {
                if (this._BestellBesucher != value)
                {
                    this._BestellBesucher = value;
                    this.OnPropertyChanged();
                }
            }
        }



        /// <summary>
        /// Internes Feld für die Eigenschaft
        /// </summary>
        private bool _Gesamtbestellungendruck = false;

        /// <summary>
        /// Ruft den Wert ob eine Gesamtbestellung gedruckt werden soll ab oder legt diese fest
        /// </summary>
        public bool Gesamtbestellungendruck
        {
            get { return this._Gesamtbestellungendruck; }
            set
            {
                this._Gesamtbestellungendruck = value;
                this.OnPropertyChanged();
            }
        }

        /// <summary>
        /// Internes Feld für die Eigenschaft
        /// </summary>
        private bool _Bestellbestätigungendruck = false;

        /// <summary>
        /// Ruft den Wert ob die Bestellbestätigungen gedruckt werden sollen ab oder legt diese fest
        /// </summary>
        public bool Bestellbestätigungendruck
        {
            get { return this._Bestellbestätigungendruck; }
            set
            {
                this._Bestellbestätigungendruck = value;
                this.OnPropertyChanged();
            }
        }

        /// <summary>
        /// Internes Feld für die Eigenschaft
        /// </summary>
        private WIFI.Anwendung.Befehl _AusstellungAbschließen = null;

        /// <summary>
        /// Beendet die Ausstellungsphase und speichert die PDF's
        /// </summary>
        public WIFI.Anwendung.Befehl AusstellungAbschließen
        {
            get
            {

                if (this._AusstellungAbschließen == null)
                {
                    // Den Befehl mit anoymen Methoden initialisieren

                    this._AusstellungAbschließen = new WIFI.Anwendung.Befehl(

                        // TODO: Abschluss der Bestellung umsetzen
                        p =>
                        {
                            //Fragen ob die Gesammtbestelliste gedruckt werden soll und dieses machen


                            //Fragen ob die Besucherbestellungen gedruckt werden sollen und dieses machen


                            //am Client alle listen löschen


                            //Stadium auf der Datenbank ändern
                        }
                    );

                }


                return this._AusstellungAbschließen;
            }
            set { this._AusstellungAbschließen = value; }
        }




    }
}
