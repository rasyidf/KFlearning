// 
//  PROJECT  :   KFlearning
//  FILENAME :   App.xaml.cs
//  AUTHOR   :   Fahmi Noor Fiqri
//  WEBSITE  : https://kodesiana.com
//  REPO     : https://github.com/Kodesiana or https://github.com/fahminlb33
// 
//  This file is part of KFlearning, licensed under MIT license.
//  See this code in repository URL above!

#region

using System.Windows;
using Castle.Windsor;
using KFlearning.IDE.Views;

#endregion

namespace KFlearning.IDE
{
    /// <summary>
    ///     Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public static readonly WindsorContainer Container = new WindsorContainer();

        public App()
        {
            Container.Install(new AppContainerInstaller());
            InitializeComponent();
        }

        protected override void OnExit(ExitEventArgs e)
        {
            base.OnExit(e);
        }

        protected override void OnStartup(StartupEventArgs e)
        {
            var window = Container.Resolve<ShellView>();
            MainWindow = window;
            MainWindow?.Show();
        }
    }
}