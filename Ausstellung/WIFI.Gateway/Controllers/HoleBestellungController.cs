namespace WIFI.Gateway.Controllers
{
    /// <summary>
    /// Stellt einen REST-API-Controller zum Laden einer einzelnen Bestellung
    /// </summary>
    public class HoleBestellungController : Controllers.BasisApiController
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
        /// Ruft die Daten einer
        /// einzelnen Bestellung ab
        /// </summary>
        /// <param name="bestellId">Die interne ID
        /// der Bestellung</param>
        /// <returns>DTO Objekt der Bestellung</returns>
        public Gateway.DTO.Bestellung Get(int bestellId)
        {
            return ClientSqlController.HoleBestellung(bestellId);
        }
    }
}