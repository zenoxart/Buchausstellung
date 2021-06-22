using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace WIFI.Gateway.Controllers
{

    /// <summary>
    /// Stellt einen REST-API-Controller zum Laden aller Bestellungen
    /// </summary>
    public class HoleBestellungenController : Controllers.BasisApiController
    {
        /// <summary>
        /// Internes Feld für die Eigenschaft
        /// </summary>
        private WIFI.Gateway.Controller.BestellungSqlClientController _ClientSqlController;

        /// <summary>
        /// Ruft den Clientcontroller für die Bestellungen ab 
        /// </summary>
        public WIFI.Gateway.Controller.BestellungSqlClientController ClientSqlController
        {
            get
            {
                if (this._ClientSqlController == null)
                {
                    this._ClientSqlController = this.AppKontext.Produziere<WIFI.Gateway.Controller.BestellungSqlClientController>();
                }
                return this._ClientSqlController;
            }
        }

        /// <summary>
        /// Ruft den Befehl zum Abrufen aller
        /// Bestellungen aus der Datenbank ab
        /// </summary>
        /// <returns></returns>
        public Gateway.DTO.Bestellungen Get()
        {
            return ClientSqlController.HoleBestellungen();
        }

        /// <summary>
        /// Ruft die Bücher zu der Bestellung der bestellNr ab
        /// </summary>
        public Gateway.DTO.Bücher Get(int bestellNr)
        {
            return ClientSqlController.HoleBücherZuBestellung(bestellNr);
        }


    }
}