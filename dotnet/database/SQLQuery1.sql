--displays family library
SELECT DISTINCT
	b.book_id,
	b.title,
	b.author_firstname,
	b.author_lastname,
	b.isbn
FROM
	users u
	INNER JOIN personal_library pl ON u.user_id = pl.user_id
	INNER JOIN book b ON b.book_id = pl.book_id
WHERE
	u.family_id = 1;


-- Non user registers (just needs to add family library)
INSERT INTO users
	(username,
	password_hash,
	salt,
	user_role,
	family_id)
VALUES
	('a',
	'asdf',
	'asdf',
	'parent',
	(1 + (SELECT MAX(family_id)
	FROM users)))

-- user registers new family member
INSERT INTO users
	(username,
	password_hash,
	salt,
	user_role,
	family_id)
VALUES
	('a',
	'asdf',
	'asdf',
	'child',
	(SELECT family_id
	FROM users
	WHERE user_id = 4))


SELECT * FROM users

SELECT 
	MAX(family_id)
FROM
	users

SELECT *
FROM personal_library

--Add a book to library
INSERT INTO
WHERE


-- get children of a family account
SELECT
	user_id,
	username
FROM
	users
WHERE
	user_role = 'child'
	AND family_id = @family_id

SELECT
	
FROM
	users u
	INNER JOIN family_library fl ON u.family_id = fl.family_id
	INNER JOIN book b ON fl.book_id = b.book_id
WHERE
	u.user_id = 1