namespace WIFI.Gateway.Controllers
{
    /// <summary>
    /// Stellt einen REST-API-Controller zum Ändern der Buchgruppe
    /// </summary>
    public class AktualisiereBuchgruppeController : Controllers.BasisApiController
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
        /// Aktualisiert den Eintrag einer 
        /// Buchgruppe in der Datenbank
        /// </summary>
        /// <param name="id">Interne ID 
        /// der Buchgruppe</param>
        /// <returns></returns>
        public string Get(DTO.Buchgruppe id)
        {
            ClientSqlController.AktualisiereBuchgruppe(id);
            return null;
        }

    }
}