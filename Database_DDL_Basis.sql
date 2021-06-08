#################################################
#					Datenbank
#				-----------------
#			 für die Buchausstellung
#################################################


#################################################
#			Beschreibung der Tabellen
#################################################

# verlag
# +-------+--------------+------+-----+---------+----------------+
# | Field | Type         | Null | Key | Default | Extra          |
# +-------+--------------+------+-----+---------+----------------+
# | id    | int(11)      | NO   | PRI | NULL    | auto_increment |
# | name  | varchar(150) | NO   |     | NULL    |                |
# +-------+--------------+------+-----+---------+----------------+

# buch
# +-----------+--------------+------+-----+---------+----------------+
# | Field     | Type         | Null | Key | Default | Extra          |
# +-----------+--------------+------+-----+---------+----------------+
# | id        | int(11)      | NO   | PRI | NULL    | auto_increment |
# | buchnr    | varchar(4)   | NO   |     | NULL    |                |
# | titel     | varchar(150) | NO   |     | NULL    |                |
# | autor     | varchar(150) | NO   |     | NULL    |                |
# | preis     | double       | NO   |     | NULL    |                |
# | rabgr     | int(2)       | NO   |     | NULL    |                |
# | katgr     | int(2)       | NO   |     | NULL    |                |
# | verlag_id | int(3)       | NO   | MUL | NULL    |                |
# +-----------+--------------+------+-----+---------+----------------+

# besucher
# +------------+--------------+------+-----+---------+----------------+
# | Field      | Type         | Null | Key | Default | Extra          |
# +------------+--------------+------+-----+---------+----------------+
# | id         | int(11)      | NO   | PRI | NULL    | auto_increment |
# | vorname    | varchar(150) | NO   |     | NULL    |                |
# | nachname   | varchar(150) | NO   |     | NULL    |                |
# | strasse    | varchar(150) | NO   |     | NULL    |                | 
# | hausnummer | varchar(3)   | NO   |     | NULL    |                |
# | plz        | varchar(5)   | NO   |     | NULL    |                |
# | ort        | varchar(150) | NO   |     | NULL    |                |
# +------------+--------------+------+-----+---------+----------------+

#besucher_kommunikation
# +--------------+-------------------+------+-----+---------+----------------+
# | Field        | Type              | Null | Key | Default | Extra          |
# +--------------+-----   -----------+------+-----+---------+----------------+
# | id           | int(11)           | NO   | PRI | NULL    | auto_increment |
# | besucher_id  | int(11)           | NO   | MUL | NULL    |                |
# | typ          | enum('Telefon')   | NO   |     | NULL    |                | 
# | wert         | varchar(150)      | NO   |     | NULL    |                |
# +--------------+-------------------+------+-----+---------+----------------+

# veranstaltung
# +----------+-------------------------------------------------------------+------+-----+---------+-------+
# | Field    | Type                                                        | Null | Key | Default | Extra |
# +----------+-------------------------------------------------------------+------+-----+---------+-------+
# | datumvon | date                                                        | NO   |     | NULL    |       |
# | datumbis | date                                                        | NO   |     | NULL    |       |
# | ort      | varchar(150)                                                | NO   |     | NULL    |       |
# | stadium  | enum('Vorbereitung','Veranstaltung','Lieferung','Abholung') | NO   |     | NULL    |       |
# +----------+-------------------------------------------------------------+------+-----+---------+-------+

# bestellung_hat_buch
# +----------------+---------+------+-----+---------+----------------+
# | Field          | Type    | Null | Key | Default | Extra          |
# +----------------+---------+------+-----+---------+----------------+
# | id      	   | int(11) | NO   | PRI | NULL    | auto_increment |
# | buch_id 	   | int(11) | NO   | MUL | NULL    |                |
# | anzahl  	   | int(11) | NO   |     | 1       |                |
# | bestellung_id  | int(11) | NO   | MUL | NULL    |                |
# +----------------+---------+------+-----+---------+----------------+

# bestellung
# +------------------------+---------+------+-----+---------+----------------+
# | Field                  | Type    | Null | Key | Default | Extra          |
# +------------------------+---------+------+-----+---------+----------------+
# | id                     | int(11) | NO   | PRI | NULL    | auto_increment |
# | besucher_id            | int(11) | NO   | MUL | NULL    |                |
# +------------------------+---------+------+-----+---------+----------------+

