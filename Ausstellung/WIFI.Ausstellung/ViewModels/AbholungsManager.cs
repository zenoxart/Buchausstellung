using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WIFI.Ausstellung.ViewModels
{
    /// <summary>
    /// Stellt einen Dienst zum Verwalten der Daten zu der Bücherabholung der Ausstellung
    /// </summary>
    public class AbholungsManager : WIFI.Anwendung.ViewModelAppObjekt
    {
        /// <summary>
        /// Internes Feld für die Eigenschaft
        /// </summary>
        private WIFI.Anwendung.DTO.Bestellungen _Abholungsliste = null;

        /// <summary>
        /// Ruft eine Auflistung der Bestellungen für die 
        /// Abholung ab, oder legt diese fest
        /// </summary>
        public WIFI.Anwendung.DTO.Bestellungen Abholungsliste
        {
            get
            {
                if (this._Abholungsliste == null)
                {
                    HoleBestellungenAsync();
                }

                return this._Abholungsliste;
            }
            set
            {
                this._Abholungsliste = value;
                this.OnPropertyChanged();
            }
        }

        /// <summary>
        /// Holt die Daten zu einer Bestellung Asynchon
        /// </summary>
        private async void HoleBestellungenAsync()
        {
            await System.Threading.Tasks.Task.Run(
                () =>
                {

                    this.Abholungsliste = this.AppKontext.DBControllerManager.BestellungController.HoleBestellungen();
                });

        }

        /// <summary>
        /// Internes Feld für die Eigenschaft
        /// </summary>
        private WIFI.Anwendung.Befehl _Abholungsbestätigung = null;

        /// <summary>
        /// Befehl um alle Abgeholten Bücher in die Datenbank zu schreiben
        /// </summary>
        public WIFI.Anwendung.Befehl Abholungsbestätigung
        {
            get
            {
                if (this._Abholungsbestätigung == null)
                {
                    this._Abholungsbestätigung = new WIFI.Anwendung.Befehl(
                        p =>
                        {
                            // Aktuallisiere alle Bücher welche Abgeholt wurden
                            if (this.Abholungsliste != null)
                            {
                                this.AppKontext.DBControllerManager.BestellungController.FürAlleBestellungenAbgeholt(this.Abholungsliste);

                                // TODO: Zeige die Oberfläche an, dass der Programmdurchlauf abgeschlossen ist
                            }
                        }
                    );
                }

                return this._Abholungsbestätigung;
            }
            set { this._Abholungsbestätigung = value; }
        }


    }
}
