using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WIFI.Gateway.Controller
{

    /// <summary>
    /// Stellt einen Clientcontroller für das Verwalten der Bestellungen
    /// </summary>
    public class BestellungSqlClientController : WIFI.Anwendung.SqlClient.Controller
    {
        /// <summary>
        /// Erstellt für den Besucher eine Bestellung
        /// und gibt die Bestellungs_ID zurück
        /// </summary>
        public int ErstelleBestellung(DTO.Besucher besucher)
        {
            int BestellNr = -1;

            try
            {
                using (var Verbindung = new System.Data.SqlClient.SqlConnection(ConnectionString))
                {
                    // Befehl zum Erstellen der Bestellung
                    using (var Befehl = new System.Data.SqlClient.SqlCommand("ErstelleEinzelBestellung", Verbindung))
                    {
                        Befehl.CommandType = System.Data.CommandType.StoredProcedure;

                        Befehl.Parameters.AddWithValue("PersonenId", besucher.Id);

                        Verbindung.Open();

                        Befehl.Prepare();

                        Befehl.ExecuteScalar();
                    }

                    // Befehl zur Rückgabe der Bestell-ID
                    using (var Befehl = new System.Data.SqlClient.SqlCommand("BekommeBestellungsId", Verbindung))
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
                }
            }
            catch (Exception e)
            {
                this.AppKontext.Protokoll.Eintragen(
                    new WIFI.Anwendung.Daten.ProtokollEintrag
                    {
                        Text = $"Im {this.GetType().FullName} in der Funktion {typeof(BestellungSqlClientController).GetMethod("ErstelleEinzelBestellung")} ist ein Fehler aufgetreten \n" +
                               $"{e.GetType().FullName} = {e.Message} \n " +
                               $"{e.StackTrace}",
                        Typ = WIFI.Anwendung.Daten.ProtokollEintragTyp.Normal
                    });
            }

            return BestellNr;
        }

        /// <summary>
        /// Fügt ein Buch der Bestellung hinzu
        /// </summary>
        public void BuchbestellungHinzufügen(DTO.Buch buch, int bestellNr, int anzahl)
        {
            try
            {
                using (var Verbindung = new System.Data.SqlClient.SqlConnection(ConnectionString))
                {
                    using (var Befehl = new System.Data.SqlClient.SqlCommand("BuchbestellungHinzufügen", Verbindung))
                    {
                        Befehl.CommandType = System.Data.CommandType.StoredProcedure;

                        Befehl.Parameters.AddWithValue("buchID", buch.ID.ToString());
                        Befehl.Parameters.AddWithValue("bestellungID", bestellNr.ToString());
                        Befehl.Parameters.AddWithValue("Anzahl", anzahl.ToString());

                        Verbindung.Open();

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
                        Text = $"Im {this.GetType().FullName} in der Funktion {typeof(BestellungSqlClientController).GetMethod("BuchbestellungHinzufügen")} ist ein Fehler aufgetreten \n" +
                               $"{e.GetType().FullName} = {e.Message} \n " +
                               $"{e.StackTrace}",
                        Typ = WIFI.Anwendung.Daten.ProtokollEintragTyp.Normal
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
        public Gateway.DTO.Bestellungen HoleBestellungen()
        {
            var bestellungen = new Gateway.DTO.Bestellungen();

            try
            {
                using (var Verbindung = new System.Data.SqlClient.SqlConnection(ConnectionString))
                {
                    using (var Befehl = new System.Data.SqlClient.SqlCommand("HoleBestellungsInfo", Verbindung))
                    {
                        Befehl.CommandType = System.Data.CommandType.StoredProcedure;

                        Verbindung.Open();

                        Befehl.Prepare();

                        using (var DatenLeser = Befehl.ExecuteReader(System.Data.CommandBehavior.CloseConnection))
                        {
                            while (DatenLeser.Read())
                            {
                                bool abgeholtBool = false;
                                if (Convert.ToInt32(DatenLeser["Abgeholt"]) == 1)
                                {
                                    abgeholtBool = true;
                                }

                                bestellungen.Add(
                                    new Gateway.DTO.Bestellung
                                    {
                                        BestellNr = Convert.ToInt32(DatenLeser["ID"]),
                                        ZugehörigerBesucher = new Gateway.DTO.Besucher
                                        {
                                            Id = Convert.ToInt32(DatenLeser["Besucherid"]),
                                            Vorname = DatenLeser["Vorname"].ToString(),
                                            Nachname = DatenLeser["Nachname"].ToString(),
                                            Straßenname = DatenLeser["Strasse"].ToString(),
                                            Hausnummer = Convert.ToInt32(DatenLeser["Hausnummer"]),
                                            Postleitzahl = Convert.ToInt32(DatenLeser["PLZ"]),
                                            Ort = DatenLeser["Ort"].ToString()
                                        },
                                        Buchliste = new Dictionary<DTO.Buch, int>() { },
                                        Abgeholt = abgeholtBool
                                    }
                                );
                            }
                        }
                    }
                }

                // Alle Bücher zu der dazugehörigen
                // Bestellung ermitteln


            }
            catch (Exception e)
            {
                this.AppKontext.Protokoll.Eintragen(
                    new WIFI.Anwendung.Daten.ProtokollEintrag
                    {
                        Text = $"Im {this.GetType().FullName} in der Funktion {typeof(BestellungSqlClientController).GetMethod("HoleBestellungsInfo")} ist ein Fehler aufgetreten \n" +
                               $"{e.GetType().FullName} = {e.Message} \n " +
                               $"{e.StackTrace}",
                        Typ = WIFI.Anwendung.Daten.ProtokollEintragTyp.Normal
                    });
            }

            return bestellungen;
        }

        /// <summary>
        /// Läd alle Bücher zu der angegebenen BestellNr
        /// </summary>
        public Gateway.DTO.Bücher HoleBücherZuBestellung(int BestellNr)
        {
            var bücherliste = new Gateway.DTO.Bücher();
            try
            {

                using (var Verbindung = new System.Data.SqlClient.SqlConnection(this.ConnectionString))
                {
                    using (var ZweiterBefehl = new System.Data.SqlClient.SqlCommand("HoleBücherZuBestellungsInfo", Verbindung))
                    {
                        ZweiterBefehl.CommandType = System.Data.CommandType.StoredProcedure;
                        Verbindung.Open();
                        ZweiterBefehl.Parameters.AddWithValue("bestellungsid", BestellNr);
                        ZweiterBefehl.Prepare();

                        using (var ZweiterDatenLeser = ZweiterBefehl.ExecuteReader())
                        {
                            while (ZweiterDatenLeser.Read())
                            {
                                var newBook = new Gateway.DTO.Buch
                                {
                                    ID = Convert.ToInt32(ZweiterDatenLeser["BuchId"]),
                                    Anzahl = Convert.ToInt32(ZweiterDatenLeser["Buchanzahl"]),
                                    Buchnummer = Convert.ToInt32(ZweiterDatenLeser["BuchNr"]),
                                    Titel = ZweiterDatenLeser["BuchTitel"].ToString(),
                                    AutorName = ZweiterDatenLeser["Autor"].ToString(),
                                    Preis = Convert.ToDecimal(ZweiterDatenLeser["Preis"]),
                                    Rabattgruppe = Convert.ToInt32(ZweiterDatenLeser["Rabatt"]),
                                    Kategoriegruppe = Convert.ToInt32(ZweiterDatenLeser["Kategorie"]),
                                    VerlagName = ZweiterDatenLeser["Verlag"].ToString()
                                };

                                bücherliste.Add(newBook);
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
                        Text = $"Im {this.GetType().FullName} in der Funktion {typeof(BestellungSqlClientController).GetMethod("HoleBestellungsInfo")} ist ein Fehler aufgetreten \n" +
                               $"{e.GetType().FullName} = {e.Message} \n " +
                               $"{e.StackTrace}",
                        Typ = WIFI.Anwendung.Daten.ProtokollEintragTyp.Normal
                    });
            }

            return bücherliste;
        }

        /// <summary>
        /// Ruft die Daten einer einzelnen
        /// Bestellung ab
        /// </summary>
        public Gateway.DTO.Bestellung HoleBestellung(int BestellNr)
        {
            Gateway.DTO.Bestellung bestellung1 = new DTO.Bestellung();
            try
            {
                using (var Verbindung = new System.Data.SqlClient.SqlConnection(ConnectionString))
                {
                    using (var Befehl = new System.Data.SqlClient.SqlCommand("HoleEinzelBestellungsInfo", Verbindung))
                    {
                        Befehl.CommandType = System.Data.CommandType.StoredProcedure;

                        Befehl.Parameters.AddWithValue("bestellnr", BestellNr);
                        Verbindung.Open();

                        Befehl.Prepare();

                        using (var DatenLeser = Befehl.ExecuteReader(System.Data.CommandBehavior.CloseConnection))
                        {
                            while (DatenLeser.Read())
                            {
                                bool abgeholtBool = false;
                                if (Convert.ToInt32(DatenLeser["Abgeholt"]) == 1)
                                {
                                    abgeholtBool = true;
                                }

                                bestellung1 =  new Gateway.DTO.Bestellung
                                    {
                                        BestellNr = Convert.ToInt32(DatenLeser["ID"]),
                                        ZugehörigerBesucher = new Gateway.DTO.Besucher
                                        {
                                            Id = Convert.ToInt32(DatenLeser["Besucherid"]),
                                            Vorname = DatenLeser["Vorname"].ToString(),
                                            Nachname = DatenLeser["Nachname"].ToString(),
                                            Straßenname = DatenLeser["Strasse"].ToString(),
                                            Hausnummer = Convert.ToInt32(DatenLeser["Hausnummer"]),
                                            Postleitzahl = Convert.ToInt32(DatenLeser["PLZ"]),
                                            Ort = DatenLeser["Ort"].ToString(),
                                            Telefon = DatenLeser["Telefon"].ToString()
                                        },
                                        Buchliste = new Dictionary<Gateway.DTO.Buch, int>() { },
                                        Abgeholt = abgeholtBool
                                    };
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
                        Text = $"Im {this.GetType().FullName} in der Funktion {typeof(BestellungSqlClientController).GetMethod("HoleBestellungsInfo")} ist ein Fehler aufgetreten \n" +
                               $"{e.GetType().FullName} = {e.Message} \n " +
                               $"{e.StackTrace}",
                        Typ = WIFI.Anwendung.Daten.ProtokollEintragTyp.Normal
                    });
            }
            return bestellung1;
        }

        /// <summary>
        /// Aktualisiert alle abgeholten Bestellungen
        /// </summary>
        public void FürAlleBestellungenAbgeholt(WIFI.Anwendung.DTO.Bestellungen bestellungs)
        {
            foreach (var item in bestellungs)
            {
                if (item.Abgeholt)
                {
                    BestellungAbgeholt(item.BestellNr);
                }
            }
        }

        /// <summary>
        /// Aktualisiert die einzelne Bestellung
        /// </summary>
        /// <param name="BestellNr">Interne Nummer der Bestellung</param>
        public void BestellungAbgeholt(int BestellNr)
        {
            try
            {
                using (var Verbindung = new System.Data.SqlClient.SqlConnection(ConnectionString))
                {
                    using (var Befehl = new System.Data.SqlClient.SqlCommand("BestellungAbgeholt", Verbindung))
                    {
                        Befehl.CommandType = System.Data.CommandType.StoredProcedure;

                        Befehl.Parameters.AddWithValue("ID", BestellNr);
                        Verbindung.Open();

                        Befehl.Prepare();

                        Befehl.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception e)
            {
                this.AppKontext.Protokoll.Eintragen(
                    new WIFI.Anwendung.Daten.ProtokollEintrag
                    {
                        Text = $"Im {this.GetType().FullName} in der Funktion {typeof(BestellungSqlClientController).GetMethod("HoleBestellungsInfo")} ist ein Fehler aufgetreten \n" +
                               $"{e.GetType().FullName} = {e.Message} \n " +
                               $"{e.StackTrace}",
                        Typ = WIFI.Anwendung.Daten.ProtokollEintragTyp.Normal
                    });
            }
        }

        /// <summary>
        /// Aktualisiert eine Bestellung und alle untergeordneten Elemente in der Datenbank
        /// </summary>
        public void AktualisiereBestellung(int BestellNr, int BesucherId, Gateway.DTO.Besucher besucher)
        {
            using (var Verbindung = new System.Data.SqlClient.SqlConnection(ConnectionString))
            {
                using (var Befehl = new System.Data.SqlClient.SqlCommand("AktualisiereBestellungsInfo", Verbindung))
                {
                    Befehl.CommandType = System.Data.CommandType.StoredProcedure;

                    Befehl.Parameters.AddWithValue("ID", BestellNr);
                    Befehl.Parameters.AddWithValue("Besucher_ID", BesucherId);

                    Verbindung.Open();

                    Befehl.Prepare();

                    Befehl.ExecuteScalar();
                }
            }


            // Aktualisiert die Besucherdaten der Bestellung
            using (var Verbindung = new System.Data.SqlClient.SqlConnection(ConnectionString))
            {
                using (var Befehl = new System.Data.SqlClient.SqlCommand("AktualisiereBesucher", Verbindung))
                {
                    Befehl.CommandType = System.Data.CommandType.StoredProcedure;

                    Befehl.Parameters.AddWithValue("ID", BestellNr);
                    Befehl.Parameters.AddWithValue("Vorname", besucher.Vorname);
                    Befehl.Parameters.AddWithValue("Nachname", besucher.Nachname);
                    Befehl.Parameters.AddWithValue("PLZ", besucher.Postleitzahl);
                    Befehl.Parameters.AddWithValue("Telefon", besucher.Telefon);
                    Befehl.Parameters.AddWithValue("Straße", besucher.Straßenname);
                    Befehl.Parameters.AddWithValue("Hausnummer", besucher.Hausnummer);
                    Befehl.Parameters.AddWithValue("Ort", besucher.Ort);
                    Verbindung.Open();

                    Befehl.Prepare();

                    Befehl.ExecuteScalar();
                }
            }
        }

        /// <summary>
        /// Gibt die DatenbankId einer Bestellung welche dem Besucher gehört zurück
        /// </summary>
        public int BekommeBestellungsID(DTO.Besucher besucher)
        {
            int BestellNr = -1;

            try
            {
                using (var Verbindung = new System.Data.SqlClient.SqlConnection(ConnectionString))
                {
                    // Befehl zur Rückgabe der Bestell-ID
                    using (var Befehl = new System.Data.SqlClient.SqlCommand("BekommeBestellungsId", Verbindung))
                    {
                        Befehl.CommandType = System.Data.CommandType.StoredProcedure;

                        Befehl.Parameters.AddWithValue("PersonId", besucher.Id);


                        Verbindung.Open();

                        Befehl.Prepare();

                        using (var DatenLeser = Befehl.ExecuteReader(System.Data.CommandBehavior.CloseConnection))
                        {
                            while (DatenLeser.Read())
                            {
                                BestellNr = Convert.ToInt32(DatenLeser["id"]);
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
                        Text = $"Im {this.GetType().FullName} in der Funktion {typeof(BestellungSqlClientController).GetMethod("ErstelleEinzelBestellung")} ist ein Fehler aufgetreten \n" +
                               $"{e.GetType().FullName} = {e.Message} \n " +
                               $"{e.StackTrace}",
                        Typ = WIFI.Anwendung.Daten.ProtokollEintragTyp.Normal
                    });
            }

            return BestellNr;
        }

        /// <summary>
        /// Ändert die Daten zu dem Buch
        /// </summary>
        public void AktualisiereBestellungBuch(int buchid, int anzahl, int bestellId)
        {
            try
            {
                using (var Verbindung = new System.Data.SqlClient.SqlConnection(ConnectionString))
                {
                    // Befehl zur Rückgabe der Bestell-ID
                    using (var Befehl = new System.Data.SqlClient.SqlCommand("AktualisiereBestellungBuch", Verbindung))
                    {
                        Befehl.CommandType = System.Data.CommandType.StoredProcedure;

                        Befehl.Parameters.AddWithValue("BuchId", buchid);
                        Befehl.Parameters.AddWithValue("Anzahl", anzahl);
                        Befehl.Parameters.AddWithValue("BestellID", bestellId);

                        Verbindung.Open();

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
                        Text = $"Im {this.GetType().FullName} in der Funktion {typeof(BestellungSqlClientController).GetMethod("ErstelleEinzelBestellung")} ist ein Fehler aufgetreten \n" +
                               $"{e.GetType().FullName} = {e.Message} \n " +
                               $"{e.StackTrace}",
                        Typ = WIFI.Anwendung.Daten.ProtokollEintragTyp.Normal
                    });
            }

        }

       
    }
}
