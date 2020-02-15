<?php
error_reporting(-1);
ini_set('display_errors', 'On');    

function array_remove_object(&$array, $value, $prop)
{
    return array_filter($array, function($a) use($value, $prop) {
        return $a[$prop] !== $value;
    });
} 
function Register($name, $ip, $domain, $style, $ownerid)
{
    $lobs = "";
    if ( file_exists("lobby.json")) {
        $lobs = file_get_contents("lobby.json");
    }
    $lobby = json_decode($lobs, true); 
    //print_r($lobby["data"]);
    if ( $lobby == null) {
     $lobby = array("data");
    }

    $arr =array("Name" => $name, "IP"=>$ip, "Domain"=>$domain, "WorldType"=>$style, "Owner"=>$ownerid, "Users"=>"0", "LastUsed"=>"2019-01-30");
    //array_push($lobby["data"][], $arr);
    
    $lobby2 = array();
    //remove any existing server with same UserID
    $lobby2["data"] = array_remove_object($lobby["data"], $ownerid, "Owner");
    
    $lobby2["data"][]= json_decode(json_encode($arr), true);
    
    $json = json_encode($lobby2);
    file_put_contents("lobby.json", $json . "\n");
    echo "New Server Registered with Lobby.";
}

function GetSafe($s){
    if (isset($_REQUEST[$s])) {
        return $_REQUEST[$s];
    }
    else {
    return "";
    }
}

if (isset($_REQUEST["f"])){
    $f = GetSafe("f");
    $name = GetSafe("name");
    $ip = GetSafe("ip");
    $domain = GetSafe("domain");
    $style = GetSafe("style");
    $ownerid = GetSafe("ownerid");    
    Register($name, $ip, $domain, $style, $ownerid);
   // echo "Registered";
}
else {

    $data = file_get_contents("lobby.json");
    //TODO: REMOVE Owner from $data (to avoid server owners overwriting each other maliciously)
    //$json = file_get_contents('icanhazip.co');
    $data = str_replace("{LOCALIP}", "197.83.235.177", $data);
    echo $data;
}

?>