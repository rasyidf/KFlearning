// 
//  PROJECT  :   KFlearning
//  FILENAME :   AppContainerInstaller.cs
//  AUTHOR   :   Fahmi Noor Fiqri
//  WEBSITE  : https://kodesiana.com
//  REPO     : https://github.com/Kodesiana or https://github.com/fahminlb33
// 
//  This file is part of KFlearning, licensed under MIT license.
//  See this code in repository URL above!

#region

using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;
using KFlearning.Core.API;
using KFlearning.Core.DAL;
using KFlearning.Core.IO;
using KFlearning.Core.Services;
using KFlearning.Core.Services.Installer;
using KFlearning.Installer.ApplicationServices;
using KFlearning.Installer.Views;

#endregion

namespace KFlearning.Installer
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
                Classes.FromAssemblyNamed("KFlearning.Core").InSameNamespaceAs<IProjectManager>()
                    .WithServiceDefaultInterfaces().LifestyleSingleton(),
                Classes.FromAssemblyNamed("KFlearning.Core").InSameNamespaceAs<IPathManager>()
                    .WithServiceDefaultInterfaces().LifestyleSingleton(),

                // application specific
                Component.For<IProgressBroker>().ImplementedBy<ProgressBroker>().LifestyleSingleton(),
                Classes.FromThisAssembly().InSameNamespaceAs<MainForm>().LifestyleTransient()
            );
        }
    }
}