CREATE OR REPLACE FUNCTION getVerses (bookNumber INT, chapternumber INT) 
 RETURNS TABLE (
 versenum INT
) 
AS $$
BEGIN
 RETURN QUERY     
 	SELECT DISTINCT B.versenum
	FROM BIBLE AS B
	WHERE B.booknum = booknumber
	AND B.chapter = chapternumber
	ORDER BY B.versenum;
END; $$ 
LANGUAGE 'plpgsql';