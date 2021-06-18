using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace WIFI.Gateway.Controllers
{

    /// <summary>
    /// Stellt einen REST-API-Controller zum erstellen eines Besuchers
    /// </summary>
    public class ErstelleBesucherController : Controllers.BasisApiController
    {

        /// <summary>
        /// Internes Feld für die Eigenschaft
        /// </summary>
        private WIFI.Gateway.Controller.BesucherSqlClientController _ClientSqlController;

        /// <summary>
        /// Ruft den Clientcontroller für die Veranstaltung ab 
        /// </summary>
        public WIFI.Gateway.Controller.BesucherSqlClientController ClientSqlController
        {
            get
            {
                if (this._ClientSqlController == null)
                {
                    this._ClientSqlController = this.AppKontext.Produziere<WIFI.Gateway.Controller.BesucherSqlClientController>();
                }
                return this._ClientSqlController;
            }
        }
        // GET api/<controller>
        public DTO.Besucher Get(DTO.Besucher besucher)
        {
            return ClientSqlController.ErstelleBesucher(besucher);
           
        }

       
    }
}