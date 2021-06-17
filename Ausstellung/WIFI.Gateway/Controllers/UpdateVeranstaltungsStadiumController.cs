using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace WIFI.Gateway.Controllers
{
    public class UpdateVeranstaltungsStadiumController : Controllers.BasisApiController
    {


        /// <summary>
        /// Internes Feld für die Eigenschaft
        /// </summary>
        private WIFI.Gateway.Controller.VeranstaltungSqlClientController _ClientSqlController;

        /// <summary>
        /// Ruft den Clientcontroller für die Veranstaltung ab 
        /// </summary>
        public WIFI.Gateway.Controller.VeranstaltungSqlClientController ClientSqlController
        {
            get
            {
                if (this._ClientSqlController == null)
                {
                    this._ClientSqlController = this.AppKontext.Produziere<WIFI.Gateway.Controller.VeranstaltungSqlClientController>();
                }
                return this._ClientSqlController;
            }
        }

        // GET api/<controller>
        public object Get(DTO.AusstellungsstadiumTyp Typ)
        {
            ClientSqlController.UpdateVeranstaltungsStadium(Typ);
            return null;
        }

      
    }
}