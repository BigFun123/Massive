<?php

print_r($_FILES);
     if ($_FILES["file"]["error"] > 0){
         echo "Error: " . $_FILES["file"]["error"];
     } else {
         echo "Upload: " . $_FILES["file"]["name"];
         echo "Type: " . $_FILES["file"]["type"];
         echo "Size: " . ($_FILES["file"]["size"] / 1024) . "kB";
         if(file_exists("images/" . $_FILES["file"]["name"])) {
             echo $_FILES["file"]["name"] . "already exists. ";
         } else {
             move_uploaded_file($_FILES["file"]["tmp_name"], "files/" . $_FILES["file"]["name"]);
         }
         echo "Stored in: " . $_FILES["file"]["name"];
     }
     ?>