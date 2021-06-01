using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WIFI.Ausstellung.ViewModels
{
    /// <summary>
    /// Stellt einen Dienst zum Verwalten
    /// aller gelieferten Bücher bereit
    /// </summary>
    public class BuchManager : WIFI.Anwendung.ViewModelAppObjekt
    {
        #region BücherView
        /// <summary>
        /// Internes Feld für die Eigenschaft
        /// </summary>
        private static WIFI.Anwendung.DTO.Bücher _Buchausstellungsliste = null;

        /// <summary>
        /// Ruft eine Auflistung aller Bücher, welche bei der Veranstaltung erhältlich sind, ab oder legt diese fest
        /// </summary>
        public WIFI.Anwendung.DTO.Bücher Buchausstellungsliste
        {
            get
            {
                if (BuchManager._Buchausstellungsliste == null)
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

                return BuchManager._Buchausstellungsliste;
            }
            set
            {
                BuchManager._Buchausstellungsliste = value;
                this.OnPropertyChanged();
            }
        }

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
        #endregion

        /// <summary>
        /// Internes Feld für die Eigenschaft
        /// </summary>
        private static WIFI.Anwendung.DTO.Bücher _AktuelleBücher = null;

        /// <summary>
        /// Ruft die aktuell bestellten Bücher ab oder legt diese fest
        /// </summary>
        public static WIFI.Anwendung.DTO.Bücher AktuelleBücher
        {
            get
            {
                if (BuchManager._AktuelleBücher == null)
                {
                    BuchManager._AktuelleBücher = new WIFI.Anwendung.DTO.Bücher();
                }
                return BuchManager._AktuelleBücher;
            }
            set
            {
                if (BuchManager._AktuelleBücher != value)
                {
                    BuchManager._AktuelleBücher = value;
                }
                // Da statische Eigenschaften nicht OnPropertyChanged aufrufen können, wird das gesammte Layout geupdatet
            }
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
                                //b = (from l in Buchausstellungsliste
                                //     where string.Compare(l.Titel.ToString(), id, ignoreCase: true) == 0
                                //     select l).FirstOrDefault();

                            }
                            // Nehme das Erste Element welches die selbe Id schon hat

                            // Wenn kein Element mit der ID in der Liste existiert, füge es hinzu
                            if (b == null)
                            {
                                ViewModels.AusstellungsManager.AktuelleBücherbestellung.Add(
                                new WIFI.Anwendung.DTO.Buch
                                {

                                    //AutorName = Autor,
                                    //ID = Convert.ToInt32(Id),
                                    //Kategoriegruppe = Convert.ToInt32(Buchgruppe),
                                    //Preis = Convert.ToDouble(Preis),
                                    //Rabattgruppe = Convert.ToInt32(Rabatt),
                                    //Titel = Titel,
                                    //VerlagName = Verlag
                                }
                            );
                            }


                            //ParrentViewModel.Ausstellung.AktuelleBücherbestellung.add(new Buch() { Preis =  });
                        }
                        );
                }

                return this._BuchHinzufügen;
            }

            set 
            { 
                this._BuchHinzufügen = value;
                this.OnPropertyChanged();
            }
        }



        private void InitialisiereBuecherListe()
        {
            this.Buchausstellungsliste =
                this.AppKontext.DBControllerManager.BücherController.HoleBücher();
        }


        /// <summary>
        /// Internes Feld für die Eigenschaft
        /// </summary>
        private WIFI.Anwendung.DTO.Buch _AktuellesBuch = null;

        /// <summary>
        /// Ruft das aktuelle Buch ab
        /// oder legt dieses fest
        /// </summary>
        public WIFI.Anwendung.DTO.Buch AktuellesBuch
        {
            get
            {
                if (this._AktuellesBuch == null)
                {
                    this._AktuellesBuch = new WIFI.Anwendung.DTO.Buch();
                }

                return this._AktuellesBuch;
            }
            set
            {
                this._AktuellesBuch = value;
                this.OnPropertyChanged();
            }
        }
    }
}
