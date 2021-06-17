using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WIFI.Ausstellung
{
    /// <summary>
    /// Stellt einen Dienst zum Verwalten von Datenbank-Controllern
    /// </summary>
    public class DBControllerManager : WIFI.Anwendung.ViewModelAppObjekt
    {
        /// <summary>
        /// Internes Feld für die Eigenschaft
        /// </summary>
        private WIFI.Gateway.Controller.VeranstaltungsSqlClientController _VeranstaltungsController;

        /// <summary>
        /// Ruft den Controller zum Verwalten der Veranstaltungsdaten 
        /// ab oder legt diesen fest
        /// </summary>
        public WIFI.Gateway.Controller.VeranstaltungsSqlClientController VeranstaltungsController
        {
            get
            {

                if (this._VeranstaltungsController == null)
                {
                    this._VeranstaltungsController = this.AppKontext.Produziere<WIFI.Gateway.Controller.VeranstaltungsSqlClientController>();
                }

                return this._VeranstaltungsController;
            }
            set { this._VeranstaltungsController = value; }
        }

    }
}
