using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
        public string NetworkCode { get; set; }

        private void cmdLogin_Click(object sender, EventArgs e)
        {
            using (var frm = Program.Container.Resolve<AdminForm>()) frm.ShowDialog(this);
        }
    }
}
