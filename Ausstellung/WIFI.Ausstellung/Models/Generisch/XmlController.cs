namespace WIFI.Ausstellung.Models.Generisch
{
    /// <summary>
    /// Stellt einen typsicheren Dienst zum Schreiben
    /// und Lesen einer Datenliste im Xml-Format bereit
    /// Ermöglicht es auf den Datenbank-AppKontext zuzugreifen
    /// </summary>
    public class XmlController<T> : WIFI.Anwendung.ViewModelAppObjekt
    {
        /// <summary>
        /// Serialisiert die Daten der
        /// Liste in die angegebene Datei
        /// im Xml Format
        /// </summary>
        /// <param name="liste">Die zu speichernden Daten</param>
        /// <param name="inPfad">Die vollständige Pfadangabe der Xml Datei</param>
        /// <exception cref="System.Exception">Tritt auf, wenn das Serialisieren nicht funktioniert hat</exception>
        public void Speichern(T liste, string inPfad)
        {
            var Serialisierer = new System.Xml.Serialization.XmlSerializer(liste.GetType());

            using (var Schreiber = new System.IO.StreamWriter(inPfad))
            {
                Serialisierer.Serialize(Schreiber, liste);
            }
        }

        /// <summary>
        /// Gibt die deserialisierten Xml Daten
        /// aus der Datei zurück
        /// </summary>
        /// <param name="ausPfad">Die vollständige Pfadangabe der Xml Datei</param>
        /// <exception cref="System.Exception">Tritt auf, wenn das Deserialisieren nicht funktioniert hat</exception>
        public T Laden(string ausPfad)
        {
            var Serialisierer = new System.Xml.Serialization.XmlSerializer(typeof(T));

            using (var Leser = new System.IO.StreamReader(ausPfad))
            {
                return (T)Serialisierer.Deserialize(Leser);
            }
        }
    }
}
