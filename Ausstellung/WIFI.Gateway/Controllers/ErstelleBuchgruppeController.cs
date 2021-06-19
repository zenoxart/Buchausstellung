using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace WIFI.Gateway.Controllers
{
    /// <summary>
    /// Stellt einen REST-API-Controller zum Erstellen einer Buchgruppe
    /// </summary>
    public class ErstelleBuchgruppeController : Controllers.BasisApiController
    {
        /// <summary>
        /// Internes Feld für die Eigenschaft
        /// </summary>
        private WIFI.Gateway.Controller.BuchgruppeSqlClientController _ClientSqlController;

        /// <summary>
        /// Ruft den Clientcontroller für die Buchgruppen ab 
        /// </summary>
        public WIFI.Gateway.Controller.BuchgruppeSqlClientController ClientSqlController
        {
            get
            {
                if (this._ClientSqlController == null)
                {
                    this._ClientSqlController = this.AppKontext.Produziere<WIFI.Gateway.Controller.BuchgruppeSqlClientController>();
                }
                return this._ClientSqlController;
            }
        }

        /// <summary>
        /// Befehl zum Erstellen einer
        /// Buchgruppe in der Datenbank
        /// </summary>
        /// <param name="buchgruppe">Daten der Buchgruppe</param>
        public void Get(DTO.Buchgruppe buchgruppe)
        {
            ClientSqlController.BuchgruppeHinzufügen(buchgruppe);
        }
    }
}