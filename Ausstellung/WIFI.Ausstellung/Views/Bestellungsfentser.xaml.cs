﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

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
            var BehandlerKopie = this.PropertyChanged;

            if (BehandlerKopie != null)
            {
                BehandlerKopie(
                    this,
                    new System.ComponentModel.PropertyChangedEventArgs(eigenschaft));
            }
        }


        /// <summary>
        /// Haupteinstiegspunkt der Anwendung
        /// </summary>
        public Bestellungsfentser()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Internes Feld für die Eigenschaft
        /// </summary>
        private WIFI.Anwendung.DTO.Bestellung _AktuelleBestellung;

        /// <summary>
        /// Legt die Aktuelle Bestellung fest oder gibt diese
        /// </summary>
        public WIFI.Anwendung.DTO.Bestellung AktuelleBestellung
        {
            get
            {
                if (this._AktuelleBestellung == null)
                {
                    if (!string.IsNullOrEmpty(this.BestellNr))
                    {
                        this._AktuelleBestellung = this.AppKontext.DBControllerManager.BestellungController.HoleBestellung(Convert.ToInt32(this.BestellNr));
                    }
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
        private WIFI.Anwendung.DTO.Bücher _BücherDerSelektiertenBestellung;

        /// <summary>
        /// Ruft ein Auflistung an Büchern ab oder legt diese fest
        /// </summary>
        public WIFI.Anwendung.DTO.Bücher BücherDerSelektiertenBestellung
        {
            get {
                if (this._BücherDerSelektiertenBestellung == null)
                {
                    HoleSelektierteBuchBestellungAsync();
                }
                return this._BücherDerSelektiertenBestellung; }
            set
            {
                this._BücherDerSelektiertenBestellung = value;
                this.OnPropertyChanged();
                PusheSelektierteBuchBestellungAsync();
            }
        }


        /// <summary>
        /// Läd die Bücher der Selektierten Bestellung Asyncron
        /// </summary>
        protected async void HoleSelektierteBuchBestellungAsync()
        {
            await System.Threading.Tasks.Task.Run(
                 () =>
                 {
                     BücherDerSelektiertenBestellung = new WIFI.Anwendung.DTO.Bücher();
                     // Wenn Eine Bestellung ausgewählt ist, nimm dessen Bücher, ansonst eine leere liste

                     if (this.AktuelleBestellung != null && this.AktuelleBestellung.Buchliste != null)
                     {
                         var bücher = new WIFI.Anwendung.DTO.Bücher();

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
        protected async void PusheSelektierteBuchBestellungAsync()
        {
            await System.Threading.Tasks.Task.Run(
                () =>
                {

                    if (BücherDerSelektiertenBestellung != null)
                    {
                        if (BücherDerSelektiertenBestellung.Count > 0)
                        {
                            Dictionary<WIFI.Anwendung.DTO.Buch, int> TempListe = new Dictionary<WIFI.Anwendung.DTO.Buch, int>();

                            foreach (var item in BücherDerSelektiertenBestellung)
                            {
                                TempListe.Add(item, item.Anzahl);
                            }

                            this.AktuelleBestellung.Buchliste = TempListe;
                        }
                    }


                });
        }

        /// <summary>
        /// Internes Feld für die Eigenschaft
        /// </summary>
        private string _BestellNr = string.Empty;

        /// <summary>
        /// Ruft die Bestellungsnummer ab oder legt diese fest
        /// </summary>
        public string BestellNr
        {
            get { return this._BestellNr; }
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
            get {
                if (this._AktualisiereBestellung == null)
                {
                    this._AktualisiereBestellung = new Anwendung.Befehl(
                        p => {
                            //TODO: Implementiere 
                            this.AppKontext.DBControllerManager.BestellungController.AktualisiereBestellung(this.AktuelleBestellung);
                        }
                    );
                }
                return this._AktualisiereBestellung; }
            set { this._AktualisiereBestellung = value; }
        }
    }
}
