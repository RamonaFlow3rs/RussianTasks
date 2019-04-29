using System;
using System.Windows.Forms;
using System.Drawing;
using RussianTasks.src.common;
using RussianTasks.src.exercises.repeating;

namespace RussianTasks.src.ui.repeating
{
    public partial class RepeatingExerciseWindow : Form
    {
        //constants
        private const string ICON_VOCABULARY = "images\\icons\\icon_notepad_45.png";
        private const string ICON_VOCABULARY_MARK = "images\\icons\\icon_notepad_mark_45.png";

        private const int INPUTBOX_WIDTH = 60;

        //private members;
        private RepeatingExercise _repeatingExercise;
        private int _totalWordsCount;
        private int _currentWordNumber;
        private int _panelWidth;
        private int _panelHeight;
        private Image _image_haveInVocabulary;
        private Image _image_haveNotInVocabulary;

        private static Font sFont = new Font("Arial", 22.0f, FontStyle.Regular);

        public RepeatingExerciseWindow(RepeatingExercise repeatingExercise, string windowTitle, string sectionTitle, int wordsCount)
        {
            _repeatingExercise = repeatingExercise;
            _totalWordsCount = wordsCount;
            InitializeComponent();
            initValues();
            setupView();
            setupTitle(windowTitle, sectionTitle);
        }

        private void initValues()
        {
            _panelWidth = mainPanel.Size.Width;
            _panelHeight = mainPanel.Size.Height;
        }

        private void setupView()
        {
            _image_haveInVocabulary = Image.FromFile(String.Concat(BuildConfig.FOLDER_RESOURCES, ICON_VOCABULARY_MARK));
            _image_haveNotInVocabulary = Image.FromFile(String.Concat(BuildConfig.FOLDER_RESOURCES, ICON_VOCABULARY));

            _tooltip.IsBalloon = true;

            skipButton.Text = Properties.Strings.exercise_window_button_skip;
            skipButton.Click += delegate
            {
                skipCurrentWord();
            };

            Form frm = this;
            UIHelpers.setupWindowIcon(ref frm);
        }

        public void showWord(RepeatingWordInfo repeatingWord, bool haveWordInVocabulary)
        {
            answerInputBox.Clear();
            answerInputBox.BackColor = Color.White;
            answerInputBox.Tag = utils.StringUtils.replaceMootLetters(repeatingWord.answer);
            questionLabel.Font = sFont;
            questionLabel.Text = repeatingWord.word;

            setupVocabularyButton(haveWordInVocabulary);
            _currentWordNumber++;
            setupContinueButton(ContinueButtonState.GIVE_ANSWER);
            wordsCounterLabel.Text = string.Format(Properties.Strings.exercise_window_progress_label_template, _currentWordNumber, _totalWordsCount);
            answerInputBox.Focus();
        }

        private void formClosingHandler(object sender, FormClosingEventArgs e)
        {
            onTapExitExercise();
            e.Cancel = true;
        }

        private void setupTitle(string windowTitle, string sectionTitle)
        {
            this.Text = windowTitle;
            TitleLabel.Text = sectionTitle;
            int newX = this.Size.Width / 2 - TitleLabel.Size.Width / 2;
            TitleLabel.Location = new Point(newX, TitleLabel.Location.Y);
        }

        private void onTap_addWordToVocabulary(object sender, EventArgs e)
        {
            _repeatingExercise.addCurrentWordToVocabulary();
            setupVocabularyButton(true);
        }

        private void onTap_removeWordFromVocabulary(object sender, EventArgs e)
        {
            _repeatingExercise.removeCurrentWordFromVocabulary();
            setupVocabularyButton(false);
        }

