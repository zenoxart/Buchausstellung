namespace WIFI.Gateway.Controllers
{

    /// <summary>
    /// Stellt einen REST-API-Controller zum Laden der Buchgruppen
    /// </summary>
    public class HoleBuchgruppenController : Controllers.BasisApiController
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
        /// Gibt die Buchgruppen aus 
        /// der Datenbank zurück
        /// </summary>
        /// <returns></returns>
        public DTO.Buchgruppen Get()
        {
            return ClientSqlController.HoleBuchgruppen();

        }
    }
}