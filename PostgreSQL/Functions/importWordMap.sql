CREATE OR REPLACE FUNCTION importWordMap (wd varchar(50), bk varchar(25), ch INT, vnum INT, refnum INT) 
 RETURNS VARCHAR(100)
AS $$
BEGIN
 										
 IF EXISTS (WITH VERSEID AS (SELECT verseid FROM bible AS B WHERE B.book = bk AND B.chapter = ch AND B.versenum = vnum )
		   SELECT 1 FROM wordmap AS W
		   INNER JOIN VERSEID AS V ON W.verseid = V.verseid
		   WHERE W.word = wd) THEN
 	UPDATE wordmap
	SET referencenum = refnum;
	RETURN CONCAT('Updated Reference Number ', CAST(refnum AS VARCHAR(10)), ' for ',wd, ' in ', bk, ' ', ch, ':', vnum);
 ELSE
 	INSERT INTO wordmap (word, verseid, referencenum)
	SELECT wd, B.verseid, refnum
    FROM bible AS B											
    WHERE B.book = bk AND B.chapter = ch AND B.versenum = vnum;											
	RETURN CONCAT('Added Reference Number ', CAST(refnum AS VARCHAR(10)), ' to ',wd, ' in ', bk, ' ', ch, ':', vnum);
 END IF;
											
 EXCEPTION WHEN OTHERS THEN
 RETURN 'Invalid Reference Number';											

END; $$ 
LANGUAGE 'plpgsql';