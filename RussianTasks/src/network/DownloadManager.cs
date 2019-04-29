using System;
using System.Collections.Generic;
using System.Net;

namespace RussianTasks.src.network
{
    class DownloadManager
    {
        public static uint INVALID_HANDLE = 0;

        private static DownloadManager _instance;

        private Dictionary<uint, WebClient> _downloadTasks = new Dictionary<uint, WebClient>();
        private uint _taskHandle = INVALID_HANDLE;

        public static DownloadManager getInstance()
        {
            if (_instance == null)
            {
                _instance = new DownloadManager();
            }
            return _instance;
        }

        private void storeDownloadHandle(uint handle, WebClient client)
        {
            _downloadTasks[handle] = client;
        }

        private void clearDownloadHandle(uint handle)
        {
            if (_downloadTasks.ContainsKey(handle))
            {
                _downloadTasks.Remove(handle);
            }
        }

        public uint downloadFile(string url, string destination, Action<long, long, int> onDownloadProgressChanged, Action onDownloadCompleted)
        {
            using (WebClient wc = new WebClient())
            {
                var downloadHandle = ++_taskHandle;
                wc.DownloadProgressChanged += (object sender, DownloadProgressChangedEventArgs e) =>
                {
                    onDownloadProgressChanged(e.TotalBytesToReceive, e.BytesReceived, e.ProgressPercentage);
                };
                wc.DownloadFileCompleted += (object sender, System.ComponentModel.AsyncCompletedEventArgs e) =>
                {
                    clearDownloadHandle(downloadHandle);
                    onDownloadCompleted.Invoke();
                };

                wc.DownloadFileAsync(new Uri(url), destination);
                storeDownloadHandle(++_taskHandle, wc);
                return _taskHandle;
            }
        }

        public void cancelDownloadFile(uint handle)
        {
            if (_downloadTasks.ContainsKey(handle))
            {
                _downloadTasks[handle].CancelAsync();
                clearDownloadHandle(handle);
            }
        }
    }
}
