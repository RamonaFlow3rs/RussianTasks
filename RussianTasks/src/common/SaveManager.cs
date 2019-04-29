using Newtonsoft.Json.Linq;

namespace RussianTasks.src.common
{
    class SaveManager
    {
        static SaveManager _instance;
        public static SaveManager getInstance()
        {
            if (_instance == null)
            {
                _instance = new SaveManager();
            }
            return _instance;
        }


        private SaveManager() { }


        public void writeSave(JObject dict)
        {
            string stateStr = dict.ToString();
            string saveFolder = BuildConfig.FOLDER_SAVES;
            string saveFileFullPath = string.Concat(saveFolder, "save.sav");

            if (!System.IO.Directory.Exists(saveFolder)) System.IO.Directory.CreateDirectory(saveFolder);

            byte[] array = System.Text.Encoding.UTF8.GetBytes(stateStr);
            utils.Utils.encode(ref array);
            System.IO.FileStream file = System.IO.File.Open(saveFileFullPath, System.IO.FileMode.Create);
            file.Write(array, 0, array.Length);
        }


        public JObject readSave()
        {
            JObject dict = null;

            string saveFileName = BuildConfig.FOLDER_SAVES + "save.sav";

            if (System.IO.File.Exists(saveFileName))
            {
                byte[] array = System.IO.File.ReadAllBytes(BuildConfig.FOLDER_SAVES + "save.sav");
                utils.Utils.encode(ref array);
                string stateStr = System.Text.Encoding.UTF8.GetString(array);
                if (!string.IsNullOrEmpty(stateStr))
                {
                    dict = JObject.Parse(stateStr);
                }
            }

            return dict;
        }
    }
}
