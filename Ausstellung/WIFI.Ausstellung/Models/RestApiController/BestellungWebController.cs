using System;

namespace WIFI.Ausstellung.Models.RestApiController
{
    /// <summary>
    /// Stellt einen Dienst zum Verwalten der Bestellungen über die Rest-Api
    /// </summary>
    public class BestellungWebController : WIFI.Anwendung.SqlClient.Controller
    {
        /// <summary>
        /// Legt eine Neue Bestellung für einen Besucher an und gibt die Bestell-ID zurück
        /// </summary>
        /// <param name="besucher"></param>
        /// <returns></returns>
        public async System.Threading.Tasks.Task<int> ErstelleBestellung(Gateway.DTO.Besucher besucher)
        {



            const string Adresse = "{0}ErstelleBestellung?Id={1}&Vorname={2}&Nachname={3}&Hausnummer={4}&Ort={5}&PLZ={6}&Straßenname={7}&Telefon={8}";

            using (var Antwort = await this.HttpClient.GetAsync(
                   string.Format(
                       Adresse,
                       Properties.Settings.Default.UrlGatewayAPI,
                       besucher.Id,
                       besucher.Vorname,
                       besucher.Nachname,
                       besucher.Hausnummer,
                       besucher.Ort,
                       besucher.Postleitzahl,
                       besucher.Straßenname,
                       besucher.Telefon
                       )))
            {
                var AntwortText = await Antwort.Content.ReadAsStringAsync();


                this.AppKontext.Protokoll.Eintragen($"Der Status der Abfrage ErstelleBestellung beträgt {Antwort.StatusCode}");
                // Weil JSON erst ab .Net 5 intern unterstützt ist,
                // Newtonsoft.Json Nuget
                return Newtonsoft.Json.JsonConvert.DeserializeObject<int>(AntwortText);
            }
        }

        /// <summary>
        /// Fügt ein Buch mit der Anzahl einer BestellNr hinzu
        /// </summary>
        public async System.Threading.Tasks.Task BuchbestellungHinzufügen(Gateway.DTO.Buch buch, int bestellNr, int anzahl)
        {

            const string Adresse = "{0}BuchbestellungHinzufügen?Id={1}&Titel={2}&Autor={3}&buchnummr={4}&kategorie={5}&rabatt={6}&preis={7}&verlag={8}&anzahl={9}&bestellNr={10}";

            string ZielAdresse = string.Format(
                       Adresse,
                       Properties.Settings.Default.UrlGatewayAPI,
                       buch.ID,
                       buch.Titel,
                       buch.AutorName,
                       buch.Buchnummer,
                       buch.Kategoriegruppe,
                       buch.Rabattgruppe,
                       buch.Preis,
                       buch.VerlagName,
                       bestellNr,
                       anzahl);

            using (var Antwort = await this.HttpClient.GetAsync(
                  ZielAdresse)) {
                this.AppKontext.Protokoll.Eintragen($"Der Status der Abfrage BuchbestellungHinzufügen beträgt {Antwort.StatusCode}");
            };

        }

        /// <summary>
        /// Läd alle Bestellungen
        /// </summary>
        /// <returns></returns>

        public async System.Threading.Tasks.Task<Gateway.DTO.Bestellungen> HoleBestellungen()
        {
            const string Adresse = "{0}HoleBestellungen";

            using (var Antwort = await this.HttpClient.GetAsync(
                   string.Format(
                       Adresse,
                       Properties.Settings.Default.UrlGatewayAPI
                       )))
            {
                var AntwortText = await Antwort.Content.ReadAsStringAsync();

                // Weil JSON erst ab .Net 5 intern unterstützt ist,
                // Newtonsoft.Json Nuget
                // TODO: Buchliste in der Bestellung kann nicht deserializiert werden


                this.AppKontext.Protokoll.Eintragen($"Der Status der Abfrage HoleBestellungen beträgt {Antwort.StatusCode}");
                return Newtonsoft.Json.JsonConvert.DeserializeObject<Gateway.DTO.Bestellungen>(AntwortText);
            }
        }

