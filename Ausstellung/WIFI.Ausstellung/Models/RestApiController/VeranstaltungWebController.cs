using System;

namespace WIFI.Ausstellung.Models.RestApiController
{
    /// <summary>
    /// Stellt einen Dienst zum Verwalten der Veranstaltung über die Rest-Api
    /// </summary>
    public class VeranstaltungWebController : WIFI.Anwendung.ViewModelAppObjekt
    {
        /// <summary>
        /// Erstellt einen Eintrag unter dem Table Veranstaltung in der Datenbank, falls noch keiner existiert
        /// </summary>
        public async System.Threading.Tasks.Task ErstelleVeranstaltung()
        {
            const string Adresse = "{0}ErstelleVeranstaltung";

            using (var Antwort = await this.HttpClient.GetAsync(
                   string.Format(
                       Adresse,
                       Properties.Settings.Default.UrlGatewayAPI
                       )))
            {



                this.AppKontext.Protokoll.Eintragen($"Der Status der Abfrage ErstelleVeranstaltung beträgt {Antwort.StatusCode}");
            }
        }


        /// <summary>
        /// Ändert Daten der Veranstaltung sowie das Stadium
        /// </summary>
        public async System.Threading.Tasks.Task StarteVeranstaltung(DateTime StartDatum, DateTime EndDatum, string Ort)
        {
            const string Adresse = "{0}StarteVeranstaltung?StartDatum={1}&EndDatum={2}&Ort={3}";
            string ZielAdresse = string.Format(
                       Adresse,
                       Properties.Settings.Default.UrlGatewayAPI,
                       StartDatum.ToString("yyyy/MM/dd"),
                       EndDatum.ToString("yyyy/MM/dd"),
                       Ort
                       );
            using (var Antwort = await this.HttpClient.GetAsync(
                   ZielAdresse))
            {

                this.AppKontext.Protokoll.Eintragen($"Der Status der Abfrage StarteVeranstaltung beträgt {Antwort.StatusCode}");
            }
        }

        /// <summary>
        /// Aktuallisiert das Stadium der Veranstaltung
        /// </summary>
        public async System.Threading.Tasks.Task UpdateVeranstaltungsStadium(WIFI.Gateway.DTO.AusstellungsstadiumTyp typ)
        {
            const string Adresse = "{0}UpdateVeranstaltungsStadium?Typ={1}";

            using (var Antwort = await this.HttpClient.GetAsync(
                 string.Format(
                     Adresse,
                     Properties.Settings.Default.UrlGatewayAPI,
                     typ
                     )))
            {

                this.AppKontext.Protokoll.Eintragen($"Der Status der Abfrage UpdateVerantaltungsStadium beträgt {Antwort.StatusCode}");
            }
        }

        /// <summary>
        /// Läd das aktuelle Stadium der Veranstaltung
        /// </summary>
        /// <returns></returns>
        public async System.Threading.Tasks.Task<WIFI.Gateway.DTO.AusstellungsstadiumTyp> HoleVeranstaltungsStadium()
        {
            const string Adresse = "{0}HoleVeranstaltungsStadium";

            using (var Antwort = await this.HttpClient.GetAsync(
                 string.Format(
                     Adresse,
                     Properties.Settings.Default.UrlGatewayAPI
                     )))
            {
                var AntwortText = await Antwort.Content.ReadAsStringAsync();
                // Weil JSON erst ab .Net 5 intern unterstützt ist,
                // Newtonsoft.Json Nuget

                this.AppKontext.Protokoll.Eintragen($"Der Status der Abfrage HoleVeranstaltungStadium beträgt {Antwort.StatusCode}");

                return Newtonsoft.Json.JsonConvert.DeserializeObject<WIFI.Gateway.DTO.AusstellungsstadiumTyp>(AntwortText);
            }
        }

        /// <summary>
        /// Löscht alle Informationen zu der Veranstaltung
        /// </summary>
        public async System.Threading.Tasks.Task BeendeVeranstaltung()
        {
            const string Adresse = "{0}BeendeVeranstaltung";

            using (var Antwort = await this.HttpClient.GetAsync(
                string.Format(
                    Adresse,
                    Properties.Settings.Default.UrlGatewayAPI
                    )))
            {

                this.AppKontext.Protokoll.Eintragen($"Der Status der Abfrage BeendeVeranstaltung beträgt {Antwort.StatusCode}");
            }
        }

        /// <summary>
        /// Läd den Ort einer Veranstaltung
        /// </summary>
        public async System.Threading.Tasks.Task<string> HoleGemeinde()
        {
            const string Adresse = "{0}HoleGemeinde";

            using (var Antwort = await this.HttpClient.GetAsync(
                string.Format(
                    Adresse,
                    Properties.Settings.Default.UrlGatewayAPI
                    )))
            {

                var AntwortText = await Antwort.Content.ReadAsStringAsync();
                // Weil JSON erst ab .Net 5 intern unterstützt ist,
                // Newtonsoft.Json Nuget

                this.AppKontext.Protokoll.Eintragen($"Der Status der Abfrage HoleGemeinde beträgt {Antwort.StatusCode}");

                return Newtonsoft.Json.JsonConvert.DeserializeObject<string>(AntwortText);
            }
        }
    }
}
