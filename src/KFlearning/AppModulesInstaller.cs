using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;
using KFlearning.Views;

namespace KFlearning
{
    public class AppModulesInstaller : IWindsorInstaller
    {
        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            container.Register(
                Classes.FromAssemblyNamed("KFlearning.Core").Pick().WithServiceDefaultInterfaces().WithServiceSelf()
                    .LifestyleTransient(),
                Classes.FromThisAssembly().InSameNamespaceAs<StartupForm>().LifestyleTransient()
            );
        }
    }
}
