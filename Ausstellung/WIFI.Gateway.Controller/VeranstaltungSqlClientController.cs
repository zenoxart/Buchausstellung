using System;

namespace WIFI.Gateway.Controller
{
    /// <summary>
    /// Stellt einen Clientcontroller für das Verwalten der Veranstaltung
    /// </summary>
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

                        Befehl.ExecuteNonQuery();
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

                        Befehl.Parameters.AddWithValue("StartDatum", StartDatum);
                        Befehl.Parameters.AddWithValue("EndDatum", EndDatum);
                        Befehl.Parameters.AddWithValue("Ort", Ort);

                        Verbindung.Open();

                        Befehl.Prepare();

                        Befehl.ExecuteScalar();
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

        /// <summary>
        /// Aktualisiert das Stadium der Veranstaltung
        /// </summary>
        /// <param name="stadium">Das Stadium, welches für
        /// die Veranstaltung eingestellt wird</param>
        public void BeendeVeranstaltung()
        {
            try
            {
                // Erstelle eine Datenbankverbindung
                using (var Verbindung = new System.Data.SqlClient.SqlConnection(this.ConnectionString))
                {
                    // Erstelle einen Befehl mit einer MySQL-Stored-Procedure
                    using (var Befehl = new System.Data.SqlClient.SqlCommand("BeendeVeranstaltung", Verbindung))
                    {
                        Befehl.CommandType = System.Data.CommandType.StoredProcedure;

                        Verbindung.Open();

                        Befehl.Prepare();

                        Befehl.ExecuteScalar();
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
        /// Läd den Ort der Veranstaltung
        /// </summary>
        public string HoleGemeinde()
        {
            string result = "";
            try
            {
                // Erstelle eine Datenbankverbindung
                using (var Verbindung = new System.Data.SqlClient.SqlConnection(this.ConnectionString))
                {
                    // Erstelle einen Befehl mit einer MySQL-Stored-Procedure
                    using (var Befehl = new System.Data.SqlClient.SqlCommand("HoleGemeinde", Verbindung))
                    {
                        Befehl.CommandType = System.Data.CommandType.StoredProcedure;

                        Verbindung.Open();

                        Befehl.Prepare();
                        using (var DatenLeser = Befehl.ExecuteReader(System.Data.CommandBehavior.CloseConnection))
                        {
                            while (DatenLeser.Read())
                            {
                                result = DatenLeser["ort"].ToString();


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

            return result;
        }
    }
}
