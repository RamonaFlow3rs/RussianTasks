using System;
using System.Windows.Forms;
using RussianTasks.src.exercises.accents;
using System.Drawing;
using System.Collections.Generic;
using RussianTasks.src.common;

namespace RussianTasks.src.ui.accents
{
    public partial class AccentExerciseWindow : Form
    {
        //constants
        private static string ICON_VOCABULARY = "images\\icons\\icon_notepad_45.png";
        private static string ICON_VOCABULARY_MARK = "images\\icons\\icon_notepad_mark_45.png";
        private static int DEFAULT_LETTER_SIZE = 60;
        private static int ADDITION_TOP_Y_POS = 30;
        private static int LETTERS_Y_POS = 120;
        private static int ADDITION_BOTTOM_Y_POS = 210;
        private static int ADDITION_MARGIN_H = 40;

        //private members;
        private AccentExercise _accentExercise;
        private int _totalWordsCount;
        private int _currentWordNumber;
        private Timer sTimer = new Timer();
        Image _image_haveInVocabulary;
        Image _image_haveNotInVocabulary;


        public AccentExerciseWindow(AccentExercise accentExercise, uint section, int wordsCount)
        {
            _accentExercise = accentExercise;
            _totalWordsCount = wordsCount;
            _currentWordNumber = 0;
            InitializeComponent();
            setupView();
            setupTitle(section);
        }


        private void setupView()
        {
            _image_haveInVocabulary = Image.FromFile(String.Concat(BuildConfig.FOLDER_RESOURCES, ICON_VOCABULARY_MARK));
            _image_haveNotInVocabulary = Image.FromFile(String.Concat(BuildConfig.FOLDER_RESOURCES, ICON_VOCABULARY));

            _tooltip.IsBalloon = true;

            sTimer.Interval = Constants.BUTTON_FREEZE_TIME;
            sTimer.Enabled = false;
            sTimer.Tick += delegate
            {
                continueButton.Enabled = true;
                sTimer.Enabled = false;
            };

            Form frm = this;
            UIHelpers.setupWindowIcon(ref frm);
        }


        public void showWord(AccentWordInfo accentWord, bool haveWordInVocabulary)
        {
            LettersPanel.Controls.Clear();

            sTimer.Enabled = true;
            continueButton.Enabled = false;

            char[] word = accentWord.word.ToCharArray();
            int lettersCount = word.Length;

            int buttonSize = DEFAULT_LETTER_SIZE;
            float fontCoef = 1.0f;

            int lettersDrawingSpaceWidth = LettersPanel.Size.Width;
            int panelHeight = LettersPanel.Size.Height;

            int additionWidth = 0;
            Label additionLabel = null;
            Addition addition = accentWord.addition;
            if (addition != null)
            {
                additionLabel = buildAddition(addition);
                additionWidth = TextRenderer.MeasureText(additionLabel.Text, additionLabel.Font).Width;
                additionLabel.Size = new Size(additionWidth, DEFAULT_LETTER_SIZE);
                if (addition.position == Addition.Position.LEFT || addition.position == Addition.Position.RIGHT)
                {
                    lettersDrawingSpaceWidth -= additionWidth + ADDITION_MARGIN_H;
                }
            }

            bool haveEnoughSpace = lettersDrawingSpaceWidth >= DEFAULT_LETTER_SIZE * lettersCount;
            if (!haveEnoughSpace)
            {
                buttonSize = lettersDrawingSpaceWidth / lettersCount;
                fontCoef = (float)buttonSize / (float)DEFAULT_LETTER_SIZE;
            }

            int xStartPos = LettersPanel.Size.Width / 2 - lettersCount * buttonSize / 2;

            if (additionLabel != null)
            {
                if (addition.position == Addition.Position.LEFT)
                {
                    xStartPos += (additionWidth  + ADDITION_MARGIN_H) / 2;
                }
                else if (addition.position == Addition.Position.RIGHT)
                {
                    xStartPos -= (additionWidth + ADDITION_MARGIN_H) / 2;
                }
            }

            for (int i = 0; i < lettersCount; i++)
            {
                bool isVowel = utils.StringUtils.isVowel(word[i]);
                Label lab = new Label();
                lab.Size = new Size(buttonSize, buttonSize);
                lab.TextAlign = ContentAlignment.MiddleCenter;
                lab.Location = new Point(xStartPos + i * buttonSize, LETTERS_Y_POS);
                lab.Text = word[i].ToString();
                lab.BorderStyle = isVowel ? BorderStyle.FixedSingle : BorderStyle.None;
                lab.Font = new Font("Arial", 28.0f * fontCoef, FontStyle.Regular);
                lab.TabStop = false;
                lab.Tag = i;
                if (isVowel) { lab.Click += onLetterSelectedHandler; }
                LettersPanel.Controls.Add(lab);
            }

            if (additionLabel != null)
            {
                Point additionLocation = new Point(0, 0);
                int lettersWidth = lettersCount * buttonSize;

                switch (accentWord.addition.position)
                {
                    case Addition.Position.TOP:
                        additionLocation = new Point(LettersPanel.Size.Width / 2 - additionWidth / 2, ADDITION_TOP_Y_POS);
                        break;
                    case Addition.Position.RIGHT:
                        additionLocation = new Point(xStartPos + lettersWidth + ADDITION_MARGIN_H, LETTERS_Y_POS);
                        break;

                    case Addition.Position.BOTTOM:
                        additionLocation = new Point(LettersPanel.Size.Width / 2 - additionWidth / 2, ADDITION_BOTTOM_Y_POS);
                        break;

                    case Addition.Position.LEFT:
                        additionLocation = new Point(xStartPos - additionWidth - ADDITION_MARGIN_H, LETTERS_Y_POS);
                        break;
                }
                additionLabel.Location = additionLocation;

                LettersPanel.Controls.Add(additionLabel);
            }


            setupVocabularyButton(haveWordInVocabulary);
            setupContinueButton(ContinueButtonState.SKIP_WORD);

            _currentWordNumber++;
            wordsCounterLabel.Text = String.Format(Properties.Strings.exercise_window_progress_label_template, _currentWordNumber, _totalWordsCount);
        }

