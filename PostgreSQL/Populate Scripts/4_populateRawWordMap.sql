CREATE TABLE RAWWORDMAP (
	wordid INT,
	strongsid INT
);

COPY RAWWORDMAP FROM 'C:\MultifunctionalDictionary\rawwordmap.csv' WITH CSV ESCAPE as '"';