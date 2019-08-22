// 
//  PROJECT  :   KFlearning
//  FILENAME :   MainForm.cs
//  AUTHOR   :   Fahmi Noor Fiqri
//  WEBSITE  : https://kodesiana.com
//  REPO     : https://github.com/Kodesiana or https://github.com/fahminlb33
// 
//  This file is part of KFlearning, licensed under MIT license.
//  See this code in repository URL above!

#region

using System;
using System.IO;
using System.Windows.Forms;
using KFlearning.Core.IO;
using KFlearning.Core.Services.Installer;
using KFlearning.Installer.ApplicationServices;

#endregion

namespace KFlearning.Installer.Views
{
    public partial class MainForm : Form
    {
        #region Fields

        private enum ViewState
        {
            Install,
            Uninstall,
            Cancel,
            WaitOpen,
            WaitExit
        }

        private ViewState _viewState;
        private bool _isInstall;

        #endregion

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
            var broker = (ProgressBroker) ProgressBroker;
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
            switch (_viewState)
            {
                case ViewState.Cancel:
                    cmdInstall.Enabled = false;
                    TaskGraph.Cancel();
                    break;

                case ViewState.WaitExit:
                    Application.Exit();
                    break;

                case ViewState.WaitOpen:
                {
                    var path = PathManager.Combine(PathKind.PathKflearningRoot, "kflearning.ide.exe");
                    PathManager.LaunchUri(path);
                    break;
                }

                default:
                {
                    var path = PathManager.Combine(PathKind.PathBase, @"installer\data");
                    var extensions = File.ReadAllLines(PathManager.Combine(path, "vscode-extensions.txt"));
                    var definition = new InstallDefinition(path, extensions, x => Program.Container.Resolve(x));
                    var sequence = _viewState == ViewState.Install
                        ? SequenceFactory.GetInstallSequence()
                        : SequenceFactory.GetUninstallSequence();

                    _isInstall = _viewState == ViewState.Install;
                    TaskGraph.RunSequence(definition, sequence);
                    break;
                }
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

                if (!overall || obj != 100) return;
                _viewState = _isInstall ? ViewState.WaitOpen : ViewState.WaitExit;
                UpdateViewState();
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
                    case ViewState.WaitExit:
                        cmdInstall.Text = "Keluar";
                        break;
                    default:
                        throw new ArgumentOutOfRangeException(nameof(_viewState), _viewState, null);
                }
            }
        }

        #endregion
    }
}