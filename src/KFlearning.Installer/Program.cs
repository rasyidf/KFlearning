// 
//  PROJECT  :   KFlearning
//  FILENAME :   Program.cs
//  AUTHOR   :   Fahmi Noor Fiqri
//  WEBSITE  : https://kodesiana.com
//  REPO     : https://github.com/Kodesiana or https://github.com/fahminlb33
// 
//  This file is part of KFlearning, licensed under MIT license.
//  See this code in repository URL above!

#region

using System;
using System.Windows.Forms;
using Castle.Windsor;
using KFlearning.Installer.Views;

#endregion

namespace KFlearning.Installer
{
    internal static class Program
    {
        public static IWindsorContainer Container = new WindsorContainer();

        /// <summary>
        ///     The main entry point for the application.
        /// </summary>
        [STAThread]
        private static void Main()
        {
            Container.Install(new AppContainerInstaller());

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(Container.Resolve<MainForm>());
        }
    }
}