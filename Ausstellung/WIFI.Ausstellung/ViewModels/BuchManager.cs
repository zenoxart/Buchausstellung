using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WIFI.Ausstellung.ViewModels
{
    public class BuchManager : WIFI.Anwendung.ViewModelAppObjekt
    {
        #region BücherView
        /// <summary>
        /// Internes Feld für die Eigenschaft
        /// </summary>
        private static WIFI.Anwendung.DTO.Bücher _Liste = null;

        public WIFI.Anwendung.DTO.Bücher Buchausstellungsliste
        {
            get
            {
                if (BuchManager._Liste == null)
                {
                    Buchausstellungsliste = new WIFI.Anwendung.DTO.Bücher
                    {
                        new WIFI.Anwendung.DTO.Buch
                        {
                            Buchnummer = "0",
                            Titel = "Buchtitel werden geladen...",
                            AutorName = "Bitte warten.",
                            ID = 0,
                            Kategoriegruppe = 0,
                            Rabattgruppe = 0,
                            Preis = 0,
                            VerlagName = string.Empty
                        }
                    };
                }

                return BuchManager._Liste;
            }
            set
            {
                BuchManager._Liste = value;
                this.OnPropertyChanged();
            }
        }
        #endregion
    }
}
