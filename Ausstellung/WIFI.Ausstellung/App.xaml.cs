using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace WIFI.Ausstellung
{
    /// <summary>
    /// Interaktionslogik für "App.xaml"
    /// </summary>
    public partial class App : Application
    {
        /// <summary>
        /// Stellt den Haupteinstiegspunkt bereit
        /// </summary>
        /// <remarks>Für das eigene Main muss
        /// bei WPF "ApplicationDefinition" 
        /// bei App.xaml auf "Page" umgestellt werden</remarks>
        [System.STAThread]
        static void Main()
        {

            // die erweiterte Infrastruktur hochfahren
            var AppKontext = new WIFI.Anwendung.DatenbankAppKontext();

            // Aktiviert das automatische Komprimieren, wenn die Settings-Property aus der Anwendung auf true gesetzt ist
            AppKontext.Protokoll.AutomatischKomprimieren = WIFI.Ausstellung.Properties.Settings.Default.ProtokollKomprimieren;

            // Damit das eigene ViewModel intialisieren
            var VM = AppKontext.Produziere<ViewModels.Anwendung>();

            // Damit die WPF Ressourcen geladen werden,
            // die WPF Anwendung intialisieren
            // (Damit System.Windows.Application.Current
            //  initialisiert wird)
            var WpfApp = new WIFI.Ausstellung.App();
            WpfApp.InitializeComponent();

            // Damit das Protokoll für die WPF Anwendung threadsicher wird,
            // den Dispatcher der Anwendung mitteilen
            AppKontext.Protokoll.Dispatcher = WpfApp.Dispatcher;

            // Das ViewModel mit einem Xaml Fenster verbinden,
            // dem ViewModel die View mitteilen
            VM.Starten<WIFI.Ausstellung.MainWindow>();
                     
            // Speichern der Einstellungen
            WIFI.Ausstellung.Properties.Settings.Default.Save();

            // die Infrastruktur herunterfahren
            AppKontext.Fenster.Speichern();

        }
    }
}
