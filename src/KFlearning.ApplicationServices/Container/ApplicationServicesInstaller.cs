using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;

namespace KFlearning.ApplicationServices
{
    public class ApplicationServicesInstaller : IWindsorInstaller
    {
        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            container.Register(
                Component.For<IApplicationHelpers>().ImplementedBy<ApplicationHelpers>().LifestyleSingleton(),
                Component.For<IEventAggregator>().ImplementedBy<EventAggregator>().LifestyleSingleton(),

                Component.For<NotifyPropertChangedInterceptor>().LifestyleTransient(),
                Component.For<IAutoNotifyPropertyChanged>().ImplementedBy<ViewModelBase>()
                    .LifestyleTransient()
            );
        }
    }
}
