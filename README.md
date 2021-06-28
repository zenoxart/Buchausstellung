# Buchausstellungs Verwaltungs Software (BAVS)
* WIFI Linz 27.04.2021
* Daniel Kasper & Peter Humer

> Die Idee zu der Software entstand aus dem Lastenheft des 
> 3 Teiles des WIFI-Kurses:
> 3881 Software Developer in C# - Komplettausbildung
> Die Software entstand im Zeitrahmen von 27.04.2021 bis 29.06.2021 




# Datenbank-Skizze
ein Buch besitzt
+	Id
+	Titel
+	Author
+	Verlag_Id
+	Preis
+	Rab.Gr Rabattgruppe
+	Kat.Gr Kategoriegruppe
	
ein Benutzer besitzt
+	Id
+	Name
+	Anschrift
+	Telnummer
+	Typ		(Besteller oder Veranstallter)



eine Bestellung besitzt
+	Id
+	Bestellung_Nummer
+	Bestellung_hat_Bücher_Id
+	Benutzer_Id

eine Bestellung_hat_Bücher bsitzt
+	Id
+	Buch_Id
+	Anzahl

ein Verlag besitzt
+	Id
+	Name

eine Veranstaltung besitzt
+	DatumVon
+	DatumBis
+	Ort
+	Stadium		enum(Vorbereitung, Veranstaltung, Lieferung, Abholung)


# Masken
Anwendung

		// Vor der Veranstaltung
		Kategorieverwaltung
		Bücherverwaltung
		ErstelleVeranstaltung

		// Wärend der Veranstaltung
		Ausstellung
		Bestellung
		Bestellungenliste -> bearbeitbare Bestellung
		Ausstellungsabschluss

		// Lieferung nach der Veranstaltung 
		Lieferverwaltung
		
		// Abholung nach der Veranstaltung
		Abholungsverwaltung
		
# Fremdkomponenten
In diesem Projekt wurde das Framework PDFSharp zum erstellen von PDFs aus einer HTML-Seite benutzt. 

		// Wenn keine Datenbankverbindung aufgebaut werden kann oder kein Netz vorhanden ist
		Offlineanzeige
		





