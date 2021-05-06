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
# +---------+---------+------+-----+---------+----------------+
# | id      | int(11) | NO   | PRI | NULL    | auto_increment |
# | buch_id | int(11) | NO   | MUL | NULL    |                |
# | anzahl  | int(11) | NO   |     | 1       |                |
# +---------+---------+------+-----+---------+----------------+

# bestellung
# +------------------------+---------+------+-----+---------+----------------+
# | Field                  | Type    | Null | Key | Default | Extra          |
# +------------------------+---------+------+-----+---------+----------------+
# | id                     | int(11) | NO   | PRI | NULL    | auto_increment |
# | besucher_id            | int(11) | NO   | MUL | NULL    |                |
# | bestellung_hat_buch_id | int(11) | NO   | MUL | NULL    |                |
# +------------------------+---------+------+-----+---------+----------------+


#################################################
#			Erstellung der Tabellen
#################################################

DROP DATABASE IF EXISTS buchausstellung;

# Erstellt die Datenbank, wenn sie noch nicht existiert
CREATE DATABASE IF NOT EXISTS buchausstellung;

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
							 
# Erstellt eine Buchbestellungen-Tabelle
CREATE TABLE bestellung_hat_buch( id INT PRIMARY KEY NOT NULL auto_increment,
								  buch_id INT NOT NULL,
								  FOREIGN KEY(buch_id) REFERENCES buch(id),
								  anzahl INT NOT NULL DEFAULT 1);
								  
# Erstellt eine Bestellungen-Tabelle
CREATE TABLE bestellung ( id INT PRIMARY KEY NOT NULL auto_increment,
						  bestellnr INT NOT NULL ,
						  besucher_id INT NOT NULL,
						  FOREIGN KEY(besucher_id) REFERENCES besucher(id),
						  bestellung_hat_buch_id INT NOT NULL,
						  FOREIGN KEY(bestellung_hat_buch_id) REFERENCES bestellung_hat_buch(id));
						  
						  
#################################################
#				Berechtigung auf die 
# 					 Datenbank
#################################################
use mysql;

# Erstellen eines Datenbank-Benutzers
CREATE USER 'clientbenutzer'@'%' IDENTIFIED BY 'cl1.entp4ssW0rt';


# Vergeben der Berechtigung Daten zu lesen, updaten oder einzufügen auf die Buchausstellung-DB
GRANT SELECT,UPDATE,INSERT ON buchausstellung . * TO 'clientbenutzer'@'%';