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
    }
}
