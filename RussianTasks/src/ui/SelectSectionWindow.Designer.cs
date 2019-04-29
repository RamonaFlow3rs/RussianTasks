namespace RussianTasks.src.ui
{
    partial class SelectSectionWindow
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
            this.sectionsTitleLabel = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // sectionsTitleLabel
            // 
            this.sectionsTitleLabel.Dock = System.Windows.Forms.DockStyle.Top;
            this.sectionsTitleLabel.Font = new System.Drawing.Font("Arial", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.sectionsTitleLabel.Location = new System.Drawing.Point(0, 0);
            this.sectionsTitleLabel.Name = "sectionsTitleLabel";
            this.sectionsTitleLabel.Size = new System.Drawing.Size(339, 47);
            this.sectionsTitleLabel.TabIndex = 2;
            this.sectionsTitleLabel.Text = "Части речи";
            this.sectionsTitleLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // SelectSectionWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(339, 197);
            this.Controls.Add(this.sectionsTitleLabel);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "SelectSectionWindow";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Выбор раздела";
            this.Activated += new System.EventHandler(this.SelectSectionWindow_Activated);
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.SelectSectionWindow_FormClosing);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label sectionsTitleLabel;
    }
}