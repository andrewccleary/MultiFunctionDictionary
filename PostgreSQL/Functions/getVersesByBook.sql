CREATE OR REPLACE FUNCTION getVersesByBook (bookNumber INT) 
 RETURNS TABLE (
 verseid INT,
 language VARCHAR(25),
 testamentnum INT,
 testament VARCHAR(25),
 booknum INT,
 book VARCHAR(25),
 chapter INT,
 versenum INT,
 verse VARCHAR(1000)
) 
AS $$
BEGIN
 RETURN QUERY     
 	SELECT B.verseid, B.language, B.testamentnum, B.testament, B.booknum, B.book, B.chapter, B.versenum, B.verse
	FROM BIBLE AS B
	WHERE B.booknum = bookNumber
	ORDER BY B.chapter, B.versenum;
END; $$ 
LANGUAGE 'plpgsql';