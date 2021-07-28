
// Zum Sortieren der Standardliste der Sprachen
// wird die sprachintegrierte Abfrage, realisiert
// als Erweiterungsmethoden benutzt
using System.Linq;
//---


namespace WIFI.Anwendung
{
    /// <summary>
    /// Stellt einen Dienst zum Verwalten
    /// der Anwendungssprachen bereit
    /// </summary>
    public class SprachenManager : WIFI.Anwendung.AppObjekt
    {

        // Versionsänderungen
        // 20210211 Die Sprachen der Standardliste
        //          werden jetzt nach Name sortiert geliefert

        /// <summary>
        /// Internes Feld für die Eigenschaft
        /// </summary>
        private WIFI.Anwendung.Controller.SprachenXmlController _Controller = null;

        /// <summary>
        /// Ruft den Dienst zum Lesen und Schreiben
        /// von Sprachen im Xml Format ab
        /// </summary>
        private WIFI.Anwendung.Controller.SprachenXmlController Controller
        {
            get
            {
                if (this._Controller == null)
                {
                    this._Controller = this.AppKontext
                        .Produziere<WIFI.Anwendung.Controller.SprachenXmlController>();
                }

                return this._Controller;
            }
        }

        /// <summary>
        /// Internes Feld für die gecachten Sprachen aus den Ressourcen
        /// </summary>
        private static WIFI.Anwendung.Daten.Sprachen _StandardListe = null;

        /// <summary>
        /// Legt die Standardliste fest
        /// </summary>
        /// <param name="sprachen"></param>
        private void SetStandardListe(WIFI.Anwendung.Daten.Sprachen sprachen)
        {
            _StandardListe = sprachen;
        }

        /// <summary>
        /// Läd die Standardliste ( Gibt zurück)
        /// </summary>
        /// <returns></returns>
        private WIFI.Anwendung.Daten.Sprachen GetStandardListe()
        {
            return _StandardListe;
        }

        /// <summary>
        /// Ruft die Liste der in den Ressourcen
        /// untersützten Sprachen ab
        /// </summary>
        public WIFI.Anwendung.Daten.Sprache[] StandardListe
        {
            get
            {

                Action();

                return GetStandardListe().ToArray();
            }
        }

        /// <summary>
        /// Hilfsmethode für das initialisieren der Standardliste
        /// </summary>
        private void Action()
        {

            if (GetStandardListe() == null)
            {
                // 20210211 Die Liste nach Namen sortieren


                SetStandardListe(new Daten.Sprachen());
                GetStandardListe().AddRange((from s in this.Controller.HoleStandardListe()
                                             orderby s.Name
                                             select s).ToArray()
                                                         );

            }
        }

        /// <summary>
        /// Stellt sicher, dass die Namen der Sprachen
        /// der Oberflächen-Sprache entsprechen
        /// </summary>
        public virtual void Aktualisieren()
        {
            // Damit die Sprachen neu gelesen werden,
            // den Cache zurücksetzen
            SetStandardListe(null);
        }

        /// <summary>
        /// Ruft die aktuelle Anwendungssprache ab
        /// oder legt diese fest
        /// </summary>
        public Daten.Sprache Aktuell { get; set; } = null;

        /// <summary>
        /// Legt die aktuelle Sprache fest
        /// </summary>
        /// <param name="sprachCode">CultureInfo-Kürzel
        /// der festzulegenden Sprache</param>
        /// <remarks>Wird keine Sprache gefunden,
        /// wird die erste Sprache aus der
        /// Standardliste benutzt</remarks>
        public void Einstellen(string sprachCode)
        {
            // Damit das Feld initialisiert ist...
            var Sprachen = this.StandardListe;

            // Weil die Eigenschaft ein Array ist,
            // die Suchen - Methode aus dem Feld verwenden
            this.Aktuell = SprachenManager._StandardListe.Suchen(sprachCode);

            // Sollte keine Sprache gefunden werden,
            // die erste Sprache aus der Standardliste verwenden
            if (this.Aktuell == null && Sprachen.Length > 0)
            {
                this.Aktuell = Sprachen[0];
            }
        }
    }
}
