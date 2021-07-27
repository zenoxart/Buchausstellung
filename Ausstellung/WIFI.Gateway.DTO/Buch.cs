
using System.Collections.Generic;

namespace WIFI.Gateway.DTO
{
    /// <summary>
    /// Stellt eine Liste aller
    /// vorhandenen Bücher bereit.
    /// </summary>
    public class Bücher : System.Collections.ObjectModel.ObservableCollection<Buch>
    {

    }

    /// <summary>
    /// Stellt die Information eines
    /// Buches der Ausstellung bereit.
    /// </summary>
    public class Buch : WIFI.Anwendung.Daten.DatenBasis
    {
        /// <summary>
        /// Ruft die interne Nummer des Buches ab,
        /// oder legt diese fest
        /// </summary>
        public int ID { get; set; } = 0;

        /// <summary>
        /// Internes Feld für die Eigenschaft
        /// </summary>
        private int _Buchnummer = 0;

        /// <summary>
        /// Ruft den Nummerierung des Buches ab,
        /// oder legt diesen fest
        /// </summary>
        public int Buchnummer
        {
            get
            {
                return this._Buchnummer;
            }
            set
            {
                if (this._Buchnummer != value)
                {
                    this._Buchnummer = value;
                    this.OnPropertyChanged();
                }
            }
        }

        /// <summary>
        /// Internes Feld für die Eigenschaft
        /// </summary>
        private string _Titel = string.Empty;

        /// <summary>
        /// Ruft den Titel des Buches ab,
        /// oder legt diesen fest
        /// </summary>
        public string Titel
        {
            get
            {
                return this._Titel;
            }
            set
            {
                if (this._Titel != value)
                {
                    this._Titel = value;
                    this.OnPropertyChanged();
                }
            }
        }

        /// <summary>
        /// Internes Feld für die Eigenschaft
        /// </summary>
        private string _AutorName = string.Empty;

        /// <summary>
        /// Ruft den Namen des Autors des Buches ab,
        /// oder legt diesen fest
        /// </summary>
        public string AutorName
        {
            get
            {
                return this._AutorName;
            }
            set
            {
                if (this._AutorName != value)
                {
                    this._AutorName = value;
                    this.OnPropertyChanged();
                }
            }
        }

        /// <summary>
        /// Internes Feld für die Eigenschaft
        /// </summary>
        private string _VerlagName = string.Empty;

        /// <summary>
        /// Ruft den Namen des Verlages des Buches ab,
        /// oder legt diesen fest
        /// </summary>
        public string VerlagName
        {
            get
            {
                return this._VerlagName;
            }
            set
            {
                if (this._VerlagName != value)
                {
                    this._VerlagName = value;
                    this.OnPropertyChanged();
                }
            }
        }

        /// <summary>
        /// Internes Feld für die Eigenschaft
        /// </summary>
        private decimal? _Preis = 0;

        /// <summary>
        /// Ruft den Preis des Buches ab,
        /// oder legt diesen fest
        /// </summary>
        public decimal? Preis
        {
            get
            {
                return this._Preis;
            }
            set
            {
                if (this.Preis != value)
                {
                    this._Preis = value;
                    this.OnPropertyChanged();
                }
            }
        }

        /// <summary>
        /// Internes Feld für die Eigenschaft
        /// </summary>
        private int _Kategoriegruppe = 0;

        /// <summary>
        /// Ruft die Kategorie des Buches ab,
        /// oder legt diese fest
        /// </summary>
        public int Kategoriegruppe
        {
            get
            {
                return this._Kategoriegruppe;
            }
            set
            {
                if (this._Kategoriegruppe != value)
                {
                    this._Kategoriegruppe = value;
                    this.OnPropertyChanged();
                }
            }
        }

        /// <summary>
        /// Internes Feld für die Eigenschaft
        /// </summary>
        private int _Rabattgruppe = 0;

        /// <summary>
        /// Ruft die Rabattgruppe des Buches ab,
        /// oder legt diese fest
        /// </summary>
        public int Rabattgruppe
        {
            get
            {
                return this._Rabattgruppe;
            }
            set
            {
                if (this._Rabattgruppe != value)
                {
                    this._Rabattgruppe = value;
                    this.OnPropertyChanged();
                }
            }
        }

        /// <summary>
        /// Internes Feld für die Eigenschaft
        /// </summary>
        private int _Anzahl = 1;

        /// <summary>
        /// Ruft die Anzahl der zu bestellenden Bücher ab oder legt diese fest
        /// </summary>
        public int Anzahl
        {
            get
            {
                return this._Anzahl;
            }
            set
            {
                if (this._Anzahl != value)
                {
                    this._Anzahl = value;
                    this.OnPropertyChanged();
                }
            }
        }


    }
}
