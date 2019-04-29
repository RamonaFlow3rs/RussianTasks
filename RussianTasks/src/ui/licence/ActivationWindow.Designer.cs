namespace RussianTasks.src.ui.licence
{
    partial class ActivationWindow
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ActivationWindow));
            this.keyInputTextbox = new System.Windows.Forms.TextBox();
            this.applyKeyButton = new System.Windows.Forms.Button();
            this.messagelabel = new System.Windows.Forms.Label();
            this.activationProgressbar = new System.Windows.Forms.ProgressBar();
            this.nameInputTextbox = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // keyInputTextbox
            // 
            this.keyInputTextbox.Font = new System.Drawing.Font("Arial", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.keyInputTextbox.Location = new System.Drawing.Point(12, 89);
            this.keyInputTextbox.Name = "keyInputTextbox";
            this.keyInputTextbox.Size = new System.Drawing.Size(316, 29);
            this.keyInputTextbox.TabIndex = 2;
            this.keyInputTextbox.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // applyKeyButton
            // 
            this.applyKeyButton.Font = new System.Drawing.Font("Arial", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.applyKeyButton.Location = new System.Drawing.Point(111, 124);
            this.applyKeyButton.Name = "applyKeyButton";
            this.applyKeyButton.Size = new System.Drawing.Size(121, 34);
            this.applyKeyButton.TabIndex = 3;
            this.applyKeyButton.UseVisualStyleBackColor = true;
            this.applyKeyButton.Click += new System.EventHandler(this.applyKeyButton_Click);
            // 
            // messagelabel
            // 
            this.messagelabel.Dock = System.Windows.Forms.DockStyle.Top;
            this.messagelabel.Font = new System.Drawing.Font("Arial", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.messagelabel.Location = new System.Drawing.Point(0, 0);
            this.messagelabel.Name = "messagelabel";
            this.messagelabel.Size = new System.Drawing.Size(339, 51);
            this.messagelabel.TabIndex = 0;
            this.messagelabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // activationProgressbar
            // 
            this.activationProgressbar.Location = new System.Drawing.Point(12, 68);
            this.activationProgressbar.Name = "activationProgressbar";
            this.activationProgressbar.Size = new System.Drawing.Size(315, 29);
            this.activationProgressbar.Style = System.Windows.Forms.ProgressBarStyle.Marquee;
            this.activationProgressbar.TabIndex = 3;
            this.activationProgressbar.Visible = false;
            // 
            // nameInputTextbox
            // 
            this.nameInputTextbox.Font = new System.Drawing.Font("Arial", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.nameInputTextbox.Location = new System.Drawing.Point(12, 54);
            this.nameInputTextbox.Name = "nameInputTextbox";
            this.nameInputTextbox.Size = new System.Drawing.Size(316, 29);
            this.nameInputTextbox.TabIndex = 1;
            this.nameInputTextbox.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // ActivationWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(339, 168);
            this.Controls.Add(this.activationProgressbar);
            this.Controls.Add(this.nameInputTextbox);
            this.Controls.Add(this.messagelabel);
            this.Controls.Add(this.applyKeyButton);
            this.Controls.Add(this.keyInputTextbox);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "ActivationWindow";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.ActivationWindow_FormClosing);
            this.Load += new System.EventHandler(this.ActivationWindow_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox keyInputTextbox;
        private System.Windows.Forms.Button applyKeyButton;
        private System.Windows.Forms.Label messagelabel;
        private System.Windows.Forms.ProgressBar activationProgressbar;
        private System.Windows.Forms.TextBox nameInputTextbox;
    }
}