# bestellung
# +------------------------+-------         --+------+-----+---------+----------------+
# | Field                  | Type             | Null | Key | Default | Extra          |
# +------------------------+---------+------+-----+---------+----------------+
# | id                     | int(11) | NO   | PRI | NULL    | auto_increment |
# | nr                     | int(11)        | NO   | MUL | NULL    |                |
# | bezeichnung            | varchar(150)      | NO   |     | NULL    |                |
# +------------------------+---------+------+-----+---------+----------------+


#################################################
#			Erstellung der Tabellen
#################################################

DROP DATABASE IF EXISTS buchausstellung;

# Erstellt die Datenbank, wenn sie noch nicht existiert
CREATE DATABASE IF NOT EXISTS buchausstellung;

USE buchausstellung;

# Erstellt eine Verlag-Tabelle
CREATE TABLE verlag (id INT PRIMARY KEY NOT NULL auto_increment,
					 name VARCHAR(150) NOT NULL);
					 
# Erstellt eine Buchgruppen-Tabelle
CREATE TABLE buchgruppe (id INT PRIMARY KEY NOT NULL auto_increment,
						 nr INT, bezeichnung VARCHAR(40) NOT NULL)
					 
# Erstellt eine Buch-Tabelle 
CREATE TABLE buch ( id INT PRIMARY KEY NOT NULL auto_increment, 
					  buchnr VARCHAR(4) NOT NULL,
					  titel VARCHAR(150) NOT NULL, 
					  autor VARCHAR(150) NOT NULL,
					  preis DOUBLE NOT NULL,
					  rabgr INT NOT NULL,
					  katgr INT NOT NULL,
					  verlag_id INT NOT NULL,
					  FOREIGN KEY(verlag_id) REFERENCES verlag(id),
					  FOREIGN KEY(rabgr) REFERENCES buchgruppe(id));
					
# Erstellt eine Besucher-Tabelle					
CREATE TABLE besucher ( id INT PRIMARY KEY NOT NULL auto_increment,
						vorname VARCHAR(150) NOT NULL,
						nachname VARCHAR(150) NOT NULL,
						strasse VARCHAR(150) NOT NULL,
						hausnummer VARCHAR(3) NOT NULL,
						plz VARCHAR(5) NOT NULL,
						ort VARCHAR(150) NOT NULL);
						
# Erstellt eine Kommunikations-Tabelle
CREATE TABLE besucher_kommunikation ( id INT PRIMARY KEY NOT NULL auto_increment,
									  besucher_id INT NOT NULL,
									  FOREIGN KEY (besucher_id) REFERENCES besucher(id),
									  typ ENUM('Telefon, Handy, Mail') NOT NULL,
									  wert VARCHAR(150) NOT NULL);

# Erstellt eine Veranstaltungs-Tabelle
CREATE TABLE veranstaltung ( datumvon DATE NOT NULL,
							 datumbis DATE NOT NULL,
							 ort VARCHAR(150) NOT NULL,
							 stadium ENUM('Vorbereitung','Veranstaltung','Lieferung','Abholung') NOT NULL);
							 
# Erstellt eine Bestellungen-Tabelle
CREATE TABLE bestellung ( id INT PRIMARY KEY NOT NULL auto_increment,
						  besucher_id INT NOT NULL, abgeholt TINYINT(1) DEFAULT 0,
						  FOREIGN KEY(besucher_id) REFERENCES besucher(id)
						  );
						  
# Erstellt eine Buchbestellungen-Tabelle
CREATE TABLE bestellung_hat_buch( id INT PRIMARY KEY NOT NULL auto_increment,
								  buch_id INT NOT NULL,
								  FOREIGN KEY(buch_id) REFERENCES buch(id),
								  anzahl INT NOT NULL DEFAULT 1,
								  bestellung_id INT NOT NULL,
								  anzahl_geliefert INT NOT NULL,
								  FOREIGN KEY(bestellung_id) REFERENCES bestellung(id)
								  );
								  
						  
						  
#################################################
#				Berechtigung auf die 
# 					 Datenbank
#################################################
USE mysql;

DROP USER IF EXISTS 'clientbenutzer'@'%';

# Erstellen eines Datenbank-Benutzers
CREATE USER 'clientbenutzer'@'%' IDENTIFIED BY 'cl1.entp4ssW0rt';

# Vergeben der Berechtigung Daten zu lesen, updaten oder einzufügen auf die Buchausstellung-DB
GRANT SELECT,UPDATE,INSERT ON buchausstellung . * TO 'clientbenutzer'@'%';


