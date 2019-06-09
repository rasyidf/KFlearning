using System.Windows.Input;
using KFlearning.ApplicationServices;
using KFlearning.ApplicationServices.Models;
using KFlearning.IDE.Views;
using MahApps.Metro.Controls;
using MahApps.Metro.IconPacks;

namespace KFlearning.IDE.ViewModels
{
    public class ShellViewModel : ViewModelBase
    {
        public ICommand WebCommand { get; set; }

        public ICommand GitHubCommand { get; set; }

        public ICommand ItemClickCommand { get; set; }

        [NotifyChanged]
        public virtual object PageContent { get; set; }
        
        [NotifyChanged]
        public virtual HamburgerMenuItemCollection SidebarItems { get; set; }

        [NotifyChanged]
        public virtual HamburgerMenuItemCollection SidebarOptionsItems { get; set; }

        public ShellViewModel(IApplicationHelpers helpers)
        {
            WebCommand = new RelayCommand(x => helpers.OpenUrl(Strings.WebUrl));
            GitHubCommand = new RelayCommand(x => helpers.OpenUrl(Strings.GitHubUrl));
            ItemClickCommand = new RelayCommand(ItemClick_Command);
        }

        private void ItemClick_Command(object obj)
        {
            PageContent = ((HamburgerMenuIconItem) obj).Tag;
        }

        private void PopulateView()
        {
            SidebarItems = new HamburgerMenuItemCollection
            {
                new HamburgerMenuIconItem
                {
                    Icon = new PackIconMaterial {Kind = PackIconMaterialKind.Archive},
                    Label = "Proyek",
                    Tag = ViewLocator.GetView<ProjectView>()
                },
                new HamburgerMenuIconItem
                {
                    Icon = new PackIconMaterial {Kind = PackIconMaterialKind.Newspaper},
                    Label = "Artikel",
                    Tag = ViewLocator.GetView<ArticleView>()
                },
                new HamburgerMenuIconItem
                {
                    Icon = new PackIconMaterial {Kind = PackIconMaterialKind.Approval},
                    Label = "Apps",
                    Tag = ViewLocator.GetView<WebServerView>()
                }
            };
            SidebarOptionsItems = new HamburgerMenuItemCollection
            {
                new HamburgerMenuIconItem
                {
                    Icon = new PackIconMaterial {Kind = PackIconMaterialKind.Information},
                    Label = "Tentang",
                    Tag = ViewLocator.GetView<AboutView>()
                }
            };
        }

        protected override void OnBootstrap(AppEventArgs e)
        {
            PopulateView();
            PageContent = SidebarItems[0].Tag;
        }
    }
}
