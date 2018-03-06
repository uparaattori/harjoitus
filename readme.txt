Ohjeet harjoitus sovelluksen ajamista varten.

Ympäristö
---------
Suse Linux
dotnet core 2.1.4
MariaDB (Server version: 10.0.33-MariaDB SLE 12 SP1 package). Itse asiassa MySQL.

Kanta
---------
Kannan muodostamiseen tarvittavat komennot löytyvät tiedostosta:
sightings.sql


Sovellus
---------
Ohjelma käännetään menemällä päähakemistoon ja annetaan komento:
dotnet build

Sovellus käynnistetään komennolla:
dotnet run

tai jos haluaa ajaa Release tilassa
dotnet run -c Release


Selain
--------
Sovellus vastaa osoitteesta http://localhost:5000/


App.Settings 
------------
Sql kanta-asetukset
SQLConnectionString

Linnut, jotka lisätään, jos birds taulussa ei ole lintuja.
FirstBird
SecondBird

log4net asetukset, jossa mm. lokin polku.
Loki kirjoitetaan nyt sovelluksen juureen loki.txt.



