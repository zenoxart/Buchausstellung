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
        private WIFI.Gateway.DTO.Bestellungen _Abholungsliste = null;

        /// <summary>
        /// Ruft eine Auflistung der Bestellungen für die 
        /// Abholung ab, oder legt diese fest
        /// </summary>
        public WIFI.Gateway.DTO.Bestellungen Abholungsliste
        {
            get
            {
                if (this._Abholungsliste == null)
                {
                    // Erwartet die asyncrone Ladung von der Bestellungen
                    async void HoleBestellungenAsyncCalled()
                    {
                        await HoleBestellungenAsync();
                    }

                    // Triggert die asyncrone Ladung der Bestellungen
                    HoleBestellungenAsyncCalled();
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
        private async System.Threading.Tasks.Task HoleBestellungenAsync()
        {
            await System.Threading.Tasks.Task.Run(
                () =>
                {
                    // Läd die Bestellungen asyncron
                    async void LadeListeAsync()
                    {
                        this.Abholungsliste = await
                        WIFI.Ausstellung.DBControllerManager.BestellungController.HoleBestellungen();
                    }

                    // Triggert die asyncrone Ladung
                    LadeListeAsync();
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

                                foreach (var item in this.Abholungsliste)
                                {
                                    if (item.Abgeholt)
                                    {

                                        // Bestätigt die Abholung des
                                        async void BestätigeBestellungsAbholungAsync()
                                        {

                                            await WIFI.Ausstellung.DBControllerManager.BestellungController.BestellungAbgeholt(item);
                                        }

                                        // Triggert die asyncrone Ladung der Bestellung
                                        BestätigeBestellungsAbholungAsync();
                                    }
                                }



                            }
                        }
                    );
                }

                return this._Abholungsbestätigung;
            }
            set => this._Abholungsbestätigung = value;
        }


    }
}
