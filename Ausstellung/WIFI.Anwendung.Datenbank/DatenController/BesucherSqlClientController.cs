using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WIFI.Anwendung.DatenController
{
    /// <summary>
    /// Stellt einen Dienst zum Verwalten der Ausstellungs-Besucher
    /// </summary>
    public class BesucherSqlClientController : WIFI.Anwendung.MySqlClient.Basiscontroller
    {
        /// <summary>
        /// Holt eine Sammlung an allen Büchern aus der Datenbank
        /// </summary>
        public DTO.Besucher ErstelleBesucher(DTO.Besucher neuerBesucher)
        {

            int BekommeBesucherId(DTO.Besucher NB)
            {

                int userIdIntern = 0;
                try
                {
                    using (var Verbindung = new MySqlConnector.MySqlConnection(this.ConnectionString))
                    {

                        // Erstelle einen Befehl mit einer MySQL-Stored-Procedure
                        using (var Befehl = new MySqlConnector.MySqlCommand("BekommeBesucherId", Verbindung))
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
                        new Daten.ProtokollEintrag
                        {
                            Text = $"Im {this.GetType().FullName} in der Funktion {typeof(BesucherSqlClientController).GetMethod("BekommeBesucherId")} ist ein Fehler aufgetreten \n" +
                                   $"{e.GetType().FullName} = {e.Message} \n " +
                                   $"{e.StackTrace}",
                            Typ = Daten.ProtokollEintragTyp.Normal
                        });
                }
                return userIdIntern;
            }


            // Zuerst Abfragen, gibt es schon einen BekommeBesucherId


            // Erstelle eine Datenbankverbindung



            int userId = BekommeBesucherId(neuerBesucher);



            if (userId == 0)
            {
                try
                {
                    using (var Verbindung = new MySqlConnector.MySqlConnection(this.ConnectionString))
                    {
                        // Erstelle einen Befehl mit einer MySQL-Stored-Procedure
                        using (var Befehl = new MySqlConnector.MySqlCommand("ErstelleBesucher", Verbindung))
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
                        new Daten.ProtokollEintrag
                        {
                            Text = $"Im {this.GetType().FullName} in der Funktion {typeof(BesucherSqlClientController).GetMethod("ErstelleBesucher")} ist ein Fehler aufgetreten \n" +
                                   $"{e.GetType().FullName} = {e.Message} \n " +
                                   $"{e.StackTrace}",
                            Typ = Daten.ProtokollEintragTyp.Normal
                        });
                }

                userId = BekommeBesucherId(neuerBesucher);



            }


            neuerBesucher.Id = userId;



            // Falls Nein Erstellung und dann die Id dazu bekommen
            return neuerBesucher;

        }
    }
}
