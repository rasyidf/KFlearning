using System;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using KFlearning.Core.API;
using KFlearning.Core.IO;
using KFlearning.Core.Services.Graph;
using KFlearning.Installer.ApplicationServices;

namespace KFlearning.Installer.Views
{
    public partial class MainForm : Form
    {
        public IProgressBroker ProgressBroker { get; set; }
        public ITaskGraph TaskGraph { get; set; }

        public MainForm()
        {
            InitializeComponent();
        }

        protected override void OnLoad(EventArgs e)
        {
            var broker = (ProgressBroker) ProgressBroker;
            broker.MessageAction = MessageAction;
            broker.ProgressAction= ProgressAction;

            base.OnLoad(e);
        }

        private void CmdInstall_Click(object sender, EventArgs e)
        {
            var def = new InstallerDefinition(t=>Program.Container.Resolve(t))
            {
                Mode = InstallMode.Install,
                Packages = new PackageCatalog
                {
                    MingwUri = new Uri("https://osdn.net/dl/mingw/mingw-get-0.6.3-mingw32-pre-20170905-1-bin.zip")
                }
            };

            TaskGraph.RunGraph(def, Program.Container.Resolve<InstallGraph>());
        }

        private void ProgressAction(int obj)
        {
            if (InvokeRequired)
            {
                Invoke(new Action<int>(ProgressAction), obj);
            }
            else
            {
                if (obj > 100 || obj < 0)
                {
                    prgCurrent.Style = ProgressBarStyle.Marquee;
                }
                else
                {
                    prgCurrent.Style = ProgressBarStyle.Blocks;
                    prgCurrent.Value = obj;
                }
            }
        }

        private void MessageAction(string obj)
        {
            if (InvokeRequired)
            {
                Invoke(new Action<string>(MessageAction), obj);
            }
            else
            {
                Debug.Print(obj);
                txtLog.AppendText(obj);
                txtLog.AppendText(Environment.NewLine);
            }
        }

        private void CmdOpen_Click(object sender, System.EventArgs e)
        {
            TaskGraph.Cancel();
        }
    }
}
