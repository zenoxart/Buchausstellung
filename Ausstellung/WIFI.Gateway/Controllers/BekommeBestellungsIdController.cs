namespace WIFI.Gateway.Controllers
{
    /// <summary>
    /// Stellt einen REST-API-Controller zum Ermitteln der Bestellung-ID
    /// </summary>
    public class BekommeBestellungsIdController : Controllers.BasisApiController
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
        /// Ruft anhand des aktuellen Besuchers
        /// die dazugehörige Bestellung ab
        /// </summary>
        /// <param name="besucher">Daten des Besuchers</param>
        /// <returns>Interne Nummer der Bestellung</returns>
        public int Get(DTO.Besucher besucher)
        {
            return ClientSqlController.BekommeBestellungsID(besucher);
        }
    }
}