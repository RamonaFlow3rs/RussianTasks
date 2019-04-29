using RussianTasks.src.common;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Printing;
using System.Windows.Forms;
using System;
using RussianTasks.src.vocabulary;

namespace RussianTasks.src.ui
{
    public partial class VocabularyWindow : Form
    {
        private const string IMAGE_ICON_REMOVE = "images\\icons\\remove.png";
        private const string IMAGE_EMPTY_STUB = "images\\interface\\accents\\sceme.png";
        
        private const string CELL_NAME_TEMPLATE = "cell_id_";
        private const string TITLE_NAME_TEMPLATE = "section_";

        //Wanted to make all controls programmatically....
        private const int TITLE_OFFSET_Y = 0;

        private const int CELLS_START_POS_Y = TITLE_OFFSET_Y + TITLE_HEIGHT;

        //cell label
        private const int LABEL_WIDTH = 200;
        private const int LABEL_HEIGHT = 30;

        //cell remove button
        private const int X_BUTTON_WIDTH = 30;
        private const int X_BUTTON_HEIGHT = 30;
        private const int X_BUTTON_MARGIN_LEFT = 5;

        //cell
        private const int CELL_LEFT_RIGHT_PADDING = 5;
        private const int CELL_WIDTH = LABEL_WIDTH + X_BUTTON_WIDTH + CELL_LEFT_RIGHT_PADDING;
        private const int CELL_HEIGHT = 30;

        //cells container
        private static int SYSTEM_SCROLLBAR_WIDTH = SystemInformation.VerticalScrollBarWidth;
        private static int CELLS_CONTAINER_WIDTH = CELL_WIDTH + SYSTEM_SCROLLBAR_WIDTH;
        private const int CELLS_CONTAINER_HEIGHT = 500;

        //form
        private const int FORM_LEFT_RIGHT_PADDING = 30;
        private static int FORM_WIDTH = CELLS_CONTAINER_WIDTH + FORM_LEFT_RIGHT_PADDING * 2;

        //title
        private const int TITLE_WIDTH = 200;
        private const int TITLE_HEIGHT = 40;

        //train button
        private const int TRAIN_BUTTON_WIDTH = 200;
        private const int TRAIN_BUTTON_HEIGHT = 50;

        //study block
        private const int STUDY_BLOCK_MARGIN_TOP = 10;
        private const int STUDY_BLOCK_HEIGHT = 35;

        Form _parentWindow;
        private bool _showParentWindow = true;

        List<VocabularySet> _vocabularyContents;
        Func<uint, bool> _removeWordFromVocabularyCallback;
        Action _beginStudyVocabularyCallback;

        Image removeImage;
        Panel _cellsContainer;
        Panel _studyBlock;
        Panel _stubContainer;

        public VocabularyWindow(List<VocabularySet> vocabularyContents, Form parentWindow, Func<uint, bool> removeWordFromVocabularyCallback, Action beginStudyVocabularyCallback)
        {
            InitializeComponent();
            _parentWindow = parentWindow;
            _vocabularyContents = vocabularyContents;
            _removeWordFromVocabularyCallback = removeWordFromVocabularyCallback;
            _beginStudyVocabularyCallback = beginStudyVocabularyCallback;
            removeImage = Image.FromFile(string.Concat(BuildConfig.FOLDER_RESOURCES, IMAGE_ICON_REMOVE));
            buildWindow();
            Form frm = this;
            UIHelpers.setupWindowIcon(ref frm);
        }


        public void buildWindow()
        {
            buildWindowTitle();

            if (_vocabularyContents.Count > 0)
            {
                _cellsContainer = new Panel();

                foreach (VocabularySet set in _vocabularyContents)
                {
                    Label sectionTitle = buildSectionTitle(set.title, set.section);
                    _cellsContainer.Controls.Add(sectionTitle);
                    _cellsContainer.Controls.SetChildIndex(sectionTitle, 0);

                    foreach (VocabularyItem item in set.words)
                    {
                        Panel cell = makeWordCell(item);
                        cell.Tag = item;
                        _cellsContainer.Controls.Add(cell);
                        _cellsContainer.Controls.SetChildIndex(cell, 0);
                    }
                }

                _cellsContainer.AutoScroll = true;
                _cellsContainer.Size = new Size(CELLS_CONTAINER_WIDTH, CELLS_CONTAINER_HEIGHT);

                recolourCells();

                _studyBlock = buildStudyBlock();

                _cellsContainer.Location = new Point(FORM_WIDTH / 2 - CELLS_CONTAINER_WIDTH / 2, CELLS_START_POS_Y);
                _studyBlock.Location = new Point(_cellsContainer.Location.X, _cellsContainer.Location.Y + _cellsContainer.Size.Height + STUDY_BLOCK_MARGIN_TOP);

                this.Controls.Add(_cellsContainer);
                this.Controls.Add(_studyBlock);
            } else
            {
                showStubPanel();
            }


            this.Size = new Size(FORM_WIDTH, CELLS_START_POS_Y + TITLE_HEIGHT + CELLS_CONTAINER_HEIGHT + STUDY_BLOCK_MARGIN_TOP * 2 + STUDY_BLOCK_HEIGHT);
        }