#################################################
#				Datenbankprozeduren
#################################################

USE buchausstellung;


# Erstellen der Veranstaltung
DELIMITER $$
CREATE PROCEDURE ErstelleVeranstaltung()
BEGIN
	INSERT INTO veranstaltung (stadium) VALUES('Vorbereitung');
END$$
DELIMITER ;

USE mysql;
GRANT EXECUTE ON PROCEDURE buchausstellung.ErstelleVeranstaltung TO 'clientbenutzer'@'%';
USE buchausstellung;


# Starten der Veranstaltung
DELIMITER $$ 
CREATE PROCEDURE StarteVeranstaltung(
	StartDatum VARCHAR(10),
	EndDatum VARCHAR(10),
	Ort VARCHAR(150)
	)
BEGIN 
	UPDATE veranstaltung
		SET datumvon = StartDatum,
			datumbis = EndDatum,
			ort = Ort,
			stadium = 'Veranstaltung'
	WHERE stadium = 'Vorbereitung';
END$$
DELIMITER ;

USE mysql;
GRANT EXECUTE ON PROCEDURE buchausstellung.StarteVeranstaltung TO 'clientbenutzer'@'%';
USE buchausstellung;

# Updaten der Veranstaltung
DELIMITER $$ 
CREATE PROCEDURE UpdateVeranstaltung(
	Stadium VARCHAR(100)
	)
BEGIN 
	UPDATE veranstaltung
		SET stadium = Stadium;
END$$
DELIMITER ;

USE mysql;
GRANT EXECUTE ON PROCEDURE buchausstellung.UpdateVeranstaltung TO 'clientbenutzer'@'%';
USE buchausstellung;

# Bereinigen aller Tabellen und beenden der Veranstaltung
DELIMITER $$ 
CREATE PROCEDURE BeendeVeranstaltung()
BEGIN 
	DELETE FROM verlag;
	DELETE FROM buch;
	DELETE FROM besucher;
	DELETE FROM bestellung_hat_buch;
	DELETE FROM bestellung;
	DELETE FROM veranstaltung;
END$$
DELIMITER ;

USE mysql;
GRANT EXECUTE ON PROCEDURE buchausstellung.BeendeVeranstaltung TO 'clientbenutzer'@'%';
USE buchausstellung;

# Rückgabe des Veranstaltungs-Stadiums
DELIMITER $$ 
CREATE PROCEDURE VeranstaltungsStadium()
BEGIN 
	SELECT stadium FROM veranstaltung;
END$$
DELIMITER ;

USE mysql;
GRANT EXECUTE ON PROCEDURE buchausstellung.VeranstaltungsStadium TO 'clientbenutzer'@'%';
USE buchausstellung;


# Rückgabe aller Bücher
DELIMITER $$
CREATE PROCEDURE HoleBücher()
BEGIN
	SELECT b.id AS "buchid",buchnr,titel,autor,preis,rabgr,katgr, name FROM buch b JOIN verlag v ON v.id = b.verlag_id LEFT JOIN buchgruppe g ON g.id = b.katgr;
END$$
DELIMITER ;

USE mysql;
GRANT EXECUTE ON PROCEDURE buchausstellung.HoleBücher TO 'clientbenutzer'@'%';
USE buchausstellung;

# Erstellt einen Besucher und eine zugehörige ID
DELIMITER $$
CREATE PROCEDURE ErstelleBesucher(
	Vorname VARCHAR(150),
	Nachname VARCHAR(150),
	Strasse VARCHAR(150),
	Hausnummer VARCHAR(3),
	PLZ VARCHAR(5),
	Ort VARCHAR(150),
	Telefon VARCHAR(80)
)
BEGIN
	INSERT INTO besucher (vorname,nachname,strasse,hausnummer,plz,ort) VALUES (Vorname,Nachname,Strasse,Hausnummer,PLZ,Ort);
	SELECT MAX(besucher.id) AS "NeueID" FROM besucher;
	INSERT INTO besucher_kommunikation(besucher_id,typ,wert) VALUES (NeueID,Typ,Telefon);
END$$
DELIMITER ;

USE mysql;
GRANT EXECUTE ON PROCEDURE buchausstellung.ErstelleBesucher TO 'clientbenutzer'@'%';
USE buchausstellung;


