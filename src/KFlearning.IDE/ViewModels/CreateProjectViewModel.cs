using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Windows;
using System.Windows.Input;
using KFlearning.IDE.ApplicationServices;
using KFlearning.IDE.Models;
using MahApps.Metro.Controls;
using MahApps.Metro.Controls.Dialogs;

namespace KFlearning.IDE.ViewModels
{
    public class CreateProjectViewModel : PropertyChangedBase, IDialog
    {
        #region Properties
        
        public ICommand CreateCommand { get; set; }

        public ICommand CancelCommand { get; set; }

        public BaseMetroDialog DialogInstance { get; set; }

        [NotifyChanged]
        public virtual string ProjectName { get; set; }

        [NotifyChanged]
        public virtual ObservableCollection<string> ProjectTypes { get; set; }

        [NotifyChanged]
        public virtual string ProjectType { get; set; }

        [NotifyChanged]
        public virtual string ProjectPath { get; set; }

        #endregion

        #region Constructor
        
        public CreateProjectViewModel()
        {
            CreateCommand = new RelayCommand(Create_Command);
            CancelCommand = new RelayCommand(Cancel_Command);
        }

        #endregion

        #region Commands
        
        private async void Cancel_Command(object obj)
        {
            var mainWindow = (MetroWindow)Application.Current.MainWindow;
            Debug.Print("Hide dialog...");
            await mainWindow.HideMetroDialogAsync(DialogInstance);
            Debug.Print("Hide dialog.");
        }

        private async void Create_Command(object obj)
        {
            var mainWindow = (MetroWindow)Application.Current.MainWindow;
            //await mainWindow.HideMetroDialogAsync(DialogInstance);
        } 

        #endregion

    }
}
