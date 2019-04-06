CREATE OR REPLACE FUNCTION getReferenceNumbers (referenceNumber INT)
 RETURNS TABLE (
 referencenum INT,
 word varchar(50),
 booknum INT,
 book VARCHAR(20),
 chapter INT,
 versenum INT,
 verse VARCHAR(1000)
)
AS $$
BEGIN
 RETURN QUERY
 	SELECT DISTINCT W.referencenum, W.word, B.booknum, B.book, B.chapter, B.versenum, B.verse
  FROM wordmap AS W
	INNER JOIN bible AS B ON W.verseid = B.verseid
	WHERE W.referencenum = referenceNumber
  ORDER BY B.booknum, B.chapter, B.versenum;
END; $$
LANGUAGE 'plpgsql';
