use scoring_system;

CREATE TABLE test(
	id INTEGER Primary Key NOT NULL IDENTITY(1,1),
	name VARCHAR(36) NOT NULL,
	students_class INTEGER NOT NULL
);

CREATE TABLE test_to_questions(
	question_id INTEGER PRIMARY KEY NOT NULL,
	test_id INTEGER NOT NULL
)

select * from test;