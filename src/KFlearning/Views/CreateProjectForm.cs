// SOLUTION : KFlearning
// PROJECT  : KFlearning
// FILENAME : CreateProjectForm.cs
// AUTHOR   : Fahmi Noor Fiqri, Kodesiana.com
// WEBSITE  : https://kodesiana.com
// REPO     : https://github.com/Kodesiana or https://github.com/fahminlb33
// 
// This file is part of KFlearning, see LICENSE.
// See this code in repository URL above!

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

        private readonly bool _isRaf = Settings.Default.Raf;
        private readonly IProjectService _project;

        public Project Project { get; set; }

        public CreateProjectForm()
        {
            var template = Program.Container.Resolve<ITemplateService>();
            _project = Program.Container.Resolve<IProjectService>();

            InitializeComponent();

            cboTemplate.DataSource = template.GetTemplates();
            txtProjectName.Text = _isRaf ? "Mode Run-and-Forget" : "";
            txtProjectName.ReadOnly = _isRaf;
        }

        private void cmdCreate_Click(object sender, EventArgs e)
        {
            if (_isRaf)
            {
                _basePath = Path.GetTempPath();
                txtProjectName.Text = Path.GetFileNameWithoutExtension(Path.GetTempFileName());
            }

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
                Path = _project.GetPathForProject(txtProjectName.Text, _basePath),
                Template = (Template) cboTemplate.SelectedItem
            };

            DialogResult = DialogResult.OK;
            Close();
        }

        private void cmdBrowse_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            if (_isRaf)
            {
                MessageBox.Show(Resources.RafModeMessage, Resources.AppName, MessageBoxButtons.OK,
                    MessageBoxIcon.Exclamation);
                return;
            }

            if (fbd.ShowDialog() != DialogResult.OK) return;
            _basePath = fbd.SelectedPath;
            txtProjectName_TextChanged(null, null);
        }

        private void txtProjectName_TextChanged(object sender, EventArgs e)
        {
            if (_isRaf) return;
            txtLocation.Text = _project.GetPathForProject(txtProjectName.Text, _basePath);
        }
    }
}