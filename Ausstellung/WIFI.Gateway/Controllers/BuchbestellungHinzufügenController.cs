using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace WIFI.Gateway.Controllers
{

    /// <summary>
    /// Stellt einen REST-API-Controller um eine Bestellung der Datenbank hinzuzufügen
    /// </summary>
    public class BuchbestellungHinzufügenController : Controllers.BasisApiController
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
        /// Ruft den Befehl zum Hinzufügen von
        /// Büchern zu einer Bestellung ab
        /// </summary>
        /// <param name="buch">Daten des Buches</param>
        /// <param name="bestellNr">Interne Nr der Bestellung</param>
        /// <param name="anzahl">Anzahl, wie viele Stück 
        /// des Buches bestellt werden</param>
        /// <returns></returns>
        public object Get(DTO.Buch buch, int bestellNr, int anzahl)
        {
            ClientSqlController.BuchbestellungHinzufügen(buch, bestellNr, anzahl);
            return null;
        }
    }
}