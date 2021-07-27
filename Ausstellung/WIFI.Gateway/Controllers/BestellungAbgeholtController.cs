namespace WIFI.Gateway.Controllers
{
    /// <summary>
    /// Stellt einen REST-API-Controller zum Setzen des Bestell-Status
    /// </summary>
    public class BestellungAbgeholtController : Controllers.BasisApiController
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
        /// Setzt den Status einer
        /// Bestellung in der Datenbank
        /// </summary>
        /// <param name="bestellung">Daten
        /// der Bestellung</param>
        public void Get(DTO.Bestellung bestellung)
        {
            ClientSqlController.BestellungAbgeholt(bestellung.BestellNr);
        }
    }
}