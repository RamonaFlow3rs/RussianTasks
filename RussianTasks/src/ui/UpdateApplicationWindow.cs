using RussianTasks.src.network;
using System;
using System.Windows.Forms;

namespace RussianTasks.src.ui
{
    public partial class UpdateApplicationWindow : Form
    {
        private uint _downloadhandle = DownloadManager.INVALID_HANDLE;
        private string _destinationFile = null;
        private string _downloadUrl = null;
        private string _fileMd5 = null;

        public UpdateApplicationWindow(string downloadUrl, string destination, string md5)
        {
            _downloadUrl = downloadUrl;
            _destinationFile = destination;
            _fileMd5 = md5;

            InitializeComponent();
            createView();
            Form frm = this;
            UIHelpers.setupWindowIcon(ref frm);
        }

        private void createView()
        {
            actionButton.Text = Properties.Strings.update_application_window_button_cancel;
            actionButton.Click += onTapCancelDownloading;
        }

        private void onTapCancelDownloading(object sender, EventArgs e)
        {
            if (_downloadhandle != DownloadManager.INVALID_HANDLE)
            {
                DownloadManager.getInstance().cancelDownloadFile(_downloadhandle);
                closeWindow();
            }
        }

        public void onDownloadCompleted()
        {
            string fileHash = utils.Utils.createMD5(System.IO.File.ReadAllBytes(_destinationFile));
            if (fileHash == _fileMd5)
            {
                actionButton.Text = Properties.Strings.update_application_window_button_update;
                actionButton.Click += delegate
                {
                    System.Diagnostics.Process.Start(_destinationFile);
                    MainActivity.getInstance().terminate();
                };
            }
            else
            {
                AlertWindow.show(
                    Properties.Strings.update_application_window_error_title,
                    Properties.Strings.update_application_window_error_message,
                    closeWindow,
                    Properties.Strings.update_application_window_error_button_retry,
                    () =>
                    {
                        System.IO.File.Delete(_destinationFile);
                        startDownloading();
                    },
                    Properties.Strings.update_application_window_error_button_cancel,
                    closeWindow);
            }
        }

        public void updateProgress(long totalBytesToReceive, long bytesReceived, int progressPersentage)
        {
            progressbar.Value = progressPersentage;
            if (progressPersentage > 0 && progressPersentage < 100)
            {
                //crutch for real progressbar animation
                //https://stackoverflow.com/questions/6071626/progressbar-is-slow-in-windows-forms
                progressPersentage--;
                progressbar.Value = progressPersentage;
            }
            messageLabel.Text = string.Format("Загружено: {0}% ({1} kb / {2} kb)", progressPersentage, bytesReceived / 1024, totalBytesToReceive / 1024);
        }

        private void closeWindow()
        {
            Close();
            Dispose();
        }

        private void startDownloading()
        {
            _downloadhandle = DownloadManager.getInstance().downloadFile(_downloadUrl, _destinationFile, updateProgress, onDownloadCompleted);
        }

        private void UpdateApplicationWindow_Shown(object sender, EventArgs e)
        {
            startDownloading();
        }
    }
}
