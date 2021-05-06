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
        private DatenController.VeranstaltungsSqlClientController _VeranstaltungsController;


        /// <summary>
        /// Ruft deinen Controller zur Verwaltung der Veranstaltung ab 
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

    }
}
