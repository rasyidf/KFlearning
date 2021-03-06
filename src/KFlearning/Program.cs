﻿// SOLUTION : KFlearning
// PROJECT  : KFlearning
// FILENAME : Program.cs
// AUTHOR   : Fahmi Noor Fiqri, Kodesiana.com
// WEBSITE  : https://kodesiana.com
// REPO     : https://github.com/Kodesiana or https://github.com/fahminlb33
// 
// This file is part of KFlearning, see LICENSE.
// See this code in repository URL above!

using System;
using System.Threading.Tasks;
using System.Windows.Forms;
using Castle.Windsor;
using KFlearning.Core.IO;
using KFlearning.Core.Services;
using KFlearning.Properties;
using KFlearning.Services;
using KFlearning.Views;

namespace KFlearning
{
    static class Program
    {
        public static WindsorContainer Container = new WindsorContainer();

        /// <summary>
        ///     The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main(string[] args)
        {
            Container.Install(new AppModulesInstaller());

            // check RAF mode
            Container.Resolve<IHistoryService>().RecordHistory = !Settings.Default.Raf;

            // find vscode
            var path = Container.Resolve<IPathManager>();
            if (path.GetPath(PathKind.VisualStudioCodeExecutable) == null)
            {
                MessageBox.Show(Resources.VscodeNotInstalled, Resources.AppName, MessageBoxButtons.OK,
                    MessageBoxIcon.Exclamation);
                return;
            }

            // app exit handler
            Application.ApplicationExit += Application_ApplicationExit;

            // bootstrapper
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(Container.Resolve<StartupForm>());
        }

        private static void Application_ApplicationExit(object sender, EventArgs e)
        {
            try
            {
                foreach (var usesPersistance in Container.ResolveAll<IUsesPersistance>())
                {
                    usesPersistance.Save();
                }

                Task.WaitAll(Container.Resolve<IUserService>().Sync());
            }
            catch
            {
                // ignore
            }

            Container.Dispose();
        }
    }
}