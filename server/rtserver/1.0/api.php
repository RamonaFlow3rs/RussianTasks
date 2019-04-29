<?php
//enableErrorsLog();
define("LOG_ENABLED", false);

include 'activation.php';

$REQUEST_SALT = "g#d5Ao@Pdu3#Aw1!dk";

$ACTION_ACTIVATION = 			"activation";
$ACTION_CHECK_NEW_VERSION = 	"check_new_version";

$PARAM_ACTION = 				"action";
$PARAM_DATA = 					"data";
$PARAM_ERROR =					"error";
$PARAM_TIME = 					"time";
$PARAM_HASH = 					"hash";
$PARAM_VERSION =				"version";
$PARAM_URL = 					"url";
$PARAM_MD5 = 					"md5";
$PARAM_USERNAME = 				"u";
$PARAM_USERKEY = 				"k";
$PARAM_MACHINE_ID = 			"i";


#======== request accepting ========
$DISABLE_REQUEST_CHECKING = false;
$time = $_REQUEST[$PARAM_TIME];
$hash = $_REQUEST[$PARAM_HASH];
if (!$DISABLE_REQUEST_CHECKING && (empty($time) || empty($hash) || md5(sprintf("%s_%s", $time, $REQUEST_SALT)) != $hash)) exit();
#===================================

$action = $_REQUEST[$PARAM_ACTION];
if ($action == $ACTION_ACTIVATION) {
	$result = checkActivation($_REQUEST[$PARAM_USERNAME], $_REQUEST[$PARAM_USERKEY], $_REQUEST[$PARAM_MACHINE_ID]);
	$data = array();
	$data["result"] = $result;
} else if ($action == $ACTION_CHECK_NEW_VERSION) {
	$fileName = "russian_tasks_1.4.1.msi";
	$fileVersion = "1.4.1.0";
	$data = array();
	$data[$PARAM_VERSION] = $fileVersion;
	$data[$PARAM_MD5] = md5_file(sprintf("../files/%s", $fileName));
	$data[$PARAM_URL] = sprintf("http://englishinfocus.ru/rtserver/files/%s", $fileName);
}

if (!empty($data)) {
	$response = array();
	$response[$PARAM_DATA] = $data;
	echo json_encode($response);
}


function enableErrorsLog() {
	error_reporting(E_ALL);
	ini_set("display_errors","On");
}

?>