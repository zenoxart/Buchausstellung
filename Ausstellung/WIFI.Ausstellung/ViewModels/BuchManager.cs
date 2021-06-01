using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WIFI.Ausstellung.ViewModels
{
    public class BuchManager : WIFI.Anwendung.ViewModelAppObjekt
    {
        #region BücherView
        /// <summary>
        /// Internes Feld für die Eigenschaft
        /// </summary>
        private static WIFI.Anwendung.DTO.Bücher _Liste = null;

        public WIFI.Anwendung.DTO.Bücher Buchausstellungsliste
        {
            get
            {
                if (BuchManager._Liste == null)
                {
                    Buchausstellungsliste = new WIFI.Anwendung.DTO.Bücher
                    {
                        new WIFI.Anwendung.DTO.Buch
                        {
                            Buchnummer = "0",
                            Titel = "Buchtitel werden geladen...",
                            AutorName = "Bitte warten.",
                            ID = 0,
                            Kategoriegruppe = 0,
                            Rabattgruppe = 0,
                            Preis = 0,
                            VerlagName = string.Empty
                        }
                    };

                    //  Liste muss asynchron initialisiert werden
                    InitialisiereBuecherAsync();
                }

                return BuchManager._Liste;
            }
            set
            {
                BuchManager._Liste = value;
                this.OnPropertyChanged();
            }
        }
        #endregion

        /// <summary>
        /// Ruft einen Wahrheitswert ab,
        /// ob die Liste der Bücher
        /// bereits geladen wurde oder nicht
        /// </summary>
        private bool BuecherListeGeladen { get; set; }


        /// <summary>
        /// Füllt die Liste mit den Aufgaben
        /// asynchron in einem eigenen Thread
        /// </summary>
        protected virtual async void InitialisiereBuecherAsync()
        {
            // Damit die Thread-Methode nicht mehrmals gestartet wird
            if (this.BuecherListeGeladen)
            {
                return;
            }

            await System.Threading.Tasks.Task.Run(
                () =>
                {
                    this.BuecherListeGeladen = true;

                    // SO NIE, nicht mit dem Feld!!!
                    //this._Liste = this.Controller.HoleAusRessourcen();
                    // a) Damit WPF mitbekommt, dass sich die Liste
                    //    geändert hat, wird PropertyChanged benötigt
                    // b) Weil kein Thread in die Daten von einem
                    //    anderen Thread greifen darf, nicht hier
                    //    das PropertyChanged. Das muss in der Eigenschaft sein!

                    this.StartProtokollieren();

                    System.Threading.Thread.Sleep(7000);

                    this.Buchausstellungsliste = this.AppKontext.DBControllerManager.BücherController.HoleBücher();

                    this.EndeProtokollieren();
                    this.BuecherListeGeladen = false;
                }
                );
        }


        /// <summary>
        /// Internes Feld für die Eigenschaft
        /// </summary>
        private WIFI.Anwendung.Befehl _BuchHinzufügen = null;

        /// <summary>
        /// Ein neu erfasstes Buch der Liste hinzufügen
        /// </summary>
        public WIFI.Anwendung.Befehl BuchHinzufügen
        {
            get
            {
                if (this._BuchHinzufügen == null)
                {
                    // Den Befehl mit anoymen Methoden initialisieren

                    this._BuchHinzufügen = new WIFI.Anwendung.Befehl(

                        // TODO: Werte des Buches in der Bücherliste hinzufügen
                        p =>
                        {

                            if (this.Buchausstellungsliste == null)
                            {

                                this.Buchausstellungsliste = new WIFI.Anwendung.DTO.Bücher();
                            }

                            WIFI.Anwendung.DTO.Buch b = null;
                            if (Buchausstellungsliste.Count > 0)
                            {
                                b = (from l in Buchausstellungsliste
                                     where string.Compare(l.ID.ToString(), Id, ignoreCase: true) == 0
                                     select l).FirstOrDefault();

                            }
                            // Nehme das Erste Element welches die selbe Id schon hat

                            // Wenn kein Element mit der ID in der Liste existiert, füge es hinzu
                            if (b == null)
                            {
                                ViewModels.AusstellungsManager.AktuelleBücherbestellung.Add(
                                new WIFI.Anwendung.DTO.Buch
                                {

                                    AutorName = Autor,
                                    ID = Convert.ToInt32(Id),
                                    Kategoriegruppe = Convert.ToInt32(Buchgruppe),
                                    Preis = Convert.ToDouble(Preis),
                                    Rabattgruppe = Convert.ToInt32(Rabatt),
                                    Titel = Titel,
                                    VerlagName = Verlag
                                }
                            );
                            }


                            //ParrentViewModel.Ausstellung.AktuelleBücherbestellung.add(new Buch() { Preis =  });
                        }
                        );
                }

                return this._BuchHinzufügen;
            }

            set { this._BuchHinzufügen = value; }
        }

        private void InitialisiereBuecherListe()
        {
            this.Buchausstellungsliste =
                this.AppKontext.DBControllerManager.BücherController.HoleBücher();
        }

    }
}
