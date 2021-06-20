﻿using System;
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
        public DTO.Bestellungen HoleBestellungen()
        {
            var bestellungen = new DTO.Bestellungen();

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
                                    new DTO.Bestellung
                                    {
                                        BestellNr = Convert.ToInt32(DatenLeser["ID"]),
                                        ZugehörigerBesucher = new DTO.Besucher
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
                foreach (var item in bestellungen)
                {
                    using (var Verbindung = new System.Data.SqlClient.SqlConnection(this.ConnectionString))
                    {
                        using (var ZweiterBefehl = new System.Data.SqlClient.SqlCommand("HoleBücherZuBestellungsInfo", Verbindung))
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
                                            AutorName = ZweiterDatenLeser["Autor"].ToString(),
                                            ID = Convert.ToInt32(ZweiterDatenLeser["BuchId"]),
                                            Buchnummer = ZweiterDatenLeser["BuchNr"].ToString(),
                                            Kategoriegruppe = Convert.ToInt32(ZweiterDatenLeser["Kategorie"]),
                                            Preis = Convert.ToDecimal(ZweiterDatenLeser["Preis"]),
                                            Titel = ZweiterDatenLeser["BuchTitel"].ToString(),
                                            VerlagName = ZweiterDatenLeser["Verlag"].ToString(),
                                            Rabattgruppe = Convert.ToInt32(ZweiterDatenLeser["Rabatt"]),
                                            Anzahl = Convert.ToInt32(ZweiterDatenLeser["Buchanzahl"])
                                        }, Convert.ToInt32(ZweiterDatenLeser["Buchanzahl"])

                                        );
                                }
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

            return bestellungen;
        }

        /// <summary>
        /// Ruft die Daten einer einzelnen
        /// Bestellung ab
        /// </summary>
        /// <param name="BestellNr">Die interne ID der Bestellung</param>
        /// <returns></returns>
        public DTO.Bestellung HoleBestellung(int BestellNr)
        {
            var bestellungen = new DTO.Bestellungen();

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
                                    new DTO.Bestellung
                                    {
                                        BestellNr = Convert.ToInt32(DatenLeser["ID"]),
                                        ZugehörigerBesucher = new DTO.Besucher
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

                foreach (var item in bestellungen)
                {
                    using (var Verbindung = new System.Data.SqlClient.SqlConnection(this.ConnectionString))
                    {
                        using (var ZweiterBefehl = new System.Data.SqlClient.SqlCommand("HoleBücherZuBestellungsInfo", Verbindung))
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
                                            AutorName = ZweiterDatenLeser["Autor"].ToString(),
                                            ID = Convert.ToInt32(ZweiterDatenLeser["BuchId"]),
                                            Buchnummer = ZweiterDatenLeser["BuchNr"].ToString(),
                                            Kategoriegruppe = Convert.ToInt32(ZweiterDatenLeser["Kategorie"]),
                                            Preis = Convert.ToDecimal(ZweiterDatenLeser["Preis"]),
                                            Titel = ZweiterDatenLeser["BuchTitel"].ToString(),
                                            VerlagName = ZweiterDatenLeser["Verlag"].ToString(),
                                            Rabattgruppe = Convert.ToInt32(ZweiterDatenLeser["Rabatt"]),
                                            Anzahl = Convert.ToInt32(ZweiterDatenLeser["Buchanzahl"])
                                        }, Convert.ToInt32(ZweiterDatenLeser["Buchanzahl"])

                                        );
                                }
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
            var bestellung = new WIFI.Gateway.DTO.Bestellung();

            foreach (var item in bestellungen)
            {
                if (item.BestellNr == BestellNr)
                {
                    bestellung = item;
                }
            }

            return bestellung;
        }

        /// <summary>
        /// Aktualisiert alle abgeholten Bestellungen
        /// </summary>
        /// <param name="bestellungs"></param>
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
        public void AktualisiereBestellung(DTO.Bestellung bestellung)
        {
            using (var Verbindung = new System.Data.SqlClient.SqlConnection(ConnectionString))
            {
                using (var Befehl = new System.Data.SqlClient.SqlCommand("AktualisiereBestellungsInfo", Verbindung))
                {
                    Befehl.CommandType = System.Data.CommandType.StoredProcedure;

                    Befehl.Parameters.AddWithValue("ID", bestellung.BestellNr);
                    Befehl.Parameters.AddWithValue("Besucher_ID", bestellung.ZugehörigerBesucher.Id);

                    Verbindung.Open();

                    Befehl.Prepare();

                    Befehl.ExecuteNonQuery();
                }
            }

            // Aktualisiert die Besucherdaten der Bestellung
            using (var Verbindung = new System.Data.SqlClient.SqlConnection(ConnectionString))
            {
                using (var Befehl = new System.Data.SqlClient.SqlCommand("AktualisiereBesucher", Verbindung))
                {
                    Befehl.CommandType = System.Data.CommandType.StoredProcedure;

                    Befehl.Parameters.AddWithValue("ID", bestellung.ZugehörigerBesucher.Id);
                    Befehl.Parameters.AddWithValue("Vorname", bestellung.ZugehörigerBesucher.Vorname);
                    Befehl.Parameters.AddWithValue("Nachname", bestellung.ZugehörigerBesucher.Nachname);
                    Befehl.Parameters.AddWithValue("PLZ", bestellung.ZugehörigerBesucher.Postleitzahl);
                    Befehl.Parameters.AddWithValue("Telefon", bestellung.ZugehörigerBesucher.Telefon);
                    Befehl.Parameters.AddWithValue("Straße", bestellung.ZugehörigerBesucher.Straßenname);
                    Befehl.Parameters.AddWithValue("Hausnummer", bestellung.ZugehörigerBesucher.Hausnummer);
                    Befehl.Parameters.AddWithValue("Ort", bestellung.ZugehörigerBesucher.Ort);
                    Verbindung.Open();

                    Befehl.Prepare();

                    Befehl.ExecuteScalar();
                }
            }

            // Aktualisiert die Bücher der Bestellung
            foreach (DTO.Buch item in bestellung.Buchliste.Keys)
            {
                using (var Verbindung = new System.Data.SqlClient.SqlConnection(ConnectionString))
                {
                    using (var Befehl = new System.Data.SqlClient.SqlCommand("AktualisiereBuch", Verbindung))
                    {
                        Befehl.CommandType = System.Data.CommandType.StoredProcedure;

                        Befehl.Parameters.AddWithValue("ID", item.ID);
                        Befehl.Parameters.AddWithValue("Buchnr", item.Buchnummer);
                        Befehl.Parameters.AddWithValue("Autor", item.AutorName);
                        Befehl.Parameters.AddWithValue("Preis", item.Preis);
                        Befehl.Parameters.AddWithValue("Rabgr", item.Rabattgruppe);
                        Befehl.Parameters.AddWithValue("Katgr", item.Kategoriegruppe);
                        Befehl.Parameters.AddWithValue("Verlagname", item.VerlagName);
                        Befehl.Parameters.AddWithValue("Titel", item.Titel);
                        Befehl.Parameters.AddWithValue("Anzahl", item.Anzahl);
                        Befehl.Parameters.AddWithValue("BestellID", bestellung.BestellNr);

                        Verbindung.Open();

                        Befehl.Prepare();

                        Befehl.ExecuteScalar();
                    }
                }
            }
        }

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
    }
}