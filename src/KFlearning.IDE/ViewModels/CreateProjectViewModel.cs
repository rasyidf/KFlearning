using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Input;
using KFlearning.ApplicationServices;
using KFlearning.IDE.Views;
using MahApps.Metro.Controls;
using MahApps.Metro.Controls.Dialogs;

namespace KFlearning.IDE.ViewModels
{
    public class CreateProjectViewModel : ViewModelBase
    {
        public CreateProjectView DialogInstance;

        public ICommand CreateCommand { get; set; }

        public ICommand CancelCommand { get; set; }

        [NotifyChanged]
        public virtual string ProjectName { get; set; }

        [NotifyChanged]
        public virtual ObservableCollection<string> ProjectTypes { get; set; }

        [NotifyChanged]
        public virtual string ProjectType { get; set; }

        [NotifyChanged]
        public virtual string ProjectPath { get; set; }

        public CreateProjectViewModel()
        {
            CreateCommand = new RelayCommand(Create_Command);
            CancelCommand = new RelayCommand(Cancel_Command);
        }

        private void Cancel_Command(object obj)
        {
            var mainWindow = (MetroWindow)Application.Current.MainWindow;
            mainWindow.HideMetroDialogAsync(DialogInstance);
            
        }

        private void Create_Command(object obj)
        {
            var mainWindow = (MetroWindow)Application.Current.MainWindow;
            mainWindow.HideMetroDialogAsync(DialogInstance);
        }
    }
}
