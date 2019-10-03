using System;
using System.Collections.Generic;
using System.Threading;
using System.Windows.Forms;
using KFlearning.Core.Services;
using KFlearning.Properties;

namespace KFlearning.Views
{
    public partial class InstallerForm : Form
    {
        private Thread _thread;
        private CancellationTokenSource _cancellation = new CancellationTokenSource();

        public InstallerForm()
        {
            InitializeComponent();
        }

        private void cmdCancel_Click(object sender, EventArgs e)
        {
            _cancellation.Cancel();
            cmdCancel.Enabled = false;
        }

        private void InstallerForm_Load(object sender, EventArgs e)
        {
            _thread = new Thread(InstallThread) {IsBackground = true};
            _thread.Start();
        }

        private void InstallThread()
        {
            try
            {
                var sequence = new List<IInstallable>
                {
                    Program.Container.Resolve<MinGW>(),
                    Program.Container.Resolve<Vscode>(),
                    Program.Container.Resolve<KflearningShortcut>()
                };

                var callback = new Action<int>(ProgressCallback);
                foreach (IInstallable installable in sequence)
                {
                    if (Program.InstallMode) installable.Install(callback, _cancellation.Token);
                    else installable.Uninstall(callback, _cancellation.Token);
                }

                MessageBox.Show(Resources.InstallSuccessMessage, Resources.AppName, MessageBoxButtons.OK,
                    MessageBoxIcon.Information);
        }
            catch (OperationCanceledException)
            {
                MessageBox.Show(Resources.InstallCanceledMessage, Resources.AppName, MessageBoxButtons.OK,
                    MessageBoxIcon.Exclamation);
            }
            catch (Exception e)
            {
                MessageBox.Show(string.Format(Resources.InstallFailedMessage, e), Resources.AppName,
                    MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            finally
            {
                Application.Exit();
            }
        }

        private void ProgressCallback(int progress)
        {
            if (InvokeRequired)
            {
                Invoke(new Action<int>(ProgressCallback), progress);
            }
            else
            {
                if (progress < 0 || progress > 100)
                {
                    prgProgress.Style = ProgressBarStyle.Marquee;
                }
                else
                {
                    prgProgress.Style = ProgressBarStyle.Blocks;
                    prgProgress.Value = progress;
                }
            }
        }
    }
}
