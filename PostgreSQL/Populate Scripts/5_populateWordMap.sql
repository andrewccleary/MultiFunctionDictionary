CREATE TABLE RAWINDEX (
	bookid INT,
	chapter varchar(50),
	paraid INT,
	sentid INT,
	verseid INT,
	versenum INT,
	versepos INT,
	wordid INT,
	word varchar(50),
	punc varchar(3),
	italic INT,
	cparen INT,
	oparen INT,
	placeid INT,
	syllables INT,
	yearnum INT,
	personid INT
);

COPY RAWINDEX FROM 'C:\MultifunctionalDictionary\rawindex.csv' WITH CSV ESCAPE as '"';

ALTER TABLE rawindex DROP COLUMN bookid, DROP COLUMN chapter, DROP COLUMN paraid, DROP COLUMN sentid, DROP COLUMN punc, DROP COLUMN italic, DROP COLUMN cparen, DROP COLUMN oparen, DROP COLUMN placeid, DROP COLUMN syllables, DROP COLUMN personid, DROP COLUMN yearnum;

SELECT word, verseid, strongsid INTO tempwordmap
FROM rawindex
INNER JOIN rawwordmap
ON rawindex.wordid = rawwordmap.wordid;

INSERT INTO wordmap(word, verseid, referencenum)
SELECT word, verseid, strongsid FROM tempwordmap
ON CONFLICT DO NOTHING;

DROP TABLE tempwordmap;

