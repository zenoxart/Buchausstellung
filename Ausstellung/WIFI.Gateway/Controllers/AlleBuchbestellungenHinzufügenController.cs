using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace WIFI.Gateway.Controllers
{

    /// <summary>
    /// Stellt einen REST-API-Controller zum Hinzufügen aller Bestellungen
    /// </summary>
    public class AlleBuchbestellungenHinzufügenController : Controllers.BasisApiController
    {

        /// <summary>
        /// Internes Feld für die Eigenschaft
        /// </summary>
        private WIFI.Gateway.Controller.BestellungSqlClientController _ClientSqlController;

        /// <summary>
        /// Ruft den Clientcontroller für die Bestellung ab 
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
        /// Ruft den Befehl zum Hinzufügen der
        /// Bücher zu einer Bestellung ab
        /// </summary>
        /// <param name="Bestellung">Interne ID der Bestellung</param>
        /// <returns></returns>
        public object Get(Gateway.DTO.Bestellung Bestellung)
        {
            ClientSqlController.AlleBuchbestellungenHinzufügen(Bestellung);
            return null;
        }

      
    }
}