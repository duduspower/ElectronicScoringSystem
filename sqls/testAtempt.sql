use scoring_system;

CREATE TABLE test_atempt(
	id INTEGER Primary Key NOT NULL IDENTITY(1,1),
	name VARCHAR(36) NOT NULL,
	test_id INTEGER NOT NULL,
	correct_answears INTEGER NOT NULL,
	incorrect_answears INTEGER NOT NULL,
	student_id INTEGER NOT NULL
);

select * from test_atempt;

drop table test_atempt;