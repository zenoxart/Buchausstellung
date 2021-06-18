using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WIFI.Gateway.Controller
{

    /// <summary>
    /// Stellt einen Clientcontroller für das Verwalten der Buchgruppen
    /// </summary>
    public class BuchgruppeSqlClientController : WIFI.Anwendung.SqlClient.Controller
    {
        /// <summary>
        /// Holt eine Sammlung von allen 
        /// Buchgruppen aus der Datenbank
        /// </summary>
        /// <returns>Ein Objekt mit allen
        /// Buchgruppen</returns>
        public DTO.Buchgruppen HoleBuchgruppen()
        {
            DTO.Buchgruppen NeueListe = new WIFI.Gateway.DTO.Buchgruppen();

            try
            {
                using (var Verbindung = new System.Data.SqlClient.SqlConnection(this.ConnectionString))
                {
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
                                    new DTO.Buchgruppe
                                    {
                                        ID = Convert.ToInt32(DatenLeser["buchid"]),
                                        Gruppennummer = Convert.ToInt32(DatenLeser["nummer"]),
                                        Beschreibung = DatenLeser["bezeichnung"].ToString()
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
        /// Speichert eine Buchgruppe
        /// in der Datenbank
        /// </summary>
        /// <param name="gruppe">Daten der Buchgruppe</param>
        public void BuchgruppeHinzufügen(DTO.Buchgruppe gruppe)
        {
            try
            {
                using (var Verbindung = new System.Data.SqlClient.SqlConnection(this.ConnectionString))
                {
                    using (var Befehl = new System.Data.SqlClient.SqlCommand("ErstelleBuchgruppe", Verbindung))
                    {
                        Befehl.CommandType = System.Data.CommandType.StoredProcedure;

                        Verbindung.Open();

                        Befehl.Parameters.AddWithValue("Nr", gruppe.Gruppennummer);
                        Befehl.Parameters.AddWithValue("Bezeichnung", gruppe.Beschreibung);

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
                        Text = $"Im {this.GetType().FullName} in der Funktion {typeof(BuchSqlClientController).GetMethod("ErstelleBuch")} ist ein Fehler aufgetreten \n" +
                               $"{e.GetType().FullName} = {e.Message} \n " +
                               $"{e.StackTrace}",
                        Typ = WIFI.Anwendung.Daten.ProtokollEintragTyp.Normal
                    });
            }

        }

        /// <summary>
        /// Ändert die Daten einer 
        /// Buchgruppe in der Datenbank
        /// </summary>
        public void AktualisiereBuchgruppe(WIFI.Gateway.DTO.Buchgruppe gruppe)
        {
            try
            {
                using (var Verbindung = new System.Data.SqlClient.SqlConnection(this.ConnectionString))
                {
                    using (var Befehl = new System.Data.SqlClient.SqlCommand("AktualisiereBuchgruppe", Verbindung))
                    {
                        Befehl.CommandType = System.Data.CommandType.StoredProcedure;

                        Verbindung.Open();

                        Befehl.Parameters.AddWithValue("ID", gruppe.ID);
                        Befehl.Parameters.AddWithValue("Nr", gruppe.Gruppennummer);
                        Befehl.Parameters.AddWithValue("Bezeichnung", gruppe.Beschreibung);

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
                        Text = $"Im {this.GetType().FullName} in der Funktion {typeof(BuchSqlClientController).GetMethod("ErstelleBuch")} ist ein Fehler aufgetreten \n" +
                               $"{e.GetType().FullName} = {e.Message} \n " +
                               $"{e.StackTrace}",
                        Typ = WIFI.Anwendung.Daten.ProtokollEintragTyp.Normal
                    });
            }
        }

        /// <summary>
        /// Entfernt die Daten einer 
        /// Buchgruppe in der Datenbank
        /// </summary>
        public void EntferneBuchgruppe(WIFI.Gateway.DTO.Buchgruppe gruppe)
        {
            try
            {
                using (var Verbindung = new System.Data.SqlClient.SqlConnection(this.ConnectionString))
                {
                    using (var Befehl = new System.Data.SqlClient.SqlCommand("EntferneBuchgruppe", Verbindung))
                    {
                        Befehl.CommandType = System.Data.CommandType.StoredProcedure;

                        Verbindung.Open();

                        Befehl.Parameters.AddWithValue("ID", gruppe.ID);

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
                        Text = $"Im {this.GetType().FullName} in der Funktion {typeof(BuchSqlClientController).GetMethod("ErstelleBuch")} ist ein Fehler aufgetreten \n" +
                               $"{e.GetType().FullName} = {e.Message} \n " +
                               $"{e.StackTrace}",
                        Typ = WIFI.Anwendung.Daten.ProtokollEintragTyp.Normal
                    });
            }
        }
    }
}
