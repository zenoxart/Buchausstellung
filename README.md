# Buchausstellung
Software zum Abwickeln einer Buchausstellung


# Pflichtenheft
Verwalterung der Bücher 
die Bücher besitzen Gruppen nachdenen man filtern kann

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
		Neue Veranstaltung
		Bücherverwaltung

		// Wärend der Veranstaltung
		Ausstellung
		Warenkorb

		// Nach der Veranstaltung
		Ausstellungsabschluss
		Lieferverwaltung
		Abholungsverwaltung
		
		// Wenn keine Datenbankverbindung aufgebaut werden kann
		Offlineanzeige
		



# Aktuell Offene Arbeit!!!

+ HoleBestellungen(); Datenbank-Controller
  * Priorität: WICHTIG
  * Status   : [offen]
  * Probleme :
  
+ Bearbeiten und Aktualisieren einer Bestellung
  * Priorität: WICHTIG
  * Status   : [offen]
  * Probleme :
  
+ Lieferung Aktualisieren
  * Priorität: WICHTIG
  * Status   : [offen]
  * Probleme :
  
+ Abholung Laden & Aktualisieren
  * Priorität: WICHTIG
  * Status   : [offen]
  * Probleme :
  
+ PDF's testen und Pfade in den Einstellungen nutzen
  * Priorität: WICHTIG
  * Status   : [offen]
  * Probleme :
  
+ Bestellungliste die Kategorie aus Zeile und Header entfernen
  * Priorität: WICHTIG
  * Status   : [offen]
  * Probleme :
  
+ Bild für Anwendungsstart
  * Priorität: MEDIUM
  * Status   : [offen]
  * Probleme :
  
# RESTZEIT:	Hell-Dunkel-Design für die Listen bei folgenden Views
+	Kategorieverwaltung
+	Buchverwaltung
+	Ausstellung
+	Bestellung
+	Bestellungen
+	Lieferung
+	Abholung




