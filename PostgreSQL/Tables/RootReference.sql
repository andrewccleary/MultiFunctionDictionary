CREATE TABLE ROOTREFERENCE (
	RootReferenceNum INT NOT NULL,
	ChildReferenceNum INT NOT NULL,
	PRIMARY KEY (RootReferenceNum, ChildReferenceNum),
	FOREIGN KEY (RootReferenceNum) REFERENCES TRANSLATION(ReferenceNum),
	FOREIGN KEY (ChildReferenceNum) REFERENCES TRANSLATION(ReferenceNum)
);