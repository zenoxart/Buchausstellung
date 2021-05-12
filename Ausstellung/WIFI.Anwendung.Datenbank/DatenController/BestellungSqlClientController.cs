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
                using(var Befehl = new MySqlConnector.MySqlCommand("ErstelleEinzelBestellung", Verbindung))
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
        public void BuchbestellungHinzufügen(DTO.Buch buch, int bestellNr ,int anzahl)
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

                    Verbindung.Clone();
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
    }
}
