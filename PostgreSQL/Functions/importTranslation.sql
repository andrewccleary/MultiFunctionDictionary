CREATE OR REPLACE FUNCTION importTranslation (refnum INT, word varchar(50), trans varchar(50), pronun varchar(100), def varchar(1000)) 
 RETURNS VARCHAR(100)
AS $$
BEGIN
 
 IF EXISTS (SELECT 1 FROM translation AS T WHERE T.referencenum = refnum) THEN
 	UPDATE translation
	SET hebrewword = word, hebrewtranslation = trans, pronunciation = pronun, definition = def
    WHERE referencenum = refnum;
	RETURN CONCAT('Reference Number ', CAST(refnum AS VARCHAR(10)), ' Updated');
 ELSE
 	INSERT INTO translation (referencenum, hebrewword, hebrewtranslation, pronunciation, definition)
	VALUES (refnum, word, trans, pronun, def);
	RETURN CONCAT('Reference Number ', CAST(refnum AS VARCHAR(10)), ' Imported');
 END IF;

END; $$ 
LANGUAGE 'plpgsql';