<?php

	$id = $_GET["id"];

	$db_servername = "localhost";
	$db_username = "root";
	$db_password = "FuckingCunt123";
	$db_name = "arlearn";
	$conn = new mysqli($db_servername, $db_username, $db_password, $db_name);

	$stmt = $conn->prepare("SELECT name, description, models FROM packages WHERE id = ?");
	$stmt->bind_param("s", $id);
	$stmt->execute();
	$stmt->bind_result($name, $desc, $models);
	$stmt->fetch();
	
	$data = array("name" => $name, "description" => $desc, "nofmodels" => $models);
	echo $models;

	$stmt->close();
	$conn->close();
?>
