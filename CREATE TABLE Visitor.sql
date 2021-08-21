USE visitorinfo;
CREATE TABLE Visitor
(Visitor_Name VARCHAR(50), Visitor_SurName VARCHAR(50),
 Visitor_Mobile VARCHAR(15), Visitor_Email VARCHAR(50),
 Meeting_Date varchar(100), Meeting_Time varchar(50),
 Meeting_Aim VARCHAR(50), Staff_ID INT,
 FOREIGN KEY (Staff_ID) REFERENCES Staff (Staff_ID));