namespace WIFI.Anwendung.MySqlClient
{
    /// <summary>
    /// Stellt die Grundklasse eines Datenbank-Controllers
    /// </summary>
    public abstract class Basiscontroller : WIFI.Anwendung.ViewModelAppObjekt
    {

        /// <summary>
        /// Internes Feld für die Eigenschaft
        /// </summary>
        private static string _ConnectionString = null;

        /// <summary>
        /// Setzt den Wert für die Verbindungszeichenfolge
        /// </summary>
        private void SetConnectionString(string value)
        {
            _ConnectionString = value;
        }


        /// <summary>
        /// Gibt den Wert fpr die Verbindungszeichenfolge
        /// </summary>
        /// <returns></returns>
        private string GetConnectionString()
        {
            return _ConnectionString;
        }

        /// <summary>
        /// Ruft den ConnectionString ab, der
        /// zum Verbinden zu einer MySQL Datenbank benutzt wird
        /// </summary>
        /// <remarks>
        /// Es handelt sich um einen singleton Wert,
        /// d.h. während der Laufzeit ändert wich die Eigenschaft nicht.
        /// Die Information zum Berechnen wird aus der Infrastruktur bezogen</remarks>
        protected string ConnectionString
        {
            get
            {
                if (GetConnectionString() == null)
                {

                    var CB = new MySqlConnector.MySqlConnectionStringBuilder
                    {
                        // Auf alle Fälle den Server einstellen
                        Server = this.AppKontext.SqlServer,

                        // Einstellen der Datenbank
                        Database = "buchausstellung"
                    };

                    SetConnectionString(CB.ConnectionString);


                    this.AppKontext.Protokoll.Eintragen(
                        $"{this} hat den ConnectionString für die Datenbank berechnet und gecached."
                        );
                }

                return GetConnectionString();
            }
        }

    }
}
