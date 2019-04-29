using System.Windows.Forms;

namespace RussianTasks.src.ui
{
    public partial class PreloaderScreen : Form
    {
        public PreloaderScreen()
        {
            InitializeComponent();
            createView();
        }

        private void createView()
        {
            titlelabel.Text = System.Text.RegularExpressions.Regex.Unescape(Properties.Strings.preloader_title);
            versionLabel.Text = string.Format("v{0}", utils.Utils.getApplicationVersion());
            copyrightLabel.Text = Properties.Strings.preloader_copyright_text;
        }
    }
}
