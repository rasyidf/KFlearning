using System;
using System.Windows.Forms;
using Castle.Windsor;
using KFlearning.Installer.Views;

namespace KFlearning.Installer
{
    static class Program
    {
        public static IWindsorContainer Container = new WindsorContainer();

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Container.Install(new AppContainerInstaller());

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(Container.Resolve<MainForm>());
        }
    }
}
