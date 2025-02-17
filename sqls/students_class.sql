use scoring_system;

CREATE TABLE students_class(
	id INTEGER Primary Key NOT NULL IDENTITY(1,1),
	name VARCHAR(32) NOT NULL UNIQUE,
	teacher_id INTEGER NOT NULL
);

CREATE TABLE students_class_to_tests(
	test_id INTEGER PRIMARY KEY NOT NULL,
	students_class_id INTEGER NOT NULL
);

CREATE TABLE students_class_to_students(
	student_id INTEGER NOT NULL,
	students_class_id INTEGER NOT NULL
)

select * from students_class_to_tests;
delete from students_class_to_tests;