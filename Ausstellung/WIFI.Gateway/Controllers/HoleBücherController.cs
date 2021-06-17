using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace WIFI.Gateway.Controllers
{
    public class HoleBücherController : Controllers.BasisApiController
    {
        /// <summary>
        /// Internes Feld für die Eigenschaft
        /// </summary>
        private WIFI.Gateway.Controller.BuchSqlClientController _ClientSqlController;

        /// <summary>
        /// Ruft den Clientcontroller für die Bücher ab 
        /// </summary>
        public WIFI.Gateway.Controller.BuchSqlClientController ClientSqlController
        {
            get
            {
                if (this._ClientSqlController == null)
                {
                    this._ClientSqlController = this.AppKontext.Produziere<WIFI.Gateway.Controller.BuchSqlClientController>();
                }
                return this._ClientSqlController;
            }
        }

        /// <summary>
        /// Gibt die Bücher aus der Datenbank zurück
        /// </summary>
        /// <returns></returns>
        public object Get()
        {
            ClientSqlController.HoleBücher();
            return null;
        }
    }
}