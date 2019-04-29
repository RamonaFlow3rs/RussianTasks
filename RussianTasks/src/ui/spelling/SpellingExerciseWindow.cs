using System;
using System.Windows.Forms;
using RussianTasks.src.exercises.spelling;
using System.Drawing;
using System.Collections.Generic;
using RussianTasks.src.common;

namespace RussianTasks.src.ui.spelling
{
    public partial class SpellingExerciseWindow : Form
    {
        //constants
        private const string ICON_VOCABULARY = "images\\icons\\icon_notepad_45.png";
        private const string ICON_VOCABULARY_MARK = "images\\icons\\icon_notepad_mark_45.png";

        private const int INPUTBOX_WIDTH = 60;

        //private members;
        private SpellingExercise _spellingExercise;
        private int _totalWordsCount;
        private int _currentWordNumber;
        private int _inputBoxHeight;
        private int _panelWidth;
        private int _panelHeight;
        private Image _image_haveInVocabulary;
        private Image _image_haveNotInVocabulary;

        private static Font sMainFont = new Font("Arial", 22.0f, FontStyle.Regular);
        private List<TextBox> _inputBoxes = new List<TextBox>();
        private int _selectedInputBoxIdx;

        public SpellingExerciseWindow(SpellingExercise spellingExercise, string windowTitle, string sectionTitle, int wordsCount)
        {
            _spellingExercise = spellingExercise;
            _totalWordsCount = wordsCount;
            InitializeComponent();
            initValues();
            setupView();
            setupTitle(windowTitle, sectionTitle);
        }

        private void initValues()
        {
            TextBox tmp = new TextBox();
            tmp.Font = sMainFont;
            _inputBoxHeight = tmp.Height;
            _panelWidth = lettersPanel.Size.Width;
            _panelHeight = lettersPanel.Size.Height;
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

        public void showWord(SpellingWordInfo spellingWord, bool haveWordInVocabulary)
        {
            _inputBoxes.Clear();
            lettersPanel.Controls.Clear();

            List<SpellingWordPart> wordParts = spellingWord.wordParts;

            int hiddenPartsCount = 0;
            int visibleLettersWidth = 0;
            List<int> visiblePartsWidths = new List<int>();
            foreach (SpellingWordPart part in wordParts)
            {
                if (part.isHidden)
                {
                    hiddenPartsCount++;
                }
                else
                {
                    int width = TextRenderer.MeasureText(part.letters, sMainFont).Width;
                    visiblePartsWidths.Add(width);
                    visibleLettersWidth += width;
                }
            }

            int totalWidth = visibleLettersWidth + hiddenPartsCount * INPUTBOX_WIDTH;

            int xCurrent = _panelWidth / 2 - totalWidth / 2;
            int yPos = _panelHeight / 2 - _inputBoxHeight / 2;

            int visibleCount = 0;
            int invisibleCount = 0;
            foreach (SpellingWordPart part in wordParts)
            {
                if (part.isHidden)
                {
                    TextBox inputBox = new TextBox();
                    inputBox.Width = INPUTBOX_WIDTH;
                    inputBox.Location = new Point(xCurrent, yPos);
                    inputBox.Font = sMainFont;
                    inputBox.TabStop = false;
                    inputBox.TextAlign = HorizontalAlignment.Center;
                    inputBox.Tag = utils.StringUtils.replaceMootLetters(part.letters);
                    inputBox.Click += delegate
                    {
                        inputBox.BackColor = Color.White;
                    };
                    inputBox.KeyUp += onInputFieldTextPressed;
                    _inputBoxes.Add(inputBox);
                    xCurrent += INPUTBOX_WIDTH;
                    lettersPanel.Controls.Add(inputBox);
                    invisibleCount++;
                }
                else
                {
                    Label visiblePart = new Label();
                    visiblePart.AutoSize = false;
                    visiblePart.Text = part.letters;
                    visiblePart.TextAlign = ContentAlignment.MiddleLeft;
                    visiblePart.Font = sMainFont;
                    visiblePart.Size = new Size(visiblePartsWidths[visibleCount], _inputBoxHeight);
                    visiblePart.Location = new Point(xCurrent, yPos);
                    lettersPanel.Controls.Add(visiblePart);
                    xCurrent += visiblePartsWidths[visibleCount];
                    visibleCount++;
                }
            }

            _selectedInputBoxIdx = 0;
            if (_inputBoxes.Count > 0)
            {
                _inputBoxes[0].Focus();
            }

            setupVocabularyButton(haveWordInVocabulary);
            _currentWordNumber++;
            setupContinueButton(ContinueButtonState.GIVE_ANSWER);

            wordsCounterLabel.Text = string.Format(Properties.Strings.exercise_window_progress_label_template, _currentWordNumber, _totalWordsCount);
        }

        private void skipCurrentWord()
        {
            if (_currentWordNumber < _totalWordsCount)
            {
                _spellingExercise.skipCurrentWord();
            }
            else
            {
                _spellingExercise.skipCurrentWord();
                finishExercise();
            }
        }

        private void onInputFieldTextPressed(object sender, KeyEventArgs e)
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
            else if (keyCode == Keys.Tab)
            {
                switchInputField();
            }
        }

        private void switchInputField()
        {
            if (_inputBoxes.Count > 1)
            {
                _inputBoxes[++_selectedInputBoxIdx % _inputBoxes.Count].Focus();
            }
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
            _spellingExercise.addCurrentWordToVocabulary();
            setupVocabularyButton(true);
        }

        private void onTap_removeWordFromVocabulary(object sender, EventArgs e)
        {
            _spellingExercise.removeCurrentWordFromVocabulary();
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
            _spellingExercise.showExerciseResults();
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
            _spellingExercise.showNextWord();
        }

        private void giveAnswerHandler(object sender, EventArgs e)
        {
            bool hasError = false;

            foreach (TextBox textbox in _inputBoxes)
            {
                string variant = utils.StringUtils.replaceMootLetters(textbox.Text.Trim().ToLower());
                if (variant != (string)textbox.Tag)
                {
                    textbox.BackColor = Color.Red;
                    hasError = true;
                }
                else
                {
                    textbox.BackColor = Color.LightGreen;
                }
            }

            _spellingExercise.onAnswerGiven(hasError);

            if (!hasError)
            {
                setupContinueButton(_currentWordNumber == _totalWordsCount ? ContinueButtonState.FINISH_EXERCISE : ContinueButtonState.NEXT_WORD);
            }
        }
    }
}