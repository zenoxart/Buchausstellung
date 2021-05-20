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
