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
                using (var Verbindung = new MySqlConnector.MySqlConnection(this.ConnectionString))
                {

                    // Erstelle einen Befehl mit einer MySQL-Stored-Procedure
                    using (var Befehl = new MySqlConnector.MySqlCommand("BekommeBesucherId", Verbindung))
                    {
                        Befehl.CommandType = System.Data.CommandType.StoredProcedure;

                        Verbindung.Open();

                        Befehl.Parameters.AddWithValue("Name", NB.Name);
                        Befehl.Parameters.AddWithValue("Anschrift", NB.Anschrift);
                        Befehl.Parameters.AddWithValue("Telefon", NB.Telefon);

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
                return userIdIntern;
            }


            // Zuerst Abfragen, gibt es schon einen BekommeBesucherId


            // Erstelle eine Datenbankverbindung



            int userId = BekommeBesucherId(neuerBesucher);



            if (userId == 0)
            {

                using (var Verbindung = new MySqlConnector.MySqlConnection(this.ConnectionString))
                {
                    // Erstelle einen Befehl mit einer MySQL-Stored-Procedure
                    using (var Befehl = new MySqlConnector.MySqlCommand("ErstelleBesucher", Verbindung))
                    {
                        Befehl.CommandType = System.Data.CommandType.StoredProcedure;

                        Verbindung.Open();

                        Befehl.Parameters.AddWithValue("Name", neuerBesucher.Name);
                        Befehl.Parameters.AddWithValue("Anschrift", neuerBesucher.Anschrift);
                        Befehl.Parameters.AddWithValue("Telefon", neuerBesucher.Telefon);


                        Befehl.Prepare();

                        Befehl.ExecuteScalar();

                    }

                    Verbindung.Close();




                }

                userId = BekommeBesucherId(neuerBesucher);



            }


            neuerBesucher.Id = userId;



            // Falls Nein Erstellung und dann die Id dazu bekommen
            return neuerBesucher;

        }
}
}
