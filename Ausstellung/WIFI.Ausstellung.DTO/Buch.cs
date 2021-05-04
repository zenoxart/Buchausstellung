using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WIFI.Ausstellung.DTO
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
        /// Internes Feld für die Eigenschaft
        /// </summary>
        private int _ID = 0;

        /// <summary>
        /// Ruft die interne Nummer des Buches ab,
        /// oder legt diese fest
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
        private int _AutorID = 0;

        /// <summary>
        /// Ruft die interne ID des Autors des Buches ab,
        /// oder legt diese fest
        /// </summary>
        public int AutorID
        {
            get
            {
                return this._AutorID;
            }
            set
            {
                if (this._AutorID != value)
                {
                    this._AutorID = value;
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
                this._AutorName = value;
            }
        }

        /// <summary>
        /// Internes Feld für die Eigenschaft
        /// </summary>
        private int _VerlagID = 0;

        /// <summary>
        /// Ruft die interne Nr des Verlags des Buches ab,
        /// oder legt diese fest
        /// </summary>
        public int VerlagID
        {
            get
            {
                return this._VerlagID;
            }
            set
            {
                if (this._VerlagID != value)
                {
                    this._VerlagID = value;
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
                this._VerlagName = value;
            }
        }

        /// <summary>
        /// Internes Feld für die Eigenschaft
        /// </summary>
        private decimal _Preis = 0m;

        /// <summary>
        /// Ruft den Preis des Buches ab,
        /// oder legt diesen fest
        /// </summary>
        public decimal Preis
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
    }
}
