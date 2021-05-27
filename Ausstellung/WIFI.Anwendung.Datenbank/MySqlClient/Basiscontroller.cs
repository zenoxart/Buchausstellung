using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
                if (Basiscontroller._ConnectionString == null)
                {

                    
                    var CB = new MySqlConnector.MySqlConnectionStringBuilder();


                    // Auf alle Fälle den Server einstellen
                    CB.Server = this.AppKontext.SqlServer;

                    // Einstellen der Datenbank
                    CB.Database = "buchausstellung";

                    //// Abhängig davon, ob es eine angehängte Datebank ist oder nicht
                    //if (this.AppKontext.DatenbankPfad != null
                    //    && this.AppKontext.DatenbankPfad != string.Empty)
                    //{
                    //    // es ist eine dynamisch angehängte Datenbank
                    //    CB.AttachDBFilename = System.IO.Path.Combine(
                    //        this.AppKontext.DatenbankPfad,
                    //        this.AppKontext.DatenbankName);
                    //}
                    //else
                    //{
                    //    // es ist eine fix angehängte Datenbank
                    //    // auf einem Sql Server
                    //    CB.InitialCatalog = this.AppKontext.DatenbankName;
                    //}

                    // Keine IntegratedSecurity weil wir nicht mit dem Active Directory die Datenbank authentifizieren
                    //CB.IntegratedSecurity = true;
                    // Das die Zugangsdaten sicher gespeichert werden muss eine .conf Datei verschlüsselt mit ausgeliefert werden
                    // Und erst wärend der Laufzeit entschlüsselt und gesetzt werden
                    // Hier gehört 
                    // Port einer MySQL-Datenbank
                    //CB.Port = 3306;


                    CB.UserID = MySqlClient.Encryptor.Decrypt(Properties.Settings.Default.DatenbankBenutzer);
                    CB.Password = MySqlClient.Encryptor.Decrypt(Properties.Settings.Default.DatenbankPasswort);



                    Basiscontroller._ConnectionString = CB.ConnectionString;
                    this.AppKontext.Protokoll.Eintragen(
                        $"{this} hat den ConnectionString für die Datenbank berechnet und gecached."
                        );
                }
                return Basiscontroller._ConnectionString;
            }
        }

}
}
