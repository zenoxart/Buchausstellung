using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WIFI.Ausstellung.DTO
{
    /// <summary>
    /// Stellt eine Liste aller
    /// vorhandenen Verlage bereit.
    /// </summary>
    public class Verlage : System.Collections.ObjectModel.ObservableCollection<Verlag>
    {

    }

    /// <summary>
    /// Stellt die Daten eines
    /// Verlages bereit.
    /// </summary>
    public class Verlag : WIFI.Anwendung.Daten.DatenBasis
    {
        /// <summary>
        /// Internes Feld für die Eigenschaft
        /// </summary>
        private int _ID = 0;

        /// <summary>
        /// Die interne Nummer des Verlages
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
        private string _Name = string.Empty;
        
        /// <summary>
        /// Der Name des Verlages
        /// </summary>
        public string Name
        {
            get
            {
                return this._Name;
            }
            set
            {
                if (this._Name != value)
                {
                    this._Name = value;
                    this.OnPropertyChanged();
                }
            }
        }
    }
}
