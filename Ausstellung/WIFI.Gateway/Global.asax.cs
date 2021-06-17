using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Routing;

namespace WIFI.Gateway
{
    public class WebApiApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            var AppKontext = new WIFI.Anwendung.DatenbankAppKontext();

            AppKontext.Protokoll.Pfad = System.Web.Configuration.WebConfigurationManager.AppSettings["Protokollpfad"];

            if (AppKontext.Protokoll.Pfad != string.Empty)
            {
                AppKontext.Protokoll.Pfad = this.Server.MapPath(AppKontext.Protokoll.Pfad);
            }

            AppKontext.SqlServer = System.Web.Configuration.WebConfigurationManager.AppSettings["SqlServer"];

            AppKontext.DatenbankName = System.Web.Configuration.WebConfigurationManager.AppSettings["DatenbankName"];

            AppKontext.DatenbankPfad = System.Web.Configuration.WebConfigurationManager.AppSettings["DatenbankPfad"];
            AppKontext.DatenbankPfad = this.Server.MapPath(AppKontext.DatenbankPfad);

            GlobalConfiguration.Configuration.Properties.TryAdd(AppKontext.GetType().FullName, AppKontext);

            GlobalConfiguration.Configure(WebApiConfig.Register);

        }
    }
}
