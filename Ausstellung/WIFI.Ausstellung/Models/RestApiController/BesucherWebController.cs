namespace WIFI.Ausstellung.Models.RestApiController
{

    /// <summary>
    /// Stellt einen Dienst zum Verwalten der Besucher über die Rest-Api
    /// </summary>
    public class BesucherWebController : WIFI.Anwendung.ViewModelAppObjekt
    {
        /// <summary>
        /// Erstellt einen Neuen Besucher in der Datenbank und gibt das erstelle Objekt zurück
        /// </summary>
        public async System.Threading.Tasks.Task<WIFI.Gateway.DTO.Besucher> ErstelleBesucher(WIFI.Gateway.DTO.Besucher besucher)
        {
            const string Adresse = "{0}ErstelleBesucher?Vorname={1}&Nachname={2}&Hausnummer={3}&Ort={4}&PLZ={5}&Straßenname={6}&Telefon={7}";
            string ZielAdresse = string.Format(
                       Adresse,
                       Properties.Settings.Default.UrlGatewayAPI,
                       besucher.Vorname,
                       besucher.Nachname,
                       besucher.Hausnummer,
                       besucher.Ort,
                       besucher.Postleitzahl,
                       besucher.Straßenname,
                       besucher.Telefon

                       );
            using (var Antwort = await this.HttpClient.GetAsync(
                 ZielAdresse))
            {
                var AntwortText = await Antwort.Content.ReadAsStringAsync();

                // Weil JSON erst ab .Net 5 intern unterstützt ist,
                // Newtonsoft.Json Nuget

                this.AppKontext.Protokoll.Eintragen($"Der Status der Abfrage ErstelleBesucher beträgt {Antwort.StatusCode}");

                return Newtonsoft.Json.JsonConvert.DeserializeObject<WIFI.Gateway.DTO.Besucher>(AntwortText);
            }
        }

        /// <summary>
        /// Ändert den übergebenen Besucher
        /// </summary>
        public async System.Threading.Tasks.Task AktualisiereBesucher(Gateway.DTO.Besucher besucher)
        {
            const string Adresse = "{0}AktualisiereBesucher?besucher={1}";

            using (var Antwort = await this.HttpClient.GetAsync(
                  string.Format(
                      Adresse,
                      Properties.Settings.Default.UrlGatewayAPI,
                      besucher
                      )))
            {

                this.AppKontext.Protokoll.Eintragen($"Der Status der Abfrage AktualisiereBesucher beträgt {Antwort.StatusCode}");
            }
        }

        /// <summary>
        /// Gibt die Id des Besuchers zurück, zudem die übergebenen Informationen stimmen
        /// </summary>
        public async System.Threading.Tasks.Task<int> BekommeBesucherId(Gateway.DTO.Besucher besucher)
        {
            const string Adresse = "{0}BekommeBesucherId?besucher={1}";

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

                this.AppKontext.Protokoll.Eintragen($"Der Status der Abfrage BekommeBesucherId beträgt {Antwort.StatusCode}");

                return Newtonsoft.Json.JsonConvert.DeserializeObject<int>(AntwortText);
            }


        }
    }
}
