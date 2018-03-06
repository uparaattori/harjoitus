CREATE DATABASE IF NOT EXISTS test;

USE test;

CREATE TABLE IF NOT EXISTS birds (
  BirdID INT NOT NULL PRIMARY KEY AUTO_INCREMENT, 
  Name VARCHAR(100) NOT NULL, 
  Count INT NOT NULL,
  Inserted TIMESTAMP DEFAULT CURRENT_TIMESTAMP);


CREATE TABLE IF NOT EXISTS sightings 
(SightingID INT NOT NULL PRIMARY KEY AUTO_INCREMENT,
BirdId INT NOT NULL,
SightingDate DATETIME NOT NULL
);



ALTER TABLE birds ADD INDEX Birds_IX (BirdID);
ALTER TABLE sightings ADD INDEX Sightings_IX (SightingID);


INSERT INTO birds (Name,Count) 
VALUES('Harakka',0), 
      ('Varis',0);
      

 