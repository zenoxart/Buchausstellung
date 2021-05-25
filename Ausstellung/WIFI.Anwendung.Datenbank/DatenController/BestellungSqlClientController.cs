using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WIFI.Anwendung.DatenController
{
    /// <summary>
    /// Stellt einen Dienst zum Verwalten der Ausstellungs-Bestellungen
    /// </summary>
    public class BestellungSqlClientController : WIFI.Anwendung.MySqlClient.Basiscontroller
    {
        /// <summary>
        /// Erstellt für den Besucher eine Bestellung und gibt die Bestellungs_ID zurück
        /// </summary>
        public int ErstelleBestellung(DTO.Besucher besucher)
        {
            int BestellNr = -1;

            try
            {
                using (var Verbindung = new MySqlConnector.MySqlConnection(ConnectionString))
                {
                    using (var Befehl = new MySqlConnector.MySqlCommand("ErstelleEinzelBestellung", Verbindung))
                    {
                        Befehl.CommandType = System.Data.CommandType.StoredProcedure;

                        Befehl.Parameters.AddWithValue("PersonenId", besucher.Id);

                        Verbindung.Open();

                        Befehl.Prepare();



                        Befehl.ExecuteScalar();


                    }

                    using (var Befehl = new MySqlConnector.MySqlCommand("BekommeBestellungsId", Verbindung))
                    {

                        Befehl.CommandType = System.Data.CommandType.StoredProcedure;

                        Befehl.Parameters.AddWithValue("PersonId", besucher.Id);

                        Befehl.Prepare();

                        using (var DatenLeser = Befehl.ExecuteReader(System.Data.CommandBehavior.CloseConnection))
                        {
                            while (DatenLeser.Read())
                            {
                                BestellNr = Convert.ToInt32(DatenLeser["id"]);

                            }
                        }
                    }

                    Verbindung.Close();
                }


            }
            catch (Exception e)
            {
                this.AppKontext.Protokoll.Eintragen(
                    new Daten.ProtokollEintrag
                    {
                        Text = $"Im {this.GetType().FullName} in der Funktion {typeof(VeranstaltungsSqlClientController).GetMethod("ErstelleVeranstaltung")} ist ein Fehler aufgetreten \n" +
                               $"{e.GetType().FullName} = {e.Message} \n " +
                               $"{e.StackTrace}",
                        Typ = Daten.ProtokollEintragTyp.Normal
                    });
            }

            return BestellNr;
        }

        /// <summary>
        /// Fügt 1 Buch der Bestellung hinzu
        /// </summary>
        public void BuchbestellungHinzufügen(DTO.Buch buch, int bestellNr, int anzahl)
        {
            try
            {
                using (var Verbindung = new MySqlConnector.MySqlConnection(ConnectionString))
                {
                    using (var Befehl = new MySqlConnector.MySqlCommand("BuchbestellungHinzufügen", Verbindung))
                    {

                        Befehl.CommandType = System.Data.CommandType.StoredProcedure;

                        Befehl.Parameters.AddWithValue("buchID", buch.ID.ToString());
                        Befehl.Parameters.AddWithValue("bestellungID", bestellNr.ToString());
                        Befehl.Parameters.AddWithValue("Anzahl", anzahl.ToString());

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
                    new Daten.ProtokollEintrag
                    {
                        Text = $"Im {this.GetType().FullName} in der Funktion {typeof(VeranstaltungsSqlClientController).GetMethod("ErstelleVeranstaltung")} ist ein Fehler aufgetreten \n" +
                               $"{e.GetType().FullName} = {e.Message} \n " +
                               $"{e.StackTrace}",
                        Typ = Daten.ProtokollEintragTyp.Normal
                    });
            }
        }

        /// <summary>
        /// Fügt zu einer Bestellung alle Bücher und dessen Anzahl hinzu
        /// </summary>
        public void AlleBuchbestellungenHinzufügen(DTO.Bestellung bestellung)
        {
            foreach (var buch in bestellung.Buchliste)
            {
                BuchbestellungHinzufügen(buch.Key, bestellung.BestellNr, buch.Value);
            }
        }

        /// <summary>
        /// Ruft alle Bestellungen ab 
        /// </summary>
        public DTO.Bestellungen HoleBestellungen()
        {
            var bestellungen = new DTO.Bestellungen();

            try
            {

                using (var Verbindung = new MySqlConnector.MySqlConnection(ConnectionString))
                {
                    using (var Befehl = new MySqlConnector.MySqlCommand("HoleBestellungsInfo", Verbindung))
                    {

                        Befehl.CommandType = System.Data.CommandType.StoredProcedure;


                        Verbindung.Open();

                        Befehl.Prepare();

                        using (var DatenLeser = Befehl.ExecuteReader(System.Data.CommandBehavior.CloseConnection))
                        {
                            while (DatenLeser.Read())
                            {
                                bestellungen.Add(
                                    new DTO.Bestellung
                                    {
                                        BestellNr = Convert.ToInt32(DatenLeser["ID"]),
                                        ZugehörigerBesucher = new DTO.Besucher
                                        {
                                            Id = Convert.ToInt32(DatenLeser["Besucherid"]),
                                            Name = DatenLeser["Besuchername"].ToString(),
                                            Anschrift = DatenLeser["Besucheranschrift"].ToString(),
                                            Telefon = DatenLeser["Besuchertelefon"].ToString()
                                        },
                                        Buchliste = new Dictionary<DTO.Buch, int>() { }
                                    }

                                    );



                            }
                        }
                        Verbindung.Close();

                    }


                }

                foreach (var item in bestellungen)
                {
                    using (var Verbindung = new MySqlConnector.MySqlConnection(this.ConnectionString))
                    {
                        using (var ZweiterBefehl = new MySqlConnector.MySqlCommand("HoleBücherZuBestellungsInfo", Verbindung))
                        {
                            ZweiterBefehl.CommandType = System.Data.CommandType.StoredProcedure;
                            Verbindung.Open();
                            ZweiterBefehl.Parameters.AddWithValue("bestellungsid", item.BestellNr);
                            ZweiterBefehl.Prepare();


                            using (var ZweiterDatenLeser = ZweiterBefehl.ExecuteReader(System.Data.CommandBehavior.CloseConnection))
                            {
                                while (ZweiterDatenLeser.Read())
                                {
                                    item.Buchliste.Add(
                                        new DTO.Buch
                                        {
                                            AutorName = ZweiterDatenLeser["Author"].ToString(),
                                            ID = Convert.ToInt32(ZweiterDatenLeser["BuchId"]),
                                            Kategoriegruppe = Convert.ToInt32(ZweiterDatenLeser["Kategorie"]),
                                            Preis = Convert.ToDouble(ZweiterDatenLeser["Preis"]),
                                            Titel = ZweiterDatenLeser["BuchTitle"].ToString(),
                                            VerlagName = ZweiterDatenLeser["Verlag"].ToString(),
                                            Rabattgruppe = Convert.ToInt32(ZweiterDatenLeser["Rabatt"]),
                                            Anzahl = Convert.ToInt32(ZweiterDatenLeser["Buchanzahl"])
                                        }, Convert.ToInt32(ZweiterDatenLeser["Buchanzahl"])

                                        );

                                }
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
                        Text = $"Im {this.GetType().FullName} in der Funktion {typeof(VeranstaltungsSqlClientController).GetMethod("ErstelleVeranstaltung")} ist ein Fehler aufgetreten \n" +
                               $"{e.GetType().FullName} = {e.Message} \n " +
                               $"{e.StackTrace}",
                        Typ = Daten.ProtokollEintragTyp.Normal
                    });
            }


            return bestellungen;

        }
    }
}
