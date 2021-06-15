using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WIFI.Anwendung.DatenController
{
    /// <summary>
    /// Stellt einen Dienst zum Verwalten der Ausstellungs-Bücher
    /// </summary>
    public class BücherSqlClientController : WIFI.Anwendung.MySqlClient.Basiscontroller
    {
        /// <summary>
        /// Holt eine Sammlung an allen Büchern aus der Datenbank
        /// </summary>
        public DTO.Bücher HoleBücher()
        {

            DTO.Bücher NeueListe = new WIFI.Anwendung.DTO.Bücher();

            try
            {
                // Erstelle eine Datenbankverbindung
                using (var Verbindung = new MySqlConnector.MySqlConnection(this.ConnectionString))
                {
                    // Erstelle einen Befehl mit einer MySQL-Stored-Procedure
                    using (var Befehl = new MySqlConnector.MySqlCommand("HoleBücher", Verbindung))
                    {
                        Befehl.CommandType = System.Data.CommandType.StoredProcedure;

                        Verbindung.Open();

                        Befehl.Prepare();

                        using (var DatenLeser = Befehl.ExecuteReader(System.Data.CommandBehavior.CloseConnection))
                        {
                            while (DatenLeser.Read())
                            {
                                NeueListe.Add(
                                    new DTO.Buch
                                    {
                                        ID = Convert.ToInt32(DatenLeser["buchid"]),
                                        Buchnummer = DatenLeser["buchnr"].ToString(),
                                        AutorName = DatenLeser["autor"].ToString(),
                                        Titel = DatenLeser["titel"].ToString(),
                                        Preis = Convert.ToDouble(DatenLeser["preis"]),
                                        Kategoriegruppe = Convert.ToInt32(DatenLeser["rabgr"]),
                                        Rabattgruppe = Convert.ToInt32(DatenLeser["katgr"]),
                                        VerlagName = DatenLeser["name"].ToString()
                                    });
                            }
                        }

                        Verbindung.Close();
                    }
                }
            }
            catch (Exception e)
            {
                this.AppKontext.Protokoll.Eintragen(
                    new Daten.ProtokollEintrag
                    {
                        Text = $"Im {this.GetType().FullName} in der Funktion {typeof(BücherSqlClientController).GetMethod("HoleBücher")} ist ein Fehler aufgetreten \n" +
                               $"{e.GetType().FullName} = {e.Message} \n " +
                               $"{e.StackTrace}",
                        Typ = Daten.ProtokollEintragTyp.Normal
                    });
            }

            return NeueListe;
        }

        /// <summary>
        /// Fügt ein Buch in der Datenbank hinzu
        /// </summary>
        /// <param name="buch"></param>
        public void BuchHinzufügen(DTO.Buch buch)
        {
            try
            {
                using (var Verbindung = new MySqlConnector.MySqlConnection(this.ConnectionString))
                {
                    // Erstelle einen Befehl mit einer MySQL-Stored-Procedure
                    using (var Befehl = new MySqlConnector.MySqlCommand("ErstelleBuch", Verbindung))
                    {
                        Befehl.CommandType = System.Data.CommandType.StoredProcedure;

                        Verbindung.Open();

                        Befehl.Parameters.AddWithValue("Buchnummer", buch.Buchnummer);
                        Befehl.Parameters.AddWithValue("Titel", buch.Titel);
                        Befehl.Parameters.AddWithValue("Autor", buch.AutorName);
                        Befehl.Parameters.AddWithValue("Preis", buch.Preis);
                        Befehl.Parameters.AddWithValue("Rabattgruppe", buch.Rabattgruppe);
                        Befehl.Parameters.AddWithValue("Kategorie", buch.Kategoriegruppe);
                        Befehl.Parameters.AddWithValue("Verlag", buch.VerlagName);

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
                        Text = $"Im {this.GetType().FullName} in der Funktion {typeof(BücherSqlClientController).GetMethod("ErstelleBuch")} ist ein Fehler aufgetreten \n" +
                               $"{e.GetType().FullName} = {e.Message} \n " +
                               $"{e.StackTrace}",
                        Typ = Daten.ProtokollEintragTyp.Normal
                    });
            }
        }

        /// <summary>
        /// Ändert die Daten eines Buches in der Datenbank
        /// </summary>
        public void AktualisiereBuch(WIFI.Anwendung.DTO.Buch buch)
        {
            try
            {
                using (var Verbindung = new MySqlConnector.MySqlConnection(this.ConnectionString))
                {
                    using (var Befehl = new MySqlConnector.MySqlCommand("UpdateBuch",Verbindung))
                    {
                        Befehl.CommandType = System.Data.CommandType.StoredProcedure;

                        Verbindung.Open();

                        Befehl.Parameters.AddWithValue("Buchnummer", buch.Buchnummer);
                        Befehl.Parameters.AddWithValue("Titel", buch.Titel);
                        Befehl.Parameters.AddWithValue("Autor", buch.AutorName);
                        Befehl.Parameters.AddWithValue("Preis", buch.Preis);
                        Befehl.Parameters.AddWithValue("Rabattgruppe", buch.Rabattgruppe);
                        Befehl.Parameters.AddWithValue("Kategorie", buch.Kategoriegruppe);
                        Befehl.Parameters.AddWithValue("Verlagname", buch.VerlagName);

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
                        Text = $"Im {this.GetType().FullName} in der Funktion {typeof(BücherSqlClientController).GetMethod("ErstelleBuch")} ist ein Fehler aufgetreten \n" +
                               $"{e.GetType().FullName} = {e.Message} \n " +
                               $"{e.StackTrace}",
                        Typ = Daten.ProtokollEintragTyp.Normal
                    });
            }
        }
    }
}
