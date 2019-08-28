// 
//  PROJECT  :   KFlearning
//  FILENAME :   ShellViewModel.cs
//  AUTHOR   :   Fahmi Noor Fiqri
//  WEBSITE  : https://kodesiana.com
//  REPO     : https://github.com/Kodesiana or https://github.com/fahminlb33
// 
//  This file is part of KFlearning, licensed under MIT license.
//  See this code in repository URL above!

#region

using System.Windows.Input;
using KFlearning.IDE.ApplicationServices;
using KFlearning.IDE.Resources;
using KFlearning.IDE.Views;
using MahApps.Metro.Controls;
using MahApps.Metro.IconPacks;

#endregion

namespace KFlearning.IDE.ViewModels
{
    public class ShellViewModel : PropertyChangedBase
    {
        #region Constructor

        public ShellViewModel()
        {
            WebCommand = new RelayCommand(x => Helpers.OpenUrl(Strings.WebUrl, Strings.CampaignNav));
            GitHubCommand = new RelayCommand(x => Helpers.OpenUrl(Strings.GitHubUrl));
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