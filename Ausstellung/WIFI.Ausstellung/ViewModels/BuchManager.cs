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
        private static WIFI.Gateway.DTO.Bücher _Buchausstellungsliste = null;

        /// <summary>
        /// Ruft eine Auflistung aller Bücher, welche bei der Veranstaltung erhältlich sind, ab oder legt diese fest
        /// </summary>
        public WIFI.Gateway.DTO.Bücher Buchausstellungsliste
        {
            get
            {
                if (BuchManager._Buchausstellungsliste == null)
                {
                    Buchausstellungsliste = new WIFI.Gateway.DTO.Bücher
                    {
                        new WIFI.Gateway.DTO.Buch
                        {
                            Buchnummer = 0,
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

                    async void Load()
                    {
                        this.Buchausstellungsliste = await WIFI.Ausstellung.DBControllerManager.BuchController.HoleBücher();

                    }

                    Load();

                    this.EndeProtokollieren();
                    this.BuecherListeGeladen = false;
                }
                );
        }
        #endregion

        #region Bücher
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

                                this.Buchausstellungsliste = new WIFI.Gateway.DTO.Bücher();
                            }


                            if (this.AktuellesBuch != null)
                            {

                                this.Buchausstellungsliste.Add(this.AktuellesBuch);

                                WIFI.Ausstellung.DBControllerManager.BuchController.ErstelleBuch(this.AktuellesBuch);

                                this.AktuellesBuch = null;
                            }

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
        /// Internes Feld für die Eigenschaft
        /// </summary>
        private int _IEStatus = 0;

        /// <summary>
        /// Gibt den Status der Import oder Export auf:
        /// 0.  Neutral
        /// 1.  Erfolg
        /// 2.  Fehler
        /// </summary>
        public int IEStatus
        {
            get { return this._IEStatus; }
            set
            {
                this._IEStatus = value;
                this.OnPropertyChanged();
            }
        }



        /// <summary>
        /// Läd alle Bücher von der Datenbank in die Buchausstellungsliste
        /// </summary>
        private void InitialisiereBuecherListe()
        {
            async void Load()
            {
                this.Buchausstellungsliste = await WIFI.Ausstellung.DBControllerManager.BuchController.HoleBücher();

            }

            Load();
        }


        /// <summary>
        /// Internes Feld für die Eigenschaft
        /// </summary>
        private WIFI.Gateway.DTO.Buch _AktuellesBuch = null;

        /// <summary>
        /// Ruft das aktuelle Buch ab
        /// oder legt dieses fest
        /// </summary>
        public WIFI.Gateway.DTO.Buch AktuellesBuch
        {
            get
            {
                if (this._AktuellesBuch == null)
                {
                    this._AktuellesBuch = new WIFI.Gateway.DTO.Buch();
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
        private Gateway.DTO.Buchgruppe _AktuellesBuchKategorie;

        /// <summary>
        /// Gibt die Aktuelle Kategorie des Anzulegenden Buches zurück oder legt diese fest
        /// </summary>
        public Gateway.DTO.Buchgruppe AktuellesBuchKategorie
        {
            get
            {
                if (this._AktuellesBuchKategorie == null)
                {
                    this._AktuellesBuchKategorie = new Gateway.DTO.Buchgruppe();
                }
                return this._AktuellesBuchKategorie;
            }
            set
            {
                this._AktuellesBuchKategorie = value;
                this.AktuellesBuch.Kategoriegruppe = value.ID;
                this.OnPropertyChanged();
            }
        }

        #endregion

        #region Import-Export-Funktionen

        /// <summary>
        /// Internes Feld für die Eigenschaft
        /// </summary>
        private WIFI.Anwendung.Befehl _import = null;

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

                                WIFI.Gateway.DTO.Bücher buches = new WIFI.Gateway.DTO.Bücher();


                                for (int i = 1; i < newText.Length - 1; i++)
                                {
                                    string[] line = newText[i].Split(';');
                                    buches.Add(
                                        new WIFI.Gateway.DTO.Buch()
                                        {
                                            ID = Convert.ToInt32(line[0]),
                                            Buchnummer = Convert.ToInt32(line[1]),
                                            Titel = line[2],
                                            AutorName = line[3],
                                            VerlagName = line[4],
                                            Rabattgruppe = Convert.ToInt32(line[5]),
                                            Kategoriegruppe = Convert.ToInt32(line[6]),
                                            Preis = Convert.ToDecimal(line[7])
                                        }
                                    );
                                }

                                foreach (var item in buches)
                                {
                                    this.Buchausstellungsliste.Add(item);
                                }

                                this.OnPropertyChanged();
                                // update to Database
                                foreach (var item in buches)
                                {
                                    WIFI.Ausstellung.DBControllerManager.BuchController.ErstelleBuch(item);
                                }

                                this.IEStatus = 1;
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

                                this.IEStatus = 2;
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
        private WIFI.Anwendung.Befehl _export = null;

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


                                    this.IEStatus = 1;
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

                                    this.IEStatus = 2;
                                }
                            }

                        }
                    );
                }

                return this._export;
            }
            set { this._export = value; }
        }
        #endregion

        #region Buchgruppen
        /// <summary>
        /// Internes Feld für die Eigenschaft
        /// </summary>
        private WIFI.Gateway.DTO.Buchgruppen _Büchergruppen = null;

        /// <summary>
        /// Ruft eine Auflistung aller Buchgruppen ab oder legt diese fest
        /// </summary>
        public WIFI.Gateway.DTO.Buchgruppen Büchergruppen
        {
            get
            {
                if (this._Büchergruppen == null)
                {
                    this._Büchergruppen = new Gateway.DTO.Buchgruppen();
                    // Lade aus Datenbank
                    //this.AppKontext.DBControllerManager
                    async void Load()
                    {

                        this.Büchergruppen = await WIFI.Ausstellung.DBControllerManager.BuchgruppenController.HoleBuchgruppen();
                    }
                    Load();
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
        private WIFI.Gateway.DTO.Buchgruppe _SelektierteBuchgruppe = null;

        /// <summary>
        /// Ruft die Selektierte Buchgruppe ab oder legt diese fest
        /// </summary>
        public WIFI.Gateway.DTO.Buchgruppe SelektierteBuchgruppe
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
        private WIFI.Anwendung.Befehl _BuchgruppeHinzufügen = null;

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
                            if (this.NeuErstellteBuchgruppe != null)
                            {
                                if (this.NeuErstellteBuchgruppe.Gruppennummer != 0 && !string.IsNullOrEmpty(this.NeuErstellteBuchgruppe.Beschreibung))
                                {
                                    // Wenn das Neuzuerstellende Objekt noch nicht existiert
                                    if (!this.Büchergruppen.Contains(this.NeuErstellteBuchgruppe))
                                    {
                                        // In die Datenbank speichern
                                        WIFI.Ausstellung.DBControllerManager.BuchgruppenController.ErstelleBuchgruppe(this.NeuErstellteBuchgruppe);

                                        // Durch das Casten wird eine Neue Instanz erstellt, 
                                        // sodass eine Kopie des Objekts ensteht und kein Verweiß auf "NeuErstellteBuchgruppe"
                                        // in der Liste steht
                                        this.Büchergruppen.Add(this.NeuErstellteBuchgruppe as Gateway.DTO.Buchgruppe);

                                        this.NeuErstellteBuchgruppe = null;
                                    }

                                }
                            }
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
        private WIFI.Gateway.DTO.Buchgruppe _NeuErstellteBuchgruppe;

        /// <summary>
        /// Ruft die Daten zu der neu zu erstellenden Buchgruppe ab oder legt diese fest
        /// </summary>
        public WIFI.Gateway.DTO.Buchgruppe NeuErstellteBuchgruppe
        {
            get
            {
                if (this._NeuErstellteBuchgruppe == null)
                {
                    this._NeuErstellteBuchgruppe = new Gateway.DTO.Buchgruppe();
                }
                return this._NeuErstellteBuchgruppe;
            }
            set
            {
                this._NeuErstellteBuchgruppe = value;
                this.OnPropertyChanged();
            }
        }


        /// <summary>
        /// Internes Feld für die Eigenschaft
        /// </summary>
        private WIFI.Anwendung.Befehl _BuchgruppeEntfernen = null;

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
                                WIFI.Ausstellung.DBControllerManager.BuchgruppenController.EntferneBuchgruppe(this.SelektierteBuchgruppe);
                                this.Büchergruppen.Remove(this.SelektierteBuchgruppe);

                                // Remove von DB
                            }


                        }
                        );
                }
                return this._BuchgruppeEntfernen;
            }
            set { this._BuchgruppeEntfernen = value; }
        }

        #endregion


    }
}
