USE master
GO

-- Drop database if it exists
IF DB_ID('final_capstone') IS NOT NULL
BEGIN
	ALTER DATABASE final_capstone SET SINGLE_USER WITH ROLLBACK IMMEDIATE;
	DROP DATABASE final_capstone;
END

CREATE DATABASE final_capstone
GO

USE final_capstone
GO

-- Create Tables
CREATE TABLE family (
	family_id int IDENTITY (1, 1) NOT NULL
	-- family_name varchar(50) ??? add later?
	PRIMARY KEY (family_id)
)

CREATE TABLE users (
	user_id int IDENTITY(1,1) NOT NULL,
	username varchar(50) NOT NULL,
	password_hash varchar(200) NOT NULL,
	salt varchar(200) NOT NULL,
	user_role varchar(50) NOT NULL, 
	family_id int NOT NULL,

	PRIMARY KEY (user_id),
	FOREIGN KEY (family_id) REFERENCES family(family_id)
)

CREATE TABLE book(
	book_id int IDENTITY(1,1) NOT NULL,
	title varchar(255) NOT NULL,
	author_firstName varchar(50) NOT NULL,
	author_lastName varchar(50) NOT NULL,
	isbn bigint NOT NULL,
	--maybe link isbn to format?

	PRIMARY KEY (book_id)
)
--add a book url
CREATE TABLE reading_format(
	format_id int IDENTITY(1, 1) NOT NULL,
	format_type varchar(50) NOT NULL,

	PRIMARY KEY (format_id)
)


CREATE TABLE personal_library(
	ID INT IDENTITY(1, 1) NOT NULL,
	user_id int NOT NULL,
	book_id int NOT NULL,
	isCompleted int NOT NULL

	CHECK(isCompleted <= 1 AND isCompleted >= 0),
	PRIMARY KEY(ID), 
	FOREIGN KEY (user_id) REFERENCES users(user_id),
	FOREIGN KEY (book_id) REFERENCES book(book_id),
)

CREATE TABLE family_library (
	family_id int NOT NULL,
	book_id int NOT NULL,

	FOREIGN KEY (family_id) REFERENCES family(family_id),
	FOREIGN KEY (book_id) REFERENCES book(book_id)
)


CREATE TABLE reading_log(
	reading_log_id int IDENTITY(1,1) NOT NULL,
	personal_library_id int NOT NULL,
	format_id int NOT NULL,
	total_time int NOT NULL,
	notes varchar(255)

	PRIMARY KEY (reading_log_id),
	FOREIGN KEY (personal_library_id) REFERENCES personal_library(id),
	FOREIGN KEY (format_id) REFERENCES reading_format(format_id)
)

-- Populate default data for testing: user and admin with password of 'password'
-- These values should not be kept when going to Production
INSERT INTO family DEFAULT VALUES;
INSERT INTO family DEFAULT VALUES;

INSERT INTO users (username, password_hash, salt, user_role, family_id) 
VALUES 
	('parent','Jg45HuwT7PZkfuKTz6IB90CtWY4=','LHxP4Xh7bN0=','parent', 1),
	('child','YhyGVQ+Ch69n4JMBncM4lNF/i9s=', 'Ar/aB2thQTI=','child', 1),
	('test11','P1n7OKD0UZv6x6s5UguksbdqNkY=', 'z2CZYXw40Rc=','parent', 2),
	('test12','F4AqN3NQ+UsJyZZRCzDg3U6C7mA=', 'IFuP6bTa4WE=','parent', 2),
	('test13','GqjbxhkbMUqmachwMF3Z5VU6NZo=', 'mLeFk7Ub16o=','child', 2),
	('test14','vDjyBpvCAbpb4ZYpV+aZ8tSBHE0=', 'biDQv1eD7p4=','child', 2),
	('test15','gES0wgFWu3X+JmZa1EcBzAiJD8k=', 'gDbWwxonR8I=','child', 2);


INSERT INTO reading_format (format_type) VALUES ('Paperback'), ('ebook'), ('Audiobook'), ('Read-Aloud (Reader)'), ('Read-Aloud (Listener)'), ('Other');

INSERT INTO book (title, author_firstName, author_lastName, isbn) VALUES ('HitchHikers Guide To the Galaxy', 'Douglas', 'Adams', 9781529046137);
INSERT INTO book (title, author_firstName, author_lastName, isbn) VALUES ('The Hobbit', 'J.R.R', 'Tolken', 9780345253422);
INSERT INTO book (title, author_firstName, author_lastName, isbn) VALUES ('Dune', 'Frank', 'Herbert', 9780425027066)
INSERT INTO book (title, author_firstName, author_lastName, isbn) VALUES ('Catching Fire', 'Suzanne', 'Collins', 9781407132099);
INSERT INTO family_library (family_id, book_id) 
VALUES
	(1, 1),
	(1, 2),
	(1, 3),
	(1, 4)

INSERT INTO personal_library(user_id, book_id, isCompleted) 
VALUES 
	(1, 1, 0), 
	(1, 2, 0),
	(1, 3, 0),
	(2, 2, 0);



GO


SELECT DISTINCT
	b.book_id AS book_id,
	b.title AS title,
	b.author_firstName AS author_first,
	b.author_lastName AS author_last,
	b.isbn AS isbn
FROM
	users u
	INNER JOIN personal_library pl ON u.user_id = pl.user_id
	INNER JOIN book b ON b.book_id = pl.book_id
WHERE
	u.family_id = (SELECT family_id FROM users WHERE user_id = 2);

--displays family library
SELECT 
	b.title,
	b.author_firstname,
	b.author_lastname
FROM
	users u
	INNER JOIN personal_library pl ON u.user_id = pl.user_id
	INNER JOIN book b ON b.book_id = pl.book_id
WHERE
	u.family_id = 1
GROUP BY
	b.title,
	b.author_firstname,
	b.author_lastname;



-- Non user registers (just needs to add family library)
--INSERT INTO users
--	(username,
--	password_hash,
--	salt,
--	user_role,
--	family_id)
--VALUES
--	('a',
--	'asdf',
--	'asdf',
--	'parent',
--	(1 + (SELECT MAX(family_id)
--	FROM users)))

--user registers new family member
--INSERT INTO users
--	(username,
--	password_hash,
--	salt,
--	user_role,
--	family_id)
--VALUES
--	('a',
--	'asdf',
--	'asdf',
--	'child',
--	(SELECT family_id
--	FROM users
--	WHERE user_id = 4))


SELECT * FROM users

SELECT * FROM book

SELECT * FROM reading_log

SELECT * FROM personal_library

SELECT 
	MAX(family_id)
FROM
	users

--INSERT INTO family DEFAULT VALUES;

SELECT * from users

SELECT DISTINCT * from family_library

SELECT DISTINCT
	b.book_id AS book_id,
	b.title AS title,
	b.author_firstName AS author_first,
	b.author_lastName AS author_last,
	b.isbn AS isbn
FROM
	users u
	INNER JOIN family_library fl ON u.family_id = fl.family_id
	INNER JOIN book b ON fl.book_id = b.book_id
WHERE
	u.user_id = 1

SELECT * FROM family_library

