use scoring_system;

create table login(
	id INTEGER PRIMARY KEY NOT NULL IDENTITY(1,1),
	login VARCHAR(32) NOT NULL UNIQUE,
	password VARCHAR(32) NOT NULL
);

INSERT into login(login, password) VALUES('admin','admin')

drop table login;

select * from login;