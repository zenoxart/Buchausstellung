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

namespace WIFI.Ausstellung.UserControls
{
    /// <summary>
    /// Interaktionslogik für Ausstellungslistitem.xaml
    /// </summary>
    public partial class Ausstellungslistitem : UserControl
    {


        public Ausstellungslistitem()
        {
            InitializeComponent();

            this.DataContext = this;

        }

        #region Title-Property
        // Schema F umd ein DependencyProperty zu übergeben

        /// <summary>
        /// Erstellt ein DependencyProperty
        /// </summary>
        public static readonly DependencyProperty TitleProperty =
        DependencyProperty.Register("Title", typeof(string), typeof(Ausstellungslistitem), new
            PropertyMetadata("", new PropertyChangedCallback(OnTitleChanged)));

        /// <summary>
        /// Erstellt eine Benutzbare Schnittstelle zu dem Property
        /// </summary>
        public string Title
        {
            get { return (string)GetValue(TitleProperty); }
            set { SetValue(TitleProperty, value); }
        }

        /// <summary>
        /// Erstellt ein Event, wenn dieses Property sich ändert
        /// </summary>
        private static void OnTitleChanged(DependencyObject d,
         DependencyPropertyChangedEventArgs e)
        {
            Ausstellungslistitem UCC = d as Ausstellungslistitem;
            UCC.OnTitleChanged(e);
        }

        /// <summary>
        /// Setzt dieses Property auf das Angegebene Element
        /// </summary>
        /// <param name="e"></param>
        private void OnTitleChanged(DependencyPropertyChangedEventArgs e)
        {
            TitleField.Content = e.NewValue.ToString();
        }
        #endregion


        #region Author-Property
        /// <summary>
        /// Erstellt ein DependencyProperty
        /// </summary>
        public static readonly DependencyProperty AuthorProperty =
        DependencyProperty.Register("Author", typeof(string), typeof(Ausstellungslistitem), new
            PropertyMetadata("", new PropertyChangedCallback(OnAuthorChanged)));

        /// <summary>
        /// Erstellt eine Benutzbare Schnittstelle zu dem Property
        /// </summary>
        public string Author
        {
            get { return (string)GetValue(AuthorProperty); }
            set { SetValue(AuthorProperty, value); }
        }

        /// <summary>
        /// Erstellt ein Event, wenn dieses Property sich ändert
        /// </summary>
        private static void OnAuthorChanged(DependencyObject d,
         DependencyPropertyChangedEventArgs e)
        {
            Ausstellungslistitem UCC = d as Ausstellungslistitem;
            UCC.OnAuthorChanged(e);
        }

        /// <summary>
        /// Setzt dieses Property auf das Angegebene Element
        /// </summary>
        /// <param name="e"></param>
        private void OnAuthorChanged(DependencyPropertyChangedEventArgs e)
        {
            AuthorField.Content = e.NewValue.ToString();
        }
        #endregion


        #region Verlag-Property
        /// <summary>
        /// Erstellt ein DependencyProperty
        /// </summary>
        public static readonly DependencyProperty VerlagProperty =
        DependencyProperty.Register("Verlag", typeof(string), typeof(Ausstellungslistitem), new
            PropertyMetadata("", new PropertyChangedCallback(OnVerlagChanged)));

        /// <summary>
        /// Erstellt eine Benutzbare Schnittstelle zu dem Property
        /// </summary>
        public string Verlag
        {
            get { return (string)GetValue(VerlagProperty); }
            set { SetValue(VerlagProperty, value); }
        }

        /// <summary>
        /// Erstellt ein Event, wenn dieses Property sich ändert
        /// </summary>
        private static void OnVerlagChanged(DependencyObject d,
         DependencyPropertyChangedEventArgs e)
        {
            Ausstellungslistitem UCC = d as Ausstellungslistitem;
            UCC.OnVerlagChanged(e);
        }

        /// <summary>
        /// Setzt dieses Property auf das Angegebene Element
        /// </summary>
        /// <param name="e"></param>
        private void OnVerlagChanged(DependencyPropertyChangedEventArgs e)
        {
            VerlagField.Content = e.NewValue.ToString();
        }
        #endregion


        #region Rabatt-Property
        /// <summary>
        /// Erstellt ein DependencyProperty
        /// </summary>
        public static readonly DependencyProperty RabattProperty =
        DependencyProperty.Register("Rabatt", typeof(string), typeof(Ausstellungslistitem), new
            PropertyMetadata("", new PropertyChangedCallback(OnRabattChanged)));

        /// <summary>
        /// Erstellt eine Benutzbare Schnittstelle zu dem Property
        /// </summary>
        public string Rabatt
        {
            get { return (string)GetValue(RabattProperty); }
            set { SetValue(RabattProperty, value); }
        }

        /// <summary>
        /// Erstellt ein Event, wenn dieses Property sich ändert
        /// </summary>
        private static void OnRabattChanged(DependencyObject d,
         DependencyPropertyChangedEventArgs e)
        {
            Ausstellungslistitem UCC = d as Ausstellungslistitem;
            UCC.OnRabattChanged(e);
        }

        /// <summary>
        /// Setzt dieses Property auf das Angegebene Element
        /// </summary>
        /// <param name="e"></param>
        private void OnRabattChanged(DependencyPropertyChangedEventArgs e)
        {
            RabattField.Content = e.NewValue.ToString();
        }
        #endregion


        #region Kategorie-Property
        /// <summary>
        /// Erstellt ein DependencyProperty
        /// </summary>
        public static readonly DependencyProperty KategorieProperty =
        DependencyProperty.Register("Kategorie", typeof(string), typeof(Ausstellungslistitem), new
            PropertyMetadata("", new PropertyChangedCallback(OnKategorieChanged)));

