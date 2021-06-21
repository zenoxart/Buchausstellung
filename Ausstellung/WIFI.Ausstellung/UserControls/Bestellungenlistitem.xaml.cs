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
    /// Interaktionslogik für Bestellungenlistitem.xaml
    /// </summary>
    public partial class Bestellungenlistitem : UserControl
    {
        public Bestellungenlistitem()
        {
            InitializeComponent();
        }

        #region BestellNr-Property
        // Schema F umd ein DependencyProperty zu übergeben

        /// <summary>
        /// Erstellt ein DependencyProperty
        /// </summary>
        public static readonly DependencyProperty BestellNrProperty =
        DependencyProperty.Register("BestellNr", typeof(string), typeof(Bestellungenlistitem));

        /// <summary>
        /// Erstellt eine Benutzbare Schnittstelle zu dem Property
        /// </summary>
        public string BestellNr
        {
            get { return (string)GetValue(BestellNrProperty); }
            set { SetValue(BestellNrProperty, value); }
        }

        #endregion

        #region Besucher-Property

        /// <summary>
        /// Erstellt ein DependencyProperty
        /// </summary>
        public static readonly DependencyProperty BesucherProperty =
        DependencyProperty.Register("Besucher", typeof(WIFI.Gateway.DTO.Besucher), typeof(Bestellungenlistitem));

        /// <summary>
        /// Erstellt eine Benutzbare Schnittstelle zu dem Property
        /// </summary>
        public WIFI.Gateway.DTO.Besucher Besucher
        {
            get { return (WIFI.Gateway.DTO.Besucher)GetValue(BesucherProperty); }
            set { SetValue(BesucherProperty, value); }
        }

        #endregion

        /// <summary>
        /// Internes Feld für die Eigenschaft
        /// </summary>
        private WIFI.Anwendung.Befehl _BearbeiteBestellung;

        /// <summary>
        /// Öffnet ein neues Fenster, indem die Bestellung bearbeitbar ist
        /// </summary>
        public WIFI.Anwendung.Befehl BearbeiteBestellung
        {
            get
            {
                if (this._BearbeiteBestellung == null)
                {
                    this._BearbeiteBestellung = new WIFI.Anwendung.Befehl(
                        p =>
                        {
                            var neuesFenster = new WIFI.Ausstellung.Views.Bestellungsfentser(this.BestellNr);
                            neuesFenster.Show();
                        }
                    );
                }
                return this._BearbeiteBestellung;
            }
            set { this._BearbeiteBestellung = value; }
        }


    }
}
