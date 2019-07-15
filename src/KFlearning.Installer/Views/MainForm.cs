using System;
using System.IO;
using System.Windows.Forms;
using KFlearning.Core.IO;
using KFlearning.Core.Services;
using KFlearning.Installer.ApplicationServices;

namespace KFlearning.Installer.Views
{
    public partial class MainForm : Form
    {
        private ViewState _viewState;

        private enum ViewState
        {
            Install,
            Uninstall,
            Cancel
        }

        #region Properties
        
        public IProgressBroker ProgressBroker { get; set; }
        public IPathManager PathManager { get; set; }
        public ITaskGraph TaskGraph { get; set; }
        public ISequenceFactory SequenceFactory { get; set; }
        public LogForm Log { get; set; } 

        #endregion

        #region Constructor

        public MainForm()
        {
            InitializeComponent();
        } 

        #endregion

        #region Overrides

        protected override void OnLoad(EventArgs e)
        {
            var broker = (ProgressBroker)ProgressBroker;
            broker.MessageAction = MessageAction;
            broker.ProgressOverallAction = x => ProgressAction(x, true);
            broker.ProgressCurrentAction = x => ProgressAction(x, false);

            _viewState = TaskGraph.IsInstalled() ? ViewState.Uninstall : ViewState.Install;
            UpdateViewState();

            base.OnLoad(e);
        } 

        #endregion

        #region Event Handlers

        private void CmdInstall_Click(object sender, EventArgs e)
        {
            if (_viewState == ViewState.Cancel)
            {
                cmdInstall.Enabled = false;
                TaskGraph.Cancel();
            }
            else
            {
                var path = PathManager.Combine(PathKind.PathBase, @"installer\data");
                var extensions = File.ReadAllLines(PathManager.Combine(path, "vscode-extensions.txt"));
                var definition = new InstallDefinition(path, extensions, x => Program.Container.Resolve(x));
                var sequence = _viewState == ViewState.Install
                    ? SequenceFactory.GetInstallSequence()
                    : SequenceFactory.GetUninstallSequence();
                TaskGraph.RunSequence(definition, sequence);
            }
        }

        private void CmdLog_Click(object sender, EventArgs e)
        {
            Log.Show();
        }

        #endregion

        #region Private Methods

        private void ProgressAction(int obj, bool overall)
        {
            if (InvokeRequired)
            {
                Invoke(new Action<int, bool>(ProgressAction), obj, overall);
            }
            else
            {
                var progressBar = overall ? prgOverall : prgCurrent;
                if (obj > 100 || obj < 0)
                {
                    progressBar.Style = ProgressBarStyle.Marquee;
                }
                else
                {
                    progressBar.Style = ProgressBarStyle.Blocks;
                    progressBar.Value = obj;
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
                Log.AppendLog($"[{DateTime.Now}] {Environment.NewLine}");
                Log.AppendLog(obj);
                Log.AppendLog(Environment.NewLine + Environment.NewLine);
            }
        } 

        private void UpdateViewState()
        {
            if (InvokeRequired)
            {
                Invoke(new Action(UpdateViewState));
            }
            else
            {
                cmdInstall.Enabled = true;
                switch (_viewState)
                {
                    case ViewState.Install:
                        cmdInstall.Text = "Install";
                        break;
                    case ViewState.Uninstall:
                        cmdInstall.Text = "Uninstall";
                        break;
                    case ViewState.Cancel:
                        cmdInstall.Text = "Batal";
                        break;
                    default:
                        throw new ArgumentOutOfRangeException(nameof(_viewState), _viewState, null);
                }
            }
        }

        #endregion
    }
}
