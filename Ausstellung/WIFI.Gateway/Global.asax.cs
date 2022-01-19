using System.Web.Http;

namespace WIFI.Gateway
{
    /// <summary>
    /// Stellt den Dienst einer REST-API
    /// </summary>
    public class WebApiApplication : System.Web.HttpApplication
    {
        /// <summary>
        /// Stellt den Haupteinstiegspunkt der REST-API
        /// </summary>
        protected void Application_Start()
        {
            var AppKontext = new WIFI.Anwendung.DatenbankAppKontext();

            AppKontext.Protokoll.Pfad = System.Web.Configuration.WebConfigurationManager.AppSettings["Protokollpfad"];

            if (AppKontext.Protokoll.Pfad != string.Empty) AppKontext.Protokoll.Pfad = this.Server.MapPath(AppKontext.Protokoll.Pfad);

            AppKontext.SqlServer = System.Web.Configuration.WebConfigurationManager.AppSettings["SqlServer"];
            AppKontext.DatenbankName = System.Web.Configuration.WebConfigurationManager.AppSettings["DatenbankName"];
            AppKontext.DatenbankPfad = System.Web.Configuration.WebConfigurationManager.AppSettings["DatenbankPfad"];
            AppKontext.DatenbankPfad = this.Server.MapPath(AppKontext.DatenbankPfad);

            GlobalConfiguration.Configuration.Properties.TryAdd(AppKontext.GetType().FullName, AppKontext);
            GlobalConfiguration.Configure(WebApiConfig.Register);

        }
    }
}
