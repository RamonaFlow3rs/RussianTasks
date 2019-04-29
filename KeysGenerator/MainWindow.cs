using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace KeysGenerator
{
    public partial class MainWindow : Form
    {
        private Dictionary<string, string[]> timestampsEncodeArrays = new Dictionary<string, string[]>()
        {
            { "D", new string[] { "Q", "Z", "D", "1", "L", "V", "E", "U", "R", "K" } },
            { "3", new string[] { "M", "3", "W", "L", "A", "G", "9", "B", "X", "7" } },
            { "5", new string[] { "V", "B", "N", "E", "Q", "L", "Y", "U", "R", "K" } },
            { "U", new string[] { "F", "Z", "D", "H", "5", "Y", "7", "T", "K", "S" } },
            { "M", new string[] { "Q", "4", "F", "Y", "L", "V", "E", "P", "R", "5" } }
        };

        private static int[] positions = { 0, 1, 3, 4, 7, 10, 13, 15, 17 };
        private static string SALT = "g#d5Ao@Pdu3#Aw1!dk";
        private static string KEY_TEMPLATE = "{0}-{1}-{2}-{3}";// xxxxx-xxxxx-xxxxx-xxxxx


        public MainWindow()
        {
            InitializeComponent();
            createView();
        }


        private void createView()
        {
            inputbox.Tag = "Введите имя пользователя";
            setFieldHandlers(inputbox);

            outputBox.Click += delegate
            {
                Clipboard.SetText(outputBox.Text);
                hintLabel.Text = "Скопировано в буфер обмена";
            };
        }

        private void mainButton_Click(object sender, EventArgs e)
        {
            string userName = inputbox.Text;
            if (string.IsNullOrEmpty(userName) || userName == (string)inputbox.Tag)
            {
                MessageBox.Show("Введите имя пользователя", "Внимание", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }

            //timestamp 10 numbers
            Random rand = new Random();
            KeyValuePair<string, string[]> workingEncodePair = timestampsEncodeArrays.ElementAt(rand.Next(0, timestampsEncodeArrays.Count));
            string encodeVariant = workingEncodePair.Key;
            string[] timestampEncodeArray = workingEncodePair.Value;

            string timestampStr = getCurrentTimestamp().ToString();
            StringBuilder timestampPart1 = new StringBuilder(5);
            StringBuilder timestampPart2 = new StringBuilder(5);
            for (int i = 0; i < timestampStr.Length; i++)
            {
                if (i % 2 == 0)
                {
                    timestampPart1.Append(timestampEncodeArray[Convert.ToInt16(timestampStr[i].ToString())]);
                }
                else
                {
                    timestampPart2.Append(timestampEncodeArray[Convert.ToInt16(timestampStr[i].ToString())]);
                }
            }

            string userNameMd5 = createMD5ForString(string.Format("{0}_{1}_{2}", userName, SALT, timestampStr));
            StringBuilder encodedNameBuilder = new StringBuilder(9);
            for (int i = 0; i < positions.Length; i++)
            {
                encodedNameBuilder.Append(userNameMd5[positions[i]]);
            }
            string encodedName = encodedNameBuilder.ToString();

            //md5 from encodedName 9 letters
            StringBuilder encodedNamePart1 = new StringBuilder(5);
            StringBuilder encodedNamePart2 = new StringBuilder(5);
            for (int i = 0; i < 9; i++)
            {
                if (i < 4)
                {
                    encodedNamePart1.Append(encodedName[i]);
                }
                else
                {
                    encodedNamePart2.Append(encodedName[i]);
                }
            }
            encodedNamePart1.Append(encodeVariant);

            string key = string.Format(KEY_TEMPLATE, encodedNamePart1.ToString(), timestampPart1.ToString(), encodedNamePart2.ToString(), timestampPart2.ToString());

            outputBox.Text = key;
            hintLabel.Text = "Кликни, чтобы скопировать";
            keyPanel.Visible = true;
        }

        private static string createMD5ForString(string input)
        {
            byte[] inputBytes = System.Text.Encoding.UTF8.GetBytes(input);
            using (System.Security.Cryptography.MD5 md5 = System.Security.Cryptography.MD5.Create())
            {
                byte[] hashBytes = md5.ComputeHash(inputBytes);

                // Convert the byte array to hexadecimal string
                System.Text.StringBuilder sb = new System.Text.StringBuilder();
                for (int i = 0; i < hashBytes.Length; i++)
                {
                    sb.Append(hashBytes[i].ToString("X2"));
                }
                return sb.ToString();
            }
        }

        private static long getCurrentTimestamp()
        {
            return (long)(DateTime.Now.Subtract(new DateTime(1970, 1, 1))).TotalSeconds;
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

        private void MainWindow_Load(object sender, EventArgs e)
        {
            this.ActiveControl = outputBox;
        }
    }
}
