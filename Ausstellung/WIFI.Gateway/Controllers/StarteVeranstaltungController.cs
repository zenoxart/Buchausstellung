using System;

namespace WIFI.Gateway.Controllers
{

    /// <summary>
    /// Stellt einen REST-API-Controller zum Starten der Veranstaltung
    /// </summary>
    public class StarteVeranstaltungController : Controllers.BasisApiController
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
        /// Ruft den Befehl zum Starten einer
        /// Veranstaltung in der Datenbank ab
        /// </summary>
        public object Get(DateTime StartDatum, DateTime EndDatum, string Ort)
        {
            ClientSqlController.StarteVeranstaltung(StartDatum, EndDatum, Ort);

            return null;
        }
    }
}