        private void buildWindowTitle()
        {
            Label titleLabel = new Label();
            titleLabel.Font = new Font("Arial", 20.25f, FontStyle.Regular);
            titleLabel.Size = new Size(TITLE_WIDTH, TITLE_HEIGHT);
            titleLabel.TextAlign = ContentAlignment.MiddleCenter;
            titleLabel.Text = Properties.Strings.window_title_vocabulary;
            titleLabel.Location = new Point(FORM_WIDTH / 2 - titleLabel.Size.Width / 2, TITLE_OFFSET_Y);
            Controls.Add(titleLabel);
        }


        private Label buildSectionTitle(string title, uint section)
        {
            Label titleLabel = new Label();
            titleLabel.Font = new Font("Arial", 14.0f, FontStyle.Bold);
            titleLabel.Size = new Size(CELL_WIDTH, CELL_HEIGHT);
            titleLabel.TextAlign = ContentAlignment.MiddleCenter;
            titleLabel.Text = title;
            titleLabel.Dock = DockStyle.Top;
            titleLabel.Name = string.Concat(TITLE_NAME_TEMPLATE, section);
            titleLabel.BackColor = Color.FromArgb(255, 79, 129, 189);
            titleLabel.ForeColor = Color.White;

            while (CELL_WIDTH < TextRenderer.MeasureText(title, titleLabel.Font).Width)
            {
                titleLabel.Font = new Font(titleLabel.Font.FontFamily, titleLabel.Font.Size - 0.5f, titleLabel.Font.Style);
            }

            return titleLabel;
        }


        private Panel makeWordCell(VocabularyItem vocabularyItem)
        {
            Panel panel = new Panel();
            panel.Size = new Size(CELL_WIDTH, CELL_HEIGHT);

            Label wordlabel = new Label();
            wordlabel.Text = vocabularyItem.word;
            wordlabel.Location = new Point(0, 0);
            wordlabel.Size = new Size(LABEL_WIDTH, LABEL_HEIGHT);
            wordlabel.TextAlign = ContentAlignment.MiddleLeft;
            wordlabel.Font = new Font("Arial", 14.0f, FontStyle.Regular);

            while (wordlabel.Width < TextRenderer.MeasureText(wordlabel.Text, new Font(wordlabel.Font.FontFamily, wordlabel.Font.Size, wordlabel.Font.Style)).Width)
            {
                wordlabel.Font = new Font(wordlabel.Font.FontFamily, wordlabel.Font.Size - 0.5f, wordlabel.Font.Style);
            }

            panel.Controls.Add(wordlabel);

            Button removeWordButton = new Button();
            removeWordButton.Size = new Size(X_BUTTON_WIDTH, X_BUTTON_HEIGHT);
            removeWordButton.Location = new Point(LABEL_WIDTH + X_BUTTON_MARGIN_LEFT, 0);
            removeWordButton.TabStop = false;
            removeWordButton.Tag = vocabularyItem;
            removeWordButton.Click += RemoveWordButton_Click;
            removeWordButton.BackgroundImage = removeImage;
            removeWordButton.ImageAlign = ContentAlignment.MiddleCenter;
            removeWordButton.BackgroundImageLayout = ImageLayout.Center;
            removeWordButton.Dock = DockStyle.Right;
            panel.Controls.Add(removeWordButton);
            panel.Name = string.Concat(CELL_NAME_TEMPLATE, vocabularyItem.id);
            panel.Dock = DockStyle.Top;

            return panel;
        }

        
        private void recolourCells()
        {
            int num = 0;

            for (int i = _cellsContainer.Controls.Count - 1; i >= 0; i--)
            {
                Control control = _cellsContainer.Controls[i];
                if (control.Name.StartsWith(CELL_NAME_TEMPLATE))
                {
                    control.BackColor = (num % 2 == 0) ? Color.White : Color.FromArgb(255, 211, 223, 238);
                    num++;
                }
                else if (control.Name.StartsWith(TITLE_NAME_TEMPLATE))
                {
                    num = 0;
                }
            }
        }


