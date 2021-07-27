using System.Web.Http;

namespace WIFI.Gateway
{
    /// <summary>
    /// Stellt die Konfiguration für eine REST-API
    /// </summary>
    public static class WebApiConfig
    {
        /// <summary>
        /// Registriert die übergebenen Daten
        /// </summary>
        public static void Register(HttpConfiguration config)
        {
            // Web-API-Konfiguration und -Dienste

            // Web-API-Routen
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );
        }
    }
}
