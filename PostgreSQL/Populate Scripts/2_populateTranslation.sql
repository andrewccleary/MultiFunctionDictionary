CREATE TABLE RAWSTRONGS (
	referencenum INT,
	hebrewword VARCHAR(50),
	hebrewtranslation VARCHAR(50),
  pronunciation VARCHAR(100),
  derivation VARCHAR(1000),
  strongsdefinition VARCHAR(1000),
  kjvdefinition VARCHAR(1000)
);

COPY RAWSTRONGS FROM 'C:\MultifunctionalDictionary\Hebrew.csv' WITH CSV ESCAPE as '"';

INSERT INTO translation(referencenum, hebrewword, hebrewtranslation, pronunciation, definition)
SELECT referencenum, hebrewword, hebrewtranslation, pronunciation, strongsdefinition FROM rawstrongs;