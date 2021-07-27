namespace WIFI.Anwendung
{
    /// <summary>
    /// Stellt die Anwendungsinfrastruktur bereit
    /// </summary>
    public interface IAppKontext
    {
        /// <summary>
        /// Ruft die Anwendungsinfrastruktur 
        /// ab oder legt diese fest
        /// </summary>
        AppKontext AppKontext { get; set; }
    }
}
