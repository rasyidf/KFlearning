using System.Windows.Forms;
using KFlearning.Core.Services;
using KFlearning.Properties;
using KFlearning.Views.Controls;

namespace KFlearning.Views
{
    public partial class StartupForm : Form
    {
        public IProjectManager ProjectManager { get; set; }

        public StartupForm()
        {
            InitializeComponent();
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

            //var project = ProjectManager.Load(fbd.SelectedPath);
            //ProjectManager.Launch(project);
        }

        private void cmdAdmin_Click(object sender, System.EventArgs e)
        {
            using (var frm = Program.Container.Resolve<AuthenticationForm>())
            {
                if (frm.ShowDialog(this) != DialogResult.OK) return;

            }
        }

        private void cmdAbout_Click(object sender, System.EventArgs e)
        {
            using (var frm = Program.Container.Resolve<AboutForm>()) frm.ShowDialog(this);
        }

        private void listBox1_DrawItem(object sender, DrawItemEventArgs e)
        {
            FlatListBox.DrawItemHandler(sender, e);
        }
    }
}
