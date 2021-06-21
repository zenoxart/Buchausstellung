using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace WIFI.Gateway.Controllers
{

    /// <summary>
    /// Stellt einen REST-API-Controller zum Erstellen eines Buches
    /// </summary>
    public class ErstelleBuchController : Controllers.BasisApiController
    {
        /// <summary>
        /// Internes Feld für die Eigenschaft
        /// </summary>
        private WIFI.Gateway.Controller.BuchSqlClientController _ClientSqlController;

        /// <summary>
        /// Ruft den Clientcontroller für die Bücher ab 
        /// </summary>
        public WIFI.Gateway.Controller.BuchSqlClientController ClientSqlController
        {
            get
            {
                if (this._ClientSqlController == null)
                {
                    this._ClientSqlController = this.AppKontext.Produziere<WIFI.Gateway.Controller.BuchSqlClientController>();
                }
                return this._ClientSqlController;
            }
        }

        /// <summary>
        /// Übergibt die Daten zum Speichern eines Buches
        /// in der Datenbank
        /// </summary>
        public object Get(int Anzahl, string Autorname, string Buchnummer, int Kategorie, int Rabatt, string Titel, string Verlag, decimal Preis)
        {

            Gateway.DTO.Buch Buch = new DTO.Buch {
                VerlagName = Verlag,
                Kategoriegruppe = Kategorie,
                Rabattgruppe = Rabatt,
                Titel = Titel,
                Anzahl = Anzahl,
                AutorName = Autorname,
                Buchnummer = Convert.ToInt32(Buchnummer),
                Preis = Preis
                
            };

            ClientSqlController.BuchHinzufügen(Buch);
            return null;
        }
    }
}