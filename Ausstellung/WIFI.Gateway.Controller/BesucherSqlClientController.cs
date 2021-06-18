using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WIFI.Gateway.Controller
{

    /// <summary>
    /// Stellt einen Clientcontroller für das Verwalten der Besucher
    /// </summary>
    public class BesucherSqlClientController : WIFI.Anwendung.SqlClient.Controller
    {
        /// <summary>
        /// Prozedur zum Erstellen eines neuen
        /// Besuchers in der Datenbank
        /// </summary>
        public DTO.Besucher ErstelleBesucher(DTO.Besucher neuerBesucher)
        {
            int BekommeBesucherId(DTO.Besucher NB)
            {
                int userIdIntern = 0;
                try
                {
                    using (var Verbindung = new System.Data.SqlClient.SqlConnection(this.ConnectionString))
                    {

                        // Erstelle einen Befehl mit einer MySQL-Stored-Procedure
                        using (var Befehl = new System.Data.SqlClient.SqlCommand("BekommeBesucherId", Verbindung))
                        {
                            Befehl.CommandType = System.Data.CommandType.StoredProcedure;

                            Verbindung.Open();

                            Befehl.Parameters.AddWithValue("Vorname", NB.Vorname);
                            Befehl.Parameters.AddWithValue("Nachname", NB.Nachname);

                            Befehl.Prepare();

                            using (var DatenLeser = Befehl.ExecuteReader())
                            {
                                while (DatenLeser.Read())
                                {
                                    userIdIntern = Convert.ToInt32(DatenLeser["id"]);
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
                            Text = $"Im {this.GetType().FullName} in der Funktion {typeof(BesucherSqlClientController).GetMethod("BekommeBesucherId")} ist ein Fehler aufgetreten \n" +
                                   $"{e.GetType().FullName} = {e.Message} \n " +
                                   $"{e.StackTrace}",
                            Typ = WIFI.Anwendung.Daten.ProtokollEintragTyp.Normal
                        });
                }
                return userIdIntern;
            }

            // Zuerst abfragen, ob der Besucher
            // schon angelegt ist
            int userId = BekommeBesucherId(neuerBesucher);

            if (userId == 0)
            {
                try
                {
                    // Erstelle eine Datenbankverbindung
                    using (var Verbindung = new System.Data.SqlClient.SqlConnection(this.ConnectionString))
                    {
                        // Erstelle einen Befehl mit einer MySQL-Stored-Procedure
                        using (var Befehl = new System.Data.SqlClient.SqlCommand("ErstelleBesucher", Verbindung))
                        {
                            Befehl.CommandType = System.Data.CommandType.StoredProcedure;

                            Verbindung.Open();

                            Befehl.Parameters.AddWithValue("Vorname", neuerBesucher.Vorname);
                            Befehl.Parameters.AddWithValue("Nachname", neuerBesucher.Nachname);
                            Befehl.Parameters.AddWithValue("Strasse", neuerBesucher.Straßenname);
                            Befehl.Parameters.AddWithValue("Hausnummer", neuerBesucher.Hausnummer);
                            Befehl.Parameters.AddWithValue("PLZ", neuerBesucher.Postleitzahl);
                            Befehl.Parameters.AddWithValue("Ort", neuerBesucher.Ort);
                            Befehl.Parameters.AddWithValue("Telefon", neuerBesucher.Telefon);

                            Befehl.Prepare();

                            Befehl.ExecuteScalar();
                        }

                        Verbindung.Close();
                    }
                }
                catch (Exception e)
                {
                    this.AppKontext.Protokoll.Eintragen(
                        new WIFI.Anwendung.Daten.ProtokollEintrag
                        {
                            Text = $"Im {this.GetType().FullName} in der Funktion {typeof(BesucherSqlClientController).GetMethod("ErstelleBesucher")} ist ein Fehler aufgetreten \n" +
                                   $"{e.GetType().FullName} = {e.Message} \n " +
                                   $"{e.StackTrace}",
                            Typ = WIFI.Anwendung.Daten.ProtokollEintragTyp.Normal
                        });
                }

                userId = BekommeBesucherId(neuerBesucher);
            }

            neuerBesucher.Id = userId;

            return neuerBesucher;
        }
    }
}
