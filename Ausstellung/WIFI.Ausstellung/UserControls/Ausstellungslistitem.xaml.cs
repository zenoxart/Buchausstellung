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


        }


        #region Buchnr-Property
        // Schema F umd ein DependencyProperty zu übergeben

        /// <summary>
        /// Erstellt ein DependencyProperty
        /// </summary>
        public static readonly DependencyProperty BuchnrProperty =
        DependencyProperty.Register("Buchnr", typeof(string), typeof(Ausstellungslistitem));

        /// <summary>
        /// Erstellt eine Benutzbare Schnittstelle zu dem Property
        /// </summary>
        public string Buchnr
        {
            get { return (string)GetValue(BuchnrProperty); }
            set { SetValue(BuchnrProperty, value); }
        }
        #endregion

        #region Title-Property
        // Schema F umd ein DependencyProperty zu übergeben

        /// <summary>
        /// Erstellt ein DependencyProperty
        /// </summary>
        public static readonly DependencyProperty TitelProperty =
        DependencyProperty.Register("Titel", typeof(string), typeof(Ausstellungslistitem));

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
        DependencyProperty.Register("Autor", typeof(string), typeof(Ausstellungslistitem));

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
        DependencyProperty.Register("Verlag", typeof(string), typeof(Ausstellungslistitem));

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
        DependencyProperty.Register("Rabatt", typeof(string), typeof(Ausstellungslistitem));

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
        DependencyProperty.Register("Kategorie", typeof(string), typeof(Ausstellungslistitem));

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
        DependencyProperty.Register("Preis", typeof(string), typeof(Ausstellungslistitem));

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
        DependencyProperty.Register("DunkelModus", typeof(string), typeof(Ausstellungslistitem));

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


        private string BuchId = "";



        #endregion

        /// <summary>
        /// Internes Feld für die Eigenschaft
        /// </summary>
        private WIFI.Anwendung.Befehl _BuchbestellungHinzufügen = null;

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

                            if (ViewModels.AusstellungsManager.AktuelleBücherbestellung == null)
                            {

                                ViewModels.AusstellungsManager.AktuelleBücherbestellung = new WIFI.Anwendung.DTO.Bücher();
                            }

                            WIFI.Anwendung.DTO.Buch b = null;
                            if (ViewModels.AusstellungsManager.AktuelleBücherbestellung.Count > 0)
                            {
                                b = (from l in ViewModels.AusstellungsManager.AktuelleBücherbestellung
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

                return this._BuchbestellungHinzufügen;
            }

            set { this._BuchbestellungHinzufügen = value; }
        }




    }
}
