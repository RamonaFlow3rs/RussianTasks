namespace RussianTasks.src.common
{
    public class BuildConfig
    {
#if DEBUG
        private const string WORKING_FOLDER = @"\..\..\";
#else
        private const string WORKING_FOLDER = @"\";
#endif
        private static string APPLICATION_STARTUP_PATH = System.Windows.Forms.Application.StartupPath;

        public static string COMPANY_NAME = "CyberHog";
        public static string APPLICATION_NAME = "ЕГЭ-Тренажёр";
        private static string APPLICATION_FOLER_NAME = string.Format(@"\{0}\{1}\", COMPANY_NAME, APPLICATION_NAME);

        public static string COMMON_PROGRAM_DATA = System.Environment.GetFolderPath(System.Environment.SpecialFolder.CommonApplicationData);
        public static string USERS_HOME_DIRECTORY = System.Environment.GetFolderPath(System.Environment.SpecialFolder.MyDocuments);

        public static string FOLDER_RESOURCES = APPLICATION_STARTUP_PATH + WORKING_FOLDER + @"res\";
        public static string FOLDER_SAVES = USERS_HOME_DIRECTORY + APPLICATION_FOLER_NAME + @"saves\";
        public static string APPLICATION_PROGRAM_DATA = COMMON_PROGRAM_DATA + APPLICATION_FOLER_NAME;
        public static string FOLDER_TEMP = System.IO.Path.GetTempPath() + APPLICATION_FOLER_NAME;
    }
}