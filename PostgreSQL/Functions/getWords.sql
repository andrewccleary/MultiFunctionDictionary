CREATE OR REPLACE FUNCTION getWords (searchedWord VARCHAR(50))
 RETURNS TABLE (
 word varchar(50),
 referencenum INT,
 booknum INT,
 book VARCHAR(20),
 chapter INT,
 versenum INT,
 verse VARCHAR(1000)
)
AS $$
BEGIN
 RETURN QUERY
 	SELECT DISTINCT W.word, W.referencenum, B.booknum, B.book, B.chapter, B.versenum, B.verse
  FROM wordmap AS W
	INNER JOIN bible AS B ON W.verseid = B.verseid
	WHERE W.word = searchedWord
  ORDER BY B.booknum, B.chapter, B.versenum;
END; $$
LANGUAGE 'plpgsql';
