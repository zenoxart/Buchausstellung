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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace WIFI.Ausstellung.Views.Aufgaben
{
    /// <summary>
    /// Interaktionslogik für Buchverwaltung.xaml
    /// </summary>
    public partial class Buchverwaltung : UserControl
    {
        public Buchverwaltung()
        {
            InitializeComponent();
        }

        #region Buchnummer-Property
        // Schema F umd ein DependencyProperty zu übergeben

        /// <summary>
        /// Erstellt ein DependencyProperty
        /// </summary>
        public static readonly DependencyProperty BuchnummerProperty =
        DependencyProperty.Register("Buchnummer", typeof(string), typeof(Buchverwaltung));

        /// <summary>
        /// Erstellt eine benutzbare Schnittstelle zu dem Property
        /// </summary>
        public string Buchnummer
        {
            get { return (string)GetValue(BuchnummerProperty); }
            set { SetValue(BuchnummerProperty, value); }
        }
        #endregion

        #region Titel-Property
        /// <summary>
        /// Erstellt ein DependencyProperty
        /// </summary>
        public static readonly DependencyProperty TitelProperty =
        DependencyProperty.Register("Titel", typeof(string), typeof(Buchverwaltung));

        /// <summary>
        /// Erstellt eine Benutzbare Schnittstelle zu dem Property
        /// </summary>
        public string Titel
        {
            get { return (string)GetValue(TitelProperty); }
            set { SetValue(TitelProperty, value); }
        }
        #endregion

        #region Autor-Property
        /// <summary>
        /// Erstellt ein DependencyProperty
        /// </summary>
        public static readonly DependencyProperty AutorProperty =
        DependencyProperty.Register("Autor", typeof(string), typeof(Buchverwaltung));

        /// <summary>
        /// Erstellt eine Benutzbare Schnittstelle zu dem Property
        /// </summary>
        public string Autor
        {
            get { return (string)GetValue(AutorProperty); }
            set { SetValue(AutorProperty, value); }
        }
        #endregion

        #region Verlag-Property
        /// <summary>
        /// Erstellt ein DependencyProperty
        /// </summary>
        public static readonly DependencyProperty VerlagProperty =
        DependencyProperty.Register("Verlag", typeof(string), typeof(Buchverwaltung));

        /// <summary>
        /// Erstellt eine benutzbare Schnittstelle zu dem Property
        /// </summary>
        public string Verlag
        {
            get { return (string)GetValue(VerlagProperty); }
            set { SetValue(VerlagProperty, value); }
        }
        #endregion

        #region Rabatt-Property
        /// <summary>
        /// Erstellt ein DependencyProperty
        /// </summary>
        public static readonly DependencyProperty RabattProperty =
        DependencyProperty.Register("Rabatt", typeof(string), typeof(Buchverwaltung));

        /// <summary>
        /// Erstellt eine benutzbare Schnittstelle zu dem Property
        /// </summary>
        public string Rabatt
        {
            get { return (string)GetValue(RabattProperty); }
            set { SetValue(RabattProperty, value); }
        }
        #endregion

        #region Buchgruppe-Property
        /// <summary>
        /// Erstellt ein DependencyProperty
        /// </summary>
        public static readonly DependencyProperty BuchgruppeProperty =
        DependencyProperty.Register("Kategorie", typeof(string), typeof(Buchverwaltung));

        /// <summary>
        /// Erstellt eine benutzbare Schnittstelle zu dem Property
        /// </summary>
        public string Buchgruppe
        {
            get { return (string)GetValue(BuchgruppeProperty); }
            set { SetValue(BuchgruppeProperty, value); }
        }
        #endregion

        #region Preis-Property
        /// <summary>
        /// Erstellt ein DependencyProperty
        /// </summary>
        public static readonly DependencyProperty PreisProperty =
        DependencyProperty.Register("Preis", typeof(double), typeof(Buchverwaltung));

        /// <summary>
        /// Erstellt eine Benutzbare Schnittstelle zu dem Property
        /// </summary>
        public double Preis
        {
            get { return (double)GetValue(PreisProperty); }
            set { SetValue(PreisProperty, value); }
        }
        #endregion

        #region DunkelModus-Property
        /// <summary>
        /// Erstellt ein DependencyProperty
        /// </summary>
        public static readonly DependencyProperty DunkelModusProperty =
        DependencyProperty.Register("DunkelModus", typeof(string), typeof(Buchverwaltung));

        /// <summary>
        /// Erstellt eine benutzbare Schnittstelle zu dem Property
        /// </summary>
        public string DunkelModus
        {
            get { return (string)GetValue(DunkelModusProperty); }
            set { SetValue(DunkelModusProperty, value); }
        }
        #endregion

        #region Id-Property
        /// <summary>
        /// Erstellt ein DependencyProperty
        /// </summary>
        public static readonly DependencyProperty IdProperty =
        DependencyProperty.Register("Id", typeof(string), typeof(Buchverwaltung));

        /// <summary>
        /// Erstellt eine benutzbare Schnittstelle zu dem Property
        /// </summary>
        public string Id
        {
            get { return (string)GetValue(IdProperty); }
            set { SetValue(IdProperty, value); }
        }

        #endregion
    }

}