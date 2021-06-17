using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
        public async void ErstelleVeranstaltung()
        {
            const string Adresse = "{0}ErstelleVeranstaltung";

            using (var Antwort = await this.HttpClient.GetAsync(
                   string.Format(
                       Adresse,
                       Properties.Settings.Default.UrlGatewayAPI
                       )))
            {

            }
        }

        public async void StarteVeranstaltung(DateTime StartDatum, DateTime EndDatum, string Ort)
        {
            const string Adresse = "{0}StarteVeranstaltung?StartDatum={1}&EndDatum={2}&Ort={3}";

            using (var Antwort = await this.HttpClient.GetAsync(
                   string.Format(
                       Adresse,
                       Properties.Settings.Default.UrlGatewayAPI,
                       StartDatum,
                       EndDatum,
                       Ort
                       )))
            {

            }
        }

        public async void UpdateVeranstaltungsStadium(WIFI.Gateway.DTO.AusstellungsstadiumTyp typ)
        {
            const string Adresse = "{0}UpdateVeranstaltungsStadium?Typ={1}";

            using (var Antwort = await this.HttpClient.GetAsync(
                 string.Format(
                     Adresse,
                     Properties.Settings.Default.UrlGatewayAPI,
                     typ
                     )))
            {

            }
        }

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
                return Newtonsoft.Json.JsonConvert.DeserializeObject<WIFI.Gateway.DTO.AusstellungsstadiumTyp>(AntwortText);
            }
        }
    }
}
