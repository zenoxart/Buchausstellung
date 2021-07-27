namespace WIFI.Gateway.Controllers
{

    /// <summary>
    /// Stellt einen REST-API-Controller zum Ändern des Veranstaltungs-Stadiums
    /// </summary>
    public class UpdateVeranstaltungsStadiumController : Controllers.BasisApiController
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
        /// Ruft den Befehl zum Ändern des
        /// Veranstaltungs-Stadiums in der 
        /// Datenbank ab
        /// </summary>
        /// <param name="Typ"></param>
        /// <returns></returns>
        public object Get(DTO.AusstellungsstadiumTyp Typ)
        {
            ClientSqlController.UpdateVeranstaltungsStadium(Typ);
            return null;
        }
    }
}