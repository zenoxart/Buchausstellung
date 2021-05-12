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

            int BekommeBesucherId(MySqlConnector.MySqlConnection connection)
            {

                int userId = 0;

                // Erstelle einen Befehl mit einer MySQL-Stored-Procedure
                using (var Befehl = new MySqlConnector.MySqlCommand("BekommeBesucherId", connection))
                {
                    Befehl.CommandType = System.Data.CommandType.StoredProcedure;


                    Befehl.Parameters.AddWithValue("Name", neuerBesucher.Name);


                    Befehl.Prepare();

                    using (var DatenLeser = Befehl.ExecuteReader())
                    {
                        while (DatenLeser.Read())
                        {
                            userId = Convert.ToInt32(DatenLeser["id"]);

                        }
                    }



                }

                return userId;
            }

            // Zuerst Abfragen, gibt es schon einen BekommeBesucherId


            // Erstelle eine Datenbankverbindung


            using (var Verbindung = new MySqlConnector.MySqlConnection(this.ConnectionString))
            {
                Verbindung.Open();
                int userId = BekommeBesucherId(Verbindung);

                if (userId == 0)
                {
                    // Erstelle einen Befehl mit einer MySQL-Stored-Procedure
                    using (var Befehl = new MySqlConnector.MySqlCommand("ErstelleBesucher", Verbindung))
                    {
                        Befehl.CommandType = System.Data.CommandType.StoredProcedure;


                        Befehl.Parameters.AddWithValue("Name", neuerBesucher.Name);
                        Befehl.Parameters.AddWithValue("Anschrift", neuerBesucher.Anschrift);
                        Befehl.Parameters.AddWithValue("Telefon", neuerBesucher.Telefon);


                        Befehl.Prepare();

                        Befehl.ExecuteScalar();

                    }


                    userId = BekommeBesucherId(Verbindung);







                }
                else
                {

                }

                Verbindung.Close();

                neuerBesucher.Id = userId;

            }

            // Falls Nein Erstellung und dann die Id dazu bekommen
            return neuerBesucher;

        }
    }
}
