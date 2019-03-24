CREATE OR REPLACE FUNCTION getChapters (booknumber INT) 
 RETURNS TABLE (
 chapter INT
) 
AS $$
BEGIN
 RETURN QUERY     
 	SELECT DISTINCT B.chapter
	FROM BIBLE AS B
	WHERE B.booknum = booknumber
	ORDER BY B.chapter;
END; $$ 
LANGUAGE 'plpgsql';