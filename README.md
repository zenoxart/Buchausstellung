# Buchausstellung
Software zum Abwickeln einer Buchausstellung


# Pflichtenheft
Verwalterung der Bücher 
die Bücher besitzen Gruppen nachdenen man filtern kann

# Datenbank-Skizze
ein Buch besitzt
	Id
	Titel
	Author
	Verlag_Id
	Preis
	Rab.Gr Rabattgruppe
	Kat.Gr Kategoriegruppe
	
ein Benutzer besitzt
	Id
	Name
	Anschrift
	Telnummer
	Typ		(Besteller oder Veranstallter)



eine Bestellung besitzt
	Id
	Bestellung_hat_Bücher_Id
	Anzahl

eine Bestellung_hat_Bücher bsitzt
	Id
	Buch_Id
	Benutzer_Id

ein Verlag besitzt
	Id
	Name

eine Veranstaltung besitzt
	DatumVon
	DatumBis
	Ort
	Stadium		enum(Verbereitung, Veranstaltung, Auswertung)


# Masken
Anwendung

		// Vor der Veranstaltung
		Neue Veranstaltung
		Bücherverwaltung

		// Wärend der Veranstaltung
		Ausstellung
		Warenkorb

		// Nach der Veranstaltung
		Ausstellungsabschluss
		Lieferverwaltung
		Abholungsverwaltung
		