        /// <summary>
        /// Läd alle Bücher zu der angegebenen BestellNr
        /// </summary>
        public async System.Threading.Tasks.Task<Gateway.DTO.Bücher> HoleBücherZuBestellung(int BestellNr)
        {
            const string Adresse = "{0}HoleBestellungen?BestellNr={1}";
            string ZielAdresse = string.Format(
                       Adresse,
                       Properties.Settings.Default.UrlGatewayAPI,
                       BestellNr);
            using (var Antwort = await this.HttpClient.GetAsync(

                       ZielAdresse))
            {
                var AntwortText = await Antwort.Content.ReadAsStringAsync();

                // Weil JSON erst ab .Net 5 intern unterstützt ist,
                // Newtonsoft.Json Nuget

                this.AppKontext.Protokoll.Eintragen($"Der Status der Abfrage HoleBücherZuBestellung beträgt {Antwort.StatusCode}");

                var bücher = Newtonsoft.Json.JsonConvert.DeserializeObject<Gateway.DTO.Bücher>(AntwortText);
                return bücher;
            }
        }

        /// <summary>
        /// FÜgt alle Bestellungen der Datenbank hinzu
        /// </summary>
        public async System.Threading.Tasks.Task AlleBuchbestellungenHinzufügen(Gateway.DTO.Bestellung bestellung)
        {

            foreach (var item in bestellung.Buchliste.Keys)
            {
                const string NewAdresse = "{0}BuchbestellungHinzufügen?Id={1}&Titel={2}&Autor={3}&buchnummr={4}&kategorie={5}&rabatt={6}&preis={7}&verlag={8}&bestellNr={9}&anzahl={10}";
                string ZielAdresse = string.Format(
                          NewAdresse,
                          Properties.Settings.Default.UrlGatewayAPI,
                          item.ID,
                          item.Titel,
                          item.AutorName,
                          item.Buchnummer,
                          item.Kategoriegruppe,
                          item.Rabattgruppe,
                          item.Preis.Value,
                          item.VerlagName,
                          bestellung.BestellNr,
                          item.Anzahl
                          );

                ZielAdresse = ZielAdresse.Replace(",", ".");


                using (var Antwort = await this.HttpClient.GetAsync(
                     ZielAdresse))
                {
                    // TODO: Aufruf funktioniert noch nicht so ganz

                    this.AppKontext.Protokoll.Eintragen($"Der Status der Abfrage AlleBuchbestellungenHinzufügen beträgt {Antwort.StatusCode}");
                }
            }


        }

        /// <summary>
        /// Ändert die Zusatz-Informationen zu der angegebenen Bestellung
        /// </summary>
        public async System.Threading.Tasks.Task AktualisiereBestellungsInfo(Gateway.DTO.Bestellung id)
        {

            const string Adresse = "{0}AktualisiereBestellungsInfo?id={1}";

            using (var Antwort = await this.HttpClient.GetAsync(
                 string.Format(
                     Adresse,
                     Properties.Settings.Default.UrlGatewayAPI,
                     id
                     )))
            {

                this.AppKontext.Protokoll.Eintragen($"Der Status der Abfrage AktualisiereBestellungsInfo beträgt {Antwort.StatusCode}");
            }

        }

        /// <summary>
        /// Holt die Id einer Bestellung durch die Besucher-Informationen
        /// </summary>
        /// <returns>Gibt die Id der Bestellung zurück</returns>
        public async System.Threading.Tasks.Task<int> BekommeBestellungsId(Gateway.DTO.Besucher besucher)
        {
            const string Adresse = "{0}BekommeBestellungsId?besucher={1}";

            using (var Antwort = await this.HttpClient.GetAsync(
                 string.Format(
                     Adresse,
                     Properties.Settings.Default.UrlGatewayAPI,
                     besucher
                     )))
            {
                var AntwortText = await Antwort.Content.ReadAsStringAsync();

                // Weil JSON erst ab .Net 5 intern unterstützt ist,
                // Newtonsoft.Json Nuget

                this.AppKontext.Protokoll.Eintragen($"Der Status der Abfrage BekommeBestellungsId beträgt {Antwort.StatusCode}");

                return Newtonsoft.Json.JsonConvert.DeserializeObject<int>(AntwortText);
            }
        }

