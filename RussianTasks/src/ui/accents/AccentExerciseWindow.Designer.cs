namespace RussianTasks.src.ui.accents {
    partial class AccentExerciseWindow {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing) {
            if (disposing && (components != null)) {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent() {
            this.components = new System.ComponentModel.Container();
            this.LettersPanel = new System.Windows.Forms.Panel();
            this.vocabularyButton = new System.Windows.Forms.Button();
            this.continueButton = new System.Windows.Forms.Button();
            this.TitleLabel = new System.Windows.Forms.Label();
            this.wordsCounterLabel = new System.Windows.Forms.Label();
            this._tooltip = new System.Windows.Forms.ToolTip(this.components);
            this.SuspendLayout();
            // 
            // LettersPanel
            // 
            this.LettersPanel.BackColor = System.Drawing.SystemColors.Info;
            this.LettersPanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.LettersPanel.Location = new System.Drawing.Point(12, 53);
            this.LettersPanel.Name = "LettersPanel";
            this.LettersPanel.Size = new System.Drawing.Size(800, 300);
            this.LettersPanel.TabIndex = 0;
            // 
            // vocabularyButton
            // 
            this.vocabularyButton.BackColor = System.Drawing.Color.Transparent;
            this.vocabularyButton.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.vocabularyButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.vocabularyButton.ForeColor = System.Drawing.SystemColors.ControlText;
            this.vocabularyButton.Location = new System.Drawing.Point(760, 361);
            this.vocabularyButton.Name = "vocabularyButton";
            this.vocabularyButton.Size = new System.Drawing.Size(52, 52);
            this.vocabularyButton.TabIndex = 4;
            this.vocabularyButton.TabStop = false;
            this.vocabularyButton.UseVisualStyleBackColor = false;
            // 
            // continueButton
            // 
            this.continueButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.continueButton.Location = new System.Drawing.Point(314, 367);
            this.continueButton.Name = "continueButton";
            this.continueButton.Size = new System.Drawing.Size(208, 40);
            this.continueButton.TabIndex = 0;
            this.continueButton.TabStop = false;
            this.continueButton.UseVisualStyleBackColor = true;
            // 
            // TitleLabel
            // 
            this.TitleLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.TitleLabel.Location = new System.Drawing.Point(12, 9);
            this.TitleLabel.Name = "TitleLabel";
            this.TitleLabel.Size = new System.Drawing.Size(802, 31);
            this.TitleLabel.TabIndex = 1;
            this.TitleLabel.Text = "Прилагательные";
            this.TitleLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // wordsCounterLabel
            // 
            this.wordsCounterLabel.AutoSize = true;
            this.wordsCounterLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.wordsCounterLabel.Location = new System.Drawing.Point(8, 377);
            this.wordsCounterLabel.Name = "wordsCounterLabel";
            this.wordsCounterLabel.Size = new System.Drawing.Size(141, 24);
            this.wordsCounterLabel.TabIndex = 2;
            this.wordsCounterLabel.Text = "Прогресс: 3/17";
            // 
            // AccentExerciseWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(826, 420);
            this.Controls.Add(this.vocabularyButton);
            this.Controls.Add(this.wordsCounterLabel);
            this.Controls.Add(this.TitleLabel);
            this.Controls.Add(this.continueButton);
            this.Controls.Add(this.LettersPanel);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "AccentExerciseWindow";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Проверка ударений";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.AccentExerciseWindow_FormClosing);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel LettersPanel;
        private System.Windows.Forms.Button continueButton;
        private System.Windows.Forms.Label TitleLabel;
        private System.Windows.Forms.Label wordsCounterLabel;
        private System.Windows.Forms.Button vocabularyButton;
        private System.Windows.Forms.ToolTip _tooltip;
    }
}