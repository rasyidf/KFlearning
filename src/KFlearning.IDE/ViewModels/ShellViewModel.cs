﻿// 
//  PROJECT  :   KFlearning
//  FILENAME :   ShellViewModel.cs
//  AUTHOR   :   Fahmi Noor Fiqri
//  NPM      :   065118116
// 
//  This file is part of KFlearning, licensed under MIT license.

using System.Windows.Input;
using KFlearning.IDE.ApplicationServices;
using KFlearning.IDE.Resources;
using KFlearning.IDE.Views;
using MahApps.Metro.Controls;
using MahApps.Metro.IconPacks;

namespace KFlearning.IDE.ViewModels
{
    public class ShellViewModel : PropertyChangedBase
    {
        #region Constructor

        public ShellViewModel(IApplicationHelpers helpers)
        {
            WebCommand = new RelayCommand(x => helpers.OpenUrl(Strings.WebUrl));
            GitHubCommand = new RelayCommand(x => helpers.OpenUrl(Strings.GitHubUrl));
            ItemClickCommand = new RelayCommand(ItemClick_Command);

            PopulateView();
        }

        #endregion

        #region Commands

        private void ItemClick_Command(object obj)
        {
            PageContent = ((HamburgerMenuIconItem) obj).Tag;
        }

        #endregion

        #region Private Methods

        private void PopulateView()
        {
            SidebarItems = new HamburgerMenuItemCollection
            {
                new HamburgerMenuIconItem
                {
                    Icon = new PackIconMaterial {Kind = PackIconMaterialKind.Archive},
                    Label = Texts.NavbarProject,
                    Tag = App.Container.Resolve<ProjectView>()
                },
                new HamburgerMenuIconItem
                {
                    Icon = new PackIconMaterial {Kind = PackIconMaterialKind.Newspaper},
                    Label = Texts.NavbarArticles,
                    Tag = App.Container.Resolve<ArticleView>()
                },
                new HamburgerMenuIconItem
                {
                    Icon = new PackIconMaterial {Kind = PackIconMaterialKind.Approval},
                    Label = Texts.NavbarApps,
                    Tag = App.Container.Resolve<WebServerView>()
                }
            };
            SidebarOptionsItems = new HamburgerMenuItemCollection
            {
                new HamburgerMenuIconItem
                {
                    Icon = new PackIconMaterial {Kind = PackIconMaterialKind.Information},
                    Label = Texts.NavbarAbout,
                    Tag = App.Container.Resolve<AboutView>()
                }
            };

            PageContent = SidebarItems[0].Tag;
        }

        #endregion

        #region Properties

        public ICommand WebCommand { get; set; }

        public ICommand GitHubCommand { get; set; }

        public ICommand ItemClickCommand { get; set; }

        [NotifyChanged] public virtual object PageContent { get; set; }

        [NotifyChanged] public virtual HamburgerMenuItemCollection SidebarItems { get; set; }

        [NotifyChanged] public virtual HamburgerMenuItemCollection SidebarOptionsItems { get; set; }

        #endregion
    }
}