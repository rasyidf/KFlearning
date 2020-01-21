// SOLUTION : KFlearning
// PROJECT  : KFlearning
// FILENAME : StartupForm.cs
// AUTHOR   : Fahmi Noor Fiqri, Kodesiana.com
// WEBSITE  : https://kodesiana.com
// REPO     : https://github.com/Kodesiana or https://github.com/fahminlb33
// 
// This file is part of KFlearning, see LICENSE.
// See this code in repository URL above!

using System.Linq;
using System.Windows.Forms;
using KFlearning.Core.Diagnostics;
using KFlearning.Core.Services;
using KFlearning.Properties;
using KFlearning.Views.Controls;

namespace KFlearning.Views
{
    public partial class StartupForm : Form
    {
        private readonly IProjectService _project;
        private readonly IHistoryService _history;
        private readonly ICredentialService _credential;
        private readonly IProcessManager _process;

        public StartupForm()
        {
            _project = Program.Container.Resolve<IProjectService>();
            _history = Program.Container.Resolve<IHistoryService>();
            _credential = Program.Container.Resolve<ICredentialService>();
            _process = Program.Container.Resolve<IProcessManager>();

            InitializeComponent();

            pnRaf.Visible = Settings.Default.Raf;
            ReloadHistory();
        }

        private void ReloadHistory()
        {
            lstHistory.Items.Clear();
            lstHistory.Items.AddRange(_history.GetAll().Cast<object>().ToArray());
        }

        private void OpenProject(string path)
        {
            if (!_project.IsExists(path))
            {
                MessageBox.Show(Resources.InvalidProjectMessage, Resources.AppName, MessageBoxButtons.OK,
                    MessageBoxIcon.Exclamation);
                return;
            }

            var project = _project.Load(path);
            _history.Add(project);
            _project.Launch(project);
            ReloadHistory();
        }

        private void cmdNewProject_Click(object sender, System.EventArgs e)
        {
            using (var frm = Program.Container.Resolve<CreateProjectForm>())
            {
                if (frm.ShowDialog(this) != DialogResult.OK) return;
                _project.Create(frm.Project);
                _history.Add(frm.Project);
                _project.Launch(frm.Project);
                ReloadHistory();
            }
        }

        private void cmdOpenProject_Click(object sender, System.EventArgs e)
        {
            if (fbd.ShowDialog() != DialogResult.OK) return;
            OpenProject(fbd.SelectedPath);
        }

        private void cmdAdmin_Click(object sender, System.EventArgs e)
        {
            if (!_process.IsProcessElevated())
            {
                MessageBox.Show(Resources.NotElevatedMessage, Resources.AppName, MessageBoxButtons.OK,
                    MessageBoxIcon.Exclamation);
                return;
            }

            using (var frm = Program.Container.Resolve<AuthenticationForm>())
            {
                if (frm.ShowDialog(this) != DialogResult.OK) return;
                if (!_credential.Verify(frm.AccessCode))
                {
                    MessageBox.Show(Resources.InvalidAccessCode, Resources.AppName, MessageBoxButtons.OK,
                        MessageBoxIcon.Stop);
                    return;
                }

                using (var frm2 = Program.Container.Resolve<AdminForm>())
                    frm2.ShowDialog();
            }
        }

        private void cmdAbout_Click(object sender, System.EventArgs e)
        {
            using (var frm = Program.Container.Resolve<AboutForm>())
                frm.ShowDialog(this);
        }

        private void cmdClear_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            lstHistory.Items.Clear();
            _history.Clear();
        }

        private void lstHistory_DrawItem(object sender, DrawItemEventArgs e)
        {
            FlatListBox.DrawItemHandler(sender, e);
        }

        private void lstHistory_DoubleClick(object sender, System.EventArgs e)
        {
            if (lstHistory.SelectedItem == null) return;
            var item = (Project) lstHistory.SelectedItem;
            OpenProject(item.Path);
        }
    }
}