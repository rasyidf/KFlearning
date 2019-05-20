using System.Collections.ObjectModel;
using System.Windows.Controls;
using KFlearning.ApplicationServices.Models;
using MahApps.Metro.IconPacks;

namespace KFlearning.IDE.Views
{
    /// <summary>
    /// Interaction logic for ProjectView.xaml
    /// </summary>
    public partial class ProjectView : UserControl
    {
        public ObservableCollection<ProjectItem> Projects { get; set; }

        public ProjectView()
        {
            InitializeComponent();
            Projects = new ObservableCollection<ProjectItem>
            {
                new ProjectItem {Icon = PackIconMaterialKind.Web, Path = "C:\\data", Title = "My Amazing Web"},
                new ProjectItem {Icon = PackIconMaterialKind.LanguageCpp, Path = "C:\\cff", Title = "My Amazing C++"},
                new ProjectItem {Icon = PackIconMaterialKind.LanguagePython, Path = "C:\\py", Title = "My Amazing Python"}
            };
            DataContext = this;
        }
    }
}
