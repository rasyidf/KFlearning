using System;
using System.Windows.Forms;
using Castle.Windsor;
using KFlearning.Views;

namespace KFlearning
{
    static class Program
    {
        public static WindsorContainer Container = new WindsorContainer();
        public static bool InstallMode;

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main(string[] args)
        {
            Container.Install(new AppModulesInstaller());

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            if (args.Length > 0 && args[0] == "--install")
            {
                InstallMode = true;
                Application.Run(Container.Resolve<InstallerForm>());
            }
            else if (args.Length > 0 && args[0] == "--uninstall")
            {
                InstallMode = false;
                Application.Run(Container.Resolve<InstallerForm>());
            }
            else
            {
                Application.Run(Container.Resolve<StartupForm>());
            }
        }
    }
}
