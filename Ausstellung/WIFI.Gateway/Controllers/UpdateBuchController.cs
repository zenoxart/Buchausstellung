namespace WIFI.Gateway.Controllers
{

    /// <summary>
    /// Stellt einen REST-API-Controller zum Ändern des Buches
    /// </summary>
    public class UpdateBuchController : Controllers.BasisApiController
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
        /// Aktualisiert die Daten eines
        /// Buches in der Datenbank 
        /// </summary>
        /// <param name="id">Interne ID 
        /// des Buchs</param>
        /// <returns></returns>
        public object Get(DTO.Buch id)
        {
            ClientSqlController.AktualisiereBuch(id);
            return null;
        }
    }
}