CREATE OR REPLACE FUNCTION importRootReference (rootreferencenum INT, childreferencenum INT) 
 RETURNS VARCHAR(100)
AS $$
BEGIN
 
 INSERT INTO rootreference (rootreferencenum, childreferencenum)
 VALUES (rootreferencenum, childreferencenum);
 RETURN CONCAT('Reference added from ', CAST(childreferencenum AS VARCHAR(10)), ' to ', CAST(rootreferencenum AS VARCHAR(10)));
 
 EXCEPTION WHEN OTHERS THEN
 RETURN 'Root Reference already exists or Reference Number is Invalid';

END; $$ 
LANGUAGE 'plpgsql';