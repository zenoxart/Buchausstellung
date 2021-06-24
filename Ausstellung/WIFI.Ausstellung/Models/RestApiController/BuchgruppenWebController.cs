using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WIFI.Ausstellung.Models.RestApiController
{

    /// <summary>
    /// Stellt einen Dienst zum Verwalten der Buchgruppen über die Rest-Api
    /// </summary>
    public class BuchgruppenWebController : WIFI.Anwendung.ViewModelAppObjekt
    {
        /// <summary>
        /// Ändert die Informationen zu der übergebenen Buchgruppe
        /// </summary>
        public async void AktualisiereBuchgruppe(Gateway.DTO.Buchgruppe id)
        {
            const string Adresse = "{0}AktualisiereBuchgruppe?id={1}";

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
        /// Entfernt die Buchgruppe aus der Datenbank
        /// </summary>
        public async void EntferneBuchgruppe(Gateway.DTO.Buchgruppe id)
        {
            const string Adresse = "{0}EntferneBuchgruppe?Gruppennr={1}&Beschreibung={2}";

            using (var Antwort = await this.HttpClient.GetAsync(
                   string.Format(
                       Adresse,
                       Properties.Settings.Default.UrlGatewayAPI,
                       id.Gruppennummer,
                       id.Beschreibung
                       )))
            {
            }
        }

        /// <summary>
        /// 
        /// Legt eine Buchgruppe in der Datenbank an
        /// </summary>
        public async void ErstelleBuchgruppe(Gateway.DTO.Buchgruppe gruppe)
        {
            const string Adresse = "{0}ErstelleBuchgruppe?Gruppennummer={1}&Beschreibung={2}";
            
            string ZielAdresse = string.Format(
                       Adresse,
                       Properties.Settings.Default.UrlGatewayAPI,
                       gruppe.Gruppennummer,
                       gruppe.Beschreibung
                       );

            using (var Antwort = await this.HttpClient.GetAsync(
                   ZielAdresse))
            {
            }
        }

        /// <summary>
        /// Läd alle Buchgruppen von der Datenbank
        /// </summary>
        public async System.Threading.Tasks.Task<Gateway.DTO.Buchgruppen> HoleBuchgruppen()
        {
            const string Adresse = "{0}HoleBuchgruppen";

            using (var Antwort = await this.HttpClient.GetAsync(
                   string.Format(
                       Adresse,
                       Properties.Settings.Default.UrlGatewayAPI
                       )))
            {
                var AntwortText = await Antwort.Content.ReadAsStringAsync();

                // Weil JSON erst ab .Net 5 intern unterstützt ist,
                // Newtonsoft.Json Nuget
                return Newtonsoft.Json.JsonConvert.DeserializeObject<WIFI.Gateway.DTO.Buchgruppen>(AntwortText);
            }
        }
    }
}
