using System;
using System.ComponentModel;
using System.Windows.Forms;

namespace KFlearning.Views
{
    public partial class AuthenticationForm : Form
    {
        public AuthenticationForm()
        {
            InitializeComponent();
        }

        public string AccessCode { get;set; }

        protected override void OnClosing(CancelEventArgs e)
        {
            AccessCode = txtAccessCode.Text;
            base.OnClosing(e);
        }

        private void cmdLogin_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.OK;
            Close();
        }

        private void TextBox_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode != Keys.Enter) return;
            cmdLogin_Click(null, EventArgs.Empty);
        }
    }
}
