using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WIFI.Anwendung.DatenController
{
    /// <summary>
    /// Stellt einen Dienst zum verwalten einer Veranstaltung
    /// </summary>
    public class VeranstaltungsSqlClientController : WIFI.Anwendung.MySqlClient.Basiscontroller
    {
        /// <summary>
        /// Erstellt in der Datenbank einen Eintrag für eine Veranstaltung
        /// </summary>
        public void ErstelleVeranstaltung()
        {
            // Erstelle eine Datenbankverbindung
            using (var Verbindung = new MySqlConnector.MySqlConnection(this.ConnectionString))
            {
                // Erstelle einen Befehl mit einer MySQL-Stored-Procedure
                using (var Befehl = new MySqlConnector.MySqlCommand("ErstelleVeranstaltung", Verbindung))
                {
                    Befehl.CommandType = System.Data.CommandType.StoredProcedure;

                    Befehl.Prepare();

                    Verbindung.Open();

                    Befehl.ExecuteScalar();

                    Verbindung.Close();
                }
            }
        }

        /// <summary>
        /// Ändert die Veranstaltungsinformationen und den zugehörigen Status
        /// </summary>
        public void StarteVeranstaltung(DateTime StartDatum, DateTime EndDatum, string Ort)
        {
            // Erstelle eine Datenbankverbindung
            using (var Verbindung = new MySqlConnector.MySqlConnection(this.ConnectionString))
            {
                // Erstelle einen Befehl mit einer MySQL-Stored-Procedure
                using (var Befehl = new MySqlConnector.MySqlCommand("StarteVeranstaltung", Verbindung))
                {
                    Befehl.CommandType = System.Data.CommandType.StoredProcedure;
                    
                    Befehl.Parameters.AddWithValue("StartDatum", StartDatum.ToString("yyyy-MM-dd"));
                    Befehl.Parameters.AddWithValue("EndDatum", EndDatum.ToString("yyyy-MM-dd"));
                    Befehl.Parameters.AddWithValue("Ort", Ort);

                    Befehl.Prepare();

                    Verbindung.Open();

                    Befehl.ExecuteScalar();

                    Verbindung.Close();
                }
            }
        }

        /// <summary>
        /// Ändert den Veranstaltungsstatus 
        /// </summary>
        public void UpdateVeranstaltungsStadium(WIFI.Anwendung.DTO.AusstellungsstadiumTyp stadium)
        {

            // Erstelle eine Datenbankverbindung
            using (var Verbindung = new MySqlConnector.MySqlConnection(this.ConnectionString))
            {
                // Erstelle einen Befehl mit einer MySQL-Stored-Procedure
                using (var Befehl = new MySqlConnector.MySqlCommand("UpdateVeranstaltung", Verbindung))
                {
                    Befehl.CommandType = System.Data.CommandType.StoredProcedure;

                    Befehl.Parameters.AddWithValue("Stadium", stadium.ToString());

                    Befehl.Prepare();

                    Befehl.ExecuteScalar();

                    Verbindung.Close();
                }
            }
        }

        /// <summary>
        /// Ruft das aktuelle Stadium der Veranstaltung ab oder legt dieses fest
        /// </summary>
        /// <returns>Gibt das Stadium zurück, 
        /// falls ein Fehler im Verarbeitungsprozess auftaucht,
        /// wird ein DTO.AusstellungsstadiumTyp.Verbindungsfehler
        /// zurückgegeben</returns>
        public WIFI.Anwendung.DTO.AusstellungsstadiumTyp VeranstaltungsStadium()
        {

            WIFI.Anwendung.DTO.AusstellungsstadiumTyp Stadium = DTO.AusstellungsstadiumTyp.Verbindungsfehler;
            
            // Erstelle eine Datenbankverbindung
            using (var Verbindung = new MySqlConnector.MySqlConnection(this.ConnectionString))
            {
                // Erstelle einen Befehl mit einer MySQL-Stored-Procedure
                using (var Befehl = new MySqlConnector.MySqlCommand("VeranstaltungsStadium", Verbindung))
                {
                    Befehl.CommandType = System.Data.CommandType.StoredProcedure;


                    Verbindung.Open();

                    Befehl.Prepare();


                    using (var DatenLeser = Befehl.ExecuteReader(System.Data.CommandBehavior.CloseConnection))
                    {
                        while (DatenLeser.Read())
                        {
                           var Ergebnis =  DatenLeser["stadium"].ToString();


                            if (Enum.IsDefined(
                                    typeof(WIFI.Anwendung.DTO.AusstellungsstadiumTyp), 
                                    Ergebnis))
                            {
                                Stadium = (WIFI.Anwendung.DTO.AusstellungsstadiumTyp)Enum.Parse(
                                    typeof(WIFI.Anwendung.DTO.AusstellungsstadiumTyp), 
                                    Ergebnis, 
                                    true);
                            }
                        }
                    }
                }
            }

            return Stadium;
        }
    
        /// <summary>
        /// Beendet die Veranstaltung und bereinigt alle Datenbanktabellen
        /// </summary>
        public void BeendeVeranstaltung()
        {
            // Erstelle eine Datenbankverbindung
            using (var Verbindung = new MySqlConnector.MySqlConnection(this.ConnectionString))
            {
                // Erstelle einen Befehl mit einer MySQL-Stored-Procedure
                using (var Befehl = new MySqlConnector.MySqlCommand("BeendeVeranstaltung", Verbindung))
                {
                    Befehl.CommandType = System.Data.CommandType.StoredProcedure;

                    Befehl.Prepare();

                    Befehl.ExecuteScalar();

                    Verbindung.Close();
                }
            }
        }
    }
}
