namespace KeysGenerator
{
    partial class MainWindow
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
            this.inputbox = new System.Windows.Forms.TextBox();
            this.mainButton = new System.Windows.Forms.Button();
            this.hintLabel = new System.Windows.Forms.Label();
            this.keyPanel = new System.Windows.Forms.Panel();
            this.outputBox = new System.Windows.Forms.Label();
            this.keyPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // inputbox
            // 
            this.inputbox.Font = new System.Drawing.Font("Arial", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.inputbox.Location = new System.Drawing.Point(12, 12);
            this.inputbox.Name = "inputbox";
            this.inputbox.Size = new System.Drawing.Size(415, 32);
            this.inputbox.TabIndex = 0;
            this.inputbox.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // mainButton
            // 
            this.mainButton.Font = new System.Drawing.Font("Arial", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.mainButton.Location = new System.Drawing.Point(123, 112);
            this.mainButton.Name = "mainButton";
            this.mainButton.Size = new System.Drawing.Size(190, 33);
            this.mainButton.TabIndex = 1;
            this.mainButton.Text = "Генерировать ключ";
            this.mainButton.UseVisualStyleBackColor = true;
            this.mainButton.Click += new System.EventHandler(this.mainButton_Click);
            // 
            // hintLabel
            // 
            this.hintLabel.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.hintLabel.Location = new System.Drawing.Point(3, 30);
            this.hintLabel.Name = "hintLabel";
            this.hintLabel.Size = new System.Drawing.Size(409, 21);
            this.hintLabel.TabIndex = 3;
            this.hintLabel.Text = "Кликни, чтобы скопировать";
            this.hintLabel.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // keyPanel
            // 
            this.keyPanel.Controls.Add(this.outputBox);
            this.keyPanel.Controls.Add(this.hintLabel);
            this.keyPanel.Location = new System.Drawing.Point(12, 50);
            this.keyPanel.Name = "keyPanel";
            this.keyPanel.Size = new System.Drawing.Size(415, 56);
            this.keyPanel.TabIndex = 4;
            this.keyPanel.Visible = false;
            // 
            // outputBox
            // 
            this.outputBox.Font = new System.Drawing.Font("Arial", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.outputBox.Location = new System.Drawing.Point(3, 2);
            this.outputBox.Name = "outputBox";
            this.outputBox.Size = new System.Drawing.Size(406, 28);
            this.outputBox.TabIndex = 4;
            this.outputBox.Text = "label2";
            this.outputBox.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            // 
            // MainWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(439, 154);
            this.Controls.Add(this.keyPanel);
            this.Controls.Add(this.mainButton);
            this.Controls.Add(this.inputbox);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "MainWindow";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "ЕГЭ-Тренажёр: Генератор ключей";
            this.Load += new System.EventHandler(this.MainWindow_Load);
            this.keyPanel.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox inputbox;
        private System.Windows.Forms.Button mainButton;
        private System.Windows.Forms.Label hintLabel;
        private System.Windows.Forms.Panel keyPanel;
        private System.Windows.Forms.Label outputBox;
    }
}

