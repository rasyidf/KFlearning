// 
//  PROJECT  :   KFlearning
//  FILENAME :   CreateProjectViewModel.cs
//  AUTHOR   :   Fahmi Noor Fiqri
//  NPM      :   065118116
// 
//  This file is part of KFlearning, licensed under MIT license.

using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Input;
using KFlearning.Core.Entities;
using KFlearning.IDE.ApplicationServices;
using KFlearning.IDE.Models;
using MahApps.Metro.Controls;
using MahApps.Metro.Controls.Dialogs;

namespace KFlearning.IDE.ViewModels
{
    public class CreateProjectViewModel : PropertyChangedBase, IDialog
    {
        #region Constructor

        public CreateProjectViewModel()
        {
            CreateCommand = new RelayCommand(Create_Command);
            CancelCommand = new RelayCommand(Cancel_Command);

            Initialize();
        }

        #endregion

        #region Properties

        public ICommand CreateCommand { get; set; }

        public ICommand CancelCommand { get; set; }

        public BaseMetroDialog DialogInstance { get; set; }

        public MessageDialogResult DialogResult { get; set; }

        public object State { get; set; }

        [NotifyChanged] public virtual string ProjectName { get; set; }

        [NotifyChanged] public virtual ObservableCollection<ProjectTypeItem> ProjectTypes { get; set; }

        [NotifyChanged] public virtual ProjectTypeItem SelectedType { get; set; }

        #endregion

        #region Commands

        private void Cancel_Command(object obj)
        {
            DialogResult = MessageDialogResult.Negative;
            CloseDialog();
        }

        private void Create_Command(object obj)
        {
            DialogResult = MessageDialogResult.Affirmative;
            State = new CreateProjectState
            {
                Name = ProjectName,
                Type = SelectedType.Type
            };
            CloseDialog();
        }

        #endregion

        #region Private Methods

        private void Initialize()
        {
            ProjectTypes = new ObservableCollection<ProjectTypeItem>
            {
                new ProjectTypeItem(ProjectType.Cpp, "C/C++ Project"),
                new ProjectTypeItem(ProjectType.Web, "Web PHP Project"),
                new ProjectTypeItem(ProjectType.Python, "Python Project")
            };
        }

        private async void CloseDialog()
        {
            var mainWindow = (MetroWindow) Application.Current.MainWindow;
            await mainWindow.HideMetroDialogAsync(DialogInstance);
        }

        #endregion
    }
}