        /// <summary>
        /// Erstellt eine Benutzbare Schnittstelle zu dem Property
        /// </summary>
        public string Kategorie
        {
            get { return (string)GetValue(KategorieProperty); }
            set { SetValue(KategorieProperty, value); }
        }

        /// <summary>
        /// Erstellt ein Event, wenn dieses Property sich ändert
        /// </summary>
        private static void OnKategorieChanged(DependencyObject d,
         DependencyPropertyChangedEventArgs e)
        {
            Ausstellungslistitem UCC = d as Ausstellungslistitem;
            UCC.OnKategorieChanged(e);
        }

        /// <summary>
        /// Setzt dieses Property auf das Angegebene Element
        /// </summary>
        /// <param name="e"></param>
        private void OnKategorieChanged(DependencyPropertyChangedEventArgs e)
        {
            RabattField.Content = e.NewValue.ToString();
        }
        #endregion


        #region Preis-Property
        /// <summary>
        /// Erstellt ein DependencyProperty
        /// </summary>
        public static readonly DependencyProperty PreisProperty =
        DependencyProperty.Register("Preis", typeof(string), typeof(Ausstellungslistitem), new
            PropertyMetadata("", new PropertyChangedCallback(OnPreisChanged)));

        /// <summary>
        /// Erstellt eine Benutzbare Schnittstelle zu dem Property
        /// </summary>
        public string Preis
        {
            get { return (string)GetValue(PreisProperty); }
            set { SetValue(PreisProperty, value); }
        }

        /// <summary>
        /// Erstellt ein Event, wenn dieses Property sich ändert
        /// </summary>
        private static void OnPreisChanged(DependencyObject d,
         DependencyPropertyChangedEventArgs e)
        {
            Ausstellungslistitem UCC = d as Ausstellungslistitem;
            UCC.OnPreisChanged(e);
        }

        /// <summary>
        /// Setzt dieses Property auf das Angegebene Element
        /// </summary>
        /// <param name="e"></param>
        private void OnPreisChanged(DependencyPropertyChangedEventArgs e)
        {
            PreisField.Content = e.NewValue.ToString();
        }
        #endregion


        #region DunkelModus-Property
        /// <summary>
        /// Erstellt ein DependencyProperty
        /// </summary>
        public static readonly DependencyProperty DunkelModusProperty =
        DependencyProperty.Register("DunkelModus", typeof(string), typeof(Ausstellungslistitem), new
            PropertyMetadata("", new PropertyChangedCallback(OnDunkelModusChanged)));

        /// <summary>
        /// Erstellt eine Benutzbare Schnittstelle zu dem Property
        /// </summary>
        public string DunkelModus
        {
            get { return (string)GetValue(DunkelModusProperty); }
            set { SetValue(DunkelModusProperty, value); }
        }

        /// <summary>
        /// Erstellt ein Event, wenn dieses Property sich ändert
        /// </summary>
        private static void OnDunkelModusChanged(DependencyObject d,
         DependencyPropertyChangedEventArgs e)
        {
            Ausstellungslistitem UCC = d as Ausstellungslistitem;
            UCC.OnDunkelModusChanged(e);
        }

        /// <summary>
        /// Setzt dieses Property auf das Angegebene Element
        /// </summary>
        /// <param name="e"></param>
        private void OnDunkelModusChanged(DependencyPropertyChangedEventArgs e)
        {
            PreisField.Content = e.NewValue.ToString();
        }
        #endregion


        #region Id-Property
        /// <summary>
        /// Erstellt ein DependencyProperty
        /// </summary>
        public static readonly DependencyProperty IdProperty =
        DependencyProperty.Register("Id", typeof(string), typeof(Ausstellungslistitem), new
            PropertyMetadata("", new PropertyChangedCallback(OnIdChanged)));

        /// <summary>
        /// Erstellt eine Benutzbare Schnittstelle zu dem Property
        /// </summary>
        public string Id
        {
            get { return (string)GetValue(IdProperty); }
            set { SetValue(IdProperty, value); }
        }

        /// <summary>
        /// Erstellt ein Event, wenn dieses Property sich ändert
        /// </summary>
        private static void OnIdChanged(DependencyObject d,
         DependencyPropertyChangedEventArgs e)
        {
            Ausstellungslistitem UCC = d as Ausstellungslistitem;
            UCC.OnIdChanged(e);
        }

        /// <summary>
        /// Setzt dieses Property auf das Angegebene Element
        /// </summary>
        /// <param name="e"></param>
        private void OnIdChanged(DependencyPropertyChangedEventArgs e)
        {
            BuchId = (string)e.NewValue;
        }


        private string BuchId ="";

        

        #endregion

        /// <summary>
        /// Internes Feld für die Eigenschaft
        /// </summary>
        private WIFI.Anwendung.Befehl _BuchbestellungHinzufügen;

        public WIFI.Anwendung.Befehl BuchbestellungHinzufügen
        {
            get
            {
                if (this._BuchbestellungHinzufügen == null)
                {
                    // Den Befehl mit anoymen Methoden initialisieren

                    this._BuchbestellungHinzufügen = new WIFI.Anwendung.Befehl(

                        // TODO: Werte des Buches in eine Bestellliste hinzufügen
                        p =>
                        {


                        }
                        );
                }

                return this._BuchbestellungHinzufügen;
            }

            set { this._BuchbestellungHinzufügen = value; }
        }



    }
}
