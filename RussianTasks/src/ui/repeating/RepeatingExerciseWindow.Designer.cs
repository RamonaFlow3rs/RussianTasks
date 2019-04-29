namespace RussianTasks.src.ui.repeating
{
    partial class RepeatingExerciseWindow
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.mainPanel = new System.Windows.Forms.Panel();
            this.answerInputBox = new System.Windows.Forms.TextBox();
            this.questionLabel = new System.Windows.Forms.Label();
            this.vocabularyButton = new System.Windows.Forms.Button();
            this.skipButton = new System.Windows.Forms.Button();
            this.TitleLabel = new System.Windows.Forms.Label();
            this.wordsCounterLabel = new System.Windows.Forms.Label();
            this._tooltip = new System.Windows.Forms.ToolTip(this.components);
            this.continueButton = new System.Windows.Forms.Button();
            this.mainPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // mainPanel
            // 
            this.mainPanel.BackColor = System.Drawing.SystemColors.Info;
            this.mainPanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.mainPanel.Controls.Add(this.answerInputBox);
            this.mainPanel.Controls.Add(this.questionLabel);
            this.mainPanel.Location = new System.Drawing.Point(12, 43);
            this.mainPanel.Name = "mainPanel";
            this.mainPanel.Size = new System.Drawing.Size(800, 192);
            this.mainPanel.TabIndex = 0;
            // 
            // answerInputBox
            // 
            this.answerInputBox.Font = new System.Drawing.Font("Arial", 21.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.answerInputBox.Location = new System.Drawing.Point(194, 116);
            this.answerInputBox.Name = "answerInputBox";
            this.answerInputBox.Size = new System.Drawing.Size(414, 41);
            this.answerInputBox.TabIndex = 6;
            this.answerInputBox.TabStop = false;
            this.answerInputBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.answerInputBox.TextChanged += new System.EventHandler(this.answerInputBox_TextChanged);
            this.answerInputBox.KeyUp += new System.Windows.Forms.KeyEventHandler(this.answerInputBox_KeyUp);
            // 
            // questionLabel
            // 
            this.questionLabel.AutoSize = true;
            this.questionLabel.Font = new System.Drawing.Font("Arial", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.questionLabel.Location = new System.Drawing.Point(3, 0);
            this.questionLabel.MaximumSize = new System.Drawing.Size(792, 113);
            this.questionLabel.MinimumSize = new System.Drawing.Size(792, 113);
            this.questionLabel.Name = "questionLabel";
            this.questionLabel.Size = new System.Drawing.Size(792, 113);
            this.questionLabel.TabIndex = 5;
            this.questionLabel.Text = "questionLabel";
            this.questionLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // vocabularyButton
            // 
            this.vocabularyButton.BackColor = System.Drawing.Color.Transparent;
            this.vocabularyButton.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.vocabularyButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.vocabularyButton.ForeColor = System.Drawing.SystemColors.ControlText;
            this.vocabularyButton.Location = new System.Drawing.Point(762, 241);
            this.vocabularyButton.Name = "vocabularyButton";
            this.vocabularyButton.Size = new System.Drawing.Size(52, 52);
            this.vocabularyButton.TabIndex = 4;
            this.vocabularyButton.TabStop = false;
            this.vocabularyButton.UseVisualStyleBackColor = false;
            // 
            // skipButton
            // 
            this.skipButton.Font = new System.Drawing.Font("Arial", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.skipButton.Location = new System.Drawing.Point(207, 249);
            this.skipButton.Name = "skipButton";
            this.skipButton.Size = new System.Drawing.Size(200, 40);
            this.skipButton.TabIndex = 0;
            this.skipButton.TabStop = false;
            this.skipButton.UseVisualStyleBackColor = true;
            // 
            // TitleLabel
            // 
            this.TitleLabel.Font = new System.Drawing.Font("Arial", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.TitleLabel.Location = new System.Drawing.Point(12, 9);
            this.TitleLabel.Name = "TitleLabel";
            this.TitleLabel.Size = new System.Drawing.Size(800, 32);
            this.TitleLabel.TabIndex = 1;
            this.TitleLabel.Text = "Window Title";
            this.TitleLabel.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // wordsCounterLabel
            // 
            this.wordsCounterLabel.AutoSize = true;
            this.wordsCounterLabel.Font = new System.Drawing.Font("Arial", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.wordsCounterLabel.Location = new System.Drawing.Point(8, 258);
            this.wordsCounterLabel.Name = "wordsCounterLabel";
            this.wordsCounterLabel.Size = new System.Drawing.Size(142, 22);
            this.wordsCounterLabel.TabIndex = 2;
            this.wordsCounterLabel.Text = "Прогресс: 3/17";
            // 
            // continueButton
            // 
            this.continueButton.Font = new System.Drawing.Font("Arial", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.continueButton.Location = new System.Drawing.Point(421, 249);
            this.continueButton.Name = "continueButton";
            this.continueButton.Size = new System.Drawing.Size(200, 40);
            this.continueButton.TabIndex = 4;
            this.continueButton.TabStop = false;
            this.continueButton.UseVisualStyleBackColor = true;
            this.continueButton.Click += new System.EventHandler(this.giveAnswerHandler);
            // 
            // RepeatingExerciseWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(826, 300);
            this.Controls.Add(this.continueButton);
            this.Controls.Add(this.wordsCounterLabel);
            this.Controls.Add(this.vocabularyButton);
            this.Controls.Add(this.TitleLabel);
            this.Controls.Add(this.skipButton);
            this.Controls.Add(this.mainPanel);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "RepeatingExerciseWindow";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.formClosingHandler);
            this.mainPanel.ResumeLayout(false);
            this.mainPanel.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel mainPanel;
        private System.Windows.Forms.Button skipButton;
        private System.Windows.Forms.Label TitleLabel;
        private System.Windows.Forms.Label wordsCounterLabel;
        private System.Windows.Forms.Button vocabularyButton;
        private System.Windows.Forms.ToolTip _tooltip;
        private System.Windows.Forms.Button continueButton;
        private System.Windows.Forms.TextBox answerInputBox;
        private System.Windows.Forms.Label questionLabel;
    }
}