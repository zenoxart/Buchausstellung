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


            return BestellNr;
        }

        /// <summary>
        /// Fügt 1 Buch der Bestellung hinzu
        /// </summary>
        public void BuchbestellungHinzufügen(DTO.Buch buch, int bestellNr, int anzahl)
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


                            using (var ZweiterBefehl = new MySqlConnector.MySqlCommand("HoleBücherZuBestellungsInfo", Verbindung))
                            {
                                Befehl.CommandType = System.Data.CommandType.StoredProcedure;

                                Befehl.Parameters.AddWithValue("bestellungsid", DatenLeser["ID"]);
                                Befehl.Prepare();

                                using (var ZweiterDatenLeser = Befehl.ExecuteReader())
                                {
                                    while (ZweiterDatenLeser.Read())
                                    {
                                        foreach (var item in bestellungen)
                                        {
                                            if (item.BestellNr == Convert.ToInt32(DatenLeser["ID"]))
                                            {
                                                item.Buchliste.Add(
                                                    new DTO.Buch
                                                    {
                                                        AutorName = DatenLeser["Author"].ToString(),
                                                        ID = Convert.ToInt32(DatenLeser["BuchId"]),
                                                        Kategoriegruppe = Convert.ToInt32(DatenLeser["Kategorie"]),
                                                        Preis = Convert.ToDouble(DatenLeser["Preis"]),
                                                        Titel = DatenLeser["BuchTitle"].ToString(),
                                                        VerlagName = DatenLeser["Verlag"].ToString(),
                                                        Rabattgruppe = Convert.ToInt32(DatenLeser["Rabatt"]),
                                                        Anzahl = Convert.ToInt32(DatenLeser["Buchanzahl"])
                                                    }, Convert.ToInt32(DatenLeser["Buchanzahl"])

                                                    );

                                            }
                                        }
                                    }
                                }
                            }

                        }
                    }

                }
            }

            return bestellungen;

        }
    }
}
