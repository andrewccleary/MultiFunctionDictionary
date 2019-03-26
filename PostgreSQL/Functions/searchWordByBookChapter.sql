CREATE OR REPLACE FUNCTION searchWordByBookChapter (searchedWord VARCHAR(50), bookNumber INT, chapterNumber INT) 
 RETURNS TABLE (
	 word varchar(50),
	 testament varchar(25),
	 book varchar(25),
	 chapter INT,
	 versenum INT,
	 referencenum INT,
	 hebrewword varchar(50),
	 hebrewtranslation varchar(50),
	 pronunciation varchar(100),
	 definition varchar(1000)
) 
AS $$
BEGIN
 RETURN QUERY     
 	SELECT W.word, B.testament, B.book, B.chapter, B.versenum, T.referencenum, T.hebrewword, T.hebrewtranslation, T.pronunciation, T.definition
	FROM wordmap AS W
	INNER JOIN bible AS B ON W.verseid = B.verseid
	INNER JOIN translation AS T ON T.referencenum = W.referencenum
	WHERE W.word = searchedWord
	AND B.booknum = bookNumber
	AND B.chapter = chapterNumber
	ORDER By B.booknum, B.chapter, B.versenum, T.referencenum;
END; $$ 
LANGUAGE 'plpgsql';
