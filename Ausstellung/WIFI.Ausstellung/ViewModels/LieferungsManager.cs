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
                    this.OnPropertyChanged();
                }
            }
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
                    this._BücherDerSelektiertenBestellung = new WIFI.Anwendung.DTO.Bücher();

                    // Wenn Eine Bestellung ausgewählt ist, nimm dessen Bücher, ansonst eine leere liste

                    if (this.SelektierteBestellung != null && this.SelektierteBestellung.Buchliste != null)
                    {

                        var bücher = new WIFI.Anwendung.DTO.Bücher();

                        foreach (var item in this.SelektierteBestellung.Buchliste)
                        {
                            item.Key.Anzahl = item.Value;
                            bücher.Add(item.Key);
                        }

                        this._BücherDerSelektiertenBestellung = bücher;

                    }
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


    }
}
