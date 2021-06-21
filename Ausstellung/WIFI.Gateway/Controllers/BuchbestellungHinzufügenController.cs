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
        public object Get(int Id,string Titel,string Autor, string buchnummr, int kategorie, int rabatt, decimal preis, string verlag, int bestellNr, int anzahl)
        {

            var neuBook = new Gateway.DTO.Buch()
            {
                ID = Id,
                Titel = Titel,
                AutorName = Autor,
                Buchnummer = Convert.ToInt32(buchnummr),
                Kategoriegruppe = kategorie,
                Rabattgruppe = rabatt,
                Preis = preis,
                VerlagName = verlag
            };
            ClientSqlController.BuchbestellungHinzufügen(neuBook, bestellNr, anzahl);
            return null;
        }
    }
}