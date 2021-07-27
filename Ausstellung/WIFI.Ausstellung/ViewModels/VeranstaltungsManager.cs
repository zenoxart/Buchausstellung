using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WIFI.Ausstellung.ViewModels
{
    public class VeranstaltungsManager : WIFI.Anwendung.ViewModelAppObjekt
    {
        /// <summary>
        /// Ruft einen Wahrheitswert ab oder legt diesen fest,
        /// ob die Anwendung das dunkle Design benutzen soll
        /// </summary>
        public bool DunklesDesign
        {
            get
            {
                return Properties.Settings.Default.DunklesDesign;
            }
            set
            {
                if (value != Properties.Settings.Default.DunklesDesign)
                {
                    Properties.Settings.Default.DunklesDesign = value;
                    // Es wird davon ausgegangen, dass die
                    // Einstellung im Main() gespeichert wird
                }

                // Weil die Anwendung mehrere Fenster haben kann,
                // egal ob der Wert geändert wurde oder nicht...
                this.OnPropertyChanged();
            }
        }

        /// <summary>
        /// Internes Feld für die Eigenschaft
        /// </summary>
        private string _Ort = null;

        /// <summary>
        /// Ruft den String zu der Ortsbezeichnung ab oder legt diesen fest
        /// </summary>
        public string Ort
        {
            get
            {
                if (this._Ort == null)
                {
                    this._Ort = string.Empty;
                }
                return this._Ort;
            }
            set
            {

                if (this._Ort != value)
                {
                    this._Ort = value;
                    this.OnPropertyChanged();
                }
            }
        }

        /// <summary>
        /// Internes Feld für die Eigenschaft
        /// </summary>
        private WIFI.Anwendung.Befehl _ErstelleVeranstaltung = null;

        /// <summary>
        /// Ruft den Befehl zum Speichern
        /// einer neuen Veranstaltung ab
        /// </summary>
        public WIFI.Anwendung.Befehl ErstelleVeranstaltung
        {
            get
            {
                if (this._ErstelleVeranstaltung == null)
                {
                    this._ErstelleVeranstaltung = new WIFI.Anwendung.Befehl(
                        p =>
                        {
                            if (this.VeranstaltungsEndDatum != DateTime.Today && this.Ort != string.Empty)
                            {
                                // Die Veranstaltung kann gestartet werden
                                async void Load()
                                {
                                    await WIFI.Ausstellung.DBControllerManager.VeranstaltungsController.StarteVeranstaltung(
                                    this.VeranstaltungsBeginnDatum,
                                    this.VeranstaltungsEndDatum,
                                    this.Ort);
                                }
                                Load();
                               



                                // 20210617 -> Übersiedlung von MySql auf MsSql
                                //this.AppKontext.DBControllerManager.VeranstaltungsController.StarteVeranstaltung(
                                //    this.VeranstaltungsBeginnDatum,
                                //    this.VeranstaltungsEndDatum,
                                //    this.Ort
                                //    );

                                this.AppKontext.Protokoll.Eintragen(
                                    $"Die erstellte Veranstaltung wurde gestartet"
                                    ,
                                     WIFI.Anwendung.Daten.ProtokollEintragTyp.Normal
                                    );

                            }
                        }
                    );
                }

                return this._ErstelleVeranstaltung;
            }

            set
            {
                this._ErstelleVeranstaltung = value;
                this.OnPropertyChanged();
            }
        }

        /// <summary>
        /// Internes Feld für die Eigenschaft
        /// </summary>
        private DateTime _VeranstaltungsBeginnDatum = System.DateTime.MinValue;

        /// <summary>
        /// Ausgewähltes Beginndatum des Events
        /// </summary>
        public DateTime VeranstaltungsBeginnDatum
        {
            get
            {

                if (this._VeranstaltungsBeginnDatum == System.DateTime.MinValue)
                {
                    this._VeranstaltungsBeginnDatum = DateTime.Today;
                }

                return this._VeranstaltungsBeginnDatum;
            }
            set
            {
                if (this._VeranstaltungsBeginnDatum != value)
                {

                    this._VeranstaltungsBeginnDatum = value;
                    this.OnPropertyChanged();
                }
            }
        }

        /// <summary>
        /// Internes Feld für die Eigenschaft
        /// </summary>
        private DateTime _VeranstaltungsEndDatum = System.DateTime.MinValue;

        /// <summary>
        /// Ausgewähltes Enddatum des Events
        /// </summary>
        public DateTime VeranstaltungsEndDatum
        {
            get
            {

                if (this._VeranstaltungsEndDatum == System.DateTime.MinValue)
                {
                    this._VeranstaltungsEndDatum = DateTime.Today;
                }
                return this._VeranstaltungsEndDatum;
            }
            set
            {
                if (this._VeranstaltungsEndDatum != value)
                {

                    this._VeranstaltungsEndDatum = value;
                    this.OnPropertyChanged();
                }
            }
        }
    }
}
