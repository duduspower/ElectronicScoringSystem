CREATE DATABASE scoring_system;

use scoring_system;

create table login(
	id INTEGER PRIMARY KEY NOT NULL IDENTITY(1,1),
	login VARCHAR(32) NOT NULL UNIQUE,
	password VARCHAR(32) NOT NULL
);

CREATE TABLE question(
	id INTEGER Primary Key NOT NULL IDENTITY(1,1),
	value VARCHAR(512) NOT NULL,
	correct_answear_value VARCHAR(512) NOT NULL,
);

CREATE TABLE answear(
	id INTEGER Primary Key NOT NULL IDENTITY(1,1),
	value VARCHAR(512) NOT NULL
);

CREATE TABLE question_to_answears(
	answear_id INTEGER PRIMARY KEY NOT NULL,
	question_id INTEGER NOT NULL
);

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
);

CREATE TABLE teacher(
	id INTEGER Primary Key NOT NULL IDENTITY(1,1),
	name VARCHAR(36) NOT NULL,
	surname VARCHAR(36) NOT NULL,
	academic_title VARCHAR(64) NOT NULL,
	email VARCHAR(64) NOT NULL,
	phone VARCHAR(64) NOT NULL,
	login_id INT NOT NULL UNIQUE
);

CREATE TABLE test(
	id INTEGER Primary Key NOT NULL IDENTITY(1,1),
	name VARCHAR(36) NOT NULL,
	students_class INTEGER NOT NULL
);

CREATE TABLE test_to_questions(
	question_id INTEGER PRIMARY KEY NOT NULL,
	test_id INTEGER NOT NULL
);

CREATE TABLE test_atempt(
	id INTEGER Primary Key NOT NULL IDENTITY(1,1),
	name VARCHAR(36) NOT NULL,
	test_id INTEGER NOT NULL,
	correct_answears INTEGER NOT NULL,
	incorrect_answears INTEGER NOT NULL,
	student_id INTEGER NOT NULL
);
