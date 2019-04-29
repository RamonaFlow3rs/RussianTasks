using System;
using System.Windows.Forms;
using RussianTasks.src.exercises.variant;
using System.Drawing;
using System.Collections.Generic;
using RussianTasks.src.common;
using RussianTasks.src.utils;

namespace RussianTasks.src.ui.variant
{
    public partial class VariantExerciseWindow : Form
    {
        //constants
        private const string ICON_VOCABULARY = "images\\icons\\icon_notepad_45.png";
        private const string ICON_VOCABULARY_MARK = "images\\icons\\icon_notepad_mark_45.png";

        //private members;

        private VariantExercise _variantExercise;
        private int _totalWordsCount;
        private int _currentWordNumber;
        private int _areaWidth;
        private int _areaHeight;
        private Image _image_haveInVocabulary;
        private Image _image_haveNotInVocabulary;
        private Timer sTimer = new Timer();

        private static Font sMainFont = new Font("Arial", 22.0f, FontStyle.Regular);

        private const int MARING_HORIZONTAL = 100;
        private const int MARGIN_VERTICAL = 50;
        private const int BUTTON_WIDTH = 300;
        private const int BUTTON_HEIGHT = 50;


        public VariantExerciseWindow(VariantExercise variantExercise,  string windowTitle, string sectionTitle, int wordsCount)
        {
            _variantExercise = variantExercise;
            _totalWordsCount = wordsCount;
            InitializeComponent();
            initValues();
            setupView();
            setupTitle(windowTitle, sectionTitle);
        } 

        private void initValues()
        {
            _areaWidth = mainPanel.Size.Width;
            _areaHeight = mainPanel.Size.Height;
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

            sTimer.Interval = Constants.BUTTON_FREEZE_TIME;
            sTimer.Enabled = false;
            sTimer.Tick += delegate
            {
                skipButton.Enabled = true;
                sTimer.Enabled = false;
            };

            Form frm = this;
            UIHelpers.setupWindowIcon(ref frm);
            this.FormClosing += formClosingHandler;
        }

        public void showWord(VariantWordInfo variantWord, bool haveWordInVocabulary)
        {
            mainPanel.Controls.Clear();
            continueButton.Visible = false;

            sTimer.Enabled = true;
            skipButton.Enabled = false;

            questionLabel.Text = variantWord.question;
            var variants = variantWord.variants;
            var buttonsPositions = getButtonsPositions(variants.Count);
            buttonsPositions.Shuffle();

            for (int i = 0; i < variants.Count; i++)
            {
                var variant = variants[i];
                Button button = new Button();
                button.Tag = variant.isCorrect;
                button.Location = buttonsPositions[i];
                button.Font = sMainFont;
                button.Size = new Size(BUTTON_WIDTH, BUTTON_HEIGHT);
                button.Text = variant.text;
                button.Click += giveAnswerHandler;
                button.FlatStyle = FlatStyle.Flat;
                button.BackColor = Color.White;
                mainPanel.Controls.Add(button);
            }

            setupVocabularyButton(haveWordInVocabulary);
            _currentWordNumber++;
            wordsCounterLabel.Text = string.Format(Properties.Strings.exercise_window_progress_label_template, _currentWordNumber, _totalWordsCount);
        }

        private void skipCurrentWord()
        {
            if (_currentWordNumber < _totalWordsCount)
            {
                _variantExercise.skipCurrentWord();
            }
            else
            {
                _variantExercise.skipCurrentWord();
                finishExercise();
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
            int newX = Size.Width / 2 - TitleLabel.Size.Width / 2;
            TitleLabel.Location = new Point(newX, TitleLabel.Location.Y);
        }

        private void onTap_addWordToVocabulary(object sender, EventArgs e)
        {
            _variantExercise.addCurrentWordToVocabulary();
            setupVocabularyButton(true);
        }

        private void onTap_removeWordFromVocabulary(object sender, EventArgs e)
        {
            _variantExercise.removeCurrentWordFromVocabulary();
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
            _variantExercise.showExerciseResults();
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
            }
            else if (buttonState == ContinueButtonState.NEXT_WORD)
            {
                continueButton.Text = Properties.Strings.exercise_window_button_continue;
                continueButton.Click += continueButtonHandler;
            }
            else if (buttonState == ContinueButtonState.FINISH_EXERCISE)
            {
                continueButton.Text = Properties.Strings.exercise_window_button_finish;
                continueButton.Click += delegate
                {
                    finishExercise();
                };
            }
        } 

        private void continueButtonHandler(object sender, EventArgs e)
        {
            _variantExercise.showNextWord();
        }

        private void giveAnswerHandler(object sender, EventArgs e)
        {
            bool hasError = false;
            Button button = (Button)sender;

            if ((bool)button.Tag == false)
            {
                button.BackColor = Color.Red;
                hasError = true;  
            }
            else
            {
                button.BackColor = Color.LightGreen;
                continueButton.Visible = true;

                if (_currentWordNumber == _totalWordsCount)
                {
                    setupContinueButton(ContinueButtonState.FINISH_EXERCISE);
                }
                else
                {
                    setupContinueButton(ContinueButtonState.NEXT_WORD);
                }


                foreach (Control control in mainPanel.Controls)
                {
                    if (control is Button)
                    {
                        control.Click -= giveAnswerHandler;
                    }
                }
            }
            _variantExercise.onAnswerGiven(hasError);
 
        }

        private List<Point> getButtonsPositions(int buttonsCount)
        {
            List<Point> result = null;
            switch (buttonsCount)
            {
                case 2:
                    {
                        int startX = (_areaWidth - 2 * BUTTON_WIDTH - MARING_HORIZONTAL) / 2;
                        result = new List<Point>()
                        {
                            new Point(startX, _areaHeight/2),
                            new Point(startX + BUTTON_WIDTH + MARING_HORIZONTAL, _areaHeight/2)
                        };
                        break;
                    }

                case 3:
                    {
                        int startX = (_areaWidth - 2 * BUTTON_WIDTH - MARING_HORIZONTAL) / 2;
                        int startY = (_areaHeight - 2 * BUTTON_HEIGHT - MARGIN_VERTICAL) / 2;
                        result = new List<Point>()
                        {
                            new Point(startX, startY),
                            new Point(startX + BUTTON_WIDTH + MARING_HORIZONTAL, startY),
                            new Point(_areaWidth/2, startY + BUTTON_HEIGHT + MARGIN_VERTICAL)
                        };
                        break;
                    }


                case 4:
                    {
                        int startX = (_areaWidth - 2 * BUTTON_WIDTH - MARING_HORIZONTAL) / 2;
                        int startY = (_areaHeight - 2 * BUTTON_HEIGHT - MARGIN_VERTICAL) / 2;
                        result = new List<Point>()
                        {
                            new Point(startX, startY),
                            new Point(startX + BUTTON_WIDTH + MARING_HORIZONTAL, startY),
                            new Point(startX, startY + BUTTON_HEIGHT + MARGIN_VERTICAL),
                            new Point(startX + BUTTON_WIDTH + MARING_HORIZONTAL, startY + BUTTON_HEIGHT + MARGIN_VERTICAL)
                        };
                        break;
                    }

                default:
                    getButtonsPositions(4);
                    break;
            };

            return result;
        }

    }
}
