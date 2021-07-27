using System.Windows;
using System.Windows.Controls;

namespace WIFI.Ausstellung.UserControls
{
    /// <summary>
    /// Interaktionslogik für Bestellungenkopfzeile.xaml
    /// </summary>
    public partial class Bestellungenkopfzeile : UserControl
    {
        public Bestellungenkopfzeile()
        {
            InitializeComponent();
        }

        #region 

        public static readonly DependencyProperty DunklerModus =
            DependencyProperty.Register("DarkMode", typeof(bool), typeof(Bestellungenkopfzeile));


        /// <summary>
        /// Erstellt eine Benutzbare Schnittstelle zu dem Property
        /// </summary>
        public bool DarkMode
        {
            get { return (bool)GetValue(DunklerModus); }
            set { SetValue(DunklerModus, value); }
        }

        #endregion
    }
}
