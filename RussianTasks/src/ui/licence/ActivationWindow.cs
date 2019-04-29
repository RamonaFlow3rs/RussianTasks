using System;
using System.Windows.Forms;

namespace RussianTasks.src.ui.licence
{
    public partial class ActivationWindow : Form
    {
        Action<string, string> _onKeyEnteredCallback;
        bool _exitOnFormClose = true;

        public ActivationWindow(Action<string, string> onKeyEnteredCallback)
        {
            _onKeyEnteredCallback = onKeyEnteredCallback;
            InitializeComponent();
            createView();
            Form frm = this;
            UIHelpers.setupWindowIcon(ref frm);
        }
        
        public void setBlockedView(bool blocked)
        {
            if (blocked)
            {
                messagelabel.Text = Properties.Strings.activation_window_wait_message;
                applyKeyButton.Enabled = false;
                activationProgressbar.Visible = true;
                nameInputTextbox.Visible = false;
                keyInputTextbox.Visible = false;
            }
            else
            {
                messagelabel.Text = Properties.Strings.activation_window_enter_key_message;
                activationProgressbar.Visible = false;
                applyKeyButton.Enabled = true;
                nameInputTextbox.Visible = true;
                keyInputTextbox.Visible = true;
            }
        }

        public void closeWindow()
        {
            _exitOnFormClose = false;
            Close();
            Dispose();
        }

        private void createView()
        {
            this.Text = Properties.Strings.activation_window_title;
            messagelabel.Text = Properties.Strings.activation_window_enter_key_message;
            applyKeyButton.Text = Properties.Strings.activation_window_button_text;
            Form frm = this;
            ui.UIHelpers.setupWindowIcon(ref frm);
            nameInputTextbox.Tag = Properties.Strings.activation_window_field_1;
            keyInputTextbox.Tag = Properties.Strings.activation_window_field_2;
            setFieldHandlers(nameInputTextbox);
            setFieldHandlers(keyInputTextbox);
        }

        private void setFieldHandlers(TextBox control)
        {
            control.Enter += onFocusField;
            control.Leave += onUnfocusField;

            control.Text = control.Tag as string;
            control.ForeColor = System.Drawing.Color.Gray;
        }

        private void onFocusField(object sender, EventArgs e)
        {
            TextBox field = sender as TextBox;
            if (field.Text == field.Tag as string)
            {
                field.Text = string.Empty;
                field.ForeColor = System.Drawing.Color.Black;
            }
        }

        private void onUnfocusField(object sender, EventArgs e)
        {
            TextBox field = sender as TextBox;
            if (string.IsNullOrEmpty(field.Text))
            {
                field.Text = field.Tag as string;
                field.ForeColor = System.Drawing.Color.Gray;
            }
        }

        private void ActivationWindow_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (e.CloseReason == CloseReason.UserClosing && _exitOnFormClose)
            {
                MainActivity.getInstance().terminate(false);
            }
        }

        private string checkField(string field, TextBox textbox)
        {
            string trimmedField = field.Trim();
            if (field != trimmedField)
            {
                textbox.Text = trimmedField;
            }
            return trimmedField;
        }

        private void applyKeyButton_Click(object sender, EventArgs e)
        {
            string inputedName = checkField(nameInputTextbox.Text.ToString(), nameInputTextbox);
            string inputedKey = checkField(keyInputTextbox.Text.ToString(), keyInputTextbox);

            _onKeyEnteredCallback.Invoke(inputedName, inputedKey);
        }

        private void ActivationWindow_Load(object sender, EventArgs e)
        {
            this.ActiveControl = messagelabel;
        }
    }
}
