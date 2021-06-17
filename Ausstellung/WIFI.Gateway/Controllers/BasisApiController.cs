using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace WIFI.Gateway.Controllers
{
    /// <summary>
    /// Stellt die Basis für einen API-Controller welcher zugriff auf die Infrastruktur hat
    /// </summary>
    public class BasisApiController : ApiController
    {
        /// <summary>
        /// Internes Feld für die Eigenschaft
        /// </summary>
        private WIFI.Anwendung.DatenbankAppKontext _AppKontext;

        /// <summary>
        /// Ruft die WIFI Infrastruktur ab
        /// </summary>
        public WIFI.Anwendung.DatenbankAppKontext AppKontext
        {
            get
            {
                if (this._AppKontext == null)
                {
                    this._AppKontext = this.Configuration.Properties[typeof(WIFI.Anwendung.DatenbankAppKontext).FullName]
                        as WIFI.Anwendung.DatenbankAppKontext;
                }
                return this._AppKontext;
            }
        }
    }
}