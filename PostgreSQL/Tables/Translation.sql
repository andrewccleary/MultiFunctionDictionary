CREATE TABLE TRANSLATION (
	ReferenceNum INT PRIMARY KEY,
	HebrewWord VARCHAR(50),
	HebrewTranslation VARCHAR(50),
	Pronunciation VARCHAR(100),
	Definition VARCHAR(1000) NOT NULL
);