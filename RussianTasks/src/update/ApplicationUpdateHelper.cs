using RussianTasks.src.network;
using Newtonsoft.Json.Linq;
using RussianTasks.src.utils;
using RussianTasks.src.common;

namespace RussianTasks.src.update
{
    class ApplicationUpdateHelper
    {
        public static void checkForTheNewVersion()
        {
            ServerCommandManager.checkForTheNewVersion((bool success, JObject data) =>
            {
                if (!success) return;

                JToken versionToken = data["version"];
                JToken urlToken = data["url"];
                JToken md5Token = data["md5"];
                if (versionToken != null && urlToken != null)
                {
                    string newAppVersion = (string)versionToken;
                    string url = (string)urlToken;
                    string md5 = (string)md5Token;
                    string currentAppVersion = Utils.getApplicationVersion();
                    VersionsCompareResult result = StringUtils.compareApplicationVersions(currentAppVersion, newAppVersion);
                    if (result == VersionsCompareResult.NEW_VERSION_IS_BIGGER)
                    {
                        ui.AlertWindow.show(
                            Properties.Strings.update_application_window_title,
                            string.Format(Properties.Strings.update_application_window_message, newAppVersion, currentAppVersion),
                            null,
                            Properties.Strings.update_application_window_button_update,
                            () =>
                            {
                                startProgramUpdating(url, md5);
                            },
                            Properties.Strings.update_application_window_button_cancel,
                            null);
                    }
                }
            });
        }

        private static void startProgramUpdating(string url, string md5)
        {
            string fileName = getFileNameFromUrl(url);
            string fileDestination = BuildConfig.FOLDER_TEMP + fileName;
            bool isFileDownloaded = checkAlreadyDownloadedFile(fileDestination, md5);
            if (isFileDownloaded)
            {
                System.Diagnostics.Process.Start(fileDestination);
                MainActivity.getInstance().terminate();
            }
            else
            {
                System.IO.Directory.CreateDirectory(BuildConfig.FOLDER_TEMP);
                new ui.UpdateApplicationWindow(url, fileDestination, md5).ShowDialog();
            }
        }

        private static string getFileNameFromUrl(string url)
        {
            string fileName = string.Empty;
            string[] urlParts = url.Split('/');
            if (urlParts.Length > 0)
            {
                fileName = urlParts[urlParts.Length - 1];
            }
            return fileName;
        }

        public static bool checkAlreadyDownloadedFile(string filePath, string fileMd5)
        {
            if (System.IO.File.Exists(filePath))
            {
                string downloadedFileHash = Utils.createMD5(System.IO.File.ReadAllBytes(filePath));
                if (downloadedFileHash == fileMd5)
                {
                    return true;
                }
                else
                {
                    System.IO.File.Delete(filePath);
                }
            }
            return false;
        }
    }
}
