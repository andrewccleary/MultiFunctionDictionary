CREATE OR REPLACE FUNCTION getChildReferenceNumbers (referenceNumber INT)
 RETURNS TABLE (
 referencenum INT
)
AS $$
BEGIN
 RETURN QUERY
 	SELECT DISTINCT R.childreferencenum
  FROM rootreference AS R
	WHERE R.rootreferencenum = referenceNumber
  ORDER BY R.childreferencenum;
END; $$
LANGUAGE 'plpgsql';
