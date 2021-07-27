namespace WIFI.Ausstellung.Models.RestApiController
{
    /// <summary>
    /// Stellt einen Dienst zum Verwalten der Bestellungen über die Rest-Api
    /// </summary>
    public class BuchWebController : WIFI.Anwendung.ViewModelAppObjekt
    {
        /// <summary>
        /// Entfernt das Buch aus der Datenbank
        /// </summary>
        public async System.Threading.Tasks.Task EntferneBuch(Gateway.DTO.Buch id)
        {
            const string Adresse = "{0}EntferneBuch?id={1}";

            using (var Antwort = await this.HttpClient.GetAsync(
                   string.Format(
                       Adresse,
                       Properties.Settings.Default.UrlGatewayAPI,
                       id
                       )))
            {

                this.AppKontext.Protokoll.Eintragen($"Der Status der Abfrage EntferneBuch beträgt {Antwort.StatusCode}");
            }
        }

        /// <summary>
        /// Legt ein Buch in der Datenbank an
        /// </summary>
        public async System.Threading.Tasks.Task ErstelleBuch(Gateway.DTO.Buch buch)
        {


            const string Adresse = "{0}ErstelleBuch?Anzahl={1}&Autorname={2}&Buchnummer={3}&Kategorie={4}&Rabatt={5}&Titel={6}&Verlag={7}&Preis={8}";
            string ZielAdresse = string.Format(
                       Adresse,
                       Properties.Settings.Default.UrlGatewayAPI,
                       buch.Anzahl,
                       buch.AutorName,
                       buch.Buchnummer,
                       buch.Kategoriegruppe,
                       buch.Rabattgruppe,
                       buch.Titel,
                       buch.VerlagName,
                       buch.Preis.Value
                       );
            ZielAdresse = ZielAdresse.Replace(",", ".");
            using (var Antwort = await this.HttpClient.GetAsync( ZielAdresse))
            {

                this.AppKontext.Protokoll.Eintragen($"Der Status der Abfrage ErstelleBuch beträgt {Antwort.StatusCode}");
            }
        }

        /// <summary>
        /// Läd die Daten aller Bücher
        /// </summary>
        public async System.Threading.Tasks.Task<Gateway.DTO.Bücher> HoleBücher()
        {
            const string Adresse = "{0}HoleBücher";

            using (var Antwort = await this.HttpClient.GetAsync(
                   string.Format(
                       Adresse,
                       Properties.Settings.Default.UrlGatewayAPI
                       )))
            {
                var AntwortText = await Antwort.Content.ReadAsStringAsync();

                // Weil JSON erst ab .Net 5 intern unterstützt ist,
                // Newtonsoft.Json Nuget

                this.AppKontext.Protokoll.Eintragen($"Der Status der Abfrage HoleBücher beträgt {Antwort.StatusCode}");
                return Newtonsoft.Json.JsonConvert.DeserializeObject<WIFI.Gateway.DTO.Bücher>(AntwortText);
            }
        }

        /// <summary>
        /// Aktualisiert die Daten zu dem Buch
        /// </summary>
        public async System.Threading.Tasks.Task UpdateBuch(Gateway.DTO.Buch id)
        {
            const string Adresse = "{0}UpdateBuch?id={1}";

            using (var Antwort = await this.HttpClient.GetAsync(
                   string.Format(
                       Adresse,
                       Properties.Settings.Default.UrlGatewayAPI,
                       id
                       )))
            {

                this.AppKontext.Protokoll.Eintragen($"Der Status der Abfrage UpdateBuch beträgt {Antwort.StatusCode}");
            }
        }
    }
}
