using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
            //int Id, string Vorname,string Nachname,int Hausnummer,string Ort,int PLZ,string Straßenname,string Telefon


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

                // Weil JSON erst ab .Net 5 intern unterstützt ist,
                // Newtonsoft.Json Nuget
                return Newtonsoft.Json.JsonConvert.DeserializeObject<int>(AntwortText);
            }
        }

        /// <summary>
        /// Fügt ein Buch mit der Anzahl einer BestellNr hinzu
        /// </summary>
        public async void BuchbestellungHinzufügen(Gateway.DTO.Buch buch, int bestellNr, int anzahl)
        {
            //int Id,string Titel,string Autor, string buchnummr, int kategorie, int rabatt, decimal? preis, string verlag,
            //DTO.Buch buch, int bestellNr, int anzahl
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
                  ZielAdresse))
            {
            }

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
                return Newtonsoft.Json.JsonConvert.DeserializeObject<Gateway.DTO.Bestellungen>(AntwortText);
            }
        }

        /// <summary>
        /// Läd alle Bücher zu der angegebenen BestellNr
        /// </summary>
        public async System.Threading.Tasks.Task<Gateway.DTO.Bücher> HoleBücherZuBestellung(int BestellNr)
        {
            const string Adresse = "{0}HoleBestellungen?BestellNr={1}";

            using (var Antwort = await this.HttpClient.GetAsync(
                   string.Format(
                       Adresse,
                       Properties.Settings.Default.UrlGatewayAPI,
                       BestellNr
                       )))
            {
                var AntwortText = await Antwort.Content.ReadAsStringAsync();

                // Weil JSON erst ab .Net 5 intern unterstützt ist,
                // Newtonsoft.Json Nuget
                return Newtonsoft.Json.JsonConvert.DeserializeObject<Gateway.DTO.Bücher>(AntwortText);
            }
        }

        /// <summary>
        /// FÜgt alle Bestellungen der Datenbank hinzu
        /// </summary>
        public async void AlleBuchbestellungenHinzufügen(Gateway.DTO.Bestellung bestellung)
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
                }
            }

            //const string Adresse = "{0}AlleBuchbestellungenHinzufügen?BestellNr={1}&buchliste={2}";

            //using (var Antwort = await this.HttpClient.GetAsync(
            //       string.Format(
            //           Adresse,
            //           Properties.Settings.Default.UrlGatewayAPI,
            //           bestellung.BestellNr,
            //           bestellung.Buchliste
            //           )))
            //{
            //}

        }

        /// <summary>
        /// Ändert die Zusatz-Informationen zu der angegebenen Bestellung
        /// </summary>
        public async void AktualisiereBestellungsInfo(Gateway.DTO.Bestellung id)
        {

            const string Adresse = "{0}AktualisiereBestellungsInfo?id={1}";

            using (var Antwort = await this.HttpClient.GetAsync(
                 string.Format(
                     Adresse,
                     Properties.Settings.Default.UrlGatewayAPI,
                     id
                     )))
            {
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
                //Enum.Parse(, AntwortText);
                // Weil JSON erst ab .Net 5 intern unterstützt ist,
                // Newtonsoft.Json Nuget
                return Newtonsoft.Json.JsonConvert.DeserializeObject<int>(AntwortText);
            }
        }

        /// <summary>
        /// Trägt in die Datenbank ein, dass die Bestellung abgeholt wurde
        /// </summary>
        public async void BestellungAbgeholt(Gateway.DTO.Bestellung bestellung)
        {
            const string Adresse = "{0}BestellungAbgeholt?bestellung={1}";

            using (var Antwort = await this.HttpClient.GetAsync(
                 string.Format(
                     Adresse,
                     Properties.Settings.Default.UrlGatewayAPI,
                     bestellung
                     )))
            { }
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
                    return Newtonsoft.Json.JsonConvert.DeserializeObject<Gateway.DTO.Bestellung>(AntwortText);
                }
            }
            catch (Exception e)
            {

                throw;
            }

        }

        /// <summary>
        /// Aktualisiert die Daten zu einer Bestellung
        /// </summary>
        /// <param name="bestellung"></param>
        public async void AktualisiereBestellung(Gateway.DTO.Bestellung bestellung)
        {

            // Für die Bücher der Bestellung


            //int BestellNr, int BesucherId,string BesucherVorname, string BesucherNachname, int BesucherHausnummer, string Ort, int PLZ, string Straße,string Telefon
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

            }
            foreach (var item in bestellung.Buchliste.Keys)
            {
               
                AktualisiereBestellungBuch(item.ID, bestellung.Buchliste[item], bestellung.BestellNr);
            }

        }

        /// <summary>
        /// Ändert die Buchanzahl eines Buches von einer Bestellung
        /// </summary>
        public async void AktualisiereBestellungBuch(int buchid,int anzahl,int bestellid)
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
            }
        }
    }
}
