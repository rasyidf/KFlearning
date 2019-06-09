using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;
using KFlearning.ApplicationServices;
using KFlearning.IDE.ViewModels;
using KFlearning.IDE.Views;

namespace KFlearning.IDE
{
    public class AppContainerInstaller : IWindsorInstaller
    {
        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            // Install application services
            container.Install(new ApplicationServicesInstaller());

            

            // Register Views and ViewModels
            container.Register(
                Classes.FromThisAssembly().InSameNamespaceAs<ShellView>().Configure(x => x.LifestyleSingleton()),
                Classes.FromThisAssembly().InSameNamespaceAs<ShellViewModel>()
                    .Configure(x => x.Interceptors<NotifyPropertChangedInterceptor>().LifestyleSingleton())
            );
        }
    }
}
