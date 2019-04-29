<?php
define("ERROR_CODE_UNDEFINED", -1);
define("ERROR_CODE_NONE", 0);
define("ERROR_CODE_INVALID_KEY", 1);
define("ERROR_CODE_KEY_IS_ALREADY_USED", 2);
define("ERROR_CODE_DB_PROBLEM", 3);

function checkActivation($userName, $userKey, $machineId)
{
	$errorCode = checkInputData($userName, $userKey, $machineId);
	$result = array();
	if ($errorCode == ERROR_CODE_NONE) {
		$result["success"] = true;
	} else {
		$result["errorcode"] = $errorCode;
	}

    return $result;
}

function checkInputData($userName, $userKey, $machineId) {
	$TIMESTAMP_ENCODED_ARRAYS = array(
		"D" => array("Q" => "0", "Z" => "1", "D" => "2", "1" => "3", "L" => "4", "V" => "5", "E" => "6", "U" => "7", "R" => "8", "K" => "9"),
		"3" => array("M" => "0", "3" => "1", "W" => "2", "L" => "3", "A" => "4", "G" => "5", "9" => "6", "B" => "7", "X" => "8", "7" => "9"),
		"5" => array("V" => "0", "B" => "1", "N" => "2", "E" => "3", "Q" => "4", "L" => "5", "Y" => "6", "U" => "7", "R" => "8", "K" => "9"),
		"U" => array("F" => "0", "Z" => "1", "D" => "2", "H" => "3", "5" => "4", "Y" => "5", "7" => "6", "T" => "7", "K" => "8", "S" => "9"),
		"M" => array("Q" => "0", "4" => "1", "F" => "2", "Y" => "3", "L" => "4", "V" => "5", "E" => "6", "P" => "7", "R" => "8", "5" => "9")
	);
	$POSITIONS = array(0, 1, 3, 4, 7, 10, 13, 15, 17);
	$SALT = "g#d5Ao@Pdu3#Aw1!dk";


	$parts = explode('-', $userKey);
	if (count($parts) != 4) {
		logMessage("ERROR: Wrong key parts count");
		return ERROR_CODE_INVALID_KEY;
	}

	$userPart_1 = $parts[0];
	$userPart_2 = $parts[2];
	$timestamp_1 = $parts[1];
	$timestamp_2 = $parts[3];

	$timestampArray_1 = str_split($timestamp_1);
	$timestampArray_2 = str_split($timestamp_2);
	$userPartLetters_1 = str_split($userPart_1);
	
	if (count($userPartLetters_1) != 5) {
		logMessage("ERROR: Wrong key part");
		return ERROR_CODE_INVALID_KEY;
	}

	$controlLetter = $userPartLetters_1[4];
	if (!array_key_exists($controlLetter, $TIMESTAMP_ENCODED_ARRAYS)) {
		logMessage("ERROR: Invalid control letter");
		return ERROR_CODE_INVALID_KEY;
	}

	$timestampEncodeArray = $TIMESTAMP_ENCODED_ARRAYS[$controlLetter];
	$timestampString = "";
	$idx = 0;
	for ($i = 0; $i < 10; $i++) {
		if ($i % 2 == 0) {
			$letter = $timestampArray_1[$idx];
		} else {
			$letter = $timestampArray_2[$idx];
			$idx++;
		}

		if (!array_key_exists($letter, $timestampEncodeArray)) {
			logMessage("ERROR: timestamp parse error");
			return ERROR_CODE_INVALID_KEY;
		}
		$timestampString .= $timestampEncodeArray[$letter];
	}
	
	$controlStringArray = str_split(md5(sprintf("%s_%s_%s", $userName, $SALT, $timestampString)));
	$controlString = "";
	for ($i = 0; $i < count($POSITIONS); $i++) {
		$controlString .= $controlStringArray[$POSITIONS[$i]];
	}

	$userParts = $userPart_1 . $userPart_2;
	$userControlString = strtolower(substr($userParts, 0, 4) . substr($userParts, 5));

	if ($userControlString != $controlString) {
		logMessage("ERROR: user's control string is not equal real control string");
		return ERROR_CODE_INVALID_KEY;
	}

	return checkUserKeyIsUsed($userName, $userKey, $machineId);
}

function checkUserKeyIsUsed($userName, $userKey, $machineId) {
	$servername = "localhost";
	$username = "jerihon_rt_user";
	$password = "dL5Qj279";
	$dbname = "jerihon_rt_database";

	$conn = new mysqli($servername, $username, $password, $dbname);
	if ($conn->connect_error) {
		logMessage("ERROR: can't connect to database");
		return ERROR_CODE_DB_PROBLEM;
	}
	mysqli_set_charset($conn, 'utf8');
	$userName = mysqli_real_escape_string($conn, $userName);
	$userKey = mysqli_real_escape_string($conn, $userKey);
	$machineId = mysqli_real_escape_string($conn, $machineId);

	$sql_get_results = sprintf("SELECT userkey, machineid FROM activation where username='%s'", $userName);
	$result = $conn->query($sql_get_results);

	if ($result->num_rows > 0) {
	    while($row = $result->fetch_assoc()) {
	    	if ($row["userkey"] == $userKey) {
	    		if ($row["machineid"] == $machineId) {
	    			return ERROR_CODE_NONE;
	    		} else {
	    			return ERROR_CODE_KEY_IS_ALREADY_USED;
	    		}
	    	}
	    }
	}

	$errorCode = ERROR_CODE_UNDEFINED;
	$sql_activate = sprintf("INSERT INTO activation (username, userkey, machineid, date)
	VALUES ('%s', '%s', '%s', %d)", $userName, $userKey, $machineId, getMskTime());

	if ($conn->query($sql_activate) == true) {
		$errorCode = ERROR_CODE_NONE;
	} else {
		logMessage("ERROR: can't insert data into database");
		$errorCode = ERROR_CODE_DB_PROBLEM;
	}
	$conn->close();

	return $errorCode;
}

function getMskTime() {
	$msk_timezone_offset = 60 * 60 * 3;
	return time() + $msk_timezone_offset;
}

function logMessage($message) {
	if (LOG_ENABLED) {
		echo $message . "<br>";
	}
}
?>