        /// <summary>
        /// Trägt in die Datenbank ein, dass die Bestellung abgeholt wurde
        /// </summary>
        public async System.Threading.Tasks.Task BestellungAbgeholt(Gateway.DTO.Bestellung bestellung)
        {
            const string Adresse = "{0}BestellungAbgeholt?bestellung={1}";

            using (var Antwort = await this.HttpClient.GetAsync(
                 string.Format(
                     Adresse,
                     Properties.Settings.Default.UrlGatewayAPI,
                     bestellung
                     )))
            {

                this.AppKontext.Protokoll.Eintragen($"Der Status der Abfrage BestellungAbgeholt beträgt {Antwort.StatusCode}");
            }
        }

        /// <summary>
        /// Läd alle Daten zu einer Bestellid
        /// </summary>
        public async System.Threading.Tasks.Task<Gateway.DTO.Bestellung> HoleBestellung(int bestellId)
        {

            const string Adresse = "{0}HoleBestellung?bestellId={1}";
            string ZielAdresse = string.Format(
                     Adresse,
                     Properties.Settings.Default.UrlGatewayAPI,
                     bestellId);
            try
            {
                using (var Antwort = await this.HttpClient.GetAsync(
                 ZielAdresse))
                {
                    var AntwortText = await Antwort.Content.ReadAsStringAsync();

                    // Weil JSON erst ab .Net 5 intern unterstützt ist,
                    // Newtonsoft.Json Nuget

                    this.AppKontext.Protokoll.Eintragen($"Der Status der Abfrage HoleBestellung beträgt {Antwort.StatusCode}");

                    return Newtonsoft.Json.JsonConvert.DeserializeObject<Gateway.DTO.Bestellung>(AntwortText);
                }
            }
            catch (Exception e)
            {
                this.OnFehlerAufgetreten(new Anwendung.FehlerAufgetretenEventArgs(e));
                this.AppKontext.Protokoll.Eintragen(string.Format(@"In {0} ist ein Fehler aufgetreten: {1}", this.GetType().FullName, e.Message));
            }
            return new Gateway.DTO.Bestellung();

        }

        /// <summary>
        /// Aktualisiert die Daten zu einer Bestellung
        /// </summary>
        /// <param name="bestellung"></param>
        public async System.Threading.Tasks.Task AktualisiereBestellung(Gateway.DTO.Bestellung bestellung)
        {

            // Für die Bücher der Bestellung
            const string Adresse =
                    "{0}AktualisiereBestellungsInfo?" +
                        "BestellNr={1}&BesucherId={2}&BesucherVorname={3}" +
                        "&BesucherNachname={4}&BesucherHausnummer={5}&Ort={6}" +
                        "&PLZ={7}&Straße={8}&Telefon={9}";

            using (var Antwort = await this.HttpClient.GetAsync(
                 string.Format(
                     Adresse,
                     Properties.Settings.Default.UrlGatewayAPI,
                     bestellung.BestellNr,
                     bestellung.ZugehörigerBesucher.Id,
                     bestellung.ZugehörigerBesucher.Vorname,
                     bestellung.ZugehörigerBesucher.Nachname,
                     bestellung.ZugehörigerBesucher.Hausnummer,
                     bestellung.ZugehörigerBesucher.Ort,
                     bestellung.ZugehörigerBesucher.Postleitzahl,
                     bestellung.ZugehörigerBesucher.Straßenname,
                     bestellung.ZugehörigerBesucher.Telefon

                     )))
            {

                this.AppKontext.Protokoll.Eintragen($"Der Status der Abfrage AktualisiereBestellung beträgt {Antwort.StatusCode}");
            }

        }

        /// <summary>
        /// Ändert die Buchanzahl eines Buches von einer Bestellung
        /// </summary>
        public async System.Threading.Tasks.Task AktualisiereBestellungBuch(int buchid,int anzahl,int bestellid)
        {
            const string SecAdresse = "{0}AktualisiereBestellungBuch?buchid={1}&anzahl={2}&bestellId={3}";

            using (var Antwort = await this.HttpClient.GetAsync(
                 string.Format(
                     SecAdresse,
                     Properties.Settings.Default.UrlGatewayAPI,
                     buchid,
                     anzahl,
                     bestellid

                     )))
            {
                this.AppKontext.Protokoll.Eintragen($"Der Status der Abfrage AktualisiereBestellungBuch beträgt {Antwort.StatusCode}");
            }
        }
    }
}
