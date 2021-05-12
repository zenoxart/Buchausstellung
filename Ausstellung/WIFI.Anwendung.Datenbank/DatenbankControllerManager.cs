using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WIFI.Anwendung
{
    /// <summary>
    /// Stellt einen Verwaltungsdienst an Datenbank-Controllern zur Verfügung
    /// </summary>
    public class DatenbankControllerManager : ViewModelAppObjekt
    {
        /// <summary>
        /// Internes Feld für die Eigenschaft
        /// </summary>
        private DatenController.VeranstaltungsSqlClientController _VeranstaltungsController = null;


        /// <summary>
        /// Ruft einen Controller zur Verwaltung der Veranstaltung ab 
        /// </summary>
        public DatenController.VeranstaltungsSqlClientController VeranstaltungsController
        {
            get
            {
                if (this._VeranstaltungsController == null)
                {
                    this._VeranstaltungsController = this.AppKontext.Produziere<DatenController.VeranstaltungsSqlClientController>();
                }
                return this._VeranstaltungsController;
            }
        }

        /// <summary>
        /// Internes Feld für die Eigenschaft
        /// </summary>
        private DatenController.BücherSqlClientController _BücherController = null;

        /// <summary>
        /// Ruft einen Controller zur Verwaltung der Bücher der Ausstellung ab
        /// </summary>
        public DatenController.BücherSqlClientController BücherController
        {
            get
            {
                if (this._BücherController == null)
                {
                    this._BücherController = this.AppKontext.Produziere<DatenController.BücherSqlClientController>();
                }
                return this._BücherController;
            }
        }

        /// <summary>
        /// Internes Feld für die Eigenschaft
        /// </summary>
        private DatenController.BesucherSqlClientController _BesucherController = null;

        /// <summary>
        /// Ruft einen Controller zur Verwaltung der Besucher ab 
        /// </summary>
        public DatenController.BesucherSqlClientController BesucherController
        {
            get
            {
                if (this._BesucherController == null)
                {
                    this._BesucherController = this.AppKontext.Produziere<DatenController.BesucherSqlClientController>();
                }
                return this._BesucherController;
            }
        }

        /// <summary>
        /// Internes Feld für die Eigenschaft
        /// </summary>
        private DatenController.BestellungSqlClientController _BestellungController = null;

        /// <summary>
        /// Ruft einen Controller zur Verwaltung der Bestellung ab
        /// </summary>
        public DatenController.BestellungSqlClientController BestellungController
        {
            get {
                if (this._BestellungController == null) 
                {
                    this._BestellungController = this.AppKontext.Produziere<DatenController.BestellungSqlClientController>();
                }
                return this._BestellungController; }
        }



    }
}
