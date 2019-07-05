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
using KFlearning.API;
using KFlearning.Core.Entities;
using KFlearning.Core.Hosts;
using KFlearning.Core.IO;
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
                Component.For<IProcessManager>().ImplementedBy<ProcessManager>().LifestyleSingleton(),
                Component.For<IHostsFile>().ImplementedBy<HostsFile>().LifestyleSingleton(),
                Component.For<IApacheServer>().ImplementedBy<ApacheServer>().LifestyleSingleton(),
                Component.For<IMariaDb>().ImplementedBy<MariaDb>().LifestyleSingleton(),
                Component.For<IVscode>().ImplementedBy<Vscode>().LifestyleSingleton(),
                Component.For<IDatabaseContext>().ImplementedBy<DatabaseContext>().LifestyleSingleton(),
                Component.For<IKodesianaService>().ImplementedBy<KodesianaService>().LifestyleSingleton(),
                Component.For<IHtmlTransformer>().ImplementedBy<HtmlTransformer>().LifestyleSingleton(),
                Component.For<IApplicationHelpers>().ImplementedBy<ApplicationHelpers>().LifestyleSingleton(),
                Component.For<IArticleManager>().ImplementedBy<ArticleManager>().LifestyleSingleton(),
                Component.For<IProjectManager>().ImplementedBy<ProjectManager>().LifestyleSingleton(),
                Component.For<NotifyPropertChangedInterceptor>(),
                Component.For<IAutoNotifyPropertyChanged>().ImplementedBy<PropertyChangedBase>(),
                Classes.FromThisAssembly().InSameNamespaceAs<ShellView>().LifestyleTransient(),
                Classes.FromThisAssembly().InSameNamespaceAs<ShellViewModel>()
                    .Configure(x => x.Interceptors<NotifyPropertChangedInterceptor>().LifestyleTransient())
            );
        }
    }
}