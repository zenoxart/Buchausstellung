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
        /// Ruft den Controller zum Verwalten der Veranstaltungsdaten 
        /// ab 
        /// </summary>
        public static WIFI.Ausstellung.Models.RestApiController.VeranstaltungWebController VeranstaltungsController
        {
            get
            {
                return new Models.RestApiController.VeranstaltungWebController();
            }
        }


        /// <summary>
        /// Ruft den Controller zum Verwalten der Besucherdaten 
        /// ab 
        /// </summary>
        public static WIFI.Ausstellung.Models.RestApiController.BesucherWebController BesucherController
        {
            get
            {
                return new WIFI.Ausstellung.Models.RestApiController.BesucherWebController();
            }
        }

        /// <summary>
        /// Ruft den Controller zum Verwalten der Bestellungsdaten 
        /// ab 
        /// </summary>
        public static WIFI.Ausstellung.Models.RestApiController.BestellungWebController BestellungController
        {
            get
            {
                return new Models.RestApiController.BestellungWebController();
            }
        }


        /// <summary>
        /// Ruft den Controller zum Verwalten der Daten zu den Buchgruppen 
        /// ab 
        /// </summary>
        public static WIFI.Ausstellung.Models.RestApiController.BuchgruppenWebController BuchgruppenController
        {
            get
            {
                return new Models.RestApiController.BuchgruppenWebController();
            }
        }

        /// <summary>
        /// Ruft den Controller zum Verwalten der Buchdaten 
        /// ab 
        /// </summary>
        public static WIFI.Ausstellung.Models.RestApiController.BuchWebController BuchController
        {
            get
            {
                return new Models.RestApiController.BuchWebController();
            }
        }

    }
}
