<?php
	require_once 'PostNewTarget.php';

	// Set utf8 support
	header('Content-Type: text/html; charset=utf-8');
	ini_set("default_charset", "UTF-8");
	mb_internal_encoding("UTF-8");

	// Set file size limit to 100MB
	ini_set("upload_max_filesize", "200M");

	// called on all sql statements. errors if bad
	function checkstmt($stmt) {
		if(!$stmt)
			die("<h3> Имаше проблем. Моля опитайте отново по-късно. </h3>");
	}

	// generate alphanumeric package id (3.7e41 unique ids)
	function packageid() {
		$length = 8;
		$keyspace = "0123456789abcdefghijklmnopqrstuvwxyz";
		$keyspace_len = strlen($keyspace) - 1;
		$id = "";
		for($i = 0; $i < 8; $i++) {
			$id .= $keyspace[random_int(0, $keyspace_len)];
		}

		return $id;
	}

	// encoding for the vuforia api
	function hextobase64($hex){
		$return = "";
		foreach(str_split($hex, 2) as $pair){
			$return .= chr(hexdec($pair));
		}
		return base64_encode($return);
	}

	// Check to see if request is valid
	if(!isset($_POST["packagename"]))
		die("<h3> Въведете име на пакета </h3>");
	if(!isset($_POST["packagedesc"]))
		die("<h3> Въведете описание на пакета </h3>");
	if(!isset($_POST["i"]))
		die("<h3> Имаше проблем. Моля опитайте отново по-късно. </h3>");
	if(mb_strlen($_POST["packagename"]) > 64)
		die("<h3> Името на пакета не трябва да е по дълго от 64 знака. </h3>");
	if(mb_strlen($_POST["packagedesc"]) > 64)
		die("<h3> Описанието на пакета не трябва да е по дълго от 256 знака. </h3>");

	for($i = 0; $i < intval($_POST["i"]); $i++) {
		if(!isset($_POST["m_name${i}"]))
			die("<h3> Пропуснали сте името на модел " . ($i + 1) . "</h3>");
		if(!isset($_POST["m_desc${i}"]))
			die("<h3> Пропуснали сте описанието на модел " . ($i + 1) . "</h3>");
		if(!is_uploaded_file($_FILES["m_pic${i}"]["tmp_name"]))
			die("<h3> Пропуснали сте изображението на модел " . ($i + 1) . "</h3>");
		if(!is_uploaded_file($_FILES["m_model${i}"]["tmp_name"]))
			die("<h3> Пропуснали сте модела на модел " . ($i + 1) . "</h3>");
		if(!is_uploaded_file($_FILES["m_info${i}"]["tmp_name"]))
			die("<h3> Пропуснали сте информацията за модел " . ($i + 1) . "</h3>");
	}

	// Connect to SQL DB
	$db_servername = "localhost";
	$db_username = "rooty";
	$db_password = "PASSWORD";
	$db_name = "arlearn";
	$conn = new mysqli($db_servername, $db_username, $db_password, $db_name);
	if($conn->connect_error)
		die("<h3> Връзката с базата ни данни се провали! Опитайте отново по-късно! </h3>");
	$conn->set_charset("utf8mb4");

	// Generate ids until we get one free
	$id = packageid();
	while(1) {
		$stmt = $conn->prepare("SELECT * FROM packages WHERE id = ?");
		checkstmt($stmt);
		$stmt->bind_param("s", $id);
		$stmt->execute();
		if($stmt->num_rows > 0){
			$stmt->close();
			break;
		}
	}
	$stmt->close();

	// Create package
	$stmt = $conn->prepare("INSERT INTO packages (id, name, description, models) VALUES (?, ?, ?, ?)");
	checkstmt($stmt);
	echo "AAA";
	$stmt->bind_param("sssi", $id, $_POST["packagename"], $_POST["packagedesc"], intval($_POST["i"]));
	$stmt->execute();
	$stmt->close();

	// Get stuff uploaded
	$vuforiaaccesskey = "ACCESSKEY";
	$vuforiaserverkey = "SERVERKEY";
	$url = "https://vws.vuforia.com/targets";
	$modeldir = "models/";
	$markdowndir = "markdown/";
	for($i = 0; $i < $_POST["i"]; $i++) {
		// generate name
		$name = "${id}_${i}";
		// post to vuforia
		$poster = new PostNewTarget($name, $_FILES["m_pic{$i}"]["tmp_name"], $vuforiaaccesskey, $vuforiaserverkey);
		// Upload model and MD data to SQL
		if(!file_exists($modeldir))
			mkdir($modeldir, 0777, true);
		if(!file_exists($markdowndir))
			mkdir($markdowndir, 0777, true);
		
		if(strtolower(pathinfo($_FILES["m_model{$i}"]["name"])["extension"]) != "fbx" && pathinfo($_FILES["m_model{$i}"]["name"])["extension"] != "prefab")
			die("<h3> Файлът за модел " . ($i + 1) . " не е FBX или Unity Prefab. </h3>");

		if(strtolower(pathinfo($_FILES["m_info{$i}"]["name"])["extension"]) != "md")
			die("<h3> Информационият файл за модел " . ($i + 1) . " не е Markdown. </h3>");

		$extension = "fbx";
		if(strtolower(pathinfo($_FILES["m_model{$i}"]["name"])["extension"]) == "prefab")
			$extension = "prefab";

		move_uploaded_file($_FILES["m_model${i}"]["tmp_name"], $modeldir . "${name}.${extension}");
		move_uploaded_file($_FILES["m_info${i}"]["tmp_name"], $markdowndir . "${name}.md");
		$stmt = $conn->prepare("INSERT INTO models (id, name, description, packageid) VALUES (?, ?, ?, ?)");
		checkstmt($stmt);
		$stmt->bind_param("isss", $i, $_POST["m_name${i}"], $_POST["m_desc${i}"], $id);
		$stmt->execute();
		$stmt->close();
	}

	$conn->close();
	echo "<h2> Пакетът е предложен! Неговото id е: ${id}</h2>"
		. "<button type=\"button\" class=\"btn btn-success\"> Начална страница </button>"
		. "<button  type=\"button\" class=\"btn btn-primary\"> Предлагане на още един пакет </button>";

?>
