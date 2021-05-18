using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WIFI.Ausstellung.ViewModels
{
    /// <summary>
    /// Stellt einen Dienst zum Verwalten
    /// der Anwendungspunkte bereit
    /// </summary>
    public class AufgabenManager : WIFI.Anwendung.ViewModelAppObjekt
    {
        /// <summary>
        /// Internes Feld für die Eigenschaft
        /// </summary>
        private Models.AufgabenXmlController _Controller = null;

        /// <summary>
        /// Ruft den Dienst zum Laden der
        /// Anwendungspunkte ab
        /// </summary>
        private Models.AufgabenXmlController Controller
        {
            get
            {
                if (this._Controller == null)
                {
                    this._Controller = this.AppKontext.Produziere<Models.AufgabenXmlController>();
                }

                return this._Controller;
            }
        }

        /// <summary>
        /// Internes Feld für die Eigenschaft
        /// </summary>
        /// <remarks>Da sich die Aufgaben innerhalb einer Anwendung nicht ändern,
        /// wird hier statisch gearbeitet</remarks>
        private static Models.Aufgaben _Liste = null;

        /// <summary>
        /// Ruft die aktuell unterstützten
        /// Anwendungspunkte ab.
        /// </summary>
        public Models.Aufgaben Liste
        {
            get
            {
                if (AufgabenManager._Liste == null
                    // Die Daten auch holen, wenn sie noch nicht bereitstehen
                    // Das passiert, wenn das Holen länger dauert und die
                    // Benutzer bereits ein neues Fenster geöffnet haben
                    || AufgabenManager._Liste[0].Name == Properties.Texte.DatenHolen)
                {
                    // Hier wäre die Oberfläche während des Holens blockiert
                    //this._Liste = this.Controller.HoleAusRessourcen();

                    // Damit Benutzer sehen, dass etwas passiert...
                    AufgabenManager._Liste = new Models.Aufgaben
                    {
                        new Models.Aufgabe {
                            Name=Properties.Texte.DatenHolen,
                            Symbol="6"
                        }
                    };


                    this.InitialisiereAufgabenAsync();



                }

                return AufgabenManager._Liste;
            }
            protected set
            {
                AufgabenManager._Liste = value;
                this.OnPropertyChanged();
            }
        }

        public AufgabenManager()
        {

            // Starte den Task 
            AufgabenSektionAktuellHalten();
        }


        /// <summary>
        /// Ändert die Aufgaben zu denen in der angeführten XML-Datei
        /// </summary>
        /// <param name="xmlPfad"></param>
        public void SektionLaden(string xmlPfad)
        {
            this.AppKontext.AktuelleAufgabenSektion = xmlPfad;
            this.InitialisiereAufgabenAsync();

           
            this.AktiverViewer = null;
            this.OnPropertyChanged();

            this.AppKontext.Protokoll.Eintragen($"Aufgabensektion wurde auf {xmlPfad} geändert. ");
        }



        /// <summary>
        /// Ruft in einem gewissen Interval die Datenbank und zeigt die Sektion an, welche in der Datenbank vorgegeben ist
        /// </summary>
        public void AufgabenSektionAktuellHalten()
        {

            System.Threading.Tasks.Task.Run(() =>
            {
                while (true)
                {
                    // Abfrage noch nicht Threadsicher
                    // desshalb

                    VeranstaltungsSektionLaden();


                    void VeranstaltungsSektionLaden()
                    {
                        // Frage die Datenbank ab
                        WIFI.Anwendung.DTO.AusstellungsstadiumTyp response =
                            this.AppKontext.DBControllerManager.VeranstaltungsController.VeranstaltungsStadium();

                        // Entscheide aufgrund des Status, welche XML-Datei geladen werden soll
                        switch (response)
                        {
                            case WIFI.Anwendung.DTO.AusstellungsstadiumTyp.Vorbereitung:
                                SektionLaden(WIFI.Ausstellung.Properties.Resources.Vorbereitung);
                                break;

                            case WIFI.Anwendung.DTO.AusstellungsstadiumTyp.Veranstaltung:
                                SektionLaden(WIFI.Ausstellung.Properties.Resources.Veranstaltung);
                                break;

                            case WIFI.Anwendung.DTO.AusstellungsstadiumTyp.Lieferung:
                                SektionLaden(WIFI.Ausstellung.Properties.Resources.Lieferung);
                                break;

                            case WIFI.Anwendung.DTO.AusstellungsstadiumTyp.Abholung:
                                SektionLaden(WIFI.Ausstellung.Properties.Resources.Abholung);
                                break;
                            default:
                                SektionLaden(WIFI.Ausstellung.Properties.Resources.Fehler);
                                break;
                        }

                        System.Threading.Thread.Sleep(3000);
                    }
                }

            });
        }






        /// <summary>
        /// Füllt die Liste mit den Aufgaben
        /// asynchron in einem eigenen Thread
        /// </summary>
        protected virtual async void InitialisiereAufgabenAsync()
        {
            // Damit die Thread-Methode nicht mehrmals gestartet wird
            if (this.InitialisiereAufgabenLäuft)
            {
                return;
            }

            await System.Threading.Tasks.Task.Run(
                () =>
                {
                    this.InitialisiereAufgabenLäuft = true;
                    // SO NIE, nicht mit dem Feld!!!
                    //this._Liste = this.Controller.HoleAusRessourcen();
                    // a) Damit WPF mitbekommt, dass sich die Liste
                    //    geändert hat, wird PropertyChanged benötigt
                    // b) Weil kein Thread in die Daten von einem
                    //    anderen Thread greifen darf, nicht hier
                    //    das PropertyChanged. Das muss in der Eigenschaft sein!

                    this.StartProtokollieren();

                    var DatenDerListe = this.Controller.HoleAusRessourcen();
                    if (DatenDerListe != null)
                    {
                        this.Liste = DatenDerListe;
                    }

                    this.EndeProtokollieren();
                    this.InitialisiereAufgabenLäuft = false;

                    //// Prüfe für die Threadsicherheit ,
                    //// ob ein Dispatcher vorhanden ist
                    //if (this.AppKontext.Protokoll.Dispatcher != null)
                    //{
                    //    // Die Beschreibung der InvokeAsync Methode holen
                    //    // Generisches Laden eines  Typen und von dem dann eine MethodInfo generiert wird
                    //    System.Reflection.MethodInfo InvokeMethode = this.AppKontext.Protokoll.Dispatcher.GetType()
                    //        .GetMethod(
                    //        "InvokeAsync",
                    //        new Type[] { typeof(System.Action) })
                    //        ;

                    //    // die Methode ausführen,
                    //    // d.h. den Dispatcher bitten,
                    //    // den EIntrag threadsicher zu veranlassen
                    //    if (InvokeMethode != null)
                    //    {
                    //        InvokeMethode.Invoke(
                    //        this.AppKontext.Protokoll.Dispatcher,
                    //        new object[] {
                    //    new System.Action(() => AktuelleAufgabeSetzen())
                    //        });
                    //    }
                    //    else
                    //    {
                    //        // Keine Threadsicherheit
                    //        // Weil das Objekt in der Dispatcher Eigenschaft
                    //        // kein InvokeAsync hat
                    //        AktuelleAufgabeSetzen();
                    //    }

                    //}
                    //else
                    //{
                    //    // Keine Threadsicherheit
                    //    // Weil kein Dispatcher eingestellt ist
                    //    AktuelleAufgabeSetzen();
                    //}

                    //void AktuelleAufgabeSetzen()
                    //{
                    //    // Nachdem die Liste abgerufen wurde,
                    //    // die zuletzt benutzte Aufgabe einstellen

                    //    // Die Einstellung aus der Konfiguration
                    //    // in einer Variable abkürzen und beim
                    //    // Benutzen sauber prüfen, ob der Wert
                    //    // im gültigen Bereich. Spezialbenutzer
                    //    // könnten herumgepfuscht haben
                    //    var i = Properties.Settings.Default.IndexAktuelleAufgabe;

                    //    try
                    //    {
                    //        // 20210506 Änderung -> Kasper,
                    //        // Es muss zuerst gefragt werden ob die Liste ungleich
                    //        // NULL ist bevor auf den Count zugegriffen werden kann,
                    //        // um zu verhintern, dass eine Null-Reference entsteht 
                    //        if (this.Liste != null && i >= 0 && i < this.Liste.Count)
                    //        {
                    //            this.AktuelleAufgabe = this.Liste[i];
                    //        }
                    //    }
                    //    catch (Exception)
                    //    {
                    //        Console.WriteLine("Ein Fehler ist aufgetaucht");
                    //    }

                    //}
                }
                );



        }

        /// <summary>
        /// Internes Feld für die Eigenschaft
        /// </summary>
        private Models.Aufgabe _AktuelleAufgabe = null;

        /// <summary>
        /// Ruft den im Cache der Anwendung benutzten
        /// Schlüssel für die aktuelle Aufgabe ab
        /// </summary>
        private string SchlüsselAktuelleAufgabe => this.GetType().FullName + ".AktuelleAufgabe";

        /// <summary>
        /// Ruft den aktuellen Anwendungspunkt ab, oder legt diesen fest
        /// </summary>
        public Models.Aufgabe AktuelleAufgabe
        {
            get
            {
                // Wenn die private Eigenschaft nicht null ist
                if (this._AktuelleAufgabe == null)
                {

                    // Suche im Cache nach dem Schlüssel STRING
                    if (this.AppKontext.Cache.ContainsKey(this.SchlüsselAktuelleAufgabe))
                    {
                        this._AktuelleAufgabe = this.AppKontext.Cache[this.SchlüsselAktuelleAufgabe]
                            as Models.Aufgabe;
                    }

                }

                return this._AktuelleAufgabe;
            }
            set
            {

                if (this._AktuelleAufgabe != value)
                {
                    this._AktuelleAufgabe = value;
                    OnPropertyChanged();

                    // Damit ist der aktive Viewer ungültig
                    this.AppKontext.Cache[
                        this.SchlüsselAktuelleAufgabe
                        ] = this.AktuelleAufgabe;





                    // Prüfe für die Threadsicherheit ,
                    // ob ein Dispatcher vorhanden ist
                    if (this.AppKontext.Protokoll.Dispatcher != null)
                    {
                        // Die Beschreibung der InvokeAsync Methode holen
                        // Generisches Laden eines  Typen und von dem dann eine MethodInfo generiert wird
                        System.Reflection.MethodInfo InvokeMethode = this.AppKontext.Protokoll.Dispatcher.GetType()
                            .GetMethod(
                            "InvokeAsync",
                            new Type[] { typeof(System.Action) })
                            ;

                        // die Methode ausführen,
                        // d.h. den Dispatcher bitten,
                        // den EIntrag threadsicher zu veranlassen
                        if (InvokeMethode != null)
                        {
                            InvokeMethode.Invoke(
                            this.AppKontext.Protokoll.Dispatcher,
                            new object[] {
                        new System.Action(() => SpeichernDerAktuellenAufgabe())
                            });
                        }
                        else
                        {
                            // Keine Threadsicherheit
                            // Weil das Objekt in der Dispatcher Eigenschaft
                            // kein InvokeAsync hat
                            SpeichernDerAktuellenAufgabe();
                        }

                    }
                    else
                    {
                        // Keine Threadsicherheit
                        // Weil kein Dispatcher eingestellt ist
                        SpeichernDerAktuellenAufgabe();
                    }



                    void SpeichernDerAktuellenAufgabe()
                    {
                        // Außerdem die Aufgabe in der Konfiguration
                        // hinterlegen, damit diese bei einem Neustart
                        // wieder ausgewählt werden kann
                        // Index aus den Ressourcen geladen
                        // 20210506 Änderung -> Kasper ,
                        // nur wenn die Aktuelle Aufgabe in der Liste enthalten
                        // ist und diese auch nicht null ist, bekomme den
                        // Index der Aufgabe

                        if (this.Liste != null)
                        {
                            if (this.Liste.Contains(this._AktuelleAufgabe))
                            {
                                Properties.Settings.Default.IndexAktuelleAufgabe
                                = this.Liste.IndexOf(this._AktuelleAufgabe);
                            }
                        }



                        this.AktiverViewer = null;
                    }

                }
            }
        }


        /// <summary>
        /// Internes Feld für die Eigenschaft
        /// </summary>
        private object _AktiverViewer;

        /// <summary>
        /// Ruft den Arbeitsbereich der
        /// aktuellen Aufgabe ab
        /// </summary>
        public object AktiverViewer
        {
            get
            {
                if (this._AktiverViewer == null)
                {
                    this.InitialisiereAktivenViewer();

                }

                return this._AktiverViewer;
            }
            protected set
            {
                this._AktiverViewer = value;
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// Internes Feld für die Eigenschaft
        /// </summary>
        private System.Collections.Hashtable _VorhandeneViewer = null;

        /// <summary>
        /// Ruft den Cache mit bereits initialisierten
        /// Aufgaben-Viewer ab
        /// </summary>
        protected System.Collections.Hashtable VorhandeneViewer
        {
            get
            {
                if (this._VorhandeneViewer == null)
                {
                    this._VorhandeneViewer = new System.Collections.Hashtable(this.Liste.Count);
                    this.AppKontext.Protokoll.Eintragen(
                        $"{this} hat den Cache für die Viewer initialisiert...",
                        WIFI.Anwendung.Daten.ProtokollEintragTyp.NeueInstanz);
                }

                return this._VorhandeneViewer;
            }
        }

        /// <summary>
        /// Ruft einen Wahrheitswert ab,
        /// ob das Initialisieren der Aufgaben 
        /// aktuell läuft oder nicht
        /// </summary>
        private bool InitialisiereAufgabenLäuft { get; set; }

        /// <summary>
        /// Für die aktuelle Aufgabe den
        /// Arbeitsbereich initialisieren
        /// und in der AktiverViewer Eigenschaft
        /// hinterlegen
        /// </summary><remarks>
        /// Bereits produzierte Viewer werden gecachet</remarks>
        protected virtual void InitialisiereAktivenViewer()
        {

            // Das Initialisieren ist nur sinnvoll,
            // wenn a) eine AktuelleAufgabe vorhanden ist
            // und b) die Liste mit den Aufgaben abgerufen wurde

            if (this.AktuelleAufgabe != null &&
                this.AktuelleAufgabe.Name != Properties.Texte.DatenHolen)
            {


                this.StartProtokollieren();

                object Viewer = null;

                // Prüfen, ob der Viewer bereits vorhanden ist
                if (this.VorhandeneViewer.ContainsKey(this.AktuelleAufgabe.ViewerName))
                {
                    Viewer = this.VorhandeneViewer[this.AktuelleAufgabe.ViewerName];
                }

                // Falls nicht, erstellen und merken
                else
                {

                    try
                    {
                        Viewer = System.Activator.CreateInstance(
                            Type.GetType(this.AktuelleAufgabe.ViewerName));
                    }
                    catch (Exception ex)
                    {
                        Viewer = new object();

                        var FehlerBeschreibung =
                            new System.Exception(
                                $"Keine {this.AktuelleAufgabe.ViewerName} Klasse gefunden!",
                                ex);

                        this.OnFehlerAufgetreten(new WIFI.Anwendung.FehlerAufgetretenEventArgs(FehlerBeschreibung));
                    }
                    this.VorhandeneViewer.Add(this.AktuelleAufgabe.ViewerName, Viewer);

                }
                this.AktiverViewer = Viewer;
                this.EndeProtokollieren();
            }


        }


        /// <summary>
        /// Ruft das Design aus den Ressourcen ab, 
        /// oder setzt dieses
        /// </summary>
        public bool DunklesDesign
        {
            get { return Properties.Settings.Default.DunklesDesign; }
            set
            {
                Properties.Settings.Default.DunklesDesign = value;
                OnPropertyChanged();
            }
        }



    }
}
