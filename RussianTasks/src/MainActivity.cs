using Newtonsoft.Json.Linq;
using RussianTasks.src.common;
using RussianTasks.src.services;
using RussianTasks.src.update;
using System.Windows.Forms;

namespace RussianTasks.src
{
    class MainActivity : ApplicationContext
    {
        private ui.PreloaderScreen _preloaderScreen;

        private static MainActivity _instance;

        public static MainActivity getInstance()
        {
            if (_instance == null)
            {
                _instance = new MainActivity();
            }
            return _instance;
        }


        private MainActivity()
        {
            _instance = this;
            _preloaderScreen = new ui.PreloaderScreen();
            _preloaderScreen.Show();
            licence.Licenser.getInstance().checkLicence(continueLoading);
        }


        public void terminate(bool needSaveState = true)
        {
            if (needSaveState)
            {
                saveState();
            }
            System.Environment.Exit(0);
        }


        public void showTasksWindow()
        {
            new ui.TasksWindow().Show();
        }


        public void saveState()
        {
            JObject state = new JObject();
            state.Add(exercises.ExerciseManager.MODULE_NAME, SharedLocator.getExercisesManager().save());
            SaveManager.getInstance().writeSave(state);
        }


        public void loadState()
        {
            JObject state = SaveManager.getInstance().readSave();
            if (state != null)
            {
                JObject exercisesSave = (JObject)state[exercises.ExerciseManager.MODULE_NAME];
                SharedLocator.getExercisesManager().load(exercisesSave);
            }
        }

        public void onExitApplication()
        {
            ui.AlertWindow.show(
                Properties.Strings.exit_app_tittle,
                Properties.Strings.exit_app_message,
                null, Properties.Strings.exit_app_exit_button,
                () =>
                {
                    terminate();
                },
                Properties.Strings.exit_app_cancel_button,
                null);
        }

        private void continueLoading()
        {
            SharedLocator.setStaticInfo(new StaticInfo());
            SharedLocator.setExercisesManager(new exercises.ExerciseManager());

            loadState();
            if (_preloaderScreen != null)
            {
                _preloaderScreen.Close();
                _preloaderScreen.Dispose();
            }
            showTasksWindow();
            ApplicationUpdateHelper.checkForTheNewVersion();
        }
    }
}