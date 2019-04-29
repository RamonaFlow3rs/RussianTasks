namespace RussianTasks.src.ui
{
    partial class ExerciseResultWindow
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
            this.titleLabel = new System.Windows.Forms.Label();
            this.sectionNameLabel = new System.Windows.Forms.Label();
            this.wordsTotalLabel = new System.Windows.Forms.Label();
            this.rightCountLabel = new System.Windows.Forms.Label();
            this.mistakesCountLabel = new System.Windows.Forms.Label();
            this.actionButton = new System.Windows.Forms.Button();
            this.completeButton = new System.Windows.Forms.Button();
            this.skippedWordsLabel = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // titleLabel
            // 
            this.titleLabel.AutoSize = true;
            this.titleLabel.Font = new System.Drawing.Font("Arial", 26.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.titleLabel.Location = new System.Drawing.Point(125, 9);
            this.titleLabel.Name = "titleLabel";
            this.titleLabel.Size = new System.Drawing.Size(259, 40);
            this.titleLabel.TabIndex = 0;
            this.titleLabel.Text = "Ваш результат:";
            // 
            // sectionNameLabel
            // 
            this.sectionNameLabel.Font = new System.Drawing.Font("Arial", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.sectionNameLabel.Location = new System.Drawing.Point(12, 82);
            this.sectionNameLabel.Name = "sectionNameLabel";
            this.sectionNameLabel.Size = new System.Drawing.Size(486, 32);
            this.sectionNameLabel.TabIndex = 1;
            this.sectionNameLabel.Text = "Раздел:";
            // 
            // wordsTotalLabel
            // 
            this.wordsTotalLabel.AutoSize = true;
            this.wordsTotalLabel.Font = new System.Drawing.Font("Arial", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.wordsTotalLabel.Location = new System.Drawing.Point(12, 172);
            this.wordsTotalLabel.Name = "wordsTotalLabel";
            this.wordsTotalLabel.Size = new System.Drawing.Size(161, 32);
            this.wordsTotalLabel.TabIndex = 2;
            this.wordsTotalLabel.Text = "Всего слов:";
            // 
            // rightCountLabel
            // 
            this.rightCountLabel.AutoSize = true;
            this.rightCountLabel.Font = new System.Drawing.Font("Arial", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.rightCountLabel.Location = new System.Drawing.Point(12, 217);
            this.rightCountLabel.Name = "rightCountLabel";
            this.rightCountLabel.Size = new System.Drawing.Size(93, 32);
            this.rightCountLabel.TabIndex = 3;
            this.rightCountLabel.Text = "Верно";
            // 
            // mistakesCountLabel
            // 
            this.mistakesCountLabel.AutoSize = true;
            this.mistakesCountLabel.Font = new System.Drawing.Font("Arial", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.mistakesCountLabel.Location = new System.Drawing.Point(12, 262);
            this.mistakesCountLabel.Name = "mistakesCountLabel";
            this.mistakesCountLabel.Size = new System.Drawing.Size(131, 32);
            this.mistakesCountLabel.TabIndex = 4;
            this.mistakesCountLabel.Text = "Неверно:";
            // 
            // actionButton
            // 
            this.actionButton.Font = new System.Drawing.Font("Arial", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.actionButton.Location = new System.Drawing.Point(18, 327);
            this.actionButton.Name = "actionButton";
            this.actionButton.Size = new System.Drawing.Size(237, 40);
            this.actionButton.TabIndex = 5;
            this.actionButton.Text = "Работа над ошибками";
            this.actionButton.UseVisualStyleBackColor = true;
            this.actionButton.Click += new System.EventHandler(this.actionButton_Click);
            // 
            // completeButton
            // 
            this.completeButton.Font = new System.Drawing.Font("Arial", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.completeButton.Location = new System.Drawing.Point(261, 327);
            this.completeButton.Name = "completeButton";
            this.completeButton.Size = new System.Drawing.Size(237, 40);
            this.completeButton.TabIndex = 6;
            this.completeButton.Text = "Завершить";
            this.completeButton.UseVisualStyleBackColor = true;
            this.completeButton.Click += new System.EventHandler(this.completeButton_Click);
            // 
            // skippedWordsLabel
            // 
            this.skippedWordsLabel.AutoSize = true;
            this.skippedWordsLabel.Font = new System.Drawing.Font("Arial", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.skippedWordsLabel.Location = new System.Drawing.Point(12, 127);
            this.skippedWordsLabel.Name = "skippedWordsLabel";
            this.skippedWordsLabel.Size = new System.Drawing.Size(234, 32);
            this.skippedWordsLabel.TabIndex = 7;
            this.skippedWordsLabel.Text = "Пропущено слов:";
            // 
            // ExerciseResultWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(506, 382);
            this.Controls.Add(this.skippedWordsLabel);
            this.Controls.Add(this.completeButton);
            this.Controls.Add(this.actionButton);
            this.Controls.Add(this.mistakesCountLabel);
            this.Controls.Add(this.rightCountLabel);
            this.Controls.Add(this.wordsTotalLabel);
            this.Controls.Add(this.sectionNameLabel);
            this.Controls.Add(this.titleLabel);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "ExerciseResultWindow";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Результаты.";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.AccentExerciseResultWindow_FormClosing);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label titleLabel;
        private System.Windows.Forms.Label sectionNameLabel;
        private System.Windows.Forms.Label wordsTotalLabel;
        private System.Windows.Forms.Label rightCountLabel;
        private System.Windows.Forms.Label mistakesCountLabel;
        private System.Windows.Forms.Button actionButton;
        private System.Windows.Forms.Button completeButton;
        private System.Windows.Forms.Label skippedWordsLabel;
    }
}