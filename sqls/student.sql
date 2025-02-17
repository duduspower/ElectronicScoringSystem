CREATE TABLE student(
	id INTEGER Primary Key NOT NULL IDENTITY(1,1),
	name VARCHAR(36) NOT NULL,
	surname VARCHAR(36) NOT NULL,
	date_of_birth DATE NOT NULL,
	email VARCHAR(64) NOT NULL,
	phone CHAR(12) NOT NULL,
	student_index CHAR(6) NOT NULL UNIQUE,
	login_id INTEGER UNIQUE
);

SELECT * FROM student;

INSERT INTO student VALUES(1,'Dominik', 'Duda', '2003-02-19', 'dominikduda19@gmail.com', '+48512123234', 'w69775', null);

Drop table student;