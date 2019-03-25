CREATE TABLE RAWBIBLE (
	BookNum INT,
	BookCode VARCHAR(5),
	Book VARCHAR(20),
	Chapter INT,
	VerseNum INT,
	Verse VARCHAR(1000)
);

COPY RAWBIBLE FROM 'C:\MultiFunctionalDictionary\BIBLE_KJV.csv' WITH CSV HEADER;

INSERT INTO BIBLE (Language, TestamentNum, Testament, BookNum, Book, Chapter, VerseNum, Verse)
SELECT 'English' AS Language, 
	CASE WHEN booknum < 41 THEN 1 ELSE 2 END AS TestamentNum, 
	CASE WHEN booknum < 41 THEN 'Old Testament' ELSE 'New Testament' END AS Testament,
	booknum, book, chapter, versenum, verse
FROM RAWBIBLE
ORDER BY booknum, chapter, versenum;

