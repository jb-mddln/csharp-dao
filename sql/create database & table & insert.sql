-- postgreSQL pgAdmin4
DROP DATABASE IF EXISTS "Simplon";

CREATE DATABASE "Simplon" ENCODING "UTF8";

DROP TABLE IF EXISTS dogs;

CREATE TABLE dogs
(
	id SERIAL PRIMARY KEY,
    name VARCHAR(255) NOT NULL,
    breed VARCHAR(255),
    birth_date DATE
);

INSERT INTO dogs (name, breed, birth_date) 
	VALUES ('Fido', 'Corgi', '2022-01-04'), 
	('Rex', 'Dalmatian', '2015-05-04'), 
	('Jean Marc', 'Daschund', '2019-07-25');
