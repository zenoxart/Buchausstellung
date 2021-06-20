using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace WIFI.Gateway.Controllers
{

    /// <summary>
    /// Stellt einen REST-API-Controller zum Erstellen eines Besuchers
    /// </summary>
    public class ErstelleBesucherController : Controllers.BasisApiController
    {
        /// <summary>
        /// Internes Feld für die Eigenschaft
        /// </summary>
        private WIFI.Gateway.Controller.BesucherSqlClientController _ClientSqlController;

        /// <summary>
        /// Ruft den Clientcontroller für die Besucher ab 
        /// </summary>
        public WIFI.Gateway.Controller.BesucherSqlClientController ClientSqlController
        {
            get
            {
                if (this._ClientSqlController == null)
                {
                    this._ClientSqlController = this.AppKontext.Produziere<WIFI.Gateway.Controller.BesucherSqlClientController>();
                }
                return this._ClientSqlController;
            }
        }

        /// <summary>
        /// Befehl zum Erstellen eines
        /// Besuchers in der Datenbank
        /// </summary>
        public DTO.Besucher Get(string Vorname, string Nachname, int Hausnummer, string Ort, int PLZ, string Straßenname, string Telefon)
        {

            var neu = new Gateway.DTO.Besucher
            {
                Vorname = Vorname,
                Nachname = Nachname,
                Hausnummer = Hausnummer,
                Ort = Ort,
                Postleitzahl = PLZ,
                Straßenname = Straßenname,
                Telefon = Telefon
            };
            return ClientSqlController.ErstelleBesucher(neu);  
        }
    }
}