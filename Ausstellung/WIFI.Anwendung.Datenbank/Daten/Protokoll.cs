using System;

namespace WIFI.Anwendung.Daten
{
    /// <summary>
    /// Beschreibt die Art eines Protokolleintrags
    /// </summary>
    public enum ProtokollEintragTyp
    {
        /// <summary>
        /// Beschreibt, dass ein neues Objekt initialisiert wurde
        /// </summary>
        NeueInstanz,
        /// <summary>
        /// Beschreibt einen Hinweis
        /// </summary>
        Normal,
        /// <summary>
        /// Kennzeichnet einen Hinweis, der zu beachten ist
        /// </summary>
        Warnung,
        /// <summary>
        /// Kennzeichnet einen Eintrag wegen einer Ausnahme
        /// </summary>
        Fehler
    }

    /// <summary>
    /// Stellt eine typsichere Auflistung von
    /// Protokolleinträgen bereit, die für die
    /// WPF Datenbindung benutzt werden kann
    /// </summary>
    public class ProtokollEinträge
        : System.Collections.ObjectModel.ObservableCollection<ProtokollEintrag>
    {

    }

    /// <summary>
    /// Stellt die Daten eines
    /// Protolleintrags bereit
    /// </summary>
    public class ProtokollEintrag : DatenBasis
    {
        /// <summary>
        /// Ruft den Zeitpunkt des Erstellens
        /// ab oder legt diese fest
        /// </summary>
        public DateTime Zeitstempel { get; set; } = DateTime.Now;

        /// <summary>
        /// Ruft die Art des Eintrags ab,
        /// oder legt diese fest
        /// </summary>
        [InToString(2)]
        public ProtokollEintragTyp Typ { get; set; } = ProtokollEintragTyp.Normal;

        /// <summary>
        /// Ruft die Information von diesem Eintrag
        /// ab oder legt diese fest
        /// </summary>
        [InToString(1)]
        public string Text { get; set; } = string.Empty;
    }
}
