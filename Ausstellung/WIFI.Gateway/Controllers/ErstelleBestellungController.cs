using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace WIFI.Gateway.Controllers
{

    /// <summary>
    /// Stellt einen REST-API-Controller zum Erstellen einer Bestellung
    /// </summary>
    public class ErstelleBestellungController : Controllers.BasisApiController
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
        /// Befehl zum Anlegen einer Bestellung
        /// in der Datenbank
        /// </summary>
        public int Get(int Id, string Vorname,string Nachname,int Hausnummer,string Ort,int PLZ,string Straßenname,string Telefon)
        {

            var neu = new Gateway.DTO.Besucher {
            Id = Id,
            Vorname = Vorname,
            Nachname = Nachname,
            Hausnummer = Hausnummer,
            Ort = Ort,
            Postleitzahl = PLZ,
            Straßenname = Straßenname,
            Telefon = Telefon
            };
            return ClientSqlController.ErstelleBestellung(neu);   
        }
    }
}