﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace WIFI.Gateway.Controllers
{
    /// <summary>
    /// Stellt einen REST-API-Controller zum Aktualisieren der Bestellung
    /// </summary>
    public class AktualisiereBestellungsInfoController : Controllers.BasisApiController
    {
        /// <summary>
        /// Internes Feld für die Eigenschaft
        /// </summary>
        private WIFI.Gateway.Controller.BestellungSqlClientController _ClientSqlController;

        /// <summary>
        /// Ruft den Clientcontroller für die Bestellung ab 
        /// </summary>
        public WIFI.Gateway.Controller.BestellungSqlClientController ClientSqlController
        {
            get
            {
                if (this._ClientSqlController == null)
                {
                    this._ClientSqlController = this.AppKontext.Produziere<WIFI.Gateway.Controller.BestellungSqlClientController>();
                }
                return this._ClientSqlController;
            }
        }

        /// <summary>
        /// Aktualisiert den Eintrag einer 
        /// Bestellung in der Datenbank
        /// </summary>
        /// <param name="id">Interne ID 
        /// der Bestellung</param>
        /// <returns></returns>
        public object Get(int BestellNr, int BesucherId,string BesucherVorname, string BesucherNachname, int BesucherHausnummer, string Ort, int PLZ, string Straße,string Telefon)
        {
            DTO.Bestellung bestellung = new DTO.Bestellung() { 
                BestellNr= BestellNr,

            
            };
            DTO.Besucher besucher = new DTO.Besucher { 
                Id = BesucherId,
                Vorname = BesucherVorname,
                Nachname = BesucherNachname,
                Hausnummer = BesucherHausnummer,
                Ort = Ort,
                Postleitzahl = PLZ,
                Straßenname = Straße,
                Telefon = Telefon
            
            };
            
            

            ClientSqlController.AktualisiereBestellung(BestellNr,BesucherId, besucher);

            return null;
        }
    }
}