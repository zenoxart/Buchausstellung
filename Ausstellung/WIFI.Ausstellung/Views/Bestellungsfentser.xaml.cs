using System;
using System.Collections.Generic;
using System.Windows;

namespace WIFI.Ausstellung.Views
{
    /// <summary>
    /// Interaktionslogik für Bestellungsfentser.xaml
    /// </summary>
    public partial class Bestellungsfentser : Window, System.ComponentModel.INotifyPropertyChanged
    {
        /// <summary>
        /// Wird ausgelöst, wenn sich der Inhalt
        /// einer Eigenschaft ändert
        /// </summary>
        /// <remarks>Wird von WPF benötigt, damit
        /// die Oberfläche automatisch aktualisiert wird.
        /// WPF prüft das Interface INotifyPropertyChanged</remarks>
        public event System.ComponentModel.PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// Löst das Ereignis PropertyChanged aus
        /// </summary>
        /// <param name="eigenschaft">Der Name der Eigenschaft,
        /// deren Wert geändert wurde</param>
        /// <remarks>Sollte der Name der Eigenschaft nicht angegeben
        /// werden, wird der Name vom Aufrufer benutzt</remarks>
        protected virtual void OnPropertyChanged(
            [System.Runtime.CompilerServices.CallerMemberName] string eigenschaft = "")
        {
            // Wegen des Multithreadings mit einer 
            // Kopie vom Ereignisbehandler arbeiten
            this.PropertyChanged?.Invoke(
                    this,
                    new System.ComponentModel.PropertyChangedEventArgs(eigenschaft));
        }


        /// <summary>
        /// Haupteinstiegspunkt der Anwendung
        /// </summary>
        public Bestellungsfentser(string best)
        {
            this.BestellNr = best;
            InitializeComponent();
        }

        /// <summary>
        /// Internes Feld für die Eigenschaft
        /// </summary>
        private WIFI.Gateway.DTO.Bestellung _AktuelleBestellung;

        /// <summary>
        /// Legt die Aktuelle Bestellung fest oder gibt diese
        /// </summary>
        public WIFI.Gateway.DTO.Bestellung AktuelleBestellung
        {
            get
            {
                if (this._AktuelleBestellung == null)
                {
                    async void Load()
                    {
                        if (!string.IsNullOrEmpty(this.BestellNr))
                        {
                            this.AktuelleBestellung = 
                                await WIFI.Ausstellung.DBControllerManager.
                                BestellungController.HoleBestellung(Convert.ToInt32(this.BestellNr));



                         

                        }
                    }
                    Load();
                }
                return this._AktuelleBestellung;
            }
            set
            {
                this._AktuelleBestellung = value;
                this.OnPropertyChanged();
            }
        }


        /// <summary>
        /// Internes Feld für die Eigenschaft
        /// </summary>
        private WIFI.Anwendung.DatenbankAppKontext _AppKontext;

        public WIFI.Anwendung.DatenbankAppKontext AppKontext
        {
            get { return this._AppKontext; }
            set
            {
                this._AppKontext = value;
                this.OnPropertyChanged();
            }
        }

        /// <summary>
        /// Internes Feld für die Eigenschaft
        /// </summary>
        private WIFI.Gateway.DTO.Bücher _BücherDerSelektiertenBestellung;

        /// <summary>
        /// Ruft ein Auflistung an Büchern ab oder legt diese fest
        /// </summary>
        public WIFI.Gateway.DTO.Bücher BücherDerSelektiertenBestellung
        {
            get
            {
                if (this._BücherDerSelektiertenBestellung == null || this._BücherDerSelektiertenBestellung.Count == 0)
                {


                    async void Load()
                    {
                        this.BücherDerSelektiertenBestellung =
                            await WIFI.Ausstellung.DBControllerManager.BestellungController.HoleBücherZuBestellung(Convert.ToInt32(this.BestellNr));

                    }

                    Load();
                }
                return this._BücherDerSelektiertenBestellung;
            }
            set
            {
                this._BücherDerSelektiertenBestellung = value;
                this.OnPropertyChanged();
            }
        }


        /// <summary>
        /// Läd die Bücher der Selektierten Bestellung Asyncron
        /// </summary>
        protected async System.Threading.Tasks.Task HoleSelektierteBuchBestellungAsync()
        {
            await System.Threading.Tasks.Task.Run(
                 () =>
                 {
                     BücherDerSelektiertenBestellung = new WIFI.Gateway.DTO.Bücher();
                     // Wenn Eine Bestellung ausgewählt ist, nimm dessen Bücher, ansonst eine leere liste

                     if (this.AktuelleBestellung != null && this.AktuelleBestellung.Buchliste != null)
                     {
                         var bücher = new WIFI.Gateway.DTO.Bücher();

                         foreach (var item in this.AktuelleBestellung.Buchliste)
                         {
                             item.Key.Anzahl = item.Value;
                             bücher.Add(item.Key);
                         }

                         this.BücherDerSelektiertenBestellung = bücher;

                     }
                 });

        }

        /// <summary>
        /// Übergibt die BücherDerSelektiertenBestellung der selektierten Bestellung asyncron
        /// </summary>
        protected async System.Threading.Tasks.Task PusheSelektierteBuchBestellungAsync()
        {
            await System.Threading.Tasks.Task.Run(
                () =>
                {

                    if (BücherDerSelektiertenBestellung != null && BücherDerSelektiertenBestellung.Count > 0)
                    {
                        Dictionary<WIFI.Gateway.DTO.Buch, int> TempListe = new Dictionary<WIFI.Gateway.DTO.Buch, int>();

                        foreach (var item in BücherDerSelektiertenBestellung)
                        {
                            TempListe.Add(item, item.Anzahl);
                        }

                        this.AktuelleBestellung.Buchliste = TempListe;
                    }


                });
        }

        /// <summary>
        /// Internes Feld für die Eigenschaft
        /// </summary>
        private string _BestellNr;

        /// <summary>
        /// Ruft die Bestellungsnummer ab oder legt diese fest
        /// </summary>
        public string BestellNr
        {
            get
            {
                if (this._BestellNr == null)
                {
                    this._BestellNr = "";
                }
                return this._BestellNr;
            }
            set
            {
                this._BestellNr = value;
                this.OnPropertyChanged();
            }
        }

        /// <summary>
        /// Internes Feld für die Eigenschaft
        /// </summary>
        private WIFI.Anwendung.Befehl _AktualisiereBestellung;

        /// <summary>
        /// Updatet die Bestellung in der Datenbank
        /// </summary>
        public WIFI.Anwendung.Befehl AktualisiereBestellung
        {
            get
            {
                if (this._AktualisiereBestellung == null)
                {
                    this._AktualisiereBestellung = new Anwendung.Befehl(
                        p =>
                        {
                            if (this.AktuelleBestellung != null && this.BücherDerSelektiertenBestellung != null)
                            {

                                
                                foreach (var item in this.BücherDerSelektiertenBestellung)
                                {
                                    async void Load()
                                    {
                                        await WIFI.Ausstellung.DBControllerManager.BestellungController.
                                        AktualisiereBestellungBuch(item.ID, item.Anzahl, this.AktuelleBestellung.BestellNr);
                                    }
                                    Load();
                                    
                                }

                                this.Close();
                            }
                        }
                    );
                }
                return this._AktualisiereBestellung;
            }
            set { this._AktualisiereBestellung = value; }
        }
    }
}
