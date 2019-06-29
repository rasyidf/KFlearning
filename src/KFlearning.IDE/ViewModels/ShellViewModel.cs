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
        #region Properties

        public ICommand WebCommand { get; set; }

        public ICommand GitHubCommand { get; set; }

        public ICommand ItemClickCommand { get; set; }

        [NotifyChanged] public virtual object PageContent { get; set; }

        [NotifyChanged] public virtual HamburgerMenuItemCollection SidebarItems { get; set; }

        [NotifyChanged] public virtual HamburgerMenuItemCollection SidebarOptionsItems { get; set; }

        #endregion

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
            PageContent = ((HamburgerMenuIconItem)obj).Tag;
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
                    Label = "Proyek",
                    Tag = App.Container.Resolve<ProjectView>()
                },
                new HamburgerMenuIconItem
                {
                    Icon = new PackIconMaterial {Kind = PackIconMaterialKind.Newspaper},
                    Label = "Artikel",
                    Tag = App.Container.Resolve<ArticleView>()
                },
                new HamburgerMenuIconItem
                {
                    Icon = new PackIconMaterial {Kind = PackIconMaterialKind.Approval},
                    Label = "Apps",
                    Tag = App.Container.Resolve<WebServerView>()
                }
            };
            SidebarOptionsItems = new HamburgerMenuItemCollection
            {
                new HamburgerMenuIconItem
                {
                    Icon = new PackIconMaterial {Kind = PackIconMaterialKind.Information},
                    Label = "Tentang",
                    Tag = App.Container.Resolve<AboutView>()
                }
            };

            PageContent = SidebarItems[0].Tag;
        }

        #endregion
    }
}
