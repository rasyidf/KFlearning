using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;
using KFlearning.IDE.ApplicationServices;
using KFlearning.IDE.ViewModels;
using KFlearning.IDE.Views;

namespace KFlearning.IDE
{
    public class AppContainerInstaller : IWindsorInstaller
    {
        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            // Register Views and ViewModels
            container.Register(
                Component.For<IApplicationHelpers>().ImplementedBy<ApplicationHelpers>().LifestyleSingleton(),

                Component.For<NotifyPropertChangedInterceptor>(),
                Component.For<IAutoNotifyPropertyChanged>().ImplementedBy<PropertyChangedBase>(),

                Classes.FromThisAssembly().InSameNamespaceAs<ShellView>(),
                Classes.FromThisAssembly().InSameNamespaceAs<ShellViewModel>()
                    .Configure(x => x.Interceptors<NotifyPropertChangedInterceptor>())
            );
        }
    }
}
