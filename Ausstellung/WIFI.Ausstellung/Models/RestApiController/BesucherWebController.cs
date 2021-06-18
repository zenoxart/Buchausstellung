using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
            const string Adresse = "{0}ErstelleBesucher?besucher={1}";

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
                return Newtonsoft.Json.JsonConvert.DeserializeObject<WIFI.Gateway.DTO.Besucher>(AntwortText);
            }
        }
    }
}
