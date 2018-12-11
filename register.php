<?php
  $con = mysqli_init('localhost', 'root', 'root','unityaccess');

  //checking if connection happened...
  if(mysqli_connect_errno())
  {
    echo "1: Connection failed";
    exit();
  }

  $username = $_POST["username"];
  $password = $_POST["password"];

  $namecheckquery = "SELECT username FROM userinfo WHERE username = '" . $username . "';";
  $namecheck = mysqli_query($con, $namecheckquery) or die("2: Name check query failed");

  if(mysqli_num_rows($namecheck)>0)
  {
    echo "3: Name already exists";
    exit();
  }

  $salt = "\$5\$rounds=5000\$" . "steamedhams" . $username . "\$";
  $hash = crypt($password, $salt);
  $insertuserquery = "INSERT INTO userinfo (username, hash, salt) VALUES ('" . $username . "', '" . $hash ."',  '" . $salt . "');";
  mysqli_query($con, $insertuserquery) or die("4: Insert player query failed");

  echo "0";
 ?>
