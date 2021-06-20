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

            using (var Antwort = await this.HttpClient.GetAsync(
                   string.Format(
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
                       anzahl
                       )))
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
                return Newtonsoft.Json.JsonConvert.DeserializeObject<Gateway.DTO.Bestellungen>(AntwortText);
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
    }
}
