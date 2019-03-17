# ARLearn Website
The ARLearn needs an SQL server, php and HTTP_Request2.

## HTTP_Request2
You need `HTTP_Request2` which you can get by running `pear install HTTP_Request2`.

## SQL
To set up the SQL DB for the models, use the following commands.
```
CREATE DATABASE arlearn CHARACTER SET utf8mb4 COLLATE utf8mb4_unicode_ci;

CREATE TABLE `arlearn`.`packages` (
	`id` VARCHAR(7) NOT NULL,
	`name` VARCHAR(255) NOT NULL,
	`description` VARCHAR(1023) NOT NULL,
	`models` int NOT NULL,
	PRIMARY KEY (`id`));

CREATE TABLE `arlearn`.`models` (
	`id` int NOT NULL,
	`name` VARCHAR(255) NOT NULL,
	`description` VARCHAR(1023) NOT NULL,
	`packageid` VARCHAR(7) NOT NULL,
	PRIMARY KEY (`id`));
```

## Authentication
Run the following commands to set up the authentication in the php files:
```
chmod +x setup.sh
./setup.sh [password] [vuforia access keys] [vuforia secret key]
```