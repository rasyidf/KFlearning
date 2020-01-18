using System;
using System.IO;
using System.Windows.Forms;
using KFlearning.Core.Services;
using KFlearning.Properties;

namespace KFlearning.Views
{
    public partial class CreateProjectForm : Form
    {
        private string _basePath;

        private readonly ITemplateService _template;
        private readonly IProjectService _project;

        public Project Project { get; set; }

        public CreateProjectForm()
        {
            _template = Program.Container.Resolve<ITemplateService>();
            _project = Program.Container.Resolve<IProjectService>();

            InitializeComponent();
            cboTemplate.DataSource = _template.GetTemplates();
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

            Project = new Project
            {
                Name = txtProjectName.Text,
                Path = txtLocation.Text,
                Template = (Template) cboTemplate.SelectedItem
            };
            DialogResult = DialogResult.OK;
            Close();
        }

        private void cmdBrowse_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            if (fbd.ShowDialog() != DialogResult.OK) return;
            _basePath = fbd.SelectedPath;
            txtProjectName_TextChanged(null, null);
        }

        private void txtProjectName_TextChanged(object sender, EventArgs e)
        {
            txtLocation.Text = _project.GetPathForProject(txtProjectName.Text, _basePath);
        }
    }
}
