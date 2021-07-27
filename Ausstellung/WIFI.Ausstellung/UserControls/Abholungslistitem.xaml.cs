using System.Windows;
using System.Windows.Controls;

namespace WIFI.Ausstellung.UserControls
{
    /// <summary>
    /// Interaktionslogik für Abholungslistitem.xaml
    /// </summary>
    public partial class Abholungslistitem : UserControl
    {
        public Abholungslistitem()
        {
            InitializeComponent();
        }

        #region Abgeholt

        public static readonly DependencyProperty AbgeholtProperty =
            DependencyProperty.Register("Abgeholt", typeof(bool), typeof(Abholungslistitem));


        /// <summary>
        /// Erstellt eine Benutzbare Schnittstelle zu dem Property
        /// </summary>
        public bool Abgeholt
        {
            get { return (bool)GetValue(AbgeholtProperty); }
            set { SetValue(AbgeholtProperty, value); }
        }

        #endregion


        #region BestellNr

        public static readonly DependencyProperty BestellNrProperty =
            DependencyProperty.Register("BestellNr", typeof(int), typeof(Abholungslistitem));


        /// <summary>
        /// Erstellt eine Benutzbare Schnittstelle zu dem Property
        /// </summary>
        public int BestellNr
        {
            get { return (int)GetValue(BestellNrProperty); }
            set { SetValue(BestellNrProperty, value); }
        }

        #endregion

        #region BesucherVorname

        public static readonly DependencyProperty BesucherVornameProperty =
            DependencyProperty.Register("BesucherVorname", typeof(string), typeof(Abholungslistitem));


        /// <summary>
        /// Erstellt eine Benutzbare Schnittstelle zu dem Property
        /// </summary>
        public string BesucherVorname
        {
            get { return (string)GetValue(BesucherVornameProperty); }
            set { SetValue(BesucherVornameProperty, value); }
        }

        #endregion


        #region BesucherNachname

        public static readonly DependencyProperty BesucherNachnameProperty =
            DependencyProperty.Register("BesucherNachname", typeof(string), typeof(Abholungslistitem));


        /// <summary>
        /// Erstellt eine Benutzbare Schnittstelle zu dem Property
        /// </summary>
        public string BesucherNachname
        {
            get { return (string)GetValue(BesucherNachnameProperty); }
            set { SetValue(BesucherNachnameProperty, value); }
        }

        #endregion

        
    }
}
