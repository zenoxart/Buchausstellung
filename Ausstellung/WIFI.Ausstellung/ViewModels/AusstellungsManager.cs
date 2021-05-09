using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WIFI.Ausstellung.ViewModels
{
    /// <summary>
    /// Stellt einen Dienst zum Verwalten
    /// der Buch-Ausstellung bereit
    /// </summary>
    public class AusstellungsManager : WIFI.Anwendung.ViewModelAppObjekt
    {
        /// <summary>
        /// Internes Feld für die Eigenschaft
        /// </summary>
        private static WIFI.Anwendung.DTO.Bücher _Liste = null  ;

        /// <summary>
        /// Ruft eine Auflistung aller Bücher, welche bei der Veranstaltung erhältlich sind ab oder legt diese fest
        /// </summary>
        public WIFI.Anwendung.DTO.Bücher Buchausstellungsliste
        {
            get {
                if (AusstellungsManager._Liste == null)
                {

                    Buchausstellungsliste = new WIFI.Anwendung.DTO.Bücher
                    {
                        new WIFI.Anwendung.DTO.Buch{
                            Titel="Buchtitel werden geladen...",
                            AutorName="Bitte warten.",
                            ID = 0,
                            Kategoriegruppe = 0,
                            Rabattgruppe= 0,
                            Preis = 0,
                            VerlagName = ""
                        }
                    };

                    InitialisiereAusstellungAsync();



                    // TODO: Liste muss Asyncon Initialisiert werden
                    //this._Liste 


                }

                return AusstellungsManager._Liste;

            }
            set {
                AusstellungsManager._Liste = value;
                this.OnPropertyChanged();
            }
        }


        /// <summary>
        /// Ruft einen Wahrheitswert ab,
        /// ob das Initialisieren der Ausstelltung 
        /// aktuell läuft oder nicht
        /// </summary>
        private bool InitialisiereAusstellungLäuft { get; set; }

        /// <summary>
        /// Füllt die Liste mit den Aufgaben
        /// asynchron in einem eigenen Thread
        /// </summary>
        protected virtual async void InitialisiereAusstellungAsync()
        {
            // Damit die Thread-Methode nicht mehrmals gestartet wird
            if (this.InitialisiereAusstellungLäuft)
            {
                return;
            }

            await System.Threading.Tasks.Task.Run(
                () =>
                {
                    this.InitialisiereAusstellungLäuft = true;




                    // SO NIE, nicht mit dem Feld!!!
                    //this._Liste = this.Controller.HoleAusRessourcen();
                    // a) Damit WPF mitbekommt, dass sich die Liste
                    //    geändert hat, wird PropertyChanged benötigt
                    // b) Weil kein Thread in die Daten von einem
                    //    anderen Thread greifen darf, nicht hier
                    //    das PropertyChanged. Das muss in der Eigenschaft sein!

                    this.StartProtokollieren();

                    System.Threading.Thread.Sleep(7000);
                    //this.Buchausstellungsliste = this.Controller.HoleAusRessourcen();

                   this.Buchausstellungsliste = this.AppKontext.DBControllerManager.BücherController.HoleBücher();


                    //this.Buchausstellungsliste = new WIFI.Anwendung.DTO.Bücher();

                    //this.Buchausstellungsliste.Add(
                    //    new WIFI.Anwendung.DTO.Buch { ID = 1, Titel = "Harry Potter", AutorName = "J. K. Rowling", VerlagName = "Thalia", Kategoriegruppe = 0, Preis = 15.30, Rabattgruppe = 1 }

                    //);


                    this.EndeProtokollieren();
                    this.InitialisiereAusstellungLäuft = false;
                }
                );

        }

    }
}
