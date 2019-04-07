CREATE OR REPLACE FUNCTION searchByContext (bookNumber INT, chapterNumber INT, verseNumber INT)
 RETURNS TABLE (
 booknum INT,
 chapter INT,
 versenum INT,
 word varchar(50),
 referencenum INT,
 hebrewword varchar(50),
 hebrewtranslation varchar(50),
 pronunciation varchar(100),
 definition varchar(1000),
 verse VARCHAR(1000)
)
AS $$
BEGIN
 RETURN QUERY
 	SELECT DISTINCT B.booknum, B.chapter, B.versenum, W.word, T.referencenum, T.hebrewword, T.hebrewtranslation, T.pronunciation, T.definition, B.verse
	FROM BIBLE AS B
  INNER JOIN WORDMAP AS W ON W.verseid = B.verseid
  INNER JOIN translation AS T ON T.referencenum = W.referencenum
	WHERE B.booknum = bookNumber
	AND B.chapter = chapterNumber
	AND B.verseNum = verseNumber
	ORDER BY B.booknum, B.chapter, B.versenum, T.referencenum;
END; $$
LANGUAGE 'plpgsql';
