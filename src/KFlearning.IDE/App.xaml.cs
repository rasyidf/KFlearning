using System;
using System.Windows;
using Castle.Windsor;
using KFlearning.IDE.ApplicationServices;
using KFlearning.IDE.Models;
using KFlearning.IDE.Views;

namespace KFlearning.IDE
{
    /// <summary>
    /// Interaction logic for App.xaml
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
