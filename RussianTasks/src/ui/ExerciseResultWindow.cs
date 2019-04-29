using System;
using System.Drawing;
using System.Windows.Forms;

namespace RussianTasks.src.ui
{
    public partial class ExerciseResultWindow : Form
    {
        private bool _exitOnCloseForm = true;
        private Action _finishCallback;
        private Action _mistakesCorrectionCallback;

        public ExerciseResultWindow(string windowTitle, int totalCompletedWordsCount, int mistakesCount, int skippedWordsCount, Action finishCallback, Action mistakesCorrectionCallback)
        {
            InitializeComponent();
            createView(windowTitle, totalCompletedWordsCount, mistakesCount, skippedWordsCount);
            _finishCallback = finishCallback;
            _mistakesCorrectionCallback = mistakesCorrectionCallback;
            Form frm = this;
            UIHelpers.setupWindowIcon(ref frm);
        }

        private void createView(string windowTitle, int totalCompletedWordsCount, int mistakesCount, int skippedWordsCount)
        {
            sectionNameLabel.Text = string.Format("Раздел: {0}", windowTitle);
            while (sectionNameLabel.Width < TextRenderer.MeasureText(sectionNameLabel.Text, sectionNameLabel.Font).Width)
            {
                sectionNameLabel.Font = new Font(sectionNameLabel.Font.FontFamily, sectionNameLabel.Font.Size - 0.5f, sectionNameLabel.Font.Style);
            }

            int totalWordsSeen = totalCompletedWordsCount + skippedWordsCount;
            string skippedWordsText = string.Format("Пропущено слов: {0}", skippedWordsCount);
            string completedWordsText = string.Format("Прорешано слов: {0}", totalCompletedWordsCount);

            if (skippedWordsCount > 0)
            {
                float skippedPercent = skippedWordsCount > 0 ? (float)skippedWordsCount / (float)totalWordsSeen * 100.0f : 0.0f;
                skippedWordsText += string.Format(" ({0:0.0}%)", skippedPercent);
                completedWordsText += string.Format(" ({0:0.0}%)", 100.0f - skippedPercent);
            }

            skippedWordsLabel.Text = skippedWordsText;
            wordsTotalLabel.Text = completedWordsText;

            string correctText = string.Format("Верно: {0}", Math.Max(totalCompletedWordsCount - mistakesCount, 0));
            string mistakesText = string.Format("Неверно: {0}", mistakesCount);

            if (mistakesCount > 0)
            {
                float mistakesPercent = totalCompletedWordsCount > 0 ? (float)mistakesCount / (float)totalCompletedWordsCount * 100.0f : 100.0f;
                mistakesText += string.Format(" ({0:0.0}%)", mistakesPercent);
                correctText += string.Format(" ({0:0.0}%)", 100.0f - mistakesPercent);
            }
            else
            {
                actionButton.Parent.Controls.Remove(actionButton);
                actionButton.Dispose();
                completeButton.Location = new System.Drawing.Point(this.Size.Width / 2 - completeButton.Size.Width / 2, completeButton.Location.Y);
            }
            rightCountLabel.Text = correctText;
            mistakesCountLabel.Text = mistakesText;
        }

        private void actionButton_Click(object sender, EventArgs e)
        {
            _mistakesCorrectionCallback?.Invoke();
            _exitOnCloseForm = false;
            Dispose();
            Close();
        }

        private void completeButton_Click(object sender, EventArgs e)
        {
            _finishCallback?.Invoke();
            _exitOnCloseForm = false;
            Dispose();
            Close();
        }

        private void AccentExerciseResultWindow_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (e.CloseReason == CloseReason.UserClosing && _exitOnCloseForm)
            {
                MainActivity.getInstance().terminate();
            }
        }
    }
}