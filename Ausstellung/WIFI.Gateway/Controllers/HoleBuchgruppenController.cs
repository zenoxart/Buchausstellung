﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace WIFI.Gateway.Controllers
{
    public class HoleBuchgruppenController : Controllers.BasisApiController
    {
        /// <summary>
        /// Internes Feld für die Eigenschaft
        /// </summary>
        private WIFI.Gateway.Controller.BuchgruppeSqlClientController _ClientSqlController;

        /// <summary>
        /// Ruft den Clientcontroller für die Buchgruppen ab 
        /// </summary>
        public WIFI.Gateway.Controller.BuchgruppeSqlClientController ClientSqlController
        {
            get
            {
                if (this._ClientSqlController == null)
                {
                    this._ClientSqlController = this.AppKontext.Produziere<WIFI.Gateway.Controller.BuchgruppeSqlClientController>();
                }
                return this._ClientSqlController;
            }
        }

        /// <summary>
        /// Gibt die Buchgruppen aus der Datenbank zurück
        /// </summary>
        /// <returns></returns>
        public object Get()
        {
            ClientSqlController.HoleBuchgruppen();
            return null;
        }
    }
}