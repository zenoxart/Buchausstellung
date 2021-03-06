namespace WIFI.Gateway.Controllers
{

    /// <summary>
    /// Stellt einen REST-API-Controller zum Erstellen einer Veranstaltung
    /// </summary>
    public class ErstelleVeranstaltungController : Controllers.BasisApiController
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
        /// Ruft den Befehl zum Erstellen einer
        /// Veranstaltung in der Datenbank ab
        /// </summary>
        /// <returns></returns>
        public object Get()
        {
            ClientSqlController.ErstelleVeranstaltung();

            return null;
        }
    }
}