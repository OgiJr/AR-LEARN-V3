# SQL
To set up the SQL DB for the models, use the following commands.
```
CREATE DATABASE arlearn CHARACTER SET utf8mb4 COLLATE utf8mb4_unicode_ci;

CREATE TABLE `arlearn`.`packages` (
<<<<<<< HEAD
	`id` VARCHAR(64) NOT NULL,
	`name` VARCHAR(256) NOT NULL,
	`description` VARCHAR(256) NOT NULL,
=======
	`id` VARCHAR(7) NOT NULL,
	`name` VARCHAR(255) NOT NULL,
	`description` VARCHAR(1023) NOT NULL,
>>>>>>> f658a2ea6b44743dbdcb32d1334c0903b6b8351f
	`models` int NOT NULL,
	PRIMARY KEY (`id`));

CREATE TABLE `arlearn`.`models` (
	`id` int NOT NULL,
<<<<<<< HEAD
	`name` VARCHAR(256) NOT NULL,
	`description` VARCHAR(256) NOT NULL,
	`packageid` VARCHAR(64) NOT NULL,
=======
	`name` VARCHAR(255) NOT NULL,
	`description` VARCHAR(1023) NOT NULL,
	`packageid` VARCHAR(7) NOT NULL,
>>>>>>> f658a2ea6b44743dbdcb32d1334c0903b6b8351f
	PRIMARY KEY (`id`));
```

Make sure to go in `submit.php` and set up the SQL login details. Set up your vuforia key in the same file.
Some of the code was taken from the developer provided examples from the vuforia website.
You also need `HTTP_Request2` which you can get by running `pear install HTTP_Request2`.