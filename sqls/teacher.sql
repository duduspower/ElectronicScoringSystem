use scoring_system

CREATE TABLE teacher(
	id INTEGER Primary Key NOT NULL IDENTITY(1,1),
	name VARCHAR(36) NOT NULL,
	surname VARCHAR(36) NOT NULL,
	academic_title VARCHAR(64) NOT NULL,
	email VARCHAR(64) NOT NULL,
	phone VARCHAR(64) NOT NULL,
	login_id INT NOT NULL UNIQUE
);

SELECT * FROM teacher;

drop table teacher;