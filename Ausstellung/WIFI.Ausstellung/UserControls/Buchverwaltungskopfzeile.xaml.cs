using System.Windows;
using System.Windows.Controls;

namespace WIFI.Ausstellung.UserControls
{
    /// <summary>
    /// Interaktionslogik für Buchverwaltungskopfzeile.xaml
    /// </summary>
    public partial class Buchverwaltungskopfzeile : UserControl
    {
        public Buchverwaltungskopfzeile()
        {
            InitializeComponent();
        }

        #region Verwalten des Hell/Dunkel-Modus
        public static readonly DependencyProperty DunklerModus =
            DependencyProperty.Register("DarkMode", typeof(bool), typeof(Buchverwaltungskopfzeile));

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
