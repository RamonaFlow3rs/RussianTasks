namespace RussianTasks.src.ui
{
    class UIHelpers
    {
        public static void setupWindowIcon(ref System.Windows.Forms.Form form)
        {
            form.Icon = new System.Drawing.Icon(Properties.Resources.book_icon, 64, 64);
        }
    }
}
