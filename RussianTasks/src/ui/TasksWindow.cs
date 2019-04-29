using System;
using System.Windows.Forms;
using RussianTasks.src.services;
using System.Drawing;

namespace RussianTasks.src.ui
{
    public partial class TasksWindow : Form
    {
        const int BUTTON_WIDTH = 425;
        const int BUTTON_HEIGHT = 55;
        const int BUTTON_MARGIN_TOP_BOTTOM = 10;
        const int SEPARATOR_MARGIN = 15;
        const int ADV_LABEL_MARGIN_TOP = 30;
        const int ADV_LABEL_MARGIN_BOTTOM = 0;
        const int ADV_LABEL_HEIGHT = 40;
        const int TITLE_MARGIN_BOTTOM = 5;
        const int START_X = 15;
        static int START_Y = 0;

        private bool _exitOnCloseForm = true;

        public TasksWindow()
        {
            InitializeComponent();
            createView();
            Form frm = this;
            UIHelpers.setupWindowIcon(ref frm);
        }

        private void createView()
        {
            //title
            this.Text = Properties.Strings.tasks_window_title;

            //message
            titleLabel.Text = Properties.Strings.tasks_window_message;
            START_Y = titleLabel.Height + TITLE_MARGIN_BOTTOM + BUTTON_MARGIN_TOP_BOTTOM;

            int currentPosX = START_X;
            int currentPosY = START_Y;


            {//AccentsExercise button
                Button b = createMenuButton(
                    exercises.accents.AccentExercise.sName,
                    Properties.Strings.exercise_name_accents,
                    menuButtonCallback);
                b.Location = new Point(currentPosX, currentPosY);
                currentPosY += BUTTON_HEIGHT;
                Controls.Add(b);
            }

            {//SpellingExercise button
                Button b = createMenuButton(
                    exercises.spelling.SpellingExercise.sName,
                    Properties.Strings.exercise_name_spelling,
                    menuButtonCallback);
                b.Location = new Point(currentPosX, currentPosY);
                currentPosY += BUTTON_HEIGHT;
                Controls.Add(b);
            }

            {//RepeatingExercise button
                Button b = createMenuButton(
                    exercises.repeating.RepeatingExercise.sName,
                    Properties.Strings.exercise_name_repeating,
                    menuButtonCallback);
                b.Location = new Point(currentPosX, currentPosY);
                currentPosY += BUTTON_HEIGHT;
                Controls.Add(b);
            }

            {//VariantExercise button
                Button b = createMenuButton(
                    exercises.variant.VariantExercise.sName,
                    Properties.Strings.exercise_name_variant,
                    menuButtonCallback);
                b.Location = new Point(currentPosX, currentPosY);
                currentPosY += BUTTON_HEIGHT;
                Controls.Add(b);
            }

            {//Advertisment label
                Label adv = new Label();
                adv.AutoSize = false;
                adv.Size = new Size(this.Size.Width - 12, ADV_LABEL_HEIGHT);
                adv.Font = new Font("Arial", 12.25f, FontStyle.Italic);
                currentPosY += ADV_LABEL_MARGIN_TOP;
                adv.Location = new Point(0, currentPosY);
                currentPosY += ADV_LABEL_MARGIN_BOTTOM + ADV_LABEL_HEIGHT;
                adv.Text = System.Text.RegularExpressions.Regex.Unescape(Properties.Strings.next_version_adv);
                adv.TextAlign = ContentAlignment.MiddleCenter;
                Controls.Add(adv);
            }

            this.Height = currentPosY + BUTTON_HEIGHT;
        }


        private Button createMenuButton(string tag, string buttonText, EventHandler buttonCallback)
        {
            Button b = new Button();
            b.Size = new Size(BUTTON_WIDTH, BUTTON_HEIGHT);
            b.Text = buttonText;
            b.FlatStyle = FlatStyle.System;
            b.Font = new Font("Arial", 20.0f, FontStyle.Regular);
            b.TabStop = false;
            b.Tag = tag;
            b.Click += buttonCallback;

            return b;
        }


        private void menuButtonCallback(object sender, EventArgs e)
        {
            string exerciseName = (sender as Button).Tag as string;
            if (SharedLocator.getStaticInfo().hasConfig(exerciseName))
            {
                closeThisWindow(false);
                SharedLocator.getExercisesManager().startExercise(exerciseName);
            }
            else
            {
                MessageBox.Show(Properties.Strings.error_cannot_find_config_file, Properties.Strings.error_text);
            }
        }

        private void TasksWindow_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (e.CloseReason == CloseReason.UserClosing && _exitOnCloseForm)
            {
                MainActivity.getInstance().onExitApplication();
                e.Cancel = true;
            }
        }

        private void closeThisWindow(bool exitApp)
        {
            _exitOnCloseForm = exitApp;
            this.Close();
            this.Dispose();
        }
    }
}