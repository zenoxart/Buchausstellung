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
        private WIFI.Anwendung.DTO.Bestellungen _Gesamtbestellungen;

        /// <summary>
        /// Stellt eine Liste aller Bestellungen
        /// </summary>
        public WIFI.Anwendung.DTO.Bestellungen Gesamtbestellungen
        {
            get
            {
                if (this._Gesamtbestellungen == null)
                {
                    this._Gesamtbestellungen =
                        this.AppKontext.DBControllerManager.BestellungController.HoleBestellungen();

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
        private WIFI.Anwendung.DTO.Bestellung _SelektierteBestellung;

        /// <summary>
        /// Ruft die aktuell selektierte Bestellung ab oder legt diese fest
        /// </summary>
        public WIFI.Anwendung.DTO.Bestellung SelektierteBestellung
        {
            get
            {

                if (this._SelektierteBestellung == null)
                {
                    this._SelektierteBestellung = new WIFI.Anwendung.DTO.Bestellung();

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
        private WIFI.Anwendung.DTO.Bücher _BücherDerSelektiertenBestellung;

        /// <summary>
        /// Ruft eine Liste aller Bücher der ausgewählten Bestellung ab oder legt diese fest
        /// </summary>
        public WIFI.Anwendung.DTO.Bücher BücherDerSelektiertenBestellung
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
                     BücherDerSelektiertenBestellung = new WIFI.Anwendung.DTO.Bücher();

                     // Wenn Eine Bestellung ausgewählt ist, nimm dessen Bücher, ansonst eine leere liste

                     if (this.SelektierteBestellung != null && this.SelektierteBestellung.Buchliste != null)
                     {

                         var bücher = new WIFI.Anwendung.DTO.Bücher();

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
                            Dictionary<WIFI.Anwendung.DTO.Buch, int> TempListe = new Dictionary<WIFI.Anwendung.DTO.Buch, int>();

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
        private WIFI.Anwendung.DTO.Buch _SelektiertesBuch;

        /// <summary>
        /// Ruft das zu bearbeitende Buch der Bestellung ab oder legt dieses fest
        /// </summary>
        public WIFI.Anwendung.DTO.Buch SelektiertesBuch
        {
            get
            {
                if (this._SelektiertesBuch == null)
                {
                    this._SelektiertesBuch = new WIFI.Anwendung.DTO.Buch();

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
        /// 
        /// </summary>
        public WIFI.Anwendung.Befehl Lieferabschluss
        {
            get {

                if (this._Lieferanschluss == null)
                {
                    this._Lieferanschluss = new WIFI.Anwendung.Befehl(
                        p => {

                            // TODO: Lieferabschluss
                            // Aktuallisiere alle Bestellungen und Bücher in der Datenbank

                            // Drucke alle Einzelnen Bestellungen neu, welche sich geändert haben

                            // Ändere den Status auf Abholungsverwaltung
                            WIFI.Ausstellung.DBControllerManager.VeranstaltungsController.UpdateVeranstaltungsStadium(Gateway.DTO.AusstellungsstadiumTyp.Abholung);

                            // 20210617 -> Übersiedlung von MySql auf MsSql
                            //this.AppKontext.DBControllerManager.VeranstaltungsController.UpdateVeranstaltungsStadium(WIFI.Anwendung.DTO.AusstellungsstadiumTyp.Abholung);
                            

                            this.OnPropertyChanged();
                        
                        }
                   );
                }
                return this._Lieferanschluss; }
            set { this._Lieferanschluss = value; }
        }


    }
}
