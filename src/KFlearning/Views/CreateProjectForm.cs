using System;
using System.IO;
using System.Threading.Tasks;
using System.Windows.Forms;
using KFlearning.Core.IO;
using KFlearning.Core.Services;
using KFlearning.Properties;

namespace KFlearning.Views
{
    public partial class CreateProjectForm : Form
    {
        private string _basePath;

        public IProjectManager ProjectManager { get; set; }
        public IPathManager PathManager { get; set; }

        public CreateProjectForm()
        {
            InitializeComponent();
        }

        private void CreateProjectForm_Load(object sender, EventArgs e)
        {
            cboTemplate.DataSource = ProjectManager.Templates;
        }
        
        private void cmdCreate_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtProjectName.Text))
            {
                MessageBox.Show(Resources.ProjectNameEmptyMessage, Resources.AppName, MessageBoxButtons.OK,
                    MessageBoxIcon.Exclamation);
                return;
            }
            if (Directory.Exists(txtLocation.Text))
            {
                MessageBox.Show(Resources.ProjectExistsMessage, Resources.AppName, MessageBoxButtons.OK,
                    MessageBoxIcon.Exclamation);
                return;
            }

            var scheduler = TaskScheduler.FromCurrentSynchronizationContext();
            Cursor = Cursors.WaitCursor;
            Enabled = false;
            var templateName = cboTemplate.Text;
            Task.Run(() =>
            {
                //var project = ProjectManager.Create(txtProjectName.Text, templateName, txtLocation.Text);
                //ProjectManager.Launch(project);
            }).ContinueWith(x =>
            {
                Cursor = Cursors.Default;
                Close();
            }, scheduler);
        }

        private void cmdBrowse_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            if (fbd.ShowDialog() != DialogResult.OK) return;
            _basePath = fbd.SelectedPath;
            txtProjectName_TextChanged(null, null);
        }

        private void txtProjectName_TextChanged(object sender, EventArgs e)
        {
            txtLocation.Text = ProjectManager.GetPathForProject(_basePath, txtProjectName.Text);
        }
    }
}