        private Label buildAddition(Addition addition)
        {
            Font fnt = new Font("Arial", 28.0f, FontStyle.Regular);
            Label additionLabel = new Label();
            additionLabel.AutoSize = false;
            additionLabel.TextAlign = ContentAlignment.MiddleCenter;
            additionLabel.Font = fnt;
            additionLabel.Text = addition.text;

            return additionLabel;
        }

        private void onLetterSelectedHandler(object sender, EventArgs e)
        {
            Label target = (Label)sender;
            int idx = (int)target.Tag;

            bool correct = _accentExercise.onLetterSelected(idx);
            if (!correct)
            {
                target.BackColor = Color.Red;
                target.Enabled = false;
            }
            else
            {
                target.BackColor = Color.Green;

                if (_currentWordNumber == _totalWordsCount)
                {
                    setupContinueButton(ContinueButtonState.FINISH_EXERCISE);
                }
                else
                {
                    setupContinueButton(ContinueButtonState.NEXT_WORD);
                }

                foreach (Control control in LettersPanel.Controls)
                {
                    if (control is Button)
                    {
                        control.Click -= onLetterSelectedHandler;
                    }
                }
            }
        }


        private void AccentExerciseWindow_FormClosing(object sender, FormClosingEventArgs e)
        {
            onTapExitExercise();
            e.Cancel = true;
        }


        private void setupTitle(uint section)
        {
            string titleText = _accentExercise.getLocalizedNameForSection(section);
            TitleLabel.Text = titleText;
            int newX = this.Size.Width / 2 - TitleLabel.Size.Width / 2;
            TitleLabel.Location = new Point(newX, TitleLabel.Location.Y);
        }


        private void onTap_addWordToVocabulary(object sender, EventArgs e)
        {
            _accentExercise.addCurrentWordToVocabulary();
            setupVocabularyButton(true);
        }


        private void onTap_removeWordFromVocabulary(object sender, EventArgs e)
        {
            _accentExercise.removeCurrentWordFromVocabulary();
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
            _accentExercise.showExerciseResults();
            Dispose();
            Close();
        }


        private enum ContinueButtonState
        {
            NEXT_WORD,
            SKIP_WORD,
            FINISH_EXERCISE
        }

        private void setupContinueButton(ContinueButtonState buttonState)
        {
            continueButton.Click -= continueButtonHandler;
            continueButton.Click -= skipButtonHandler;

            if (buttonState == ContinueButtonState.SKIP_WORD)
            {
                continueButton.Text = Properties.Strings.exercise_window_button_skip;
                continueButton.Click -= continueButtonHandler;
                continueButton.Click += skipButtonHandler;
            }
            else if (buttonState == ContinueButtonState.NEXT_WORD)
            {
                continueButton.Text = Properties.Strings.exercise_window_button_continue;
                continueButton.Click -= skipButtonHandler;
                continueButton.Click += continueButtonHandler;
            }
            else if (buttonState == ContinueButtonState.FINISH_EXERCISE)
            {
                continueButton.Text = Properties.Strings.exercise_window_button_finish;
                continueButton.Click -= continueButtonHandler;
                continueButton.Click -= skipButtonHandler;
                continueButton.Click += delegate
                {
                    finishExercise();
                };
            }
            else
            {
                continueButton.Text = "";
                continueButton.Click -= continueButtonHandler;
                continueButton.Click -= skipButtonHandler;
            }
        }


        private void continueButtonHandler(object sender, EventArgs e)
        {
            _accentExercise.showNextWord();
        }


        private void skipButtonHandler(object sender, EventArgs e)
        {
            if (_currentWordNumber < _totalWordsCount)
            {
                _accentExercise.skipCurrentWord();
            }
            else
            {
                _accentExercise.skipCurrentWord();
                finishExercise();
            }
        }

    }
}
