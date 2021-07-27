namespace WIFI.Gateway.Controllers
{

    /// <summary>
    /// Stellt einen REST-API-Controller zum Entfernen der Buchgruppe
    /// </summary>
    public class EntferneBuchgruppeController : Controllers.BasisApiController
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
        /// Entfernt eine Buchgruppe aus der Datenbank
        /// </summary>
        /// <param name="Gruppennr">Nummer
        /// der Buchgruppe</param>
        /// <param name="Beschreibung">Beschreibung
        /// der Buchgruppe</param>
        /// <returns></returns>
        public string Get(int Gruppennr,string Beschreibung)
        {
            Gateway.DTO.Buchgruppe buchgruppe = new DTO.Buchgruppe() { Gruppennummer = Gruppennr, Beschreibung = Beschreibung };
            ClientSqlController.EntferneBuchgruppe(buchgruppe);
            return null;
        }
    }
}