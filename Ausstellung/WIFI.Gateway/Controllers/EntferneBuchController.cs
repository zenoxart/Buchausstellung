namespace WIFI.Gateway.Controllers
{

    /// <summary>
    /// Stellt einen REST-API-Controller zum Entfernen eines Buches
    /// </summary>
    public class EntferneBuchController : Controllers.BasisApiController
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
        /// Entfernt ein Buch 
        /// aus der Datenbank
        /// </summary>
        /// <param name="id">Interne ID 
        /// des Buchs</param>
        /// <returns></returns>
        public string Get(DTO.Buch id)
        {
            ClientSqlController.EntferneBuch(id);
            return null;
        }
    }
}