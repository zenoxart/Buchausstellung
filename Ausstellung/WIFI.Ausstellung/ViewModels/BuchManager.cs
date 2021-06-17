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
        private static WIFI.Gateway.DTO.Bücher _AktuelleBücher = null;

        /// <summary>
        /// Ruft die aktuell bestellten Bücher ab oder legt diese fest
        /// </summary>
        public static WIFI.Gateway.DTO.Bücher AktuelleBücher
        {
            get
            {
                if (BuchManager._AktuelleBücher == null)
                {
                    BuchManager._AktuelleBücher = new WIFI.Gateway.DTO.Bücher();
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

                            this.Buchausstellungsliste.Add(this.AktuellesBuch);

                            this.AppKontext.DBControllerManager.BücherController.BuchHinzufügen(

                                this.AktuellesBuch);

                            this.AktuellesBuch = null;

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


        /// <summary>
        /// Läd alle Bücher von der Datenbank in die Buchausstellungsliste
        /// </summary>
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
                if (this._AktuellesBuch != value)
                {
                    this._AktuellesBuch = value;
                    this.OnPropertyChanged();
                }
            }
        }

        /// <summary>
        /// Internes Feld für die Eigenschaft
        /// </summary>
        private WIFI.Anwendung.Befehl _import;

        /// <summary>
        /// Ruft die ImportierFunktion auf
        /// </summary>
        public WIFI.Anwendung.Befehl Import
        {
            get
            {
                if (this._import == null)
                {
                    this._import = new WIFI.Anwendung.Befehl(
                        p =>
                        {

                            try
                            {
                                // Lade aus einer CSV-Datei

                                // open FileChooser

                                Microsoft.Win32.OpenFileDialog openFileDialog = new Microsoft.Win32.OpenFileDialog();


                                // read from file
                                string text = "";
                                if (openFileDialog.ShowDialog() == true)
                                    text = System.IO.File.ReadAllText(openFileDialog.FileName);


                                string[] newText = text.Split('\n');

                                WIFI.Anwendung.DTO.Bücher buches = new WIFI.Anwendung.DTO.Bücher();


                                for (int i = 1; i < newText.Length - 1; i++)
                                {
                                    string[] line = newText[i].Split(';');
                                    buches.Add(
                                        new WIFI.Anwendung.DTO.Buch()
                                        {
                                            ID = Convert.ToInt32(line[0]),
                                            Buchnummer = line[1],
                                            Titel = line[2],
                                            AutorName = line[3],
                                            VerlagName = line[4],
                                            Rabattgruppe = Convert.ToInt32(line[5]),
                                            Kategoriegruppe = Convert.ToInt32(line[6]),
                                            Preis = Convert.ToDouble(line[7])
                                        }
                                    );
                                }
                                this.Buchausstellungsliste = buches;

                                this.OnPropertyChanged();
                                // update to Database
                                foreach (var item in buches)
                                {
                                    this.AppKontext.DBControllerManager.BücherController.AktualisiereBuch(item);
                                }
                            }
                            catch (Exception e)
                            {
                                this.AppKontext.Protokoll.Eintragen(
                                   new WIFI.Anwendung.Daten.ProtokollEintrag
                                   {
                                       Text = $"Im {this.GetType().FullName} in der Funktion {typeof(BuchManager).GetMethod("ErstelleEinzelBestellung")} ist ein Fehler aufgetreten \n" +
                                              $"{e.GetType().FullName} = {e.Message} \n " +
                                              $"{e.StackTrace}",
                                       Typ = WIFI.Anwendung.Daten.ProtokollEintragTyp.Normal
                                   });
                            }



                        }
                    );
                }
                return this._import;
            }
            set { this._import = value; }
        }

        /// <summary>
        /// Internes Feld für die Eigenschaft
        /// </summary>
        private WIFI.Anwendung.Befehl _export;

        /// <summary>
        /// Ruft die ExportierFunktion auf
        /// </summary>
        public WIFI.Anwendung.Befehl Export
        {
            get
            {

                if (this._export == null)
                {
                    this._export = new WIFI.Anwendung.Befehl(
                        p =>
                        {
                            if (this.Buchausstellungsliste != null)
                            {
                                try
                                {
                                    // open FileChooser
                                    Microsoft.Win32.SaveFileDialog sfd = new Microsoft.Win32.SaveFileDialog();
                                    string fileName = "";
                                    if (sfd.ShowDialog() == true) fileName = sfd.FileName;

                                    string content = "ID;BuchNr.;Titel;Autor;Verlag;Rabattgr.;Kategoriegr.;Preis";

                                    content += "\n";


                                    foreach (var item in this.Buchausstellungsliste)
                                    {
                                        content +=
                                            item.ID + ";" +
                                            item.Buchnummer + ";" +
                                            item.Titel + ";" +
                                            item.AutorName + ";" +
                                            item.VerlagName + ";" +
                                            item.Rabattgruppe + ";" +
                                            item.Kategoriegruppe + ";" +
                                            item.Preis + "\n";
                                    }


                                    // write from file
                                    System.IO.File.WriteAllText(sfd.FileName, content);
                                }
                                catch (Exception e)
                                {
                                    this.AppKontext.Protokoll.Eintragen(
                                       new WIFI.Anwendung.Daten.ProtokollEintrag
                                       {
                                           Text = $"Im {this.GetType().FullName} in der Funktion {typeof(BuchManager).GetMethod("ErstelleEinzelBestellung")} ist ein Fehler aufgetreten \n" +
                                                  $"{e.GetType().FullName} = {e.Message} \n " +
                                                  $"{e.StackTrace}",
                                           Typ = WIFI.Anwendung.Daten.ProtokollEintragTyp.Normal
                                       });
                                }
                            }

                        }
                    );
                }

                return this._export;
            }
            set { this._export = value; }
        }

        /// <summary>
        /// Internes Feld für die Eigenschaft
        /// </summary>
        private WIFI.Anwendung.DTO.Buchgruppen _Büchergruppen;

        /// <summary>
        /// Ruft eine Auflistung aller Buchgruppen ab oder legt diese fest
        /// </summary>
        public WIFI.Anwendung.DTO.Buchgruppen Büchergruppen
        {
            get
            {
                if (this._Büchergruppen == null)
                {
                    // TODO: Lade aus Datenbank
                    //this.AppKontext.DBControllerManager
                }
                return this._Büchergruppen;
            }
            set
            {
                this._Büchergruppen = value;
                this.OnPropertyChanged();
            }
        }

        /// <summary>
        /// Internes Feld für die Eigenschaft
        /// </summary>
        private WIFI.Anwendung.DTO.Buchgruppe _SelektierteBuchgruppe;

        /// <summary>
        /// Ruft die Selektierte Buchgruppe ab oder legt diese fest
        /// </summary>
        public WIFI.Anwendung.DTO.Buchgruppe SelektierteBuchgruppe
        {
            get { return this._SelektierteBuchgruppe; }
            set
            {
                this._SelektierteBuchgruppe = value;
                this.OnPropertyChanged();
            }
        }

        /// <summary>
        /// Internes Feld für die Eigenschaft
        /// </summary>
        private WIFI.Anwendung.Befehl _BuchgruppeHinzufügen;

        /// <summary>
        /// Funktion um eine Buchgruppe der Liste hinzuzufügen
        /// </summary>
        public WIFI.Anwendung.Befehl BuchgruppeHinzufügen
        {
            get
            {
                if (this._BuchgruppeHinzufügen == null)
                {
                    this._BuchgruppeHinzufügen = new WIFI.Anwendung.Befehl(
                        p =>
                        {
                            
                        }
                        );
                }

                return this._BuchgruppeHinzufügen;
            }
            set { this._BuchgruppeHinzufügen = value; }
        }

        /// <summary>
        /// Internes Feld für die Eigenschaft
        /// </summary>
        private WIFI.Anwendung.Befehl _BuchgruppeEntfernen;

        /// <summary>
        /// Funktion um eine Buchgruppe der Liste zu entfernen
        /// </summary>
        public WIFI.Anwendung.Befehl BuchgruppeEntfernen
        {
            get
            {
                if (this._BuchgruppeEntfernen == null)
                {
                    this._BuchgruppeEntfernen = new WIFI.Anwendung.Befehl(
                        p =>
                        {
                            if (this.Büchergruppen != null && this.SelektierteBuchgruppe != null)
                            {
                                this.Büchergruppen.Remove(this.SelektierteBuchgruppe);
                            }

                            //TODO: Remove von DB

                        }
                        );
                }
                return this._BuchgruppeEntfernen;
            }
            set { this._BuchgruppeEntfernen = value; }
        }




    }
}
