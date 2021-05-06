using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WIFI.Anwendung.DTO
{
    /// <summary>
    /// Stellt eine Liste aller
    /// erfassten Bestellungen bereit.
    /// </summary>
    public class Bestellungen : System.Collections.ObjectModel.ObservableCollection<Bestellung>
    {

    }


    /// <summary>
    /// Stellt die Daten einer 
    /// einzelnen Bestellung bereit.
    /// </summary>
    public class Bestellung : WIFI.Anwendung.Daten.DatenBasis
    {
        /// <summary>
        /// Internes Feld für die Eigenschaft
        /// </summary>
        private int _ID = 0;

        /// <summary>
        /// Die interne Nummer der Bestellung
        /// </summary>
        public int ID
        {
            get
            {
                return this._ID;
            }
            set
            {
                this._ID = value;
            }
        }

        /// <summary>
        /// Internes Feld für die Eigenschaft
        /// </summary>
        private int _BestellNr = 0;

        /// <summary>
        /// Die Nummer der Bestellung
        /// </summary>
        public int BestellNr
        {
            get
            {
                return this._BestellNr;
            }
            set
            {
                if (this._BestellNr != value)
                {
                    this._BestellNr = value;
                    this.OnPropertyChanged();
                }
            }
        }

        /// <summary>
        /// Internes Feld für die Eigenschaft
        /// </summary>
        private Dictionary<Buch, int> _Buchliste;

        /// <summary>
        /// Ruft eine Auflistung der bestellten Bücher und dessen Anzahl
        /// </summary>
        public Dictionary<Buch, int> Buchliste
        {
            get { return this._Buchliste; }
            set
            {
                if (this._Buchliste != value)
                {
                    this._Buchliste = value;
                    this.OnPropertyChanged();
                }
            }
        }

        /// <summary>
        /// Internes Feld für die Eigenschaft
        /// </summary>
        private Besucher _ZugehörigerBesucher;

        /// <summary>
        /// Ruft den Besucher der Bestellung ab oder legt diesen fest
        /// </summary>
        public Besucher ZugehörigerBesucher
        {
            get { return this._ZugehörigerBesucher; }
            set
            {
                if (this._ZugehörigerBesucher != value)
                {
                    this._ZugehörigerBesucher = value;
                    this.OnPropertyChanged();
                }
            }
        }

    }
}
