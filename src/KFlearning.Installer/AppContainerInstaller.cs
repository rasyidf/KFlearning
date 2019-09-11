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
using KFlearning.Core.Installer;
using KFlearning.Core.IO;
using KFlearning.Core.Services;
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
                Classes.FromAssemblyNamed("KFlearning.Core").Pick().WithServiceAllInterfaces().LifestyleTransient(),
                Classes.FromAssemblyNamed("KFlearning.Core.Installer").Pick().WithServiceAllInterfaces().LifestyleTransient(),

                // application specific
                Classes.FromThisAssembly().InSameNamespaceAs<MainForm>().LifestyleTransient(),
                Component.For<IProgressBroker>().ImplementedBy<ProgressBroker>().LifestyleSingleton()
            );
        }
    }
}