using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WIFI.Ausstellung.ViewModels
{
    /// <summary>
    /// Stellt das Haupt-ViewModel für
    /// die WIFI C# Teil 2 MVVM Anwendung bereit.
    public class Anwendung : WIFI.Anwendung.ViewModelAppObjekt
    {
        #region Hauptfenster
        /// <summary>
        /// Ruft den Typ der View ab,
        /// die beim Starten benutzt wurde,
        /// oder legt diese fest
        /// </summary>
        /// <remarks>Hier wird die implizite
        /// Eigenschaften Deklaration benutzt,
        /// wo sich der Compiler das Feld verwaltet.
        /// Hier möglich, weil nix gemacht wird,
        /// außer die Daten zu merken</remarks>
        protected Type HauptView { get; set; }

        /// <summary>
        /// Initialisert die Hauptview und zeigt diese an
        /// </summary>
        /// <typeparam name="T">Ein System.Windows.Window,
        /// das als Hauptview benutzt werden soll</typeparam>
        public void Starten<T>() where T : System.Windows.Window, new()
        {
            // Für spätere neue Fenster den Typ merken
            this.HauptView = typeof(T);

            var View = new T();

            this.AppKontext.Protokoll.IstBeschäftigtSynchronisieren(this);

            // Dem Fenster mitteilen, dass
            // es von uns kontrolliert wird...
            View.DataContext = this;


            // Ab jetzt kann das Fenster mit {Binding} arbeiten

            this.FensterInitialisieren(View);

            System.Windows.Application.Current.MainWindow = View;
            System.Windows.Application.Current.Run(View);

        }

        /// <summary>
        /// Konfiguriert das WPF Fenster, z. B. wird
        /// die letzte Fensterposition wiederhergestellt
        /// </summary>
        /// <param name="fenster">Verweis auf das WPF Fenster,
        /// das initialisiert werden soll</param>
        protected virtual void FensterInitialisieren(System.Windows.Window fenster)
        {

            // Weil Xml ein anderes Sprachsystem als .Net benutzt,
            // den .Net Code in den alten Code übersetzen,
            // damit z.B. Datumsangaben richtig formatiert werden
            fenster.Language = System.Windows.Markup.XmlLanguage.GetLanguage(
                System.Globalization.CultureInfo.CurrentCulture.IetfLanguageTag);

            // Weil in MVVM in der Oberfläche kaum
            // etwas benannt ist, einen Namen berechnen
            fenster.Name = fenster.GetType().Name;
            // mehrere Fenster des gleichen Typs unterscheiden
            // a) die Namen aller Fenster sammeln
            //    (weil die WPF Windows-Liste kein Contains() bereitstellt)
            var NamenOffenerFenster
                = new System.Collections.ArrayList(
                    System.Windows.Application.Current.Windows.Count);

            foreach (System.Windows.Window w in System.Windows.Application.Current.Windows)
            {
                NamenOffenerFenster.Add(w.Name);
            }

            // b) Die Fenster durchnummerieren
            //    und freie Nummern wieder benutzen
            int FensterNummer = 1;
            while (NamenOffenerFenster.Contains(fenster.Name + FensterNummer))
            {
                FensterNummer += 1;
            }

            fenster.Name += FensterNummer;

            // Fensterposition über die Infrastruktur wiederherstellen
            var AltePosition = this.AppKontext.Fenster.Abrufen(fenster.Name);

            if (AltePosition != null)
            {
                // Kein neuer Benutzer weil eine alte Position vorhanden ist

                fenster.Left = AltePosition.Links ?? fenster.Left;
                fenster.Top = AltePosition.Oben ?? fenster.Top;
                fenster.Width = AltePosition.Breite ?? fenster.Width;
                fenster.Height = AltePosition.Höhe ?? fenster.Height;

                // Als Zustand wird nur "Maximiert" wiederhergestellt,
                // alles andere ist "Normal"
                var Zustand = (System.Windows.WindowState)AltePosition.Zustand;

                fenster.WindowState = Zustand == System.Windows.WindowState.Maximized ?
                    System.Windows.WindowState.Maximized :
                    System.Windows.WindowState.Normal;
            }


            // Einen anonymen Ereignisbehandler anhängen,
            // damit beim Schließen die Fensterposition 
            // gespeichert wird
            fenster.Closing += (sender, e) =>
            {
                // Ein Positionsobjekt initialisieren
                var Position = new WIFI.Anwendung.Daten.Fenster();

                // Damit die Position wieder gefunden wird
                Position.Name = fenster.Name;

                // Auf alle Fällen den Zustand merken
                Position.Zustand = (int)fenster.WindowState;

                // die Position und Größe nur, wenn
                // es ein normales Fenster ist
                if (fenster.WindowState == System.Windows.WindowState.Normal)
                {
                    // Die Windows Forms war nur Integer
                    // WPF ist jetzt double
                    // Integer geht bis 2 Mrd
                    // es gibt keinen Schirm, der
                    // 2 Mrd Pixel in der Breite
                    // Deshalb wird Double auf Integer gekürzt
                    Position.Links = (int)fenster.Left;
                    Position.Oben = (int)fenster.Top;
                    Position.Breite = (int)fenster.Width;
                    Position.Höhe = (int)fenster.Height;
                }

                // Das Positionsobjekt dem Fenstermanager übergeben
                this.AppKontext.Fenster.Hinterlegen(Position);

                // Damit der Garbage Collector
                // das Fenster freigeben kann,
                // das aktuelle ViewModel aus 
                // dem DataContext entfernen
                fenster.DataContext = null;

            };
        }

        #endregion

        #region Fenster schließen
        /// <summary>
        /// Internes Feld für die Eigenschaft
        /// </summary>
        private WIFI.Anwendung.Befehl _FensterSchließen = null;

        /// <summary>
        /// Ruft den Befehl ab, mit dem
        /// alle (anderen) Anwendungsfenster geschlossen werden
        /// </summary>
        /// <remarks>Befindet sich im Befehlsparameter,
        /// ein Fenster, wird dieses nicht geschlossen!</remarks>
        public WIFI.Anwendung.Befehl FensterSchließen
        {
            get
            {
                if (this._FensterSchließen == null)
                {
                    // Den Befehl mit anoymen Methoden initialisieren

                    this._FensterSchließen = new WIFI.Anwendung.Befehl(

                        // anoyme Methode zum Schließen
                        p =>
                        {

                            this.StartProtokollieren();
                            // Voreinstellung, wenn alle anderen Fenster
                            // Fenster geschlossen werden
                            var Antwort = System.Windows.MessageBoxResult.Yes;

                            // Falls alle Fenster geschlossen, fragen...
                            if (p == null)
                            {
                                Antwort = System.Windows.MessageBox.Show(
                                    Properties.Texte.FensterSchließenFrage,
                                    "BAVS (Buchausstellungs Verwaltungs Software)",
                                    System.Windows.MessageBoxButton.YesNo,
                                    System.Windows.MessageBoxImage.Question,
                                    // 20210209 Hr. Hannouf
                                    //      Die Standardantwort auf Nein festgelegt
                                    System.Windows.MessageBoxResult.No);
                            }

                            if (Antwort == System.Windows.MessageBoxResult.Yes)
                            {
                                foreach (System.Windows.Window w in System.Windows.Application.Current.Windows)
                                {
                                    if (w != p)
                                    {
                                        w.Close();
                                    }
                                }
                            }

                            this.EndeProtokollieren();
                        },
                        // anoyme Methoen zum Prüfen, ob der Befehl erlaubt ist
                        p => System.Windows.Application.Current.Windows.Count > 1
                        );

                }

                return this._FensterSchließen;
            }
        }

        #endregion

        #region Neues Fenster öffnen
        /// <summary>
        /// Internes Feld für die Eigenschaft
        /// </summary>
        private WIFI.Anwendung.Befehl _NeuesFenster = null;

        /// <summary>
        /// Ruft den Befehl zum Öffnen
        /// eines neuen Anwendungsfensters ab
        /// </summary>
        public WIFI.Anwendung.Befehl NeuesFenster
        {
            get
            {
                if (this._NeuesFenster == null)
                {
                    // Den Befehl mit einer benannten Methode initialisiseren
                    this._NeuesFenster
                        = new WIFI.Anwendung.Befehl(this.NeuesFensterErstellen);
                }

                return this._NeuesFenster;
            }
        }


        /// <summary>
        /// Erzeugt ein neues Fenster
        /// der Haupt-View
        /// </summary>
        /// <param name="daten">Daten der Datenbindung</param>
        protected virtual void NeuesFensterErstellen(object daten)
        {
            this.StartProtokollieren();

            // Mit dem HauptView-Typ ein neues Fenster machen
            var Fenster = System.Activator.CreateInstance(this.HauptView)
                as System.Windows.Window;

            // Damit die Fenster "unabhängig" sind,
            // ein neues ViewModel dafür
            var NeuesVM = this.AppKontext.Produziere<Anwendung>();
            NeuesVM.HauptView = this.HauptView;
            Fenster.DataContext = NeuesVM;


            // Das neue Fenster initialisieren (Fensterposition,...)
            this.FensterInitialisieren(Fenster);

            // Das Fenster anzeigen
            Fenster.Show();

            this.EndeProtokollieren();
        }
        #endregion

        #region Einstellungsfenster
        /// <summary>
        /// Internes Feld für die Eigenschaft
        /// </summary>
        private WIFI.Anwendung.Befehl _Einstellungen = null;

        public WIFI.Anwendung.Befehl Einstellungen
        {
            get
            {
                if (this._Einstellungen == null)
                {
                    // Den Befehl mit einer benannten Methode initialisiseren
                    this._Einstellungen
                        = new WIFI.Anwendung.Befehl(executeMethode: this.EinstellungenÖffnen, canExecuteMethode: p => this.AppKontext.OffeneEinstellungsFenster < 1);
                }

                return this._Einstellungen;
            }
        }

        /// <summary>
        /// Öffnet ein Einstellungsfenster mit dem gebundenen AppKontext
        /// </summary>
        /// <param name="daten"></param>
        protected virtual void EinstellungenÖffnen(object daten)
        {
            this.AppKontext.Protokoll.Eintragen("Ein neues Einstellungsfenster wurde produziert...");


            this.AppKontext.OffeneEinstellungsFenster++;

            // Dem Fenster mitteilen, dass
            // es von uns kontrolliert wird...
            var View = new Views.Einstellungsfenster();

            // Bindet den AppKontext mit dem DataContext
            View.DataContext = this;



            var dialogresult = View.ShowDialog();
            // Fenster öffnen
            if (dialogresult.HasValue)
            {
                if (!dialogresult.Value)
                {
                    this.AppKontext.OffeneEinstellungsFenster--;
                }
            }
        }
        #endregion

        #region Dunkles Design

        /// <summary>
        /// Ruft einen Wahrheitswert ab, oder legt fest
        /// ob die Anwendung das dunkle Design 
        /// benutzen soll oder nicht
        /// </summary>
        /// 20210406
        /// Weil dieses ViewModel nicht immer im DataContext ist,
        /// wurde mittlerweile dem LottoManager, d.h. der Eigenschaft Lotto,
        /// ebenfalls DunklesDesign beigebracht. Sollte ein LottoManager
        /// bereits existieren, eine Änderung der Einstellung
        /// an den LottoManager weitergeben
        public bool DunklesDesign
        {
            get
            {
                return Properties.Settings.Default.DunklesDesign;
            }

            set
            {
                // Falls die Binärentscheidung vorhanden ist,
                // wird nur im aktuellen Fenster die Einstellung 
                // an die Lotto View weitergegeben. Damit die Änderung
                // in allen offenen Fenstern zieht, ohne dieser Entscheidung
                //if (value != Properties.Settings.Default.DunklesDesign)

                Properties.Settings.Default.DunklesDesign = value;



                // Weil die Anwendung mehere Fenster haben kann,
                // egal ob der Wert geändert wurde oder nicht...
                this.OnPropertyChanged();
            }
        }

        /// <summary>
        /// Internes Feld für die Eigenschaft
        /// </summary>
        private WIFI.Anwendung.Befehl _DesignUmschalten = null;

        /// <summary>
        /// Ruft den Befehl ab, mit dem das Dunkle Design 
        /// ein- und ausgeschaltet werden kann
        /// </summary>
        public WIFI.Anwendung.Befehl DesignUmschalten
        {
            get
            {
                if (this._DesignUmschalten == null)
                {
                    // Der Befehl soll immer zulässig sein,
                    // egal ob mehrere Fenster geöffnet sind
                    this._DesignUmschalten = new WIFI.Anwendung.Befehl(
                        p =>
                        {
                            this.DunklesDesign = !this.DunklesDesign;

                            // Allen Fenster mitteilen
                            foreach (System.Windows.Window window in System.Windows.Application.Current.Windows)
                            {
                                var VM = window.DataContext as WIFI.Ausstellung.ViewModels.Anwendung;

                                if (VM != null && VM != this)
                                {
                                    VM.DunklesDesign = this.DunklesDesign;
                                }
                            }

                            this.AppKontext.Protokoll.Eintragen($"{this} hat das Dunkle Design {(this.DunklesDesign ? "aktiviert" : "deaktiviert")}");
                        }
                        );
                }
                return this._DesignUmschalten;
            }
        }
        #endregion

        #region Aufgaben
        /// <summary>
        /// Internes Feld für die Eigenschaft
        /// </summary>
        private ViewModels.AufgabenManager _Aufgaben = null;

        /// <summary>
        /// Ruft die aktuellen Aufgaben ab oder legt diese fest
        /// </summary>
        public ViewModels.AufgabenManager Aufgaben
        {
            get
            {
                if (this._Aufgaben == null)
                {
                    this._Aufgaben = this.AppKontext.Produziere<ViewModels.AufgabenManager>();
                    this._Aufgaben.DunklesDesign = this.DunklesDesign;
                }

                return _Aufgaben;
            }
            set { _Aufgaben = value; }
        }

        #endregion
        /// <summary>
        /// Internes Feld für die Eigenschaft
        /// </summary>
        private ViewModels.AusstellungsManager _Ausstellung = null;

        /// <summary>
        /// Ruft die aktuelle Ausstellung ab oder legt diese fest
        /// </summary>
        public ViewModels.AusstellungsManager Ausstellung
        {
            get
            {
                if (this._Ausstellung == null)
                {
                    this._Ausstellung = this.AppKontext.Produziere<ViewModels.AusstellungsManager>();
                }
                return this._Ausstellung;
            }
            set { this._Ausstellung = value; }
        }

        /// <summary>
        /// Internes Feld für die Eigenschaft
        /// </summary>
        private ViewModels.VeranstaltungsManager _Veranstaltung;

        /// <summary>
        /// Ruft die Aktuelle Veranstaltung ab oder legt diese fest
        /// </summary>
        public ViewModels.VeranstaltungsManager Veranstaltung
        {
            get
            {
                if (this._Veranstaltung == null)
                {
                    this._Veranstaltung = this.AppKontext.Produziere<ViewModels.VeranstaltungsManager>();
                }
                return this._Veranstaltung;
            }
            set { this._Veranstaltung = value; }
        }

        /// <summary>
        /// Internes Feld für die Eigenschaft
        /// </summary>
        private ViewModels.LieferungsManager _Lieferung = null;

        /// <summary>
        /// Ruft die Lieferungsverwaltung auf oder legt diese fest
        /// </summary>
        public ViewModels.LieferungsManager Lieferung
        {
            get
            {
                if (this._Lieferung == null)
                {
                    this._Lieferung = this.AppKontext.Produziere<ViewModels.LieferungsManager>();
                }
                return this._Lieferung;
            }
            set { this._Lieferung = value; }
        }

        /// <summary>
        /// Internes Feld für die Eigenschaft
        /// </summary>
        private bool _ZeigeStart = true;

        /// <summary>
        /// Gibt einen Wahrheitswert an ob der Startscreen angezeigt werden soll oder nicht oder setzt ihn
        /// </summary>
        public bool ZeigeStart
        {
            get { return this._ZeigeStart; }
            set
            {
                // Wenn es Falsch ist soll der Arbeitsbereich gezeigt werden, sonst nicht
                if (value == false)
                {
                    ZeigeArbeitsbereich = true;
                }
                else
                {
                    ZeigeArbeitsbereich = false;
                }
                this._ZeigeStart = value;
                this.OnPropertyChanged();
            }
        }

        /// <summary>
        /// Internes Feld für die Eigenschaft
        /// </summary>
        private bool _ZeigeArbeitsbereich = false;
        /// <summary>
        /// Gibt einen Wahrheitswert an ob der Arbeitsbereich angezeigt werden soll oder nicht oder setzt ihn
        /// </summary>
        public bool ZeigeArbeitsbereich
        {
            get { return this._ZeigeArbeitsbereich; }
            set
            {
                this._ZeigeArbeitsbereich = value;

                this.OnPropertyChanged();
            }
        }

        /// <summary>
        /// Internes Feld für die Eigenschaft
        /// </summary>
        private WIFI.Anwendung.Befehl _StarteSoftware;

        /// <summary>
        /// Stellt eine Methode zum Starten des Arbeitsbereiches
        /// </summary>
        public WIFI.Anwendung.Befehl StarteSoftware
        {
            get
            {

                this._StarteSoftware = new WIFI.Anwendung.Befehl(
                    p =>
                    {

                        // prüfe ob Veranstaltung existiert ansonst

                        // erstelle Veranstaltung


                        this.ZeigeStart = false;


                    }
                );

                return this._StarteSoftware;
            }
            set
            {
                this._StarteSoftware = value;
                this.OnPropertyChanged();
            }
        }

        /// <summary>
        /// Internes Feld für die Eigenschaft
        /// </summary>
        private WIFI.Ausstellung.ViewModels.AbholungsManager _Abholung;

        /// <summary>
        /// Ruft die Abholungsverwaltung ab oder legt diese fest
        /// </summary>
        public WIFI.Ausstellung.ViewModels.AbholungsManager Abholung
        {
            get
            {
                if (this._Abholung == null)
                {
                    this._Abholung = this.AppKontext.Produziere<WIFI.Ausstellung.ViewModels.AbholungsManager>();
                }

                return this._Abholung;
            }
            set { this._Abholung = value; }
        }


    }
}
