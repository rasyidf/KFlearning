using System.Windows.Forms;
using KFlearning.Core.Services;
using KFlearning.Properties;

namespace KFlearning.Views
{
    public partial class StartupForm : Form
    {
        public IProjectManager ProjectManager { get; set; }

        public StartupForm()
        {
            InitializeComponent();
        }
        
        private void StartupForm_Load(object sender, System.EventArgs e)
        {

        }

        private void cmdAbout_Click(object sender, System.EventArgs e)
        {
            using (var frm = Program.Container.Resolve<AboutForm>()) frm.ShowDialog(this);
        }

        private void cmdNewProject_Click(object sender, System.EventArgs e)
        {
            using (var frm = Program.Container.Resolve<CreateProjectForm>()) frm.ShowDialog(this);
        }

        private void cmdOpenProject_Click(object sender, System.EventArgs e)
        {
            if (fbd.ShowDialog() != DialogResult.OK) return;
            if (!ProjectManager.IsValidProject(fbd.SelectedPath))
            {
                MessageBox.Show(Resources.InvalidProjectMessage, Resources.AppName, MessageBoxButtons.OK,
                    MessageBoxIcon.Exclamation);
                return;
            }

            var project = ProjectManager.Load(fbd.SelectedPath);
            ProjectManager.Launch(project);
        }

        private void cmdArticles_Click(object sender, System.EventArgs e)
        {
            MessageBox.Show(Resources.UnderMaintenanceMessage, Resources.AppName, MessageBoxButtons.OK,
                MessageBoxIcon.Exclamation);
        }

        private void cmdLaragonLink_Click(object sender, System.EventArgs e)
        {
            MessageBox.Show(Resources.UnderMaintenanceMessage, Resources.AppName, MessageBoxButtons.OK,
                MessageBoxIcon.Exclamation);
        }
    }
}
