using System.Collections.Generic;
using System.Windows.Forms;
using System.Drawing;
using System;

namespace RussianTasks.src.ui
{
    public partial class SelectSectionWindow : Form
    {  
        private const int START_POS_X = 20;
        private const int START_POS_Y = 50;
        private const int BUTTON_WIDTH = 340;
        private const int BUTTON_HEIGHT = 50;
        private const int SEPARATOR_MARGIN = 10;
        private const int FORM_WIDTH = BUTTON_WIDTH + START_POS_X * 2 + 13;//WTF is 13??

        private string _formTitle;
        private string _sectionsTitle;
        private SortedDictionary<uint, string> _sectionsList;
        private Action<uint> _sectionSelectedCallback;
        private Action<Form> _showVocabularyCallback;
        private bool _exitOnCloseForm = true;


        public SelectSectionWindow(SortedDictionary<uint, string> sectionsList, Action<uint> sectionSelectedCallback, string formTitle = null, string sectionsTitle = null, Action<Form> showVocabularyCallback = null)
        {
            _formTitle = formTitle;
            _sectionsTitle = sectionsTitle;
            _sectionsList = sectionsList;
            _sectionSelectedCallback = sectionSelectedCallback;
            _showVocabularyCallback = showVocabularyCallback;
            InitializeComponent();
            buildView();
            Form frm = this;
            UIHelpers.setupWindowIcon(ref frm);
        }

        private void buildView()
        {
            this.Text = _formTitle != null ? _formTitle : Properties.Strings.select_section_window_title_default; ;
            sectionsTitleLabel.Text = _sectionsTitle != null ? _sectionsTitle : Properties.Strings.select_section_window_title_default;

            int currentPosY = START_POS_Y;
            foreach (KeyValuePair<uint, string> entry in _sectionsList)
            {
                uint section = entry.Key;
                EventHandler buttonHandler = delegate
                {
                    _sectionSelectedCallback?.Invoke(section);
                    closeThisWindow(false);
                };
                Button btn = createButton(entry.Value, section, buttonHandler);
                btn.Location = new Point(START_POS_X, currentPosY);
                Controls.Add(btn);
                currentPosY += BUTTON_HEIGHT;
            }

            //My vocabulary button
            if (_showVocabularyCallback != null)
            {
                Label sep = createSeparator();
                currentPosY += SEPARATOR_MARGIN;
                sep.Location = new Point(0, currentPosY);
                currentPosY += SEPARATOR_MARGIN;
                Controls.Add(sep);

                EventHandler vocabularyButtonHandler = delegate
                {
                    _showVocabularyCallback?.Invoke(this);
                    _exitOnCloseForm = false;
                    Hide();
                };
                Button b = createButton(Properties.Strings.window_title_vocabulary, 0, vocabularyButtonHandler);
                b.Location = new Point(START_POS_X, currentPosY);
                Controls.Add(b);
                currentPosY += BUTTON_HEIGHT;
            }

            Label separator = createSeparator();
            currentPosY += SEPARATOR_MARGIN;
            separator.Location = new Point(0, currentPosY);
            currentPosY += SEPARATOR_MARGIN;
            Controls.Add(separator);

            //Back button
            Button backButton = createButton(Properties.Strings.select_section_window_button_back, 0, delegate
            {
                services.SharedLocator.getExercisesManager().finishCurrentExercise();
                closeThisWindow(false);
            });
            backButton.Location = new Point(START_POS_X, currentPosY);
            Controls.Add(backButton);

            this.Size = new Size(FORM_WIDTH, currentPosY + START_POS_Y * 2);
        }

        static Font sFont = new Font("Arial", 20.0f, FontStyle.Regular);
        private Button createButton(string buttonText, uint tag, EventHandler buttonHandler)
        {
            Button btn = new Button();
            btn.Size = new Size(BUTTON_WIDTH, BUTTON_HEIGHT);
            btn.Text = buttonText;
            btn.FlatStyle = FlatStyle.System;
            btn.Font = sFont;
            btn.TabStop = false;
            btn.Tag = tag;
            btn.Click += buttonHandler;

            while (BUTTON_WIDTH < TextRenderer.MeasureText(buttonText, btn.Font).Width)
            {
                btn.Font = new Font(btn.Font.FontFamily, btn.Font.Size - 0.5f, btn.Font.Style);
            }

            return btn;
        }

        private Label createSeparator()
        {
            Label separator = new Label();
            separator.BorderStyle = BorderStyle.Fixed3D;
            separator.AutoSize = false;
            separator.Size = new Size(FORM_WIDTH, 2);

            return separator;
        }

        private void SelectSectionWindow_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (e.CloseReason == CloseReason.UserClosing && _exitOnCloseForm)
            {
                MainActivity.getInstance().onExitApplication();
                e.Cancel = true;
            }
        }

        private void SelectSectionWindow_Activated(object sender, System.EventArgs e)
        {
            _exitOnCloseForm = true;
        }

        private void closeThisWindow(bool exitApp)
        {
            _exitOnCloseForm = exitApp;
            this.Close();
            this.Dispose();
        }
    }
}