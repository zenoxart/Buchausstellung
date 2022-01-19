using System;

namespace WIFI.Gateway.DTO
{
    /// <summary>
    /// Eine Auflistung, in welchem Stadium
    /// sich die Ausstellung befindet
    /// </summary>
    public enum AusstellungsstadiumTyp
    {
        /// <summary>
        /// Beschreibt, dass die Ausstellung in Vorbereitung ist
        /// </summary>
        Vorbereitung = 0,
        /// <summary>
        /// Beschreibt, dass die Ausstellung aktuell stattfindet
        /// </summary>
        Veranstaltung = 1,
        /// <summary>
        /// Beschreibt, dass die Austellung beendet ist und 
        /// die Bestellungen geliefert werden müssen
        /// </summary>
        Lieferung = 2,
        /// <summary>
        /// Beschreibt, dass die Bestellungen geliefert wurden und
        /// die Bestellungen nun von den Bestellern abgeholt werden
        /// </summary>
        Abholung = 3,
        /// <summary>
        /// Beschreibt, dass die Datenbankverbindung nicht 
        /// hergestellt werden konnte
        /// </summary>
        Verbindungsfehler = 4
    }

    /// <summary>
    /// Stellt die grundsätzlichen Informationen
    /// der Buchausstellung bereit.
    /// </summary>
    public class Veranstaltung : WIFI.Anwendung.Daten.DatenBasis
    {
        /// <summary>
        /// Ruft die interne Nr der Ausstellung ab,
        /// oder legt diese fest
        /// </summary>
        public int ID { get; set; } = 0;

        /// <summary>
        /// Internes Feld für die Eigenschaft
        /// </summary>
        private string _Ort = string.Empty;

        /// <summary>
        /// Ruft den Ort der Ausstellung ab,
        /// oder legt diesen fest
        /// </summary>
        public string Ort
        {
            get
            {
                return this._Ort;
            }
            set
            {
                if (this._Ort != value)
                {
                    this._Ort = value;
                    this.OnPropertyChanged();
                }
            }
        }

        /// <summary>
        /// Internes Feld für die Eigenschaft
        /// </summary>
        private DateTime _DatumVon;

        /// <summary>
        /// Ruft das Beginndatum der Ausstellung ab,
        /// oder legt dieses fest
        /// </summary>
        public DateTime DatumVon
        {
            get
            {
                return this._DatumVon;
            }
            set
            {
                if (this._DatumVon != value)
                {
                    this._DatumVon = value;
                    this.OnPropertyChanged();
                }
            }
        }

        /// <summary>
        /// Internes Feld für die Eigenschaft
        /// </summary>
        private DateTime _DatumBis;

        /// <summary>
        /// Ruft das Enddatum der Veranstaltung ab,
        /// oder legt dieses fest
        /// </summary>
        public DateTime DatumBis
        {
            get
            {
                return this._DatumBis;
            }
            set
            {
                if (this._DatumBis != value)
                {
                    this._DatumBis = value;
                    this.OnPropertyChanged();
                }
            }
        }

        /// <summary>
        /// Internes Feld für die Eigenschaft
        /// </summary>
        private AusstellungsstadiumTyp _Stadium = AusstellungsstadiumTyp.Vorbereitung;

        /// <summary>
        /// Ruft den aktuellen Zustand der Ausstellung ab,
        /// oder legt diesen fest
        /// </summary>
        public AusstellungsstadiumTyp Stadium
        {
            get
            {

                return this._Stadium;
            }
            set
            {
                if (this._Stadium != value)
                {
                    this._Stadium = value;
                    this.OnPropertyChanged();
                }
            }
        }
    }
}
