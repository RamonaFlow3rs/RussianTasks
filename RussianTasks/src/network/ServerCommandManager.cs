using System;
using System.Collections.Generic;
using Newtonsoft.Json.Linq;

namespace RussianTasks.src.network
{
    using RequestCallback = Action<bool, JObject>;

    class ServerCommandManager
    {
        private const string SERVER_URL = "http://englishinfocus.ru/rtserver/1.0/api.php";
        //private const string SERVER_URL = "http://localhost/server/1.0/api.php";
        private static byte[] SALT = { 103, 35, 100, 53, 65, 111, 64, 80, 100, 117, 51, 35, 65, 119, 49, 33, 100, 107 };


        private const string ACTION_NAME_TIME =             "activation";
        private const string ACTION_CHECK_NEW_VERSION =     "check_new_version";

        private const string PARAM_TIME =                   "time";
        private const string PARAM_HASH =                   "hash";
        private const string PARAM_ACTION =                 "action";
        private const string PARAM_USER_NAME =              "u";
        private const string PARAM_USER_KEY =               "k";
        private const string PARAM_MACHINE_ID =             "i";

        public static void requestProductActivation(string userName, string key, string machineIdMd5, RequestCallback callback)
        {
            Dictionary<string, object> requestMap = new Dictionary<string, object>();
            requestMap.Add(PARAM_ACTION, ACTION_NAME_TIME);
            requestMap.Add(PARAM_USER_NAME, userName);
            requestMap.Add(PARAM_USER_KEY, key);
            requestMap.Add(PARAM_MACHINE_ID, machineIdMd5);
            performCommand(requestMap, callback);
        }

        public static void checkForTheNewVersion(RequestCallback callback)
        {
            Dictionary<string, object> requestMap = new Dictionary<string, object>();
            requestMap.Add(PARAM_ACTION, ACTION_CHECK_NEW_VERSION);
            performCommand(requestMap, callback);
        }

        private static void performCommand(Dictionary<string, object> requestMap, RequestCallback callback)
        {
            string time = utils.Utils.getCurrentTimestamp().ToString();
            string h = string.Format("{0}_{1}", time, System.Text.Encoding.UTF8.GetString(SALT));
            requestMap[PARAM_TIME] = time;
            requestMap[PARAM_HASH] = utils.Utils.createMD5ForString(h);
            RequestManager.getInstance().performGetRequest(SERVER_URL, requestMap, callback);
        }
    }
}
