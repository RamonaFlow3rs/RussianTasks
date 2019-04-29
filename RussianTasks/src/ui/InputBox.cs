using System;
using System.Windows.Forms;

namespace RussianTasks.src.ui
{
    public partial class InputBox : Form
    {
        Action _closeHandler;
        Action<InputBox> _buttonHandler;

        public InputBox(string title, string message, Action closeHandler, string buttonText, Action<InputBox> buttonHandler, bool hideInput = false)
        {
            InitializeComponent();

            this.Text = title;
            messagelabel.Text = message;
            applyButton.Text = buttonText;
            if (hideInput)
            {
                inputField.PasswordChar = '*';
            }
            _closeHandler = closeHandler;
            _buttonHandler = buttonHandler;
        }

        public string getValue()
        {
            return inputField.Text.ToString();
        }

        public void closeThisWindow()
        {
            this.Close();
            this.Dispose();
        }

        private void applyButton_Click(object sender, EventArgs e)
        {
            _buttonHandler?.Invoke(this);
        }

        private void InputBox_FormClosing(object sender, FormClosingEventArgs e)
        {
            _closeHandler?.Invoke();
        }
    }
}
