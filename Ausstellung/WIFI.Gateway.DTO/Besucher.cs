using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WIFI.Gateway.DTO
{
    /// <summary>
    /// Stellt die Information eines
    /// Besuchers der Ausstellung bereit.
    /// </summary>
    public class Besucher : WIFI.Anwendung.Daten.DatenBasis
    {
        /// <summary>
        /// Internes Feld für die Eigenschaft
        /// </summary>
        private int _Id;

        /// <summary>
        /// Ruft die Id eines Besuchers ab oder legt diese fest
        /// </summary>
        public int Id
        {
            get { return this._Id; }
            set
            {
                if (this._Id != value)
                {
                    this._Id = value;
                    this.OnPropertyChanged();
                }
            }
        }

        /// <summary>
        /// Internes Feld für die Eigenschaft
        /// </summary>
        private string _Vorname = string.Empty;

        /// <summary>
        /// Ruft den Vornamen eines Besuchers ab oder legt diesen fest
        /// </summary>
        public string Vorname
        {
            get { return this._Vorname; }
            set
            {

                if (this._Vorname != value)
                {
                    this._Vorname = value;
                    this.OnPropertyChanged();
                }
            }
        }

        /// <summary>
        /// Internes Feld für die Eigenschaft
        /// </summary>
        private string _Nachname = string.Empty;

        /// <summary>
        /// Ruft den Nachnamen eines Besuchers ab oder legt diesen feset
        /// </summary>
        public string Nachname
        {
            get { return this._Nachname; }
            set
            {
                if (this._Nachname != value)
                {

                    this._Nachname = value;
                    this.OnPropertyChanged();
                }
            }
        }


        /// <summary>
        /// Internes Feld für die Eigenschaft
        /// </summary>
        private int _Hausnummer = 0;

        /// <summary>
        /// Ruft die Hausnummer des Besuchers ab oder legt diese fest
        /// </summary>
        public int Hausnummer
        {
            get { return this._Hausnummer; }
            set
            {
                if (this._Hausnummer != value)
                {
                    this._Hausnummer = value;
                    this.OnPropertyChanged();
                }
            }
        }

        /// <summary>
        /// Internes Feld für die Eigenschaft
        /// </summary>
        private string _Straßenname;

        /// <summary>
        /// Ruft den Namen der Straße des Besuchers ab oder legt diese fest
        /// </summary>
        public string Straßenname
        {
            get { return this._Straßenname; }
            set
            {
                if (this._Straßenname != value)
                {
                    this._Straßenname = value;
                    this.OnPropertyChanged();
                }
            }
        }


        /// <summary>
        /// Internes Feld für die Eigenschaft
        /// </summary>
        private string _Ort = string.Empty;

        /// <summary>
        /// Ruft den Ort des Besuchers ab oder legt diesen fest
        /// </summary>
        public string Ort
        {
            get { return this._Ort; }
            set
            {
                if (this._Ort != value)
                {

                    this._Ort = value;
                    this.OnPropertyChanged();
                }
            }
        }

        /// <summary>
        /// Internes Feld für die Eigenschaft
        /// </summary>
        private int _Postleitzahl = 0;

        /// <summary>
        /// Ruft die Postleitzahl eines Besuchers ab oder legt diese fest
        /// </summary>
        public int Postleitzahl
        {
            get { return this._Postleitzahl; }
            set
            {
                if (this._Postleitzahl != value)
                {
                    this._Postleitzahl = value;
                    this.OnPropertyChanged();
                }
            }
        }



        /// <summary>
        /// Internes Feld für die Eigenschaft
        /// </summary>
        private string _Telefon;

        /// <summary>
        /// Ruft die Telefonnummer des Besuchers ab oder legt diese fest
        /// </summary>
        public string Telefon
        {
            get { return this._Telefon; }
            set
            {
                if (this._Telefon != value)
                {
                    this._Telefon = value;
                    this.OnPropertyChanged();
                }
            }
        }
    }
}
