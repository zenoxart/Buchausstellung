using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WIFI.Gateway.Controller
{
    public class VeranstaltungSqlClientController : WIFI.Anwendung.SqlClient.Controller
    {
        /// <summary>
        /// Erstellt in der Datenbank einen 
        /// Eintrag für eine Veranstaltung
        /// </summary>
        public void ErstelleVeranstaltung()
        {
            // Fehlerverarbeitung der Datenbank-Verbindung
            try
            {
                // Erstelle eine Datenbankverbindung
                using (var Verbindung = new System.Data.SqlClient.SqlConnection(this.ConnectionString))
                {
                    // Erstelle einen Befehl mit
                    // einer MySQL-Stored-Procedure
                    using (var Befehl = new System.Data.SqlClient.SqlCommand("ErstelleVeranstaltung", Verbindung))
                    {

                        
                        Befehl.CommandType = System.Data.CommandType.StoredProcedure;


                        Befehl.Prepare();

                        Verbindung.Open();

                        Befehl.ExecuteScalar();

                        Verbindung.Close();
                    }
                }
            }
            catch (Exception e)
            {
                this.AppKontext.Protokoll.Eintragen(
                    new WIFI.Anwendung.Daten.ProtokollEintrag
                    {
                        Text = $"Im {this.GetType().FullName} in der Funktion {typeof(VeranstaltungSqlClientController).GetMethod("ErstelleVeranstaltung")} ist ein Fehler aufgetreten \n" +
                               $"{e.GetType().FullName} = {e.Message} \n " +
                               $"{e.StackTrace}",
                        Typ = WIFI.Anwendung.Daten.ProtokollEintragTyp.Normal
                    });
            }
        }


        /// <summary>
        /// Enthält die Grundddaten der Buchausstellung
        /// </summary>
        /// <param name="StartDatum">Beginn der Ausstellung</param>
        /// <param name="EndDatum">Ende der Ausstellung</param>
        /// <param name="Ort">Ort der Ausstellung</param>
        public void StarteVeranstaltung(DateTime StartDatum, DateTime EndDatum, string Ort)
        {
            try
            {
                // Erstelle eine Datenbankverbindung
                using (var Verbindung = new System.Data.SqlClient.SqlConnection(this.ConnectionString))
                {
                    // Erstelle einen Befehl mit
                    // einer MySQL-Stored-Procedure
                    using (var Befehl = new System.Data.SqlClient.SqlCommand("StarteVeranstaltung", Verbindung))
                    {
                        Befehl.CommandType = System.Data.CommandType.StoredProcedure;

                        Befehl.Parameters.AddWithValue("StartDatum", StartDatum.ToString("yyyy-MM-dd"));
                        Befehl.Parameters.AddWithValue("EndDatum", EndDatum.ToString("yyyy-MM-dd"));
                        Befehl.Parameters.AddWithValue("Ort", Ort);

                        Verbindung.Open();

                        Befehl.Prepare();

                        Befehl.ExecuteScalar();

                        Verbindung.Close();
                    }
                }
            }
            catch (Exception e)
            {
                this.AppKontext.Protokoll.Eintragen(
                    new WIFI.Anwendung.Daten.ProtokollEintrag
                    {
                        Text = $"Im {this.GetType().FullName} in der Funktion {typeof(VeranstaltungSqlClientController).GetMethod("ErstelleVeranstaltung")} ist ein Fehler aufgetreten \n" +
                               $"{e.GetType().FullName} = {e.Message} \n " +
                               $"{e.StackTrace}",
                        Typ = WIFI.Anwendung.Daten.ProtokollEintragTyp.Normal
                    });
            }
        }

        /// <summary>
        /// Aktualisiert das Stadium der Veranstaltung
        /// </summary>
        /// <param name="stadium">Das Stadium, welches für
        /// die Veranstaltung eingestellt wird</param>
        public void UpdateVeranstaltungsStadium(WIFI.Gateway.DTO.AusstellungsstadiumTyp stadium)
        {
            try
            {
                // Erstelle eine Datenbankverbindung
                using (var Verbindung = new System.Data.SqlClient.SqlConnection(this.ConnectionString))
                {
                    // Erstelle einen Befehl mit einer MySQL-Stored-Procedure
                    using (var Befehl = new System.Data.SqlClient.SqlCommand("UpdateVeranstaltung", Verbindung))
                    {
                        Befehl.CommandType = System.Data.CommandType.StoredProcedure;

                        Befehl.Parameters.AddWithValue("Stadium", stadium.ToString());

                        Verbindung.Open();

                        Befehl.Prepare();

                        Befehl.ExecuteScalar();

                        Verbindung.Close();
                    }
                }

            }
            catch (Exception e)
            {
                this.AppKontext.Protokoll.Eintragen(
                    new WIFI.Anwendung.Daten.ProtokollEintrag
                    {
                        Text = $"Im {this.GetType().FullName} in der Funktion {typeof(VeranstaltungSqlClientController).GetMethod("ErstelleVeranstaltung")} ist ein Fehler aufgetreten \n" +
                               $"{e.GetType().FullName} = {e.Message} \n " +
                               $"{e.StackTrace}",
                        Typ = WIFI.Anwendung.Daten.ProtokollEintragTyp.Normal
                    });
            }
        }

        /// <summary>
        /// Ruft das aktuelle Stadium der Veranstaltung ab oder legt dieses fest
        /// </summary>
        /// <returns>Gibt das Stadium zurück, 
        /// falls ein Fehler im Verarbeitungsprozess auftaucht,
        /// wird ein DTO.AusstellungsstadiumTyp.Verbindungsfehler
        /// zurückgegeben</returns>
        public WIFI.Gateway.DTO.AusstellungsstadiumTyp VeranstaltungsStadium()
        {
            
            
            WIFI.Gateway.DTO.AusstellungsstadiumTyp Stadium = Gateway.DTO.AusstellungsstadiumTyp.Abholung;

            try
            {
                // Erstelle eine Datenbankverbindung
                using (var Verbindung = new System.Data.SqlClient.SqlConnection(this.ConnectionString))
                {
                    // Erstelle einen Befehl mit einer MySQL-Stored-Procedure
                    using (var Befehl = new System.Data.SqlClient.SqlCommand("HoleVeranstaltungsStadium", Verbindung))
                    {
                        Befehl.CommandType = System.Data.CommandType.StoredProcedure;

                        //if (Verbindung.Ping() == false)
                        //    return Stadium;

                        Verbindung.Open();

                        Befehl.Prepare();


                        using (var DatenLeser = Befehl.ExecuteReader(System.Data.CommandBehavior.CloseConnection))
                        {
                            while (DatenLeser.Read())
                            {
                                var Ergebnis = DatenLeser["stadium"].ToString();


                                if (Enum.IsDefined(
                                        typeof(WIFI.Gateway.DTO.AusstellungsstadiumTyp),
                                        Ergebnis))
                                {
                                    Stadium = (WIFI.Gateway.DTO.AusstellungsstadiumTyp)Enum.Parse(
                                        typeof(WIFI.Gateway.DTO.AusstellungsstadiumTyp),
                                        Ergebnis,
                                        true);
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception e)
            {
                this.AppKontext.Protokoll.Eintragen(
                    new WIFI.Anwendung.Daten.ProtokollEintrag
                    {
                        Text = $"Im {this.GetType().FullName} in der Funktion {typeof(VeranstaltungSqlClientController).GetMethod("ErstelleVeranstaltung")} ist ein Fehler aufgetreten \n" +
                               $"{e.GetType().FullName} = {e.Message} \n " +
                               $"{e.StackTrace}",
                        Typ = WIFI.Anwendung.Daten.ProtokollEintragTyp.Normal
                    });
            }

            return Stadium;
        }


    }
}
