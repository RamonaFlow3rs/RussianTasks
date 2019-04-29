using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.IO;

namespace InstallActions
{
    [RunInstaller(true)]
    public partial class Main : System.Configuration.Install.Installer
    {
        public Main()
        {
            InitializeComponent();
        }

        public override void Uninstall(IDictionary savedState)
        {
            base.Uninstall(savedState);

            string message = "Удалить пользовательские данные приложения:@-данные об активации программы@-прогресс пользователя?".Replace("@", System.Environment.NewLine);
            DialogResult result = MessageBox.Show(message,
                                                    "Пользовательские данные",
                                                    MessageBoxButtons.YesNo,
                                                    MessageBoxIcon.Question);
            if (result == DialogResult.Yes)
            {
                const string COMPANY_NAME = "CyberHog";
                string COMMON_PROGRAM_DATA = System.Environment.GetFolderPath(System.Environment.SpecialFolder.CommonApplicationData);
                string USERS_HOME_DIRECTORY = System.Environment.GetFolderPath(System.Environment.SpecialFolder.MyDocuments);

                string programDataPath = string.Format(@"{0}\{1}", COMMON_PROGRAM_DATA, COMPANY_NAME);
                string usersSavesPath = string.Format(@"{0}\{1}", USERS_HOME_DIRECTORY, COMPANY_NAME);

                deleteFolderIfExists(programDataPath);
                deleteFolderIfExists(usersSavesPath);
            }
        }

        private void deleteFolderIfExists(string folder)
        {
            if (Directory.Exists(folder))
            {
                Directory.Delete(folder, true);
            }
        }
    }
}