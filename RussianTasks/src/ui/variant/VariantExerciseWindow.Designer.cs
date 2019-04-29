namespace RussianTasks.src.ui.variant
{
    partial class VariantExerciseWindow
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
            this.TitleLabel = new System.Windows.Forms.Label();
            this.mainPanel = new System.Windows.Forms.Label();
            this.skipButton = new System.Windows.Forms.Button();
            this.vocabularyButton = new System.Windows.Forms.Button();
            this.wordsCounterLabel = new System.Windows.Forms.Label();
            this._tooltip = new System.Windows.Forms.ToolTip(this.components);
            this.questionLabel = new System.Windows.Forms.Label();
            this.continueButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // TitleLabel
            // 
            this.TitleLabel.Font = new System.Drawing.Font("Arial Unicode MS", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.TitleLabel.Location = new System.Drawing.Point(15, 4);
            this.TitleLabel.Name = "TitleLabel";
            this.TitleLabel.Size = new System.Drawing.Size(783, 61);
            this.TitleLabel.TabIndex = 0;
            this.TitleLabel.Text = "Грамматические нормы";
            this.TitleLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // mainPanel
            // 
            this.mainPanel.BackColor = System.Drawing.SystemColors.Info;
            this.mainPanel.Location = new System.Drawing.Point(12, 65);
            this.mainPanel.Name = "mainPanel";
            this.mainPanel.Size = new System.Drawing.Size(785, 318);
            this.mainPanel.TabIndex = 1;
            // 
            // skipButton
            // 
            this.skipButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.skipButton.Location = new System.Drawing.Point(327, 408);
            this.skipButton.Name = "skipButton";
            this.skipButton.Size = new System.Drawing.Size(170, 38);
            this.skipButton.TabIndex = 2;
            this.skipButton.UseVisualStyleBackColor = true;
            // 
            // vocabularyButton
            // 
            this.vocabularyButton.Location = new System.Drawing.Point(722, 401);
            this.vocabularyButton.Name = "vocabularyButton";
            this.vocabularyButton.Size = new System.Drawing.Size(75, 57);
            this.vocabularyButton.TabIndex = 4;
            this.vocabularyButton.UseVisualStyleBackColor = true;
            // 
            // wordsCounterLabel
            // 
            this.wordsCounterLabel.Font = new System.Drawing.Font("Arial Unicode MS", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.wordsCounterLabel.Location = new System.Drawing.Point(16, 414);
            this.wordsCounterLabel.Name = "wordsCounterLabel";
            this.wordsCounterLabel.Size = new System.Drawing.Size(151, 38);
            this.wordsCounterLabel.TabIndex = 5;
            this.wordsCounterLabel.Text = "Прогресс: 8/17";
            // 
            // questionLabel
            // 
            this.questionLabel.BackColor = System.Drawing.SystemColors.Info;
            this.questionLabel.Font = new System.Drawing.Font("Arial Unicode MS", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.questionLabel.Location = new System.Drawing.Point(13, 65);
            this.questionLabel.Name = "questionLabel";
            this.questionLabel.Size = new System.Drawing.Size(785, 80);
            this.questionLabel.TabIndex = 6;
            this.questionLabel.Text = "question label";
            this.questionLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // continueButton
            // 
            this.continueButton.Font = new System.Drawing.Font("Arial Unicode MS", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.continueButton.Location = new System.Drawing.Point(327, 408);
            this.continueButton.Name = "continueButton";
            this.continueButton.Size = new System.Drawing.Size(170, 39);
            this.continueButton.TabIndex = 7;
            this.continueButton.UseVisualStyleBackColor = true;
            // 
            // VariantExerciseWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(810, 470);
            this.Controls.Add(this.continueButton);
            this.Controls.Add(this.questionLabel);
            this.Controls.Add(this.wordsCounterLabel);
            this.Controls.Add(this.vocabularyButton);
            this.Controls.Add(this.skipButton);
            this.Controls.Add(this.mainPanel);
            this.Controls.Add(this.TitleLabel);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "VariantExerciseWindow";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Form1";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label TitleLabel;
        private System.Windows.Forms.Label mainPanel;
        private System.Windows.Forms.Button skipButton;
        private System.Windows.Forms.Button vocabularyButton;
        private System.Windows.Forms.Label wordsCounterLabel;
        private System.Windows.Forms.ToolTip _tooltip;
        private System.Windows.Forms.Label questionLabel;
        private System.Windows.Forms.Button continueButton;
    }
}