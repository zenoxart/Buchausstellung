using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace WIFI.Gateway.Controllers
{
    /// <summary>
    /// Stellt einen REST-API-Controller zum Laden des Gemeindenamens
    /// </summary>
    public class HoleGemeindeController : Controllers.BasisApiController
    {
        /// <summary>
        /// Internes Feld für die Eigenschaft
        /// </summary>
        private WIFI.Gateway.Controller.VeranstaltungSqlClientController _ClientSqlController;

        /// <summary>
        /// Ruft den Clientcontroller für die Veranstaltung ab 
        /// </summary>
        public WIFI.Gateway.Controller.VeranstaltungSqlClientController ClientSqlController
        {
            get
            {
                if (this._ClientSqlController == null)
                {
                    this._ClientSqlController = this.AppKontext.Produziere<WIFI.Gateway.Controller.VeranstaltungSqlClientController>();
                }
                return this._ClientSqlController;
            }
        }

        /// <summary>
        /// Gibt den Namen der Gemeinde
        /// aus der Datenbank zurück
        /// </summary>
        public string Get()
        {
            return ClientSqlController.HoleGemeinde(); 
        }
    }
}