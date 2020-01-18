using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;
using KFlearning.Core.Services;
using KFlearning.Services;
using KFlearning.Views;

namespace KFlearning
{
    public class AppModulesInstaller : IWindsorInstaller
    {
        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            container.Register(
                Component.For<ICredentialStorage>().ImplementedBy<AppCredentialStore>().LifestyleSingleton(),
                Classes.FromThisAssembly()
                    .InSameNamespaceAs<StartupForm>()
                    .LifestyleTransient(),
                Classes.FromAssemblyNamed("KFlearning.Core")
                    .Pick()
                    .WithServiceDefaultInterfaces()
                    .LifestyleSingleton()
            );
        }
    }
}
