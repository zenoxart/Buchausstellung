using System.Windows;
using System.Windows.Controls;

namespace WIFI.Ausstellung.UserControls
{
    /// <summary>
    /// Interaktionslogik für Ausstellungskopfzeile.xaml
    /// </summary>
    public partial class Ausstellungskopfzeile : UserControl
    {
        public Ausstellungskopfzeile()
        {
            InitializeComponent();
        }

        #region 

        public static readonly DependencyProperty DunklerModus =
            DependencyProperty.Register("DarkMode", typeof(bool), typeof(Ausstellungskopfzeile));


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
