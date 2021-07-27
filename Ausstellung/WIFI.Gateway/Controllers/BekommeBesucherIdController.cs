namespace WIFI.Gateway.Controllers
{
    /// <summary>
    /// Stellt einen REST-API-Controller zum Ermitteln der Besucher-ID
    /// </summary>
    public class BekommeBesucherIdController : Controllers.BasisApiController
    {
        /// <summary>
        /// Internes Feld für die Eigenschaft
        /// </summary>
        private WIFI.Gateway.Controller.BesucherSqlClientController _ClientSqlController;

        /// <summary>
        /// Ruft den Clientcontroller für die Besucher ab 
        /// </summary>
        public WIFI.Gateway.Controller.BesucherSqlClientController ClientSqlController
        {
            get
            {
                if (this._ClientSqlController == null)
                {
                    this._ClientSqlController = this.AppKontext.Produziere<WIFI.Gateway.Controller.BesucherSqlClientController>();
                }
                return this._ClientSqlController;
            }
        }

        /// <summary>
        /// Ruft anhand des aktuellen Besuchers
        /// die dazugehörige ID ab
        /// </summary>
        /// <param name="besucher">Daten des Besuchers</param>
        /// <returns>Interne Nummer des Besuchers</returns>
        public int Get(DTO.Besucher besucher)
        {
            return ClientSqlController.BekommeBesucherId(besucher);
        }
    }
}