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
    /// Interaktionslogik für Buchverwaltungslistitem.xaml
    /// </summary>
    public partial class Buchverwaltungslistitem : UserControl
    {
        public Buchverwaltungslistitem()
        {
            InitializeComponent();
        }

        #region Buchnr-Property
        // Schema F umd ein DependencyProperty zu übergeben

        /// <summary>
        /// Erstellt ein DependencyProperty
        /// </summary>
        public static readonly DependencyProperty BuchnrProperty =
        DependencyProperty.Register("Buchnr", typeof(string), typeof(Buchverwaltungslistitem));

        /// <summary>
        /// Erstellt eine Benutzbare Schnittstelle zu dem Property
        /// </summary>
        public string Buchnr
        {
            get { return (string)GetValue(BuchnrProperty); }
            set { SetValue(BuchnrProperty, value); }
        }
        #endregion

        #region Titel-Property
        /// <summary>
        /// Erstellt ein DependencyProperty
        /// </summary>
        public static readonly DependencyProperty TitelProperty =
        DependencyProperty.Register("Titel", typeof(string), typeof(Buchverwaltungslistitem));

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
        DependencyProperty.Register("Autor", typeof(string), typeof(Buchverwaltungslistitem));

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
        DependencyProperty.Register("Verlag", typeof(string), typeof(Buchverwaltungslistitem));

        /// <summary>
        /// Erstellt eine Benutzbare Schnittstelle zu dem Property
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
        DependencyProperty.Register("Rabatt", typeof(string), typeof(Buchverwaltungslistitem));

        /// <summary>
        /// Erstellt eine Benutzbare Schnittstelle zu dem Property
        /// </summary>
        public string Rabatt
        {
            get { return (string)GetValue(RabattProperty); }
            set { SetValue(RabattProperty, value); }
        }

        #endregion


        #region Kategorie-Property
        /// <summary>
        /// Erstellt ein DependencyProperty
        /// </summary>
        public static readonly DependencyProperty KategorieProperty =
        DependencyProperty.Register("Kategorie", typeof(string), typeof(Buchverwaltungslistitem));

        /// <summary>
        /// Erstellt eine Benutzbare Schnittstelle zu dem Property
        /// </summary>
        public string Kategorie
        {
            get { return (string)GetValue(KategorieProperty); }
            set { SetValue(KategorieProperty, value); }
        }
        #endregion


        #region Preis-Property
        /// <summary>
        /// Erstellt ein DependencyProperty
        /// </summary>
        public static readonly DependencyProperty PreisProperty =
        DependencyProperty.Register("Preis", typeof(string), typeof(Buchverwaltungslistitem));

        /// <summary>
        /// Erstellt eine Benutzbare Schnittstelle zu dem Property
        /// </summary>
        public string Preis
        {
            get { return (string)GetValue(PreisProperty); }
            set { SetValue(PreisProperty, value); }
        }

        #endregion


        #region DunkelModus-Property
        /// <summary>
        /// Erstellt ein DependencyProperty
        /// </summary>
        public static readonly DependencyProperty DunkelModusProperty =
        DependencyProperty.Register("DunkelModus", typeof(string), typeof(Buchverwaltungslistitem));

        /// <summary>
        /// Erstellt eine Benutzbare Schnittstelle zu dem Property
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
        DependencyProperty.Register("Id", typeof(string), typeof(Buchverwaltungslistitem), new
            PropertyMetadata("", new PropertyChangedCallback(OnIdChanged)));

        /// <summary>
        /// Erstellt eine benutzbare Schnittstelle zu dem Property
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
            Buchverwaltungslistitem UCC = d as Buchverwaltungslistitem;
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

        private string BuchId = "";
        #endregion

        /// <summary>
        /// Internes Feld für die Eigenschaft
        /// </summary>
        private WIFI.Anwendung.Befehl _BuchHinzufuegen = null;

        public WIFI.Anwendung.Befehl BuchHinzufügen
        {
            get
            {
                if (this._BuchHinzufuegen == null)
                {
                    // Den Befehl mit anoymen Methoden initialisieren

                    this._BuchHinzufuegen = new WIFI.Anwendung.Befehl(

                        // Werte des Buches in der Bücherliste hinzufügen
                        p =>
                        {

                            if (ViewModels.BuchManager.AktuelleBücher == null)
                            {

                                ViewModels.BuchManager.AktuelleBücher = new WIFI.Anwendung.DTO.Bücher();
                            }

                            WIFI.Anwendung.DTO.Buch b = null;
                            if (ViewModels.BuchManager.AktuelleBücher.Count > 0)
                            {
                                b = (from l in ViewModels.BuchManager.AktuelleBücher
                                     where string.Compare(l.ID.ToString(), Id, ignoreCase: true) == 0
                                     select l).FirstOrDefault();

                            }
                            // Nehme das Erste Element welches die selbe Id schon hat

                            // Wenn kein Element mit der ID in der Liste existiert, füge es hinzu
                            if (b == null)
                            {
                                ViewModels.AusstellungsManager.AktuelleBücherbestellung.Add(
                                new Anwendung.DTO.Buch
                                {

                                    AutorName = Autor,
                                    ID = Convert.ToInt32(Id),
                                    Kategoriegruppe = Convert.ToInt32(Kategorie),
                                    Preis = Convert.ToDouble(Preis),
                                    Rabattgruppe = Convert.ToInt32(Rabatt),
                                    Titel = Titel,
                                    VerlagName = Verlag
                                }
                            );
                            }


                            //ParrentViewModel.Ausstellung.AktuelleBücherbestellung.add(new Buch() { Preis =  });
                        }
                        );
                }

                return this._BuchHinzufuegen;
            }

            set { this._BuchHinzufuegen = value; }
        }
    }
}
