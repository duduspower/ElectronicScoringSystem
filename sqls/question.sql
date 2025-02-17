use scoring_system;

CREATE TABLE question(
	id INTEGER Primary Key NOT NULL IDENTITY(1,1),
	value VARCHAR(512) NOT NULL,
	correct_answear_value VARCHAR(512) NOT NULL,
);

CREATE TABLE answear(
	id INTEGER Primary Key NOT NULL IDENTITY(1,1),
	value VARCHAR(512) NOT NULL
)

CREATE TABLE question_to_answears(
	answear_id INTEGER PRIMARY KEY NOT NULL,
	question_id INTEGER NOT NULL
)


DROP TABLE question;

select * from question;
select * from test;
select * from question_to_answears;
drop table answear;


delete from question;
delete from answear;
delete from question_to_answears;
delete from test_to_questions;
delete from test;

SELECT * FROM answear a inner join question_to_answears qta on qta.answear_id=a.id
