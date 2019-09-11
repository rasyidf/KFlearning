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
using KFlearning.IDE.ApplicationServices;
using KFlearning.IDE.ViewModels;
using KFlearning.IDE.Views;

#endregion

namespace KFlearning.IDE
{
    public class AppContainerInstaller : IWindsorInstaller
    {
        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            // Register Views and ViewModels
            container.Register(
                // common core
                Classes.FromAssemblyNamed("KFlearning.Core").Pick().WithServiceAllInterfaces().LifestyleTransient(),
                Classes.FromAssemblyNamed("KFlearning.Core.IDE").Pick().WithServiceAllInterfaces().LifestyleTransient(),

                // application specific
                Component.For<NotifyPropertChangedInterceptor>().LifestyleTransient(),
                Classes.FromThisAssembly().InSameNamespaceAs<ShellView>().LifestyleTransient(),
                Classes.FromThisAssembly().InSameNamespaceAs<ShellViewModel>()
                    .Configure(x => x.Interceptors<NotifyPropertChangedInterceptor>().LifestyleTransient())
            );
        }
    }
}