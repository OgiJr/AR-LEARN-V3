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
    if(isset($_GET["id"])) {
		$id = $_GET["id"];
		$stmt = $conn->prepare("SELECT name, description, models FROM packages WHERE id = ?");
		if($stmt) {
			$stmt->bind_param("s", $id);
			$stmt->execute();
			$stmt->bind_result($name, $desc, $models);
			$stmt->fetch();
			$data = array(
				"name" => $name,
				"description" => $desc,
				"models" => $models,
			);
			$stmt->close();
			
			$arePrefabs = array();
			$stmt2 = $conn->prepare("SELECT prefab FROM models WHERE packageid = ?");
			if($stmt2) {
				$stmt2->bind_param("s", $id);
				$stmt2->execute();
				$stmt2->bind_result($isPrefab);
				$i = 0;
				while($stmt2->fetch()) {
					$arePrefabs[$i++] = $isPrefab;
				}
				$data["arePrefabs"] = $arePrefabs;
				echo json_encode($data);
				$stmt2->close();
			}
		}
    }

    $conn->close();
?>