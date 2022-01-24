using System.Collections.Generic;

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
        private static WIFI.Gateway.DTO.Bücher _Liste = null;

        /// <summary>
        /// Legt die Bücherliste fest
        /// </summary>
        /// <param name="bücher"></param>
        private static void SetListe(Gateway.DTO.Bücher bücher)
        {
            _Liste = bücher;
        }
        /// <summary>
        /// Gibt die Bücherliste zurück
        /// </summary>
        /// <returns></returns>

        private static Gateway.DTO.Bücher GetListe()
        {
            return _Liste;
        }

        /// <summary>
        /// Ruft eine Auflistung aller Bücher, welche bei der Veranstaltung erhältlich sind ab oder legt diese fest
        /// </summary>
        public WIFI.Gateway.DTO.Bücher Buchausstellungsliste
        {
            get
            {
                Gateway.DTO.Bücher liste = GetListe();

                if (liste == null)
                {

                    Buchausstellungsliste = new WIFI.Gateway.DTO.Bücher
                    {
                        new WIFI.Gateway.DTO.Buch{
                            Buchnummer = 0,
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

                return liste;

            }
            set
            {
                SetListe(value);
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
                    // a) Damit WPF mitbekommt, dass sich die Liste
                    //    geändert hat, wird PropertyChanged benötigt
                    // b) Weil kein Thread in die Daten von einem
                    //    anderen Thread greifen darf, nicht hier
                    //    das PropertyChanged. Das muss in der Eigenschaft sein!

                    this.StartProtokollieren();

                    // Läd Bücher asyncron
                    async void HoleBücherAsync()
                    {
                        this.Buchausstellungsliste = await WIFI.Ausstellung.DBControllerManager.BuchController.HoleBücher();
                    }

                    // Triggert das Laden der Bücher
                    HoleBücherAsync();




                    this.EndeProtokollieren();
                    this.InitialisiereAusstellungLäuft = false;
                }
                );

        }
        #endregion





        /// <summary>
        /// Internes Feld für die Eigenschaft
        /// </summary>
        private static WIFI.Gateway.DTO.Bücher _AktuelleBücherbestellung = null;

        /// <summary>
        /// Ruft die aktuell bestellten Bücher ab oder legt diese fest
        /// </summary>
        public static WIFI.Gateway.DTO.Bücher AktuelleBücherbestellung
        {
            get
            {
                if (AusstellungsManager._AktuelleBücherbestellung == null)
                {
                    AusstellungsManager._AktuelleBücherbestellung = new WIFI.Gateway.DTO.Bücher();
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
        private static WIFI.Gateway.DTO.Bestellung _AktuelleBestellung = null;

        /// <summary>
        /// Legt die Aktuelle Bestellung fest
        /// </summary>
        private static void SetAktuelleBestellung(Gateway.DTO.Bestellung bestellung)
        {
            _AktuelleBestellung = bestellung;
        }


        /// <summary>
        /// Gibt die Aktuelle Bestellung zurück
        /// </summary>

        private static Gateway.DTO.Bestellung GetAktuelleBestellung()
        {
            return _AktuelleBestellung;
        }

        /// <summary>
        /// Ruft die Aktuelle Bestellung mit den zugehörigen Benutzerdaten 
        /// und der Bestellnummer sowie dessen Bücher jeweilige Anzahl 
        /// ab oder setzt diese
        /// </summary>
        public static WIFI.Gateway.DTO.Bestellung AktuelleBestellung
        {
            get
            {
                if (GetAktuelleBestellung() == null)
                {
                    SetAktuelleBestellung(new Gateway.DTO.Bestellung());
                }
                return GetAktuelleBestellung();
            }
            set
            {

                if (GetAktuelleBestellung() != value)
                {
                    _AktuelleBestellung = value;

                }
            }
        }

        /// <summary>
        /// Internes Feld für die Eigenschaft
        /// </summary>
        private WIFI.Gateway.DTO.Bestellungen _BestellungenListe = null;

        /// <summary>
        /// Ruft eine Auflistung aller Bestellungen ab oder legt diese fest
        /// </summary>
        public WIFI.Gateway.DTO.Bestellungen BestellungenListe
        {
            get
            {

                if (this._BestellungenListe == null)
                {

                    this._BestellungenListe = new WIFI.Gateway.DTO.Bestellungen();


                    // Aus der Datenbank laden
                    async void Load()
                    {
                        await InitialisiereBestellungenListe();
                    }
                    Load();
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

        /// <summary>
        /// Läd die BestellungenListe Asyncron
        /// </summary>
        private async System.Threading.Tasks.Task InitialisiereBestellungenListe()
        {
            this.BestellungenListe = await
            WIFI.Ausstellung.DBControllerManager.BestellungController.HoleBestellungen();

            foreach (var item in this.BestellungenListe)
            {
                var bücher = await WIFI.Ausstellung.DBControllerManager.BestellungController.HoleBücherZuBestellung(item.BestellNr);

                item.Buchliste = new Dictionary<Gateway.DTO.Buch, int>();

                foreach (var buch in bücher)
                {
                    item.Buchliste.Add(buch, buch.Anzahl);
                }
            }


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
                if (_BestellungHinzufügen != null)
                {
                    return this._BestellungHinzufügen;
                }
                // Den Befehl mit anoymen Methoden initialisieren

                this._BestellungHinzufügen = new WIFI.Anwendung.Befehl(

                    // Werte des Buches in eine Bestellliste hinzufügen
                    p =>
                    {

                        if (AusstellungsManager.AktuelleBestellung == null)
                        {

                            AusstellungsManager.AktuelleBestellung = new WIFI.Gateway.DTO.Bestellung();
                        }


                        AusstellungsManager.AktuelleBestellung.Buchliste = new Dictionary<WIFI.Gateway.DTO.Buch, int>();
                        // Fügt jedes Buch mit der jeweiligen Anzahl der Bestellung hinzu
                        foreach (var Buch in AusstellungsManager.AktuelleBücherbestellung)
                        {
                            AusstellungsManager.AktuelleBestellung.Buchliste.Add(Buch, Buch.Anzahl);
                        }

                        if (BestellBesucher.Nachname == string.Empty
                            || BestellBesucher.Vorname == string.Empty
                            || BestellBesucher.Telefon == string.Empty
                            || BestellBesucher.Postleitzahl == 0
                            || BestellBesucher.Ort == string.Empty
                            || BestellBesucher.Straßenname == string.Empty)
                        {
                            return;
                        }


                        AusstellungsManager.AktuelleBestellung.ZugehörigerBesucher =
                        new WIFI.Gateway.DTO.Besucher
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

                        // 20210617 -> Übersiedlung von MySql auf MsSql

                        Erstelle();

                        async void Erstelle()
                        {
                            AktuelleBestellung.ZugehörigerBesucher = await
                           DBControllerManager.BesucherController.ErstelleBesucher(
                               AktuelleBestellung.ZugehörigerBesucher);

                            int AktuelleBestellNr = await DBControllerManager.BestellungController.ErstelleBestellung(AktuelleBestellung.ZugehörigerBesucher);

                            if (AktuelleBestellNr == -1)
                            {
                                return;
                            }
                            AktuelleBestellung.BestellNr = AktuelleBestellNr;

                            async void Load()
                            {
                                await DBControllerManager.BestellungController.AlleBuchbestellungenHinzufügen(AusstellungsManager.AktuelleBestellung);

                            }
                            Load();
                            // Alles von der Aktuellen Bestellung auf die Datenbank schieben



                            // Aktuelle Bestellung bereinigen
                            AktuelleBestellung = null;
                            AktuelleBücherbestellung = null;

                            BestellBesucher = null;

                            BestellBesucher = new WIFI.Gateway.DTO.Besucher();
                        }
                        BestellungenListe = null;
                    }
                    );

                return this._BestellungHinzufügen;
            }

            set { this._BestellungHinzufügen = value; }
        }



        /// <summary>
        /// Internes Feld für die Eigenschaft
        /// </summary>
        private WIFI.Gateway.DTO.Besucher _BestellBesucher = null;

        /// <summary>
        /// Ruft den aktuell Bestellenden Besucher ab oder legt diesen fest
        /// </summary>
        public WIFI.Gateway.DTO.Besucher BestellBesucher
        {
            get
            {

                if (this._BestellBesucher == null)
                {
                    this._BestellBesucher = new WIFI.Gateway.DTO.Besucher();
                }
                return this._BestellBesucher;
            }
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


                        p =>
                        {
                            var pdfManager = new WIFI.Ausstellung.PdfManager();


                            //Fragen ob die Gesammtbestelliste gedruckt werden soll und dieses machen

                            if (this.Gesamtbestellungendruck)
                            {

                                pdfManager.GeneriereBestellungenübersicht(BestellungenListe);
                            }

                            //Fragen ob die Besucherbestellungen gedruckt werden sollen und dieses machen
                            if (this.Bestellbestätigungendruck)
                            {
                                pdfManager.GeneriereBesucherBestellungen(BestellungenListe);
                            }

                            //am Client alle listen löschen


                            //Stadium auf der Datenbank ändern
                            async void Load()
                            {
                                await DBControllerManager.VeranstaltungsController.UpdateVeranstaltungsStadium(Gateway.DTO.AusstellungsstadiumTyp.Lieferung);

                            }
                            Load();

                            // 20210617 -> Übersiedlung von MySql auf MsSql
                        }
                    );

                }


                return this._AusstellungAbschließen;
            }
            set { this._AusstellungAbschließen = value; }
        }




    }
}
