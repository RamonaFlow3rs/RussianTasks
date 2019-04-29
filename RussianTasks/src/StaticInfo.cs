using Newtonsoft.Json.Linq;
using RussianTasks.src.common;
using System.Windows.Forms;

namespace RussianTasks.src
{
    class StaticInfo
    {
        public static string ACCENTS_CONFIG_PATH = BuildConfig.FOLDER_RESOURCES + "data\\accents";
        public static string SPELLING_CONFIG_PATH = BuildConfig.FOLDER_RESOURCES + "data\\spelling";
        public static string REPEATING_CONFIG_PATH = BuildConfig.FOLDER_RESOURCES + "data\\repeating";
        public static string VARIANT_CONFIG_PATH = BuildConfig.FOLDER_RESOURCES + "data\\variant";


        private JObject _accentConfig;
        private JObject _spellingConfig;
        private JObject _repeatingConfig;
        private JObject _variantConfig;

        public StaticInfo()
        {
            reinit();
        }

        public void reinit()
        {
            _accentConfig = getFileContents(ACCENTS_CONFIG_PATH, true);
            _spellingConfig = getFileContents(SPELLING_CONFIG_PATH, true);
            _repeatingConfig = getFileContents(REPEATING_CONFIG_PATH, true);
            _variantConfig = getFileContents(VARIANT_CONFIG_PATH, true);
        }

        private JObject getFileContents(string fileName, bool encoded)
        {
            if (System.IO.File.Exists(fileName))
            {
                byte[] array = System.IO.File.ReadAllBytes(fileName);
                if (encoded)
                {
                    utils.Utils.encode(ref array);
                }
                return JObject.Parse(System.Text.Encoding.UTF8.GetString(array));
            }
            else
            {
                MessageBox.Show(string.Format(Properties.Strings.error_cannot_find_config_file_with_name, fileName), Properties.Strings.error_text);
                return null;
            }
        }

        public JObject getAccentsExerciseConfig() { return _accentConfig; }
        public JObject getSpellingExerciseConfig() { return _spellingConfig; }
        public JObject getRepeatingExerciseConfig() { return _repeatingConfig; }
        public JObject getVariantExerciseConfig() { return _variantConfig; }

        public bool hasConfig(string configName)
        {
            switch (configName)
            {
                case exercises.accents.AccentExercise.sName:
                    return _accentConfig != null;
                case exercises.spelling.SpellingExercise.sName:
                    return _spellingConfig != null;
                case exercises.repeating.RepeatingExercise.sName:
                    return _repeatingConfig != null;
                case exercises.variant.VariantExercise.sName:
                    return _variantConfig != null;
                default:
                    return false;
            }
        }

    }
}