using System.Windows;
using Castle.Windsor;
using KFlearning.ApplicationServices;
using KFlearning.ApplicationServices.Models;
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

        protected override void OnStartup(StartupEventArgs e)
        {
            InitializeViews();

            MainWindow = ViewLocator.GetShell();
            MainWindow?.Show();
            Container.Resolve<IEventAggregator>().Publish(new AppEventArgs(AppAction.Bootstrap));
        }

        private void InitializeViews()
        {
            ViewModelBase.AggregatorFunc = () => Container.Resolve<IEventAggregator>();
            ViewModelLocator.ResolverFunc = t => Container.Resolve(t);

            ViewLocator.RegisterShell(Container.Resolve<ShellView>());
            ViewLocator.Register(Container.Resolve<ProjectView>());
            ViewLocator.Register(Container.Resolve<ArticleView>());
            ViewLocator.Register(Container.Resolve<WebServerView>());
            ViewLocator.Register(Container.Resolve<AboutView>());
            ViewLocator.Register(Container.Resolve<CreateProjectView>());
            ViewLocator.Register(Container.Resolve<ReaderView>());
        }
    }
}
