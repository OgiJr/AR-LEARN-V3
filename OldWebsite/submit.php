<?php
	require_once 'PostNewTarget.php';

	// called on bad request
	function badrequest() {
		die("You missed one of the request fields. Please try again.");
	}

	// called on all sql statements. errors if bad
	function checkstmt($stmt) {
		if(!$stmt)
			die("Internal error. Please try again later.");
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

	// Set file size limit to 100MB
	ini_set("upload_max_filesize", "100M");

	// Check to see if request is valid
	if(!isset($_POST["packagename"]) ||
	   !isset($_POST["packagedesc"]) ||
	   !isset($_POST["i"]))
		badrequest();

	for($i = 0; $i < intval($_POST["i"]); $i++)
		if(!isset($_POST["m_name${i}"]) ||
		   !isset($_POST["m_desc${i}"]) ||
		   !is_uploaded_file($_FILES["m_pic${i}"]["tmp_name"]) ||
		   !is_uploaded_file($_FILES["m_model${i}"]["tmp_name"]))
			badrequest();

	// Upload to SQL DB
	$db_servername = "localhost";
	$db_username = "root";
	$db_password = "FuckingCunt123";
	$db_name = "arlearn";
	$conn = new mysqli($db_servername, $db_username, $db_password, $db_name);
	if($conn->connect_error)
		die("Connection to the SQL database failed!");

	// Generate ids until we get one free
	again:
	$id = packageid();
	$stmt = $conn->prepare("SELECT * FROM packages WHERE id = ?");
	checkstmt($stmt);
	$stmt->bind_param("s", $id);
	$stmt->execute();
	if($stmt->num_rows > 0){
		$stmt->close();
		goto again;
	}
	$stmt->close();

	// Create package
	$stmt = $conn->prepare("INSERT INTO packages (id, name, description, models) VALUES (?, ?, ?, ?)");
	checkstmt($stmt);
	$stmt->bind_param("sssi", $id, $_POST["packagename"], $_POST["packagedesc"], intval($_POST["i"]));
	$stmt->execute();
	$stmt->close();


	// Get stuff uploaded
	$vuforiaaccesskey = "02290bf9304907b3f96dea3f8f2cbd08b0168e39";
	$vuforiaserverkey = "fea84fd7dec8ecc72a7a56e3908227ccd22b5a8c";
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
			die("Model file is not FBX or Unity Prefab");

		if(strtolower(pathinfo($_FILES["m_info{$i}"]["name"])["extension"]) != "md")
			die("Info file is not makrdown.");

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
	echo "<b> Your package's id is ${id}! </b>";

?>
