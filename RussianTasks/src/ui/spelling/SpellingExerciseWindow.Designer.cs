namespace RussianTasks.src.ui.spelling
{
    partial class SpellingExerciseWindow
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
            this.lettersPanel = new System.Windows.Forms.Panel();
            this.vocabularyButton = new System.Windows.Forms.Button();
            this.skipButton = new System.Windows.Forms.Button();
            this.TitleLabel = new System.Windows.Forms.Label();
            this.wordsCounterLabel = new System.Windows.Forms.Label();
            this._tooltip = new System.Windows.Forms.ToolTip(this.components);
            this.continueButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // lettersPanel
            // 
            this.lettersPanel.BackColor = System.Drawing.SystemColors.Info;
            this.lettersPanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lettersPanel.Location = new System.Drawing.Point(12, 43);
            this.lettersPanel.Name = "lettersPanel";
            this.lettersPanel.Size = new System.Drawing.Size(800, 192);
            this.lettersPanel.TabIndex = 0;
            // 
            // vocabularyButton
            // 
            this.vocabularyButton.BackColor = System.Drawing.Color.Transparent;
            this.vocabularyButton.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.vocabularyButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.vocabularyButton.ForeColor = System.Drawing.SystemColors.ControlText;
            this.vocabularyButton.Location = new System.Drawing.Point(760, 242);
            this.vocabularyButton.Name = "vocabularyButton";
            this.vocabularyButton.Size = new System.Drawing.Size(52, 52);
            this.vocabularyButton.TabIndex = 4;
            this.vocabularyButton.TabStop = false;
            this.vocabularyButton.UseVisualStyleBackColor = false;
            // 
            // skipButton
            // 
            this.skipButton.Font = new System.Drawing.Font("Arial", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.skipButton.Location = new System.Drawing.Point(217, 250);
            this.skipButton.Name = "skipButton";
            this.skipButton.Size = new System.Drawing.Size(200, 40);
            this.skipButton.TabIndex = 0;
            this.skipButton.TabStop = false;
            this.skipButton.UseVisualStyleBackColor = true;
            // 
            // TitleLabel
            // 
            this.TitleLabel.AutoSize = true;
            this.TitleLabel.Font = new System.Drawing.Font("Arial", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.TitleLabel.Location = new System.Drawing.Point(299, 9);
            this.TitleLabel.Name = "TitleLabel";
            this.TitleLabel.Size = new System.Drawing.Size(222, 32);
            this.TitleLabel.TabIndex = 1;
            this.TitleLabel.Text = "Прилагательные";
            // 
            // wordsCounterLabel
            // 
            this.wordsCounterLabel.AutoSize = true;
            this.wordsCounterLabel.Font = new System.Drawing.Font("Arial", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.wordsCounterLabel.Location = new System.Drawing.Point(8, 259);
            this.wordsCounterLabel.Name = "wordsCounterLabel";
            this.wordsCounterLabel.Size = new System.Drawing.Size(142, 22);
            this.wordsCounterLabel.TabIndex = 2;
            this.wordsCounterLabel.Text = "Прогресс: 3/17";
            // 
            // continueButton
            // 
            this.continueButton.Font = new System.Drawing.Font("Arial", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.continueButton.Location = new System.Drawing.Point(423, 250);
            this.continueButton.Name = "continueButton";
            this.continueButton.Size = new System.Drawing.Size(200, 40);
            this.continueButton.TabIndex = 4;
            this.continueButton.TabStop = false;
            this.continueButton.UseVisualStyleBackColor = true;
            this.continueButton.Click += new System.EventHandler(this.giveAnswerHandler);
            // 
            // SpellingExerciseWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(826, 301);
            this.Controls.Add(this.vocabularyButton);
            this.Controls.Add(this.continueButton);
            this.Controls.Add(this.wordsCounterLabel);
            this.Controls.Add(this.TitleLabel);
            this.Controls.Add(this.skipButton);
            this.Controls.Add(this.lettersPanel);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "SpellingExerciseWindow";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Проверка ударений";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.formClosingHandler);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel lettersPanel;
        private System.Windows.Forms.Button skipButton;
        private System.Windows.Forms.Label TitleLabel;
        private System.Windows.Forms.Label wordsCounterLabel;
        private System.Windows.Forms.Button vocabularyButton;
        private System.Windows.Forms.ToolTip _tooltip;
        private System.Windows.Forms.Button continueButton;
    }
}