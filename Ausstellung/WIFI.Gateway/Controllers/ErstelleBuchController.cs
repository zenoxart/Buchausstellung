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
        /// <param name="Anzahl">Die Stückzahl des Buches</param>
        /// <param name="Autorname">Der Autor des Buches</param>
        /// <param name="Buchnummer">Die Nummer des Buches</param>
        /// <param name="Kategorie">Die Buchkategorie des Buches</param>
        /// <param name="Rabatt">Die Rabattgruppe des Buches</param>
        /// <param name="Titel">Der Titel des Buches</param>
        /// <param name="Verlag">Der Verlag des Buches</param>
        /// <returns></returns>
        public object Get(int Anzahl, string Autorname, string Buchnummer, int? Kategorie, int? Rabatt, string Titel, string Verlag)
        {

            Gateway.DTO.Buch Buch = new DTO.Buch {
                VerlagName = Verlag,
                Kategoriegruppe = Kategorie,
                Rabattgruppe = Rabatt,
                Titel = Titel,
                Anzahl = Anzahl,
                AutorName = Autorname,
                Buchnummer = Buchnummer
                
            };

            ClientSqlController.BuchHinzufügen(Buch);
            return null;
        }
    }
}