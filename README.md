# Buchausstellung
Software zum Abwickeln einer Buchausstellung


# Pflichtenheft
Verwalterung der B�cher 
die B�cher besitzen Gruppen nachdenen man filtern kann

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
	Bestellung_hat_B�cher_Id
	Anzahl

eine Bestellung_hat_B�cher bsitzt
	Id
	Buch_Id
	Benutzer_Id

ein Verlag besitzt
	Id
	Name

eine Veranstallung besitzt
	DatumVon
	DatumBis
	Ort
	Stadium		enum(Verbereitung, Veranstaltung, Auswertung)


# Masken
Anmeldung
	+ Benutzer
		Ausstellung
		Warenkorb

	+ Veranstallter
		Veranstaltlungsverwaltung
			B�cherverwaltung

			// Nach der Veranstallung
			Bestellverwaltung
			Abholungsverwaltung
			Lieferverwaltung







