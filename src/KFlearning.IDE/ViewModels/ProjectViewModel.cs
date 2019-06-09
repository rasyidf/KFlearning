using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Input;
using KFlearning.ApplicationServices;
using KFlearning.ApplicationServices.Clients;
using KFlearning.ApplicationServices.Models;
using KFlearning.IDE.Models;
using KFlearning.IDE.Views;
using MahApps.Metro.Controls;
using MahApps.Metro.Controls.Dialogs;

namespace KFlearning.IDE.ViewModels
{
    public class ProjectViewModel : ViewModelBase
    {
        private ProjectManager _repository;
        private Vscode _vscode;

        public ICommand CreateCommand { get; set; }

        public ICommand DeleteCommand { get; set; }

        public ICommand ImportCommand { get; set; }

        public ICommand ExportCommand { get; set; }

        [NotifyChanged]
        public virtual ObservableCollection<ProjectItem> Projects { get; set; }

        public ProjectViewModel()
        {
            CreateCommand = new RelayCommand(CreateCommand_Handler);
        }

        private void CreateCommand_Handler(object obj)
        {
            var mainWindow = (MetroWindow) Application.Current.MainWindow;
            var dialog = ViewLocator.GetView<CreateProjectView>();
            ((CreateProjectViewModel) dialog.DataContext).DialogInstance = dialog;
            mainWindow.ShowMetroDialogAsync(dialog).Wait();
            

        }

        protected override void OnBootstrap(AppEventArgs e)
        {
            //Projects = new ObservableCollection<ProjectItem>(_repository.GetProjects());
        }
    }
}
