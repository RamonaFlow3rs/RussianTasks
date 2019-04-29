using System;
using System.Windows.Forms;

namespace RussianTasks.src.ui
{
    public partial class AlertWindow : Form
    {
        string _title;
        string _message;
        string _firstButtonText;
        string _secondButtonText;
        Action _closeHandler;
        Action _firstButtonHandler;
        Action _secondButtonHandler;
        bool _closedByButton = false;

        public static void show(string title, string message, Action closeHandler, string firstButtonText, Action firstButtonHandler, string secondButtonText, Action secondButtonHandler)
        {
            new AlertWindow(title,
                message,
                closeHandler,
                firstButtonText,
                firstButtonHandler,
                secondButtonText,
                secondButtonHandler).ShowDialog();
        }

        private AlertWindow(string title, string message, Action closeHandler, string firstButtonText, Action firstButtonHandler, string secondButtonText, Action secondButtonHandler)
        {
            _title = title;
            _message = message;
            _firstButtonText = firstButtonText;
            _secondButtonText = secondButtonText;
            _closeHandler = closeHandler;
            _firstButtonHandler = firstButtonHandler;
            _secondButtonHandler = secondButtonHandler;

            createView();
            Form frm = this;
            UIHelpers.setupWindowIcon(ref frm);
        }

        private void createView()
        {
            InitializeComponent();

            this.Text = _title;
            label_message.Text = _message;
            button_1.Text = _firstButtonText;
            button_2.Text = _secondButtonText;
            button_1.Click += Button_1_Click;
            button_2.Click += Button_2_Click;
        }

        private void Button_1_Click(object sender, EventArgs e)
        {
            closeWindow(true);
            _firstButtonHandler?.Invoke();
        }

        private void Button_2_Click(object sender, EventArgs e)
        {
            closeWindow(true);
            _secondButtonHandler?.Invoke();
        }

        private void AlertWindow_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (e.CloseReason == CloseReason.UserClosing && !_closedByButton)
            {
                _closeHandler?.Invoke();
            }
        }

        private void closeWindow(bool closedByButton)
        {
            _closedByButton = closedByButton;
            this.Close();
            this.Dispose();
        }
    }
}
