namespace WIFI.Gateway.Controllers
{  /// <summary>
   /// Stellt einen REST-API-Controller zum Ändern des Buches der Bestellung
   /// </summary>
    public class AktualisiereBestellungBuchController : Controllers.BasisApiController
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
        /// Aktualisiert die Bücher 
        /// in einer Bestellung
        /// </summary>
        /// <param name="buchid">Interne ID 
        /// des Buchs</param>
        /// <param name="anzahl">Anzahl 
        /// des Buchs</param>
        /// <param name="bestellId">Interne ID 
        /// der Bestellung</param>
        public object Get(int buchid, int anzahl, int bestellId)
        {
            ClientSqlController.AktualisiereBestellungBuch(buchid, anzahl, bestellId);

            return null;
        }

    }
}