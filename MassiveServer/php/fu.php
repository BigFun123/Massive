<?php
ini_set('display_errors', 1);
ini_set('display_startup_errors', 1);
error_reporting(E_ALL);
//phpInfo();

if ( empty($_FILES["file"])){
echo "WARNING: FILES is EMPTY";
}


$uploads_dir = 'files/'; //Directory to save the file that comes from client application.
$host = "http://{HOST}/massive/fu/";

$tempfilepath = $_FILES["file"]["tmp_name"];

    //move_uploaded_file($filepath,"test_file.txt");
    //print_r($_REQUEST);
    //print_r($_FILES);




function is_jpeg(&$pict)
  {
    return (bin2hex($pict[0]) == 'ff' && bin2hex($pict[1]) == 'd8');
  }

  function is_png(&$pict)
  {
    return (bin2hex($pict[0]) == '89' && $pict[1] == 'P' && $pict[2] == 'N' && $pict[3] == 'G');
  }

function GUID()
{
    if (function_exists('com_create_guid') === true)
    {
        return trim(com_create_guid(), '{}');
    }

    return sprintf('%04X%04X-%04X-%04X-%04X-%04X%04X%04X', mt_rand(0, 65535), mt_rand(0, 65535), mt_rand(0, 65535), mt_rand(16384, 20479), mt_rand(32768, 49151), mt_rand(0, 65535), mt_rand(0, 65535), mt_rand(0, 65535));
}

try {

    if(!empty($tempfilepath)) {        
        $type = exif_imagetype($tempfilepath);
        //$type = mime_content_type($tempfilepath);
        if ( $type === IMAGETYPE_PNG) $extension = ".png";
        else
        if ( $type === IMAGETYPE_JPEG) $extension = ".jpg"; 
        else
        $extension = ".jpg";    

            $destpath = $uploads_dir . GUID() . $extension;
            if ( move_uploaded_file($tempfilepath, $destpath)) {
              echo $host.$destpath;
            } else{
                echo "ERROR moving " . $path;
            }
          }
        else {
               echo "ERROR in filename " . $_FILES["file"];
        }
}
catch( Exception $e){
    echo "Exception thrown: " . $e->GetMessage();
}
?>