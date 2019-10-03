using System;
using System.Diagnostics;
using System.Windows.Forms;

namespace KFlearning.Views
{
    public partial class AboutForm : Form
    {
        public AboutForm()
        {
            InitializeComponent();
        }

        private void AboutForm_Load(object sender, EventArgs e)
        {

        }

        private void cmdInstagram_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Process.Start("https://www.instagram.com/fahminoorfiqri");
        }

        private void cmdKodesiana_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Process.Start("https://kodesiana.com");
        }

        private void cmdGitHub_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Process.Start("https://github.com/fahminlb33");
        }
    }
}
