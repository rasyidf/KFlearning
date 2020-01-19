using System;
using System.Windows.Forms;
using Castle.Windsor;
using KFlearning.Core.IO;
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

            var vscode = Container.Resolve<IPathManager>();
            if (!vscode.DiscoverVisualStudioCode(out _))
            {
                MessageBox.Show(Resources.VscodeNotInstalled, Resources.AppName, MessageBoxButtons.OK,
                    MessageBoxIcon.Exclamation);
                return;
            }

            Application.ApplicationExit += Application_ApplicationExit;
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(Container.Resolve<StartupForm>());
        }

        private static void Application_ApplicationExit(object sender, EventArgs e)
        {
            Container.Dispose();
        }
    }
}