# Ruft vom Namen des Besuchers die automatisch generierte ID ab
DELIMITER $$
CREATE PROCEDURE BekommeBesucherId(
	Vorname VARCHAR(150),
	Nachname VARCHAR(150)
)
BEGIN
	SELECT id FROM besucher WHERE vorname LIKE Vorname AND nachname LIKE Nachname LIMIT 1;
END$$
DELIMITER ;

USE mysql;
GRANT EXECUTE ON PROCEDURE buchausstellung.BekommeBesucherId TO 'clientbenutzer'@'%';
USE buchausstellung;

# Erstellt eine Bestellung für den Benutzer 
DELIMITER $$
CREATE PROCEDURE ErstelleEinzelBestellung( PersonenId INT)
BEGIN
	INSERT INTO bestellung (besucher_id) VALUES (PersonenId);
END$$
DELIMITER ;

USE mysql;
GRANT EXECUTE ON PROCEDURE buchausstellung.ErstelleEinzelBestellung TO 'clientbenutzer'@'%';
USE buchausstellung;

# Gibt die ID einer Bestellung zurück
DELIMITER $$
CREATE PROCEDURE BekommeBestellungsId(
	PersonId INT
)
BEGIN 
	SELECT id FROM bestellung WHERE besucher_id = PersonId LIMIT 1;
END$$
DELIMITER ;

USE mysql;
GRANT EXECUTE ON PROCEDURE buchausstellung.BekommeBestellungsId TO 'clientbenutzer'@'%';
USE buchausstellung;

# Fügt ein Buch auf die aktuelle Bestellung mit der übergebenen Anzahl hinzu
DELIMITER $$
CREATE PROCEDURE BuchbestellungHinzufügen(
	buchID INT,
	bestellungID INT,
	Anzahl INT
)
BEGIN
	INSERT INTO bestellung_hat_buch (buch_id, anzahl, bestellung_id) VALUES (buchID,Anzahl,bestellungID);
END$$
DELIMITER ;

USE mysql;
GRANT EXECUTE ON PROCEDURE buchausstellung.BuchbestellungHinzufügen TO 'clientbenutzer'@'%';
USE buchausstellung;

# Gibt alle Bestellungen und dessen Besucher zurück
DELIMITER $$
CREATE PROCEDURE HoleBestellungsInfo()
BEGIN

	SELECT 	bestellung.id AS "ID",
			besucher.id AS "Besucherid" , 
			besucher.vorname AS "Vorname",
			besucher.nachname AS "Nachname",
			besucher.strasse AS "Strasse",
			besucher.hausnummer AS "Hausnummer",
			besucher.ort AS "Ort",
			besucher.plz AS "PLZ",
			bestellung.abgeholt AS "Abgeholt"
			
			
	FROM bestellung 
		JOIN besucher ON besucher.id = bestellung.besucher_id;
END$$
DELIMITER ;

USE mysql;
GRANT EXECUTE ON PROCEDURE buchausstellung.HoleBestellungsInfo TO 'clientbenutzer'@'%';
USE buchausstellung;

DELIMITER $$
CREATE PROCEDURE HoleBücherZuBestellungsInfo(
bestellungsid INT
)
BEGIN
	SELECT
	bb.buch_id AS "BuchId",
	bb.anzahl AS "Buchanzahl",
	b.buchnr AS "BuchNr",
	b.titel AS "BuchTitel",
	b.autor AS "Autor",
	b.preis AS "Preis",
	g.nr AS "Rabatt",
	b.katgr AS "Kategorie",
	v.name AS "Verlag"
	FROM bestellung_hat_buch bb 
		JOIN buch b ON bb.buch_id = b.id 
		JOIN verlag v ON v.id = b.verlag_id
		JOIN buchgruppe g ON g.id = b.rabgr
			WHERE bestellung_id = bestellungsid;
END$$
DELIMITER ;

USE mysql;
GRANT EXECUTE ON PROCEDURE buchausstellung.HoleBücherZuBestellungsInfo TO 'clientbenutzer'@'%';
USE buchausstellung;

# Erstellt ein neues Buch und eine zugehörige ID
DELIMITER $$
CREATE PROCEDURE ErstelleBuch(
	Buchnummer VARCHAR(4),
	Titel VARCHAR(150),
	Autor VARCHAR(150),
	Preis DOUBLE,
	Rabattgruppe INT(2),
	Kategorie INT(2),
	Verlag INT(3)
)
BEGIN
	INSERT INTO buch (buchnr,titel,autor,preis,rabgr,katgr,verlag_id) VALUES (Buchnummer,Titel,Autor,Preis,Rabattgruppe,Kategorie,Verlag);
END$$
DELIMITER ;

USE mysql;
GRANT EXECUTE ON PROCEDURE buchausstellung.ErstelleBuch TO 'clientbenutzer'@'%';
USE buchausstellung;

# Fragt ab ob eine Veranstaltung existiert
DELIMITER $$
CREATE PROCEDURE FrageVeranstaltung()
BEGIN
	SELECT ort FROM Veranstaltung LIMIT 1;
END$$
DELIMITER ;

USE mysql;
GRANT EXECUTE ON PROCEDURE buchausstellung.FrageVeranstaltung TO 'clientbenutzer'@'%';
USE buchausstellung;

# Gibt alle Buchgruppen und deren Bezeichnung zurück
DELIMITER $$
CREATE PROCEDURE HoleBuchgruppen()
BEGIN
	SELECT 	id, nr, bezeichnung	
	FROM buchgruppe;
END$$
DELIMITER ;

USE mysql;
GRANT EXECUTE ON PROCEDURE buchausstellung.HoleBuchgruppen TO 'clientbenutzer'@'%';
USE buchausstellung;

# Erstellt eine neue Buchgruppe und eine zugehörige ID
DELIMITER $$
CREATE PROCEDURE ErstelleBuchgruppe(
	Nr INT,
	Bezeichnung VARCHAR(40)
)
BEGIN
	INSERT INTO buchgruppe (nr,bezeichnung) VALUES (Nr,Bezeichnung);
END$$
DELIMITER ;

USE mysql;
GRANT EXECUTE ON PROCEDURE buchausstellung.ErstelleBuchgruppe TO 'clientbenutzer'@'%';
USE buchausstellung;

# Entfernt eine neue Buchgruppe
DELIMITER $$
CREATE PROCEDURE EntferneBuchgruppe(
	ID INT
)
BEGIN
	DELETE FROM buchgruppe WHERE buchgruppe.id=ID;
END$$
DELIMITER ;

USE mysql;
GRANT EXECUTE ON PROCEDURE buchausstellung.EntferneBuchgruppe TO 'clientbenutzer'@'%';
USE buchausstellung;

SHOW PROCEDURE STATUS;

# Entfernt eine neue Buchgruppe
DELIMITER $$
CREATE PROCEDURE AktualisiereBuchgruppe(
	ID INT, Nr INT, Besucher VARCHAR(40)
)
BEGIN
	UPDATE buchgruppe 
	SET nr=Nr,
		besucher=Besucher
	WHERE buchgruppe.id=ID;
END$$
DELIMITER ;

USE mysql;
GRANT EXECUTE ON PROCEDURE buchausstellung.AktualisiereBuchgruppe TO 'clientbenutzer'@'%';
USE buchausstellung;

# Setzt den Status der Bestellung auf abgeholt
DELIMITER $$
CREATE PROCEDURE BestellungAbgeholt(
	ID INT)
BEGIN
	UPDATE bestellung 
	SET abgeholt = 1
	WHERE bestellung.id=ID;
END$$
DELIMITER ;

USE mysql;
GRANT EXECUTE ON PROCEDURE buchausstellung.BestellungAbgeholt TO 'clientbenutzer'@'%';
USE buchausstellung;

SHOW PROCEDURE STATUS;


###	Testdatensätze
INSERT INTO verlag (id,name) VALUES (1,"Thalia");
INSERT INTO verlag (id,name) VALUES (2,"Amazon");
INSERT INTO verlag (id,name) VALUES (3,"Abenteuer Medien Verlag");
INSERT INTO buch (buchnr,titel,autor,preis,rabgr,katgr,verlag_id) VALUES("110","Harry Potter","J.K.Rowling",13.5,1,0,3);
INSERT INTO buch (buchnr,titel,autor,preis,rabgr,katgr,verlag_id) VALUES("210","Peter Pan","James Matthew Barrie",12.5,1,0,1);
INSERT INTO buch (buchnr,titel,autor,preis,rabgr,katgr,verlag_id) VALUES("111","Herr der Ringe","J. R. R. Tolkien",18.3,0,1,3);
INSERT INTO buch (buchnr,titel,autor,preis,rabgr,katgr,verlag_id) VALUES("310","Handbuch der Tonstudiotechnik","ARD.ZDF",30.87,1,1,2);
