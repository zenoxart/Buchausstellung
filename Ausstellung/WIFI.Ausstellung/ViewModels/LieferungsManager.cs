using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WIFI.Ausstellung.ViewModels
{
    /// <summary>
    /// Stellt einen Dienst zum Verwalten der Lieferungen
    /// </summary>
    public class LieferungsManager : WIFI.Anwendung.ViewModelAppObjekt
    {
        /// <summary>
        /// Internes Feld für die Eigenschaft
        /// </summary>
        private WIFI.Gateway.DTO.Bestellungen _Gesamtbestellungen = null;

        /// <summary>
        /// Stellt eine Liste aller Bestellungen
        /// </summary>
        public WIFI.Gateway.DTO.Bestellungen Gesamtbestellungen
        {
            get
            {
                if (this._Gesamtbestellungen == null)
                {
                    async void Load()
                    {
                        this.Gesamtbestellungen = await
                            WIFI.Ausstellung.DBControllerManager.BestellungController.HoleBestellungen();

                        foreach (var item in this.Gesamtbestellungen)
                        {
                            var bücher = await WIFI.Ausstellung.DBControllerManager.BestellungController.HoleBücherZuBestellung(item.BestellNr);

                            item.Buchliste = new Dictionary<Gateway.DTO.Buch, int>();

                            foreach (var buch in bücher)
                            {
                                item.Buchliste.Add(buch, buch.Anzahl);
                            }
                        }


                        //var bestellungen = new Gateway.DTO.Bestellungen();
                        //foreach (var item in this.Gesamtbestellungen)
                        //{
                        //    bestellungen.Add(await WIFI.Ausstellung.DBControllerManager.BestellungController.HoleBestellung(item.BestellNr));
                        //}

                        //this.Gesamtbestellungen = bestellungen;

                    }
                    Load();
                    //this._Gesamtbestellungen =
                    //    this.AppKontext.DBControllerManager.BestellungController.HoleBestellungen();

                    // Aus der Datenbank holen
                    //this._Gesamtbestellungen = 
                }
                return this._Gesamtbestellungen;
            }
            set
            {
                if (this._Gesamtbestellungen != value)
                {
                    this._Gesamtbestellungen = value;
                    this.OnPropertyChanged();
                }
            }
        }


        /// <summary>
        /// Internes Feld für die Eigenschaft
        /// </summary>
        private WIFI.Gateway.DTO.Bestellung _SelektierteBestellung = null;

        /// <summary>
        /// Ruft die aktuell selektierte Bestellung ab oder legt diese fest
        /// </summary>
        public WIFI.Gateway.DTO.Bestellung SelektierteBestellung
        {
            get
            {

                if (this._SelektierteBestellung == null)
                {
                    this._SelektierteBestellung = new WIFI.Gateway.DTO.Bestellung();

                    //Wenn eine Bestellungsliste existiert
                }
                return this._SelektierteBestellung;
            }
            set
            {

                if (this._SelektierteBestellung != value)
                {
                    this._SelektierteBestellung = value;
                    HoleSelektierteBuchBestellungAsync();
                    this.OnPropertyChanged();
                }
            }
        }

        /// <summary>
        /// Internes Feld für die Eigenschaft
        /// </summary>
        private WIFI.Anwendung.Befehl _BuchanzahlAktuallisieren = null;

        /// <summary>
        /// Aktualisiert die Anzahl des Buches
        /// </summary>
        public WIFI.Anwendung.Befehl BuchanzahlAktuallisieren
        {
            get
            {
                if (this._BuchanzahlAktuallisieren == null)
                {
                    this._BuchanzahlAktuallisieren = new WIFI.Anwendung.Befehl(
                        p =>
                        {

                            this.SelektierteBestellung.Geändert = true;
                            PusheSelektierteBuchBestellungAsync();
                        }
                    );
                }

                return this._BuchanzahlAktuallisieren;
            }
            set { this._BuchanzahlAktuallisieren = value; }
        }


        /// <summary>
        /// Internes Feld für die Eigenschaft
        /// </summary>
        private WIFI.Gateway.DTO.Bücher _BücherDerSelektiertenBestellung = null;

        /// <summary>
        /// Ruft eine Liste aller Bücher der ausgewählten Bestellung ab oder legt diese fest
        /// </summary>
        public WIFI.Gateway.DTO.Bücher BücherDerSelektiertenBestellung
        {
            get
            {

                if (this._BücherDerSelektiertenBestellung == null)
                {
                    HoleSelektierteBuchBestellungAsync();
                }
                return this._BücherDerSelektiertenBestellung;
            }
            set
            {
                if (this._BücherDerSelektiertenBestellung != value)
                {

                    this._BücherDerSelektiertenBestellung = value;
                    this.OnPropertyChanged();
                }
            }
        }

        /// <summary>
        /// Läd die Bücher der Selektierten Bestellung Asyncron
        /// </summary>
        protected async void HoleSelektierteBuchBestellungAsync()
        {
            await System.Threading.Tasks.Task.Run(
                 () =>
                 {
                     BücherDerSelektiertenBestellung = new WIFI.Gateway.DTO.Bücher();

                     // Wenn Eine Bestellung ausgewählt ist, nimm dessen Bücher, ansonst eine leere liste

                     if (this.SelektierteBestellung != null && this.SelektierteBestellung.Buchliste != null)
                     {
                         // Call HoleBücherZuBestellungsInfo
                         

                         var bücher = new WIFI.Gateway.DTO.Bücher();

                         foreach (var item in this.SelektierteBestellung.Buchliste)
                         {
                             item.Key.Anzahl = item.Value;
                             bücher.Add(item.Key);
                         }

                         this.BücherDerSelektiertenBestellung = bücher;

                     }
                 });

        }

        /// <summary>
        /// Übergibt die BücherDerSelektiertenBestellung der selektierten Bestellung asyncron
        /// </summary>
        protected async void PusheSelektierteBuchBestellungAsync()
        {
            await System.Threading.Tasks.Task.Run(
                () =>
                {

                    if (BücherDerSelektiertenBestellung != null)
                    {
                        if (BücherDerSelektiertenBestellung.Count > 0)
                        {
                            Dictionary<WIFI.Gateway.DTO.Buch, int> TempListe = new Dictionary<WIFI.Gateway.DTO.Buch, int>();

                            foreach (var item in BücherDerSelektiertenBestellung)
                            {
                                TempListe.Add(item, item.Anzahl);
                            }

                            this.SelektierteBestellung.Buchliste = TempListe;
                        }
                    }


                });
        }


        /// <summary>
        /// Internes Feld für die Eigenschaft
        /// </summary>
        private WIFI.Gateway.DTO.Buch _SelektiertesBuch = null;

        /// <summary>
        /// Ruft das zu bearbeitende Buch der Bestellung ab oder legt dieses fest
        /// </summary>
        public WIFI.Gateway.DTO.Buch SelektiertesBuch
        {
            get
            {
                if (this._SelektiertesBuch == null)
                {
                    this._SelektiertesBuch = new WIFI.Gateway.DTO.Buch();

                    // Wenn eine Bestellung ausgewählt ist und die Bücherliste nicht null ist


                }
                return this._SelektiertesBuch;
            }
            set
            {
                if (this._SelektiertesBuch != value)
                {
                    this._SelektiertesBuch = value;
                    this.OnPropertyChanged();
                }
            }
        }

        /// <summary>
        /// Internes feld für die Eigenschaft
        /// </summary>
        private WIFI.Anwendung.Befehl _Lieferanschluss = null;

        /// <summary>
        /// Schließt das Stadium Lieferung ab und Speichert die Änderungen 
        /// </summary>
        public WIFI.Anwendung.Befehl Lieferabschluss
        {
            get
            {

                if (this._Lieferanschluss == null)
                {
                    this._Lieferanschluss = new WIFI.Anwendung.Befehl(
                        p =>
                        {

                            // Lieferabschluss
                            // Aktuallisiere alle Bestellungen und Bücher in der Datenbank
                            var neuZuDruckende = new Gateway.DTO.Bestellungen();

                            foreach (Gateway.DTO.Bestellung item in this.Gesamtbestellungen)
                            {
                                WIFI.Ausstellung.DBControllerManager.BestellungController.AktualisiereBestellung(item);
                                // Wenn das Element geändert wurde, drucke es neu
                                if (item.Geändert)
                                {
                                    neuZuDruckende.Add(item);
                                }

                            }
                            if (neuZuDruckende.Count != 0)
                            {
                                // Drucke alle Einzelnen Bestellungen neu, welche sich geändert haben
                                var pdfManager = new WIFI.Ausstellung.PDFManager();
                                pdfManager.GeneriereBesucherBestellungen(neuZuDruckende);
                            }


                            // Ändere den Status auf Abholungsverwaltung
                            WIFI.Ausstellung.DBControllerManager.VeranstaltungsController.UpdateVeranstaltungsStadium(Gateway.DTO.AusstellungsstadiumTyp.Abholung);

                            // 20210617 -> Übersiedlung von MySql auf MsSql
                            //this.AppKontext.DBControllerManager.VeranstaltungsController.UpdateVeranstaltungsStadium(WIFI.Anwendung.DTO.AusstellungsstadiumTyp.Abholung);


                            this.OnPropertyChanged();

                        }
                   );
                }
                return this._Lieferanschluss;
            }
            set { this._Lieferanschluss = value; }
        }


    }
}
