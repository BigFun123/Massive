<!DOCTYPE html>
<html>
    <head>
        <title>Receiving data with Postman</title>
    </head>

    <?php 
        $dir = '/';
        $file = basename($_FILES['image']['name']);

        if (move_uploaded_file($_FILES['image']['tmp_name'], $file)) {
            echo "Ok.\n";

        } else {
            echo "Error.\n";
			var_dump($_FILES);
var_dump($_REQUEST);
        }
    ?>
    <img src="<?=$file?>"></img>

</html>