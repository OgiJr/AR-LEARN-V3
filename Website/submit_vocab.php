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
        <title> ARLearn: Предай Дума </title>
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
                    <li class="nav-item dropdown">
                        <a class="nav-link dropdown-toggle" href="#" id="navbarDropdown" role="button" data-toggle="dropdown" aria-haspopup="true" aria-expanded="false"> Предаване </a>
                        <div class="dropdown-menu" aria-labelledby="navbarDropdown">
                            <a class="dropdown-item" href="submit.html"> Предай Пакет </a>
                            <a class="dropdown-item" href="submit_vocab.html"> Предай Думи </a>
                        </div>
                    </li>
                    <!-- Downloads -->
                    <li class="nav-item dropdown">
                        <a class="nav-link dropdown-toggle" href="#" id="navbarDropdown" role="button" data-toggle="dropdown" aria-haspopup="true" aria-expanded="false"> Изтегляне </a>
                        <div class="dropdown-menu" aria-labelledby="navbarDropdown">
                            <a class="dropdown-item" href="https://play.google.com/store/apps/details?id=com.ArLearn.ArLearn"> Google Play Store </a>
                            <a class="dropdown-item" href="#"> iOS App Store (Скоро) </a>
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
                        ini_set("default_charset", "UTF-8");
                        mb_internal_encoding("UTF-8");

                        // called on all sql statements. errors if bad
                        function checkstmt($stmt) {
                            if(!$stmt)
                                die("<h3> Имаше проблем. Моля опитайте отново по-късно. </h3>");
                        }

                        // Check to see if request is valid
                        if(!isset($_POST["word"]))
                            die("<h3> Въведете думата </h3>");
                        if(!isset($_POST["definition"]))
                            die("<h3> Въведете дефиницията </h3>");
                        if(mb_strlen($_POST["word"]) > 64)
                            die("<h3> Думата трябва да е не по-дълго от 64 знака. </h3>");
                        if(mb_strlen($_POST["definition"]) > 1024)
                            die("<h3> Дефиницията не трябва да е по-дълго от 1024 знака. </h3>");

                        // Connect to SQL DB
                        $db_servername = "localhost";
                        $db_username = "root";
                        $db_password = "PASSWORD";
                        $db_name = "arlearn";
                        $conn = new mysqli($db_servername, $db_username, $db_password, $db_name);
                        if($conn->connect_error)
                            die("<h3> Връзката с базата ни данни се провали! Опитайте отново по-късно! </h3>");
                        $conn->set_charset("utf8mb4");

                        // Create package
                        $stmt = $conn->prepare("INSERT INTO dictionary (word, definition ) VALUES (?, ?)");
                        checkstmt($stmt);

                        $stmt->bind_param("ss", $_POST["word"], $_POST["definition"]);
                        $stmt->execute();
                        $stmt->close();

                        $conn->close();
                        echo "<h2> Думата е предложена!</h2>"
                            . "<button type=\"button\" class=\"btn btn-success\" onclick=\"window.location.href='index.html'\"> Начална страница </button> <br>"
                            . "<button  type=\"button\" class=\"btn btn-primary\" onclick=\"window.location.href='submit_vocab.html'\"> Предлагане на още една дума </button>";
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