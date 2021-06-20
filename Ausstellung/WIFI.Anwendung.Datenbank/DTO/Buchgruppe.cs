﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WIFI.Anwendung.DTO
{

    /// <summary>
    /// Stellt die Daten einer 
    /// einzelnen Buchgruppe bereit.
    /// </summary>
    public class Buchgruppe : WIFI.Anwendung.Daten.DatenBasis
    {
        /// <summary>
        /// Internes Feld für die Eigenschaft
        /// </summary>
        private int _Id;

        /// <summary>
        /// Ruft den Indikator ab oder legt diesen fest
        /// </summary>
        public int ID
        {
            get { return this._Id; }
            set
            {
                this._Id = value;
                this.OnPropertyChanged();
            }
        }

        /// <summary>
        /// Internes Feld für die Eigenschaft
        /// </summary>
        private int _Gruppennummer;
        /// <summary>
        /// Ruft die Nummer der Gruppe ab oder legt diese fest
        /// </summary>
        public int Gruppennummer
        {
            get { return this._Gruppennummer; }
            set
            {
                this._Gruppennummer = value;
                this.OnPropertyChanged();
            }
        }

        /// <summary>
        /// Internes Feld für die Eigenschaft
        /// </summary>
        private string _Beschreibung;

        /// <summary>
        /// Ruft die Kurzbeschreibung der Gruppe ab oder legt diese fest
        /// </summary>
        public string Beschreibung
        {
            get { return this._Beschreibung; }
            set
            {
                this._Beschreibung = value;
                this.OnPropertyChanged();
            }
        }


    }

    /// <summary>
    /// Stellt eine Liste aller
    /// erfassten Buchgruppen bereit.
    /// </summary>
    public class Buchgruppen : System.Collections.ObjectModel.ObservableCollection<Buchgruppe>
    {

    }
}