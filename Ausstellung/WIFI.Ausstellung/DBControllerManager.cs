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
        private WIFI.Ausstellung.Models.RestApiController.VeranstaltungWebController _VeranstaltungsController;

        /// <summary>
        /// Ruft den Controller zum Verwalten der Veranstaltungsdaten 
        /// ab 
        /// </summary>
        public WIFI.Ausstellung.Models.RestApiController.VeranstaltungWebController VeranstaltungsController
        {
            get
            {

                if (this._VeranstaltungsController == null)
                {
                    this._VeranstaltungsController = this.AppKontext.Produziere<WIFI.Ausstellung.Models.RestApiController.VeranstaltungWebController>();
                }

                return this._VeranstaltungsController;
            }
        }


        /// <summary>
        /// Internes Feld für die Eigenschaft
        /// </summary>
        private WIFI.Ausstellung.Models.RestApiController.BesucherWebController _BesucherController;

        /// <summary>
        /// Ruft den Controller zum Verwalten der Besucherdaten 
        /// ab 
        /// </summary>
        public WIFI.Ausstellung.Models.RestApiController.BesucherWebController BesucherController
        {
            get {
                if (this._BesucherController == null)
                {
                    this._BesucherController = this.AppKontext.Produziere<WIFI.Ausstellung.Models.RestApiController.BesucherWebController>();
                }
                return this._BesucherController; }
        }


    }
}
