using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WIFI.Ausstellung.ViewModels
{
    public class VeranstaltungsManager : WIFI.Anwendung.ViewModelAppObjekt
    {
        /// <summary>
        /// Ruft einen Wahrheitswert ab oder legt diesen fest,
        /// ob die Anwendung das dunkle Design benutzen soll
        /// </summary>
        public bool DunklesDesign
        {
            get
            {
                return Properties.Settings.Default.DunklesDesign;
            }
            set
            {
                if (value != Properties.Settings.Default.DunklesDesign)
                {
                    Properties.Settings.Default.DunklesDesign = value;
                    // Es wird davon ausgegangen, dass die
                    // Einstellung im Main() gespeichert wird
                }

                // Weil die Anwendung mehrere Fenster haben kann,
                // egal ob der Wert geändert wurde oder nicht...
                this.OnPropertyChanged();
            }
        }

        /// <summary>
        /// Internes Feld für die Eigenschaft
        /// </summary>
        private WIFI.Anwendung.Befehl _ErstelleVeranstaltung = null;

        /// <summary>
        /// Ruft den Befehl zum Speichern
        /// einer neuen Veranstaltung ab
        /// </summary>
        public WIFI.Anwendung.Befehl ErstelleVeranstaltung
        {
            get
            {
                if (this._ErstelleVeranstaltung == null)
                {

                }

                return this._ErstelleVeranstaltung;
            }
        }
    }
}
