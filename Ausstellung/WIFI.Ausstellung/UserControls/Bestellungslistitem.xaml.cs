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
    /// Interaktionslogik für Bestellungslistitem.xaml
    /// </summary>
    public partial class Bestellungslistitem : UserControl
    {
        public Bestellungslistitem()
        {
            InitializeComponent();
        }

        #region Title-Property
        // Schema F umd ein DependencyProperty zu übergeben

        /// <summary>
        /// Erstellt ein DependencyProperty
        /// </summary>
        public static readonly DependencyProperty TitleProperty =
        DependencyProperty.Register("Title", typeof(string), typeof(Bestellungslistitem));

        /// <summary>
        /// Erstellt eine Benutzbare Schnittstelle zu dem Property
        /// </summary>
        public string Title
        {
            get { return (string)GetValue(TitleProperty); }
            set { SetValue(TitleProperty, value); }
        }

        #endregion


        #region Author-Property
        /// <summary>
        /// Erstellt ein DependencyProperty
        /// </summary>
        public static readonly DependencyProperty AuthorProperty =
        DependencyProperty.Register("Author", typeof(string), typeof(Bestellungslistitem));

        /// <summary>
        /// Erstellt eine Benutzbare Schnittstelle zu dem Property
        /// </summary>
        public string Author
        {
            get { return (string)GetValue(AuthorProperty); }
            set { SetValue(AuthorProperty, value); }
        }
        #endregion


        #region Verlag-Property
        /// <summary>
        /// Erstellt ein DependencyProperty
        /// </summary>
        public static readonly DependencyProperty VerlagProperty =
        DependencyProperty.Register("Verlag", typeof(string), typeof(Bestellungslistitem));

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
        DependencyProperty.Register("Rabatt", typeof(string), typeof(Bestellungslistitem));

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
        DependencyProperty.Register("Kategorie", typeof(string), typeof(Bestellungslistitem));

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
        DependencyProperty.Register("Preis", typeof(string), typeof(Bestellungslistitem));

        /// <summary>
        /// Erstellt eine Benutzbare Schnittstelle zu dem Property
        /// </summary>
        public string Preis
        {
            get { return (string)GetValue(PreisProperty); }
            set { SetValue(PreisProperty, value); }
        }

        #endregion


        #region Id-Property
        /// <summary>
        /// Erstellt ein DependencyProperty
        /// </summary>
        public static readonly DependencyProperty IdProperty =
        DependencyProperty.Register("Id", typeof(string), typeof(Bestellungslistitem));

        /// <summary>
        /// Erstellt eine Benutzbare Schnittstelle zu dem Property
        /// </summary>
        public string Id
        {
            get { return (string)GetValue(IdProperty); }
            set { SetValue(IdProperty, value); }
        }





        #endregion


        #region Anzahl-Property
        /// <summary>
        /// Erstellt ein DependencyProperty
        /// </summary>
        public static readonly DependencyProperty AnzahlProperty =
        DependencyProperty.Register("Anzahl", typeof(int), typeof(Bestellungslistitem));

        /// <summary>
        /// Erstellt eine Benutzbare Schnittstelle zu dem Property
        /// </summary>
        public int Anzahl
        {
            get { return (int)GetValue(AnzahlProperty); }
            set { SetValue(AnzahlProperty, value); }
        }





        #endregion


        /// <summary>
        /// Internes Feld für die Eigenschaft
        /// </summary>
        private WIFI.Anwendung.Befehl _BuchbestellungLöschen = null;

        public WIFI.Anwendung.Befehl BuchbestellungLöschen
        {
            get
            {
                if (this._BuchbestellungLöschen == null)
                {
                    // Den Befehl mit anoymen Methoden initialisieren

                    this._BuchbestellungLöschen = new WIFI.Anwendung.Befehl(

                        // TODO: Werte des Buches in eine Bestellliste hinzufügen
                        p =>
                        {



                            WIFI.Anwendung.DTO.Buch b = null;

                            if (ViewModels.AusstellungsManager.AktuelleBücherbestellung.Count > 0)
                            {
                                b = (from l in ViewModels.AusstellungsManager.AktuelleBücherbestellung
                                     where string.Compare(l.ID.ToString(), Id, ignoreCase: true) == 0
                                     select l).FirstOrDefault();

                            }
                            // Nehme das Erste Element welches die selbe Id schon hat

                            // Wenn ein Element mit der ID in der Liste existiert, lösche es 
                            if (b != null)
                                ViewModels.AusstellungsManager.AktuelleBücherbestellung.Remove(b);


                        }

                        );
                }

                return this._BuchbestellungLöschen;
            }

            set { this._BuchbestellungLöschen = value; }
        }



    }
}
