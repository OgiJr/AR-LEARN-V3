<!doctype html>
<html lang="en">
    <head>
        <!-- Required meta tags -->
        <meta charset="utf-8">
        <meta name="viewport" content="width=device-width, initial-scale=1, shrink-to-fit=no">
        <!-- Favicon -->
        <link rel="apple-touch-icon" sizes="180x180" href="/favicons/apple-touch-icon.png">
        <link rel="icon" type="image/png" sizes="32x32" href="/favicons/favicon-32x32.png">
        <link rel="icon" type="image/png" sizes="192x192" href="/favicons/android-chrome-192x192.png">
        <link rel="icon" type="image/png" sizes="16x16" href="/favicons/favicon-16x16.png">
        <link rel="manifest" href="/favicons/site.webmanifest">
        <link rel="mask-icon" href="/favicons/safari-pinned-tab.svg" color="#5bc0eb">
        <link rel="shortcut icon" href="/favicons/favicon.ico">
        <meta name="apple-mobile-web-app-title" content="ARLearn">
        <meta name="application-name" content="ARLearn">
        <meta name="msapplication-TileColor" content="#5bc0eb">
        <meta name="msapplication-TileImage" content="/favicons/mstile-144x144.png">
        <meta name="msapplication-config" content="/favicons/browserconfig.xml">
        <meta name="theme-color" content="#ffffff">
        <!-- Bootstrap CSS -->
        <link rel="stylesheet" href="https://stackpath.bootstrapcdn.com/bootstrap/4.3.1/css/bootstrap.min.css" integrity="sha384-ggOyR0iXCbMQv3Xipma34MD+dH/1fQ784/j6cY/iJTQUOhcWr7x9JvoRxT2MZw1T" crossorigin="anonymous">
        <!-- Our CSS-->
        <link rel="stylesheet" type="text/css" href="css/base.css">
        <title> ARLearn: Предай Пакет </title>
    </head>
    <body>
        <!-- Navbar -->
        <nav id="navbar"class="navbar navbar-expand-lg navbar-light bg-light">
            <!-- Title/Logo -->
            <a class="navbar-brand" href="index.html"> ARLearn </a>
            <button class="navbar-toggler" type="button" data-toggle="collapse" data-target="#navbarSupportedContent" aria-controls="navbarSupportedContent" aria-expanded="false" aria-label="Toggle navigation">
            <span class="navbar-toggler-icon"></span>
            </button>
            <div class="collapse navbar-collapse" id="navbarSupportedContent">
                <ul class="navbar-nav mr-auto">
                    <!-- Home Page -->
                    <li class="nav-item active">
                        <a class="nav-link" href="index.html"> Начало </a>
                    </li>
                    <!-- Authors -->
                    <li class="nav-item active">
                        <a class="nav-link" href="authors.html"> Автори </a>
                    </li>
                    <!-- Package Submission -->
                    <li class="nav-item active">
                        <a class="nav-link" href="submit.html"> Предай Пакет </a>
                    </li>
                    <!-- Downloads -->
                    <li class="nav-item dropdown">
                        <a class="nav-link dropdown-toggle" href="#" id="navbarDropdown" role="button" data-toggle="dropdown" aria-haspopup="true" aria-expanded="false"> Изтегляне </a>
                        <div class="dropdown-menu" aria-labelledby="navbarDropdown">
                            <a class="dropdown-item" href="#"> Google Play Store (Скоро) </a>
                            <a class="dropdown-item" href="#"> iOS App Store (Скоро) </a>
                            <div class="dropdown-divider"></div>
                            <a class="dropdown-item" href="#"> Download APK (Скоро)</a>
                        </div>
                    </li>
                </ul>
            </div>
        </nav>
        <!-- Hero -->
        <div id="hero">
            <!-- Form -->
            <div class="card" id="submitform">
                <div class="card-header">
                    Предай Пакет
                </div>
                <div class="card-body">
                    <?php
                        require_once 'PostNewTarget.php';

                        // Set utf8 support
                        header('Content-Type: text/html; charset=utf-8');
                        ini_set("default_charset", "UTF-8");
                        mb_internal_encoding("UTF-8");

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
                            die("<h3> Имаше проблем с заявката. Моля опитайте отново по-късно. </h3>");
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
                            if($stmt->num_rows == 0){
                                $stmt->close();
                                break;
                            }
                            $stmt->close();
                        }

                        // Create package
                        $stmt = $conn->prepare("INSERT INTO packages (id, name, description, models) VALUES (?, ?, ?, ?)");
                        checkstmt($stmt);

                        $stmt->bind_param("sssi", $id, $_POST["packagename"], $_POST["packagedesc"], intval($_POST["i"]));
                        $stmt->execute();
                        $stmt->close();

                        // Get stuff uploaded
                        $vuforiaaccesskey = "ACCESSKEY";
                        $vuforiaserverkey = "SERVERKEY";
                        $url = "https://vws.vuforia.com/targets";
                        $modeldir = "models/";
                        $markdowndir = "markdown/";
                        for($i = 0; $i < intval($_POST["i"]); $i++) {
                            // generate name
                            $name = "${id}_${i}";
                            // post to vuforia
                            $poster = new PostNewTarget($vuforiaaccesskey, $vuforiaserverkey, $name, $_FILES["m_pic{$i}"]["tmp_name"]);
                            // Upload model and MD data to SQL
                            if(!file_exists($modeldir))
                                mkdir($modeldir, 0777, true);
                            if(!file_exists($markdowndir))
                                mkdir($markdowndir, 0777, true);

                            if(strtolower(pathinfo($_FILES["m_model{$i}"]["name"])["extension"]) != "fbx" && pathinfo($_FILES["m_model{$i}"]["name"])["extension"] != "unity3d")
                                die("<h3> Файлът за модел " . ($i + 1) . " не е FBX или Unity Asset Bundle. </h3>");

                            if(strtolower(pathinfo($_FILES["m_info{$i}"]["name"])["extension"]) != "md")
                                die("<h3> Информационият файл за модел " . ($i + 1) . " не е Markdown. </h3>");

                            $extension = "fbx";
                            if(strtolower(pathinfo($_FILES["m_model{$i}"]["name"])["extension"]) == "unity3d")
                                $extension = "unity3d";

                            move_uploaded_file($_FILES["m_model${i}"]["tmp_name"], $modeldir . "${name}.${extension}");
                            move_uploaded_file($_FILES["m_info${i}"]["tmp_name"], $markdowndir . "${name}.md");
                            $stmt = $conn->prepare("INSERT INTO models (id, name, description, packageid, prefab) VALUES (?, ?, ?, ?, ?)");
                            checkstmt($stmt);
                            $isPrefab = ($extension == "unity3d");
                            $stmt->bind_param("isssi", $i, $_POST["m_name${i}"], $_POST["m_desc${i}"], $id, $isPrefab);
                            $stmt->execute();
                            $stmt->close();
                        }

                        $conn->close();
                        echo "<h2> Пакетът е предложен! Неговото id е: ${id}</h2>"
                            . "<button type=\"button\" class=\"btn btn-success\" onclick=\"window.location.href='index.html'\"> Начална страница </button> <br>"
                            . "<button  type=\"button\" class=\"btn btn-primary\" onclick=\"window.location.href='submit.html'\"> Предлагане на още един пакет </button>";
                    ?>
                </div>
            </div>
        </div>
        <!-- Footer -->
        <div class="clearfix"></div>
        <div id="footer" class="container-fluid">
            <div class="row">
                <div class="col-md-4">
                    <h4> © Copyright: arlearn.xyz </h4>
                </div>
                <div class="col-md-4">
                    <h4> b.radulov20@acsbg.org </h4>
                </div>
                <div class="col-md-4">
                    <h4> o.trajanov22@acsbg.org </h4>
                </div>
            </div>
        </div>
        <!-- Our JS -->
        <script src="js/submit.js"></script>
        <!-- Bootstrap JavaScript -->
        <!-- jQuery first, then Popper.js, then Bootstrap JS -->
        <script src="https://code.jquery.com/jquery-3.3.1.slim.min.js" integrity="sha384-q8i/X+965DzO0rT7abK41JStQIAqVgRVzpbzo5smXKp4YfRvH+8abtTE1Pi6jizo" crossorigin="anonymous"></script>
        <script src="https://cdnjs.cloudflare.com/ajax/libs/popper.js/1.14.7/umd/popper.min.js" integrity="sha384-UO2eT0CpHqdSJQ6hJty5KVphtPhzWj9WO1clHTMGa3JDZwrnQq4sF86dIHNDz0W1" crossorigin="anonymous"></script>
        <script src="https://stackpath.bootstrapcdn.com/bootstrap/4.3.1/js/bootstrap.min.js" integrity="sha384-JjSmVgyd0p3pXB1rRibZUAYoIIy6OrQ6VrjIEaFf/nJGzIxFDsf4x0xIM+B07jRM" crossorigin="anonymous"></script>
    </body>
</html>