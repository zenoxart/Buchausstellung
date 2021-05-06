using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WIFI.Ausstellung.DTO
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
        private string _Name = string.Empty;

        /// <summary>
        /// Ruft den Namen eines Besuchers ab oder legt diesen fest
        /// </summary>
        public string Name
        {
            get { return this._Name; }
            set {

                if (this._Name != value)
                {
                    this._Name = value;
                    this.OnPropertyChanged();
                }
            }
        }

        /// <summary>
        /// Internes Feld für die Eigenschaft
        /// </summary>
        private string _Anschrift = string.Empty;

        /// <summary>
        /// Ruft die Anschrift des Besuchers ab oder legt diese fest
        /// </summary>
        public string Anschrift
        {
            get { return this._Anschrift; }
            set {
                if (this._Anschrift != value)
                {
                    this._Anschrift = value;
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
