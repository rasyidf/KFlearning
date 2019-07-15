using System.Windows.Forms;

namespace KFlearning.Installer.Views
{
    public partial class LogForm : Form
    {
        public LogForm()
        {
            InitializeComponent();
        }

        public void AppendLog(string text)
        {
            txtLog.AppendText(text);
        }

        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            if (e.CloseReason == CloseReason.UserClosing)
            {
                e.Cancel = true;
                Hide();
            }
            
            base.OnFormClosing(e);
        }
    }
}
