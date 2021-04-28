﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WIFI.Ausstellung.Models
{
    /// <summary>
    /// Stellt einen Dienst zum Lesen und
    /// Schreiben von WIFI.CS.Teil2 Anwendungspunkten
    /// im Xml Format bereit.
    /// </summary>
    internal class AufgabenXmlController : WIFI.Ausstellung.Models.Generisch.XmlController<Aufgaben>
    {


        /// <summary>
        /// Gibt die Standardaufgaben aus
        /// den Anwendungsressourcen zurück.
        /// </summary>
        public Aufgaben HoleAusRessourcen()
        {


            var Xml = new System.Xml.XmlDocument();

            if (string.IsNullOrEmpty(this.AppKontext.AktuelleAufgabenSektion))
            {
                return null;
            }

            Xml.LoadXml(this.AppKontext.AktuelleAufgabenSektion);
            //Xml.LoadXml(WIFI.CS.Teil2.Properties.Resources.Aufgaben);

            var Aufgaben = new Aufgaben();

            foreach (System.Xml.XmlNode a in Xml.DocumentElement.ChildNodes)
            {
                Aufgaben.Add(
                    new Aufgabe
                    {
                        Name = a.Attributes["name"].Value,
                        Symbol = a.Attributes["symbol"].Value,
                        ViewerName = a.Attributes["view"].Value
                    }
                    );
            }


            return Aufgaben;
        }
    }
}