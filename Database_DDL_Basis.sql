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
# | title     | varchar(150) | NO   |     | NULL    |                |
# | author    | varchar(150) | NO   |     | NULL    |                |
# | preis     | double       | NO   |     | NULL    |                |
# | rabgr     | int(11)      | NO   |     | NULL    |                |
# | katgr     | int(11)      | NO   |     | NULL    |                |
# | verlag_id | int(11)      | NO   | MUL | NULL    |                |
# +-----------+--------------+------+-----+---------+----------------+

# besucher
# +-----------+--------------+------+-----+---------+----------------+
# | Field     | Type         | Null | Key | Default | Extra          |
# +-----------+--------------+------+-----+---------+----------------+
# | id        | int(11)      | NO   | PRI | NULL    | auto_increment |
# | name      | varchar(150) | NO   |     | NULL    |                |
# | anschrift | varchar(150) | NO   |     | NULL    |                |
# | telefon   | varchar(80)  | NO   |     | NULL    |                |
# +-----------+--------------+------+-----+---------+----------------+

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
# +---------+---------+------+-----+---------+----------------+
# | Field   | Type    | Null | Key | Default | Extra          |
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
					 
# Erstellt eine Buch-Tabelle 
CREATE TABLE buch 	( id INT PRIMARY KEY NOT NULL auto_increment, 
					  title VARCHAR(150) NOT NULL, 
					  author VARCHAR(150) NOT NULL,
					  preis DOUBLE NOT NULL,
					  rabgr INT NOT NULL,
					  katgr INT NOT NULL,
					  verlag_id INT NOT NULL,
					  FOREIGN KEY(verlag_id) REFERENCES verlag(id));
					
# Erstellt eine Besucher-Tabelle					
CREATE TABLE besucher ( id INT PRIMARY KEY NOT NULL auto_increment,
						name VARCHAR(150) NOT NULL,
						anschrift VARCHAR(150) NOT NULL,
						telefon VARCHAR(80) NOT NULL);
						

# Erstellt eine Veranstaltungs-Tabelle
CREATE TABLE veranstaltung ( datumvon DATE NOT NULL,
							 datumbis DATE NOT NULL,
							 ort VARCHAR(150) NOT NULL,
							 stadium ENUM('Vorbereitung','Veranstaltung','Lieferung','Abholung') NOT NULL);
							 
# Erstellt eine Bestellungen-Tabelle
CREATE TABLE bestellung ( id INT PRIMARY KEY NOT NULL auto_increment,
						  besucher_id INT NOT NULL,
						  FOREIGN KEY(besucher_id) REFERENCES besucher(id)
						  );
						  
# Erstellt eine Buchbestellungen-Tabelle
CREATE TABLE bestellung_hat_buch( id INT PRIMARY KEY NOT NULL auto_increment,
								  buch_id INT NOT NULL,
								  FOREIGN KEY(buch_id) REFERENCES buch(id),
								  anzahl INT NOT NULL DEFAULT 1,
								  bestellung_id INT NOT NULL,
								  FOREIGN KEY(bestellung_id) REFERENCES bestellung(id)
								  );
								  

						  
						  
#################################################
#				Berechtigung auf die 
# 					 Datenbank
#################################################
USE mysql;

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

# Starte Veranstaltung
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

# Rückgabe des Veranstaltuns-Stadiums
DELIMITER $$ 
CREATE PROCEDURE VeranstaltungsStadium()
BEGIN 
	SELECT stadium FROM veranstaltung;
END$$
DELIMITER ;


###	Testdatensatz
###	INSERT INTO buch (title,author,preis,rabgr,katgr,verlag_id) VALUES("Harry Potter","J.K.Rowling",13.5,1,0,1);
###	INSERT INTO buch (title,author,preis,rabgr,katgr,verlag_id) VALUES("Peter Pan","James Matthew Barrie",12.5,1,0,1);
### INSERT INTO verlag (id,name) VALUES (1,"Thalia");

# Rückgabe aller Bücher
DELIMITER $$
CREATE PROCEDURE HoleBücher()
BEGIN
	SELECT b.id AS "buchid",title,author,preis,rabgr,katgr, name FROM buch b JOIN verlag v ON v.id = b.verlag_id;
END$$
DELIMITER ;

# Erstellt einen Besucher und eine zugehörige ID
DELIMITER $$
CREATE PROCEDURE ErstelleBesucher(
	Name VARCHAR(150),
	Anschrift VARCHAR(150),
	Telefon VARCHAR(80)
)
BEGIN
	INSERT INTO besucher (name,anschrift,telefon) VALUES ( Name,Anschrift,Telefon);
END$$
DELIMITER ;

# Ruft vom Namen des Besuchers die automatisch generierte ID ab
DELIMITER $$
CREATE PROCEDURE BekommeBesucherId(
	Name VARCHAR(150)
)
BEGIN
	SELECT id FROM besucher WHERE name LIKE Name LIMIT 1;
END$$
DELIMITER ;


# Erstellt eine Bestellung für den Benutzer 
DELIMITER $$
CREATE PROCEDURE ErstelleEinzelBestellung( PersonenId INT)
BEGIN
	INSERT INTO bestellung (besucher_id) VALUES (PersonenId);
END$$
DELIMITER ;


# Gibt die ID einer Bestellung zurück
DELIMITER $$
CREATE PROCEDURE BekommeBestellungsId(
	PersonId INT
)
BEGIN 
	SELECT id FROM bestellung WHERE besucher_id = PersonId LIMIT 1;
END$$
DELIMITER ;



# Fügt ein Buch auf die Aktuelle Bestellung mit der übergebenen Anzahl hinzu
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

# Gibt alle Bestellungen und dessen Besucher zurück
DELIMITER $$
CREATE PROCEDURE HoleBestellungsInfo()
BEGIN
	SELECT 	bestellung.id AS "ID",
			besucher.id AS "Besucherid" , 
			besucher.name AS "Besuchername",
			besucher.anschrift AS "Besucheranschrift",
			besucher.telefon AS "Besuchertelefon"
			
			
	FROM bestellung 
		JOIN besucher ON besucher.id = bestellung.besucher_id;
END$$
DELIMITER ;

DELIMITER $$
CREATE PROCEDURE HoleBücherZuBestellungsInfo(
bestellungsid INT
)
BEGIN
	SELECT
	bb.buch_id AS "BuchId",
	bb.anzahl AS "Buchanzahl",
	b.title AS "BuchTitle",
	b.author AS "Author",
	b.preis AS "Preis",
	b.rabgr AS "Rabatt",
	b.katgr AS "Kategorie",
	v.name AS "Verlag"
	FROM bestellung_hat_buch bb 
		JOIN buch b ON bb.buch_id = b.id 
		JOIN verlag v ON v.id = b.verlag_id
			WHERE bestellung_id = bestellungsid;
END$$
DELIMITER ;



SHOW PROCEDURE STATUS;
