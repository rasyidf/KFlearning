// 
//  PROJECT  :   KFlearning
//  FILENAME :   AppContainerInstaller.cs
//  AUTHOR   :   Fahmi Noor Fiqri
//  NPM      :   065118116
// 
//  This file is part of KFlearning, licensed under MIT license.

using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;
using KFlearning.Core.API;
using KFlearning.Core.DAL;
using KFlearning.Core.IO;
using KFlearning.Core.Services;
using KFlearning.Core.Services.Graph;
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
                // common core
                Classes.FromAssemblyNamed("KFlearning.Core").InSameNamespaceAs<IKodesianaService>()
                    .WithServiceDefaultInterfaces().LifestyleSingleton(),
                Classes.FromAssemblyNamed("KFlearning.Core").InSameNamespaceAs<IDatabaseContext>()
                    .WithServiceDefaultInterfaces().LifestyleSingleton(),
                Classes.FromAssemblyNamed("KFlearning.Core").InSameNamespaceAs<ITaskGraph>()
                    .WithServiceDefaultInterfaces().LifestyleSingleton(),
                Classes.FromAssemblyNamed("KFlearning.Core").InSameNamespaceAs<IApacheHttpd>()
                    .WithServiceDefaultInterfaces().LifestyleSingleton(),
                Classes.FromAssemblyNamed("KFlearning.Core").InSameNamespaceAs<IPathManager>()
                    .WithServiceDefaultInterfaces().LifestyleSingleton(),

                // application specific
                Classes.FromThisAssembly().InSameNamespaceAs<IApplicationHelpers>().WithServiceDefaultInterfaces()
                    .ConfigureFor<NotifyPropertChangedInterceptor>(x => x.LifestyleTransient())
                    .ConfigureFor<PropertyChangedBase>(x => x.LifestyleTransient())
                    .Configure(x => x.LifestyleSingleton()),
                Classes.FromThisAssembly().InSameNamespaceAs<ShellView>().LifestyleTransient(),
                Classes.FromThisAssembly().InSameNamespaceAs<ShellViewModel>()
                    .Configure(x => x.Interceptors<NotifyPropertChangedInterceptor>().LifestyleTransient())
            );
        }
    }
}