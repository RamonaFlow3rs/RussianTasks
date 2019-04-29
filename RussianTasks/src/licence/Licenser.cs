using Newtonsoft.Json.Linq;
using RussianTasks.src.common;
using System;
using System.ComponentModel;
using System.Text;
using RussianTasks.src.network;

namespace RussianTasks.src.licence
{
    class Licenser
    {
        const string LICENCE_FILE_NAME = @"licence";
        const string KEY_ACTIVATED = "activated";
        const string KEY_ID = "id";
        const string KEY_KEY = "key";
        const string KEY_VERSION = "ver";
        const string KEY_DATE = "date";

        enum LicenceError
        {
            ERROR_NONE = 0,
            ERROR_INVALID_KEY = 1,
            ERROR_KEY_IS_ALREADY_USED = 2,
            ERROR_SERVER_PROBLEM = 3,
            ERROR_INVALID_MACHINE = 4,
            ERROR_NO_INTERNET_CONNECTION = 5
        }

        ui.licence.ActivationWindow _activationWindow;
        Action _successCallback;
        string _userKey = null;
        string _machineId = null;

        private static Licenser _instance;
        public static Licenser getInstance()
        {
            if (_instance == null)
            {
                _instance = new Licenser();
            }
            return _instance;
        }

        private string getLicenceFilePath()
        {
            return string.Concat(BuildConfig.APPLICATION_PROGRAM_DATA, LICENCE_FILE_NAME);
        }

        public async void checkLicence(Action successCallback)
        {
            _successCallback = successCallback;
            string licenceFilePath = getLicenceFilePath();
            bool hasLicenceFile = System.IO.File.Exists(licenceFilePath);

            if (hasLicenceFile)
            {
                JObject obj = null;
                try
                {
                    byte[] raw = System.IO.File.ReadAllBytes(licenceFilePath);
                    utils.Utils.encode(ref raw, 0x6E);
                    string str = Encoding.UTF8.GetString(raw);
                    obj = JObject.Parse(str);
                }
                catch (Exception)
                {
                    deleteLicenceFile();
                }

                if (obj != null && obj.GetValue(KEY_ACTIVATED) != null)
                {
                    string userMachineId = (string)obj[KEY_ID];
                    await utils.Utils.getMachineUniqueIdAsync((string machineId)=>
                    {
                        _machineId = machineId;
                        if (_machineId == userMachineId)
                        {
                            successCallback?.Invoke();
                        }
                        else
                        {
                            handleLicenceError(LicenceError.ERROR_INVALID_MACHINE);
                            MainActivity.getInstance().terminate(false);
                        }
                    });
                }
                else
                {
                    deleteLicenceFile();
                    showActivationWindow();
                }
            }
            else
            {
                showActivationWindow();
            }
        }

        private void showActivationWindow()
        {
            _activationWindow = new ui.licence.ActivationWindow(onKeyEntered);
            _activationWindow.Show();
        }

        private void deleteLicenceFile()
        {
            string licenceFilePath = getLicenceFilePath();
            if (System.IO.File.Exists(licenceFilePath))
            {
                System.IO.File.Delete(licenceFilePath);
            }
        }

        private async void onKeyEntered(string username, string key)
        {
            if (key.Length == 0)
            {
                handleLicenceError(LicenceError.ERROR_INVALID_KEY);
                return;
            }

            bool haveInternetConnection = System.Net.NetworkInformation.NetworkInterface.GetIsNetworkAvailable();
            if (!haveInternetConnection)
            {
                handleLicenceError(LicenceError.ERROR_NO_INTERNET_CONNECTION);
                return;
            }

            _activationWindow.setBlockedView(true);
            _userKey = key;

            await utils.Utils.getMachineUniqueIdAsync((string machineId) =>
            {
                _machineId = machineId;
                string machineIdMD5 = utils.Utils.createMD5ForString(_machineId);
                ServerCommandManager.requestProductActivation(username, key, machineIdMD5, onActivationRequestCompleted);
            });
        }

        private void onActivationRequestCompleted(bool success, JObject response)
        {
            bool result = false;
            LicenceError error = LicenceError.ERROR_NONE;

            if (response != null)
            {
                JToken resultToken = response.GetValue("result");
                if (resultToken != null)
                {
                    JToken successToken = resultToken["success"];
                    if (successToken != null)
                    {
                        result = (bool)successToken;
                    }
                    else
                    {
                        JToken errorCodeToken = resultToken["errorcode"];
                        if (errorCodeToken != null)
                        {
                            error = (LicenceError)((int)errorCodeToken);
                        }
                        else
                        {
                            error = LicenceError.ERROR_SERVER_PROBLEM;
                        }
                    }
                }
                else
                {
                    error = LicenceError.ERROR_SERVER_PROBLEM;
                }
            }
            else
            {
                error = LicenceError.ERROR_SERVER_PROBLEM;
            }

            if (result == true)
            {
                activateProduct(_userKey);
            }
            else
            {
                handleLicenceError(error);
            }
        }

        private void handleLicenceError(LicenceError error)
        {
            if (_activationWindow != null)
            {
                _activationWindow.setBlockedView(false);
            }

            string message = null;
            switch (error)
            {
                case LicenceError.ERROR_INVALID_KEY:
                    message = Properties.Strings.activation_error_invalid_key;
                    break;
                case LicenceError.ERROR_NO_INTERNET_CONNECTION:
                    message = Properties.Strings.activation_error_no_internet_connection;
                    break;
                case LicenceError.ERROR_KEY_IS_ALREADY_USED:
                    message = Properties.Strings.activation_error_key_is_already_used;
                    break;
                case LicenceError.ERROR_INVALID_MACHINE:
                    message = Properties.Strings.activation_error_activated_on_another_machine;
                    break;
                case LicenceError.ERROR_SERVER_PROBLEM:
                    message = Properties.Strings.activation_error_server_problem;
                    break;
            }
            string title = Properties.Strings.error_text;
            System.Windows.Forms.MessageBox.Show(message, title, System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Exclamation);
        }

        private void activateProduct(string key)
        {
            System.Reflection.Assembly assembly = System.Reflection.Assembly.GetExecutingAssembly();
            System.Diagnostics.FileVersionInfo fvi = System.Diagnostics.FileVersionInfo.GetVersionInfo(assembly.Location);
            string version = fvi.FileVersion;

            JObject outputObj = new JObject();
            outputObj.Add(KEY_ACTIVATED, "true");
            outputObj.Add(KEY_ID, _machineId);
            outputObj.Add(KEY_KEY, key);
            outputObj.Add(KEY_VERSION, version);
            outputObj.Add(KEY_DATE, DateTime.Today.ToShortDateString());

            byte[] bytes = Encoding.UTF8.GetBytes(outputObj.ToString());
            utils.Utils.encode(ref bytes, 0x6E);
            if (!System.IO.Directory.Exists(BuildConfig.APPLICATION_PROGRAM_DATA))
            {
                System.IO.Directory.CreateDirectory(BuildConfig.APPLICATION_PROGRAM_DATA);
            }
            System.IO.File.WriteAllBytes(getLicenceFilePath(), bytes);

            if  (_activationWindow != null)
            {
                _activationWindow.closeWindow();
                _activationWindow = null;
            }
            _successCallback?.Invoke();
            System.Windows.Forms.MessageBox.Show(Properties.Strings.activation_window_success, Properties.Strings.activation_window_title, System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Information);
        }
    }
}