        private void setupVocabularyButton(bool haveInVocabulary)
        {
            vocabularyButton.Image = haveInVocabulary ? _image_haveInVocabulary : _image_haveNotInVocabulary;

            if (haveInVocabulary)
            {
                vocabularyButton.Click -= onTap_addWordToVocabulary;
                vocabularyButton.Click += onTap_removeWordFromVocabulary;
                _tooltip.RemoveAll();
                _tooltip.SetToolTip(vocabularyButton, Properties.Strings.hint_remove_from_my_vocabulary);
            }
            else
            {
                vocabularyButton.Click -= onTap_removeWordFromVocabulary;
                vocabularyButton.Click += onTap_addWordToVocabulary;
                _tooltip.RemoveAll();
                _tooltip.SetToolTip(vocabularyButton, Properties.Strings.hint_add_to_my_vocabulary);
            }
        }

        private void onTapExitExercise()
        {
            AlertWindow.show(
                Properties.Strings.accent_exercise_exit_title,
                Properties.Strings.accent_exercise_exit_message,
                null, Properties.Strings.finish_exercise_text,
                () =>
                {
                    finishExercise();
                },
                Properties.Strings.cancel_text,
                null);
        }

        private void finishExercise()
        {
            _repeatingExercise.showExerciseResults();
            Dispose();
            Close();
        }


        private enum ContinueButtonState
        {
            GIVE_ANSWER,
            NEXT_WORD,
            FINISH_EXERCISE
        }

        private void setupContinueButton(ContinueButtonState buttonState)
        {
            continueButton.Click -= continueButtonHandler;
            continueButton.Click -= giveAnswerHandler;

            if (buttonState == ContinueButtonState.GIVE_ANSWER)
            {
                continueButton.Text = Properties.Strings.exercise_window_button_answer;
                continueButton.Click += giveAnswerHandler;
                skipButton.Enabled = true;
            }
            else if (buttonState == ContinueButtonState.NEXT_WORD)
            {
                continueButton.Text = Properties.Strings.exercise_window_button_continue;
                continueButton.Click += continueButtonHandler;
                skipButton.Enabled = false;
            }
            else if (buttonState == ContinueButtonState.FINISH_EXERCISE)
            {
                continueButton.Text = Properties.Strings.exercise_window_button_finish;
                continueButton.Click += delegate
                {
                    finishExercise();
                };
                skipButton.Enabled = false;
            }
        }

        private void continueButtonHandler(object sender, EventArgs e)
        {
            _repeatingExercise.showNextWord();
        }

        private void giveAnswerHandler(object sender, EventArgs e)
        {
            string variant = utils.StringUtils.replaceMootLetters(answerInputBox.Text.Trim().ToLower());
            bool hasError = variant != (string)answerInputBox.Tag;
            answerInputBox.BackColor = hasError ? Color.Red : Color.LightGreen;
            _repeatingExercise.onAnswerGiven(hasError);
            if (!hasError)
            {
                setupContinueButton(_currentWordNumber == _totalWordsCount ? ContinueButtonState.FINISH_EXERCISE : ContinueButtonState.NEXT_WORD);
            }
        }

        private void answerInputBox_TextChanged(object sender, EventArgs e)
        {
            //string text = answerInputBox.Text;
            //answerInputBox.Text = text.ToLower();

            //if (text.Length > 0)
            //{
            //    answerInputBox.SelectionStart = text.Length;
            //    answerInputBox.SelectionLength = 0;
            //}
        }

        private void skipCurrentWord()
        {
            if (_currentWordNumber < _totalWordsCount)
            {
                _repeatingExercise.skipCurrentWord();
            }
            else
            {
                _repeatingExercise.skipCurrentWord();
                finishExercise();
            }
        }

        private void answerInputBox_KeyUp(object sender, KeyEventArgs e)
        {
            Keys modifiers = e.Modifiers;
            Keys keyCode = e.KeyCode;
            if (modifiers == Keys.Control)
            {
                switch (keyCode)
                {
                    case Keys.Enter:
                        skipCurrentWord();
                        break;
                    case Keys.D:
                        vocabularyButton.PerformClick();
                        break;
                }
            }
            else if (keyCode == Keys.Enter)
            {
                continueButton.PerformClick();
            }
        }
    }
}