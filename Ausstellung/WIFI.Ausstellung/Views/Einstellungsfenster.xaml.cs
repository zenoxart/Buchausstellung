using System;
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
    /// Interaktionslogik für Einstellungsfenster.xaml
    /// </summary>
    public partial class Einstellungsfenster : Window, System.ComponentModel.INotifyPropertyChanged
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
            [System.Runtime.CompilerServices.CallerMemberName]string eigenschaft = "")
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



        public Einstellungsfenster()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Internes Feld für die Eigenschaft
        /// </summary>
        private WIFI.Anwendung.Befehl _PfadWählenFürGesammtListenPfad = null;

        /// <summary>
        /// Öffnet einen Pfadleser für den übergebenen Parameter
        /// </summary>
        public WIFI.Anwendung.Befehl PfadWählenFürGesammtListenPfad
        {
            get
            {
                if (this._PfadWählenFürGesammtListenPfad == null)
                {
                    this._PfadWählenFürGesammtListenPfad = new Anwendung.Befehl(
                        p =>
                        {
                            System.Windows.Forms.FolderBrowserDialog folderBrowserDialog = new System.Windows.Forms.FolderBrowserDialog();

                            folderBrowserDialog.Description = "Wählen Sie einen Ordner";
                            if (folderBrowserDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                                    GesammtListenPfad = folderBrowserDialog.SelectedPath;
                        }
                    );
                }
                return this._PfadWählenFürGesammtListenPfad;
            }
            set { this._PfadWählenFürGesammtListenPfad = value; }
        }

        /// <summary>
        /// Internes Feld für die Eigenschaft
        /// </summary>
        private WIFI.Anwendung.Befehl _PfadWählenBestellbestätigungsPfad = null;

        /// <summary>
        /// Öffnet einen Pfadleser für den übergebenen Parameter
        /// </summary>
        public WIFI.Anwendung.Befehl PfadWählenFürBestellbestätigungsPfad
        {
            get
            {
                if (this._PfadWählenBestellbestätigungsPfad == null)
                {
                    this._PfadWählenBestellbestätigungsPfad = new Anwendung.Befehl(
                        p =>
                        {
                            System.Windows.Forms.FolderBrowserDialog folderBrowserDialog = new System.Windows.Forms.FolderBrowserDialog();

                            folderBrowserDialog.Description = "Wählen Sie einen Ordner";
                            if (folderBrowserDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                                    BestellbestätigungsPfad = folderBrowserDialog.SelectedPath;
                        }
                    );
                }
                return this._PfadWählenBestellbestätigungsPfad;
            }
            set { this._PfadWählenBestellbestätigungsPfad = value; }
        }

        /// <summary>
        /// Ruft den Pfad für die Gesammtlisten ab oder legt diesen fest
        /// </summary>
        public string GesammtListenPfad
        {
            get
            {
                return WIFI.Ausstellung.Properties.Settings.Default.Gesamtbestelllistenpfad;
            }
            set
            {
                WIFI.Ausstellung.Properties.Settings.Default.Gesamtbestelllistenpfad = value;
                this.OnPropertyChanged();
            }
        }

        /// <summary>
        /// Ruft den Pfad für die Bestellbestätigungen ab oder legt diesen fest
        /// </summary>
        public string BestellbestätigungsPfad
        {
            get
            {
                return WIFI.Ausstellung.Properties.Settings.Default.Bestellbestätigungenpfad;
            }
            set
            {
                WIFI.Ausstellung.Properties.Settings.Default.Bestellbestätigungenpfad = value;
                this.OnPropertyChanged();
            }
        }


    }
}
