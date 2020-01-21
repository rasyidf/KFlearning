using System;
using System.Windows.Forms;
using Castle.Windsor;
using KFlearning.Core.IO;
using KFlearning.Core.Services;
using KFlearning.Properties;
using KFlearning.Views;

namespace KFlearning
{
    static class Program
    {
        public static WindsorContainer Container = new WindsorContainer();

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main(string[] args)
        {
            Container.Install(new AppModulesInstaller());

            // find vscode
            var path = Container.Resolve<IPathManager>();
            if (path.GetPath(PathKind.VisualStudioCodeExecutable) == null)
            {
                MessageBox.Show(Resources.VscodeNotInstalled, Resources.AppName, MessageBoxButtons.OK,
                    MessageBoxIcon.Exclamation);
                return;
            }

            // history
            Container.Resolve<IHistoryService>().RecordHistory = !Settings.Default.Raf;

            // app exit handler
            Application.ApplicationExit += Application_ApplicationExit;

            // bootstrapper
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(Container.Resolve<StartupForm>());
        }

        private static void Application_ApplicationExit(object sender, EventArgs e)
        {
            Container.Resolve<IHistoryService>().Save();
            Container.Dispose();
        }
    }
}