        private void RemoveWordButton_Click(object sender, System.EventArgs e)
        {
            Button target = (Button)sender;
            VocabularyItem itemToRemove = (VocabularyItem)target.Tag;

            string title = Properties.Strings.vocabulary_remove_word_title;
            string message = string.Format(Properties.Strings.vocabulary_remove_word_text, itemToRemove.word);
            if (MessageBox.Show(message, title, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                removeWord(itemToRemove);
            }
        }

        private void removeWord(VocabularyItem itemToRemove)
        {
            bool wordIsRemoved = _removeWordFromVocabularyCallback(itemToRemove.id);
            VocabularySet setToRemove = null;
            if (wordIsRemoved)
            {
                for (int i = 0; i < _vocabularyContents.Count; i++)
                {
                    VocabularySet set = _vocabularyContents[i];
                    if (set.section == itemToRemove.section)
                    {
                        set.words.Remove(itemToRemove);
                        if (set.words.Count == 0)
                        {
                            setToRemove = set;
                        }
                        break;
                    }
                }

                Control controlToRemove = null;
                foreach (Control control in _cellsContainer.Controls)
                {
                    if (control.Tag == itemToRemove)
                    {
                        controlToRemove = control;
                        break;
                    }
                }

                if (controlToRemove != null)
                {
                    _cellsContainer.Controls.Remove(controlToRemove);
                    controlToRemove.Dispose();

                    if (setToRemove != null)
                    {
                        checkRemoveTitle(setToRemove.section);
                        _vocabularyContents.Remove(setToRemove);
                    }

                    recolourCells();
                }
            }
        }

        private void checkRemoveTitle(uint section)
        {
            string sectionNameToRemove = string.Concat(TITLE_NAME_TEMPLATE, section);
            Control controlToRemove = null;
            for (int i = _cellsContainer.Controls.Count - 1; i >= 0; i--)
            {
                Control control = _cellsContainer.Controls[i];
                if (control.Name == sectionNameToRemove)
                {
                    controlToRemove = control;
                }
            }

            if (controlToRemove != null)
            {
                _cellsContainer.Controls.Remove(controlToRemove);
                controlToRemove.Dispose();
            }

            if (_vocabularyContents.Count == 0)
            {
                showStubPanel();
            }
        }

        private Panel buildStudyBlock()
        {
            Panel panel = new Panel();
            panel.Size = new Size(CELL_WIDTH, STUDY_BLOCK_HEIGHT);

            Button studyButton = new Button();
            studyButton.Size = new Size(panel.Width / 2, 35);
            studyButton.Text = Properties.Strings.voc_window_button_study;
            studyButton.Location = new Point(0, 0);
            studyButton.Font = new Font("Arial", 12.0f, FontStyle.Regular);
            studyButton.TabStop = false;
            studyButton.Click += StudyButton_Click;
            panel.Controls.Add(studyButton);

            Button printButton = new Button();
            printButton.Size = new Size(panel.Width / 2, 35);
            printButton.Text = Properties.Strings.voc_window_button_print;
            printButton.Location = new Point(studyButton.Width, 0);
            printButton.Font = new Font("Arial", 12.0f, FontStyle.Regular);
            printButton.TabStop = false;
            printButton.Click += PrintButton_Click;
            panel.Controls.Add(printButton);

            return panel;
        }


        private void showStubPanel()
        {
            if (_cellsContainer != null)
            {
                _cellsContainer.Visible = false;
                _studyBlock.Visible = false;
            }

            if (_stubContainer == null)
            {
                _stubContainer = buildEmptyStub();
            }
            Controls.Add(_stubContainer);
        }


        private Panel buildEmptyStub()
        {
            Panel mainContainer = new Panel();

            Label stubLabel = new Label();
            stubLabel.Font = new Font("Arial", 14.0f, FontStyle.Regular);
            stubLabel.Text = Properties.Strings.vocabulary_hint;
            stubLabel.Size = new Size(CELLS_CONTAINER_WIDTH, 100);
            stubLabel.Dock = DockStyle.Top;
            stubLabel.TextAlign = ContentAlignment.MiddleCenter;

            Panel imageContainer = new Panel();
            imageContainer.BackgroundImage = Image.FromFile(BuildConfig.FOLDER_RESOURCES + IMAGE_EMPTY_STUB);
            imageContainer.Size = new Size(282, 270);
            imageContainer.Dock = DockStyle.Top;
            imageContainer.BackgroundImageLayout = ImageLayout.Center;
            imageContainer.BorderStyle = BorderStyle.FixedSingle;

            mainContainer.Size = imageContainer.Size;
            mainContainer.Controls.Add(stubLabel);
            mainContainer.Controls.SetChildIndex(stubLabel, 0);
            mainContainer.Controls.Add(imageContainer);
            mainContainer.Controls.SetChildIndex(imageContainer, 0);

            mainContainer.Size = new Size(CELLS_CONTAINER_WIDTH, imageContainer.Size.Height + stubLabel.Size.Height + 200);
            mainContainer.Dock = DockStyle.Bottom;
            mainContainer.Padding = new Padding(0, 0, 0, 200);
            return mainContainer;
        }


        private void printVocabulary()
        {
            PrintDocument printDoc = new PrintDocument();
            printDoc.PrintPage += PrintPageHandler;
            PrintDialog printDialog = new PrintDialog();
            printDialog.Document = printDoc;
            if (printDialog.ShowDialog() == DialogResult.OK) printDialog.Document.Print();
        }


        private void saveVocabularyToFile()
        {
            System.Text.StringBuilder builder = new System.Text.StringBuilder();

            foreach (VocabularySet set in _vocabularyContents)
            {
                builder.Append(string.Concat(set.title, utils.StringUtils.CHAR_NEW_LINE));
                foreach (VocabularyItem vocabularyItem in set.words)
                {
                    builder.Append(string.Concat(vocabularyItem.word, utils.StringUtils.CHAR_NEW_LINE));
                }
                builder.Append(utils.StringUtils.CHAR_NEW_LINE);
            }

            SaveFileDialog saveDialog = new SaveFileDialog();
            saveDialog.AutoUpgradeEnabled = false;
            string defaultPath = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile);
            saveDialog.InitialDirectory = defaultPath;
            saveDialog.Filter = "txt files (*.txt)|*.txt";
            saveDialog.Title = "Сохранить мой словарь в файл";
            saveDialog.FileName = "Мой_словарик";

            if (saveDialog.ShowDialog() == DialogResult.OK)
            {
                System.IO.File.WriteAllText(saveDialog.FileName, builder.ToString(), System.Text.Encoding.UTF8);
            }
        }


        private void PrintButton_Click(object sender, EventArgs e)
        {
            AlertWindow.show(
                Properties.Strings.vocabulary_print_alert_title,
                Properties.Strings.vocabulary_print_alert_message,
                null,
                Properties.Strings.vocabulary_print_alert_print_text,
                printVocabulary,
                Properties.Strings.vocabulary_print_alert_save_text,
                saveVocabularyToFile);
        }

        private void PrintPageHandler(object sender, PrintPageEventArgs e)
        {
            const int PADDING_LEFT = 20;
            const int PADDING_TOP = 20;
            const int LINE_HEIGHT = 18;
            int linesCount = 0;

            foreach (VocabularySet set in _vocabularyContents)
            {
                string title = string.Concat(set.title, utils.StringUtils.CHAR_NEW_LINE);
                e.Graphics.DrawString(title, new Font("Arial", 12, FontStyle.Bold), Brushes.Black, PADDING_LEFT, LINE_HEIGHT * linesCount + PADDING_TOP);
                linesCount++;

                System.Text.StringBuilder builder = new System.Text.StringBuilder();
                int wordsCount = 0;
                foreach (VocabularyItem vocabularyItem in set.words)
                {
                    builder.Append(string.Concat(vocabularyItem.word, utils.StringUtils.CHAR_NEW_LINE));
                    wordsCount++;
                }
                builder.Append(utils.StringUtils.CHAR_NEW_LINE);
                e.Graphics.DrawString(builder.ToString(), new Font("Arial", 12), Brushes.Black, PADDING_LEFT, LINE_HEIGHT * linesCount + PADDING_TOP);
                linesCount += wordsCount + 1;
            }
        }

        private void StudyButton_Click(object sender, System.EventArgs e)
        {
            _beginStudyVocabularyCallback?.Invoke();
            _parentWindow.Close();
            _parentWindow.Dispose();
            _parentWindow = null;
            _showParentWindow = false;
            this.Close();
            this.Dispose();
        }

        private void VocabularyWindow_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (e.CloseReason == CloseReason.UserClosing && _showParentWindow)
            {
                _parentWindow.Show();
            }
        }
    }
}
