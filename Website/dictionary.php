<?php
	// Set utf8 support
    header('Content-Type: text/html; charset=utf-8');
    ini_set("default_charset", "UTF-8");
    mb_internal_encoding("UTF-8");

    // Connect to SQL DB
    $db_servername = "localhost";
    $db_username = "root";
    $db_password = "PASSWORD";
    $db_name = "arlearn";
    $conn = new mysqli($db_servername, $db_username, $db_password, $db_name);
    if($conn->connect_error)
        die("<h3> Връзката с базата ни данни се провали! Опитайте отново по-късно! </h3>");
    $conn->set_charset("utf8mb4");

    // Get request
    if(isset($_GET["word"])) {
		$id = strtolower($_GET["word"]);
		$stmt = $conn->prepare("SELECT definition FROM dictionary WHERE word = ?");
		if($stmt) {
			$stmt->bind_param("s", $id);
			$stmt->execute();
			$stmt->bind_result($def);
			$stmt->fetch();
			echo $def;
			$stmt->close();
		}
    }
    $conn->close();
?>