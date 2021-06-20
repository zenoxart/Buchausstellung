using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WIFI.Gateway.Controller
{

    /// <summary>
    /// Stellt einen Clientcontroller für das Verwalten der Bücher
    /// </summary>
    public class BuchSqlClientController : WIFI.Anwendung.SqlClient.Controller
    {
        /// <summary>
        /// Holt eine Sammlung an allen Büchern aus der Datenbank
        /// </summary>
        public DTO.Bücher HoleBücher()
        {
            DTO.Bücher NeueListe = new WIFI.Gateway.DTO.Bücher();

            try
            {
                // Erstelle eine Datenbankverbindung
                using (var Verbindung = new System.Data.SqlClient.SqlConnection(this.ConnectionString))
                {
                    // Erstelle einen Befehl mit einer MySQL-Stored-Procedure
                    using (var Befehl = new System.Data.SqlClient.SqlCommand("HoleBücher", Verbindung))
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
                                        Preis = Convert.ToDecimal(DatenLeser["preis"]),
                                        Kategoriegruppe = Convert.ToInt32(DatenLeser["kategorie"]),
                                        Rabattgruppe = Convert.ToInt32(DatenLeser["rabatt"]),
                                        VerlagName = DatenLeser["verlag"].ToString()
                                    });
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
                        Text = $"Im {this.GetType().FullName} in der Funktion {typeof(BuchSqlClientController).GetMethod("HoleBücher")} ist ein Fehler aufgetreten \n" +
                               $"{e.GetType().FullName} = {e.Message} \n " +
                               $"{e.StackTrace}",
                        Typ = WIFI.Anwendung.Daten.ProtokollEintragTyp.Normal
                    });
            }

            return NeueListe;
        }

        /// <summary>
        /// Fügt ein Buch in der Datenbank hinzu
        /// </summary>
        /// <param name="buch">Daten des Buchs</param>
        public void BuchHinzufügen(DTO.Buch buch)
        {
            try
            {
                using (var Verbindung = new System.Data.SqlClient.SqlConnection(this.ConnectionString))
                {
                    // Erstelle einen Befehl mit einer MySQL-Stored-Procedure
                    using (var Befehl = new System.Data.SqlClient.SqlCommand("ErstelleBuch", Verbindung))
                    {
                        Befehl.CommandType = System.Data.CommandType.StoredProcedure;

                        Verbindung.Open();

                        Befehl.Parameters.AddWithValue("Buchnummer", buch.Buchnummer);
                        Befehl.Parameters.AddWithValue("Titel", buch.Titel);
                        Befehl.Parameters.AddWithValue("Autor", buch.AutorName);
                        Befehl.Parameters.AddWithValue("Preis", Convert.ToDecimal(buch.Preis));
                        Befehl.Parameters.AddWithValue("Rabattgruppe", buch.Rabattgruppe);
                        Befehl.Parameters.AddWithValue("Kategorieid", buch.Kategoriegruppe);
                        Befehl.Parameters.AddWithValue("Verlagname", buch.VerlagName);

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
                        Text = $"Im {this.GetType().FullName} in der Funktion {typeof(BuchSqlClientController).GetMethod("ErstelleBuch")} ist ein Fehler aufgetreten \n" +
                               $"{e.GetType().FullName} = {e.Message} \n " +
                               $"{e.StackTrace}",
                        Typ = WIFI.Anwendung.Daten.ProtokollEintragTyp.Normal
                    });
            }
        }

        /// <summary>
        /// Ändert die Daten eines Buches in der Datenbank
        /// </summary>
        public void AktualisiereBuch(WIFI.Gateway.DTO.Buch buch)
        {
            try
            {
                using (var Verbindung = new System.Data.SqlClient.SqlConnection(this.ConnectionString))
                {
                    // Ändert ein einzelnes Buch
                    using (var Befehl = new System.Data.SqlClient.SqlCommand("UpdateBuch", Verbindung))
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
                        Befehl.Parameters.AddWithValue("Anzahl", buch.Anzahl);

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
                        Text = $"Im {this.GetType().FullName} in der Funktion {typeof(BuchSqlClientController).GetMethod("ErstelleBuch")} ist ein Fehler aufgetreten \n" +
                               $"{e.GetType().FullName} = {e.Message} \n " +
                               $"{e.StackTrace}",
                        Typ = WIFI.Anwendung.Daten.ProtokollEintragTyp.Normal
                    });
            }
        }

        /// <summary>
        /// Entfernt die Daten eines 
        /// Buches in der Datenbank
        /// </summary>
        public void EntferneBuch(WIFI.Gateway.DTO.Buch buch)
        {
            try
            {
                using (var Verbindung = new System.Data.SqlClient.SqlConnection(this.ConnectionString))
                {
                    using (var Befehl = new System.Data.SqlClient.SqlCommand("EntferneBuch", Verbindung))
                    {
                        Befehl.CommandType = System.Data.CommandType.StoredProcedure;

                        Verbindung.Open();

                        Befehl.Parameters.AddWithValue("ID", buch.ID);

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
                        Text = $"Im {this.GetType().FullName} in der Funktion {typeof(BuchSqlClientController).GetMethod("ErstelleBuch")} ist ein Fehler aufgetreten \n" +
                               $"{e.GetType().FullName} = {e.Message} \n " +
                               $"{e.StackTrace}",
                        Typ = WIFI.Anwendung.Daten.ProtokollEintragTyp.Normal
                    });
            }
        }
    }
}
