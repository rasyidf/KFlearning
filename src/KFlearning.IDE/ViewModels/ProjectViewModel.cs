// 
//  PROJECT  :   KFlearning
//  FILENAME :   ProjectViewModel.cs
//  AUTHOR   :   Fahmi Noor Fiqri
//  WEBSITE  : https://kodesiana.com
//  REPO     : https://github.com/Kodesiana or https://github.com/fahminlb33
// 
//  This file is part of KFlearning, licensed under MIT license.
//  See this code in repository URL above!

#region

using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using KFlearning.Core.IDE;
using KFlearning.Core.IO;
using KFlearning.IDE.ApplicationServices;
using KFlearning.IDE.Models;
using KFlearning.IDE.Resources;
using MahApps.Metro.Controls.Dialogs;
using Microsoft.Win32;

#endregion

namespace KFlearning.IDE.ViewModels
{
    public class ProjectViewModel : PropertyChangedBase
    {
        #region Fields

        private readonly IPathManager _path;
        private readonly IProjectManager _projectManager;
        private readonly IProjectHandler _projectHandler;
        private readonly OpenFileDialog _ofd;
        private readonly SaveFileDialog _sfd;

        #endregion

        #region Properties

        public ICommand CreateCommand { get; set; }

        public ICommand DeleteCommand { get; set; }

        public ICommand ImportCommand { get; set; }

        public ICommand ExportCommand { get; set; }

        public ICommand PurgeCommand { get; set; }

        public ICommand OpenFolderCommand { get; set; }

        public ICommand InitializeCppCommand { get; set; }

        public ICommand CreateLinkCommand { get; set; }

        public ICommand DeleteLinkCommand { get; set; }

        public ICommand ProjectDoubleClickCommand { get; set; }

        [NotifyChanged] public virtual ObservableCollection<ProjectItem> Projects { get; set; }

        [NotifyChanged] public virtual ProjectItem SelectedProject { get; set; }

        #endregion

        #region Constructor

        public ProjectViewModel(IProjectManager projectManager, IProjectHandler projectHandler, IPathManager path)
        {
            _projectManager = projectManager;
            _projectHandler = projectHandler;
            _path = path;

            _ofd = new OpenFileDialog
            {
                DefaultExt = Strings.FilterZipExtension,
                Filter = Strings.FilterZip,
                Multiselect = false,
                Title = Texts.DialogImportTitle
            };
            _sfd = new SaveFileDialog
            {
                DefaultExt = Strings.FilterZipExtension,
                Filter = Strings.FilterZip,
                Title = Texts.DialogExportTitle
            };

            CreateCommand = new RelayCommand(Create_Command);
            DeleteCommand = new RelayCommand(Delete_Command);
            ImportCommand = new RelayCommand(Import_Command);
            ExportCommand = new RelayCommand(Export_Command);
            PurgeCommand = new RelayCommand(Purge_Command);
            OpenFolderCommand = new RelayCommand(OpenFolder_Command);
            InitializeCppCommand = new RelayCommand(InitializeCpp_Command);
            CreateLinkCommand = new RelayCommand(CreateLink_Command);
            DeleteLinkCommand = new RelayCommand(DeleteLink_Command);
            ProjectDoubleClickCommand = new RelayCommand(Project_DoubleClick);

            Task.Run(LoadData);
        }

        #endregion

        #region Commands

        private async void Create_Command(object obj)
        {
            var projectName = await Helpers.CreateNewProjectDialog();
            if (string.IsNullOrWhiteSpace(projectName)) return;
            if (_projectManager.Exists(projectName))
            {
                await Helpers.CreateMessageDialog(Texts.TitleCreateProject, Texts.CreateProjectDuplicateMessage);
                return;
            }

            var controller = await Helpers.CreateProgressDialog(Texts.TitleCreateProject, Texts.CreateProjectMessage);
            await Task.Run(() => _projectManager.Create(projectName))
                .ContinueWith(x => controller.CloseAsync())
                .ContinueWith(x => LoadData());
        }

        private async void Delete_Command(object obj)
        {
            if (SelectedProject == null)
            {
                await Helpers.CreateMessageDialog(Texts.TitleNoProject, Texts.NoProjectMessage);
                return;
            }

            var controller = await Helpers.CreateProgressDialog(Texts.TitleDelete, Texts.DeleteMessage);
            await Task.Run(() => _projectManager.Delete(SelectedProject.Item))
                .ContinueWith(x => controller.CloseAsync())
                .ContinueWith(x => LoadData());
        }

        private async void Import_Command(object obj)
        {
            if (_ofd.ShowDialog() == false) return;
            if (!_projectManager.CheckImportZip(_ofd.FileName))
            {
                await Helpers.CreateMessageDialog(Texts.TitleImport, Strings.ImportIncompatibleMessage);
                return;
            }

            var controller = await Helpers.CreateProgressDialog(Texts.TitleImport, Texts.ImportMessage);
            await Task.Run(() => _projectManager.Import(_ofd.FileName))
                .ContinueWith(x => controller.CloseAsync())
                .ContinueWith(x => LoadData());
        }

        private async void Export_Command(object obj)
        {
            if (SelectedProject == null)
            {
                await Helpers.CreateMessageDialog(Texts.TitleNoProject, Texts.NoProjectMessage);
                return;
            }
            if (_sfd.ShowDialog() == false) return;

            var controller = await Helpers.CreateProgressDialog(Texts.TitleExport, Texts.ExportMessage);
            await Task.Run(() => _projectManager.Export(SelectedProject.Item, _sfd.FileName))
                .ContinueWith(x => controller.CloseAsync())
                .ContinueWith(x => LoadData());
        }

        private async void Purge_Command(object obj)
        {
            var result = await Helpers.CreateMessageDialog(Texts.TitlePurge, Texts.PurgeConfirmMessage,
                MessageDialogStyle.AffirmativeAndNegative);

            if (result != MessageDialogResult.Affirmative) return;
            var controller = await Helpers.CreateProgressDialog(Texts.TitlePurge, Texts.PurgeProcessMessage);
            await Task.Run(() => _projectManager.Purge())
                .ContinueWith(x => controller.CloseAsync())
                .ContinueWith(x => LoadData());
        }

        private async void OpenFolder_Command(object obj)
        {
            if (SelectedProject == null)
            {
                await Helpers.CreateMessageDialog(Texts.TitleNoProject, Texts.NoProjectMessage);
                return;
            }

            _path.LaunchExplorer(SelectedProject.Path);
        }

        private async void InitializeCpp_Command(object obj)
        {
            if (SelectedProject == null)
            {
                await Helpers.CreateMessageDialog(Texts.TitleNoProject, Texts.NoProjectMessage);
                return;
            }

            var controller = await Helpers.CreateProgressDialog(Texts.TitleInitCpp, Texts.InitCppMessage);
            await Task.Run(() => _projectHandler.InitializeCpp(SelectedProject.Item))
                .ContinueWith(x => controller.CloseAsync());
        }

        private async void CreateLink_Command(object obj)
        {
            if (SelectedProject == null)
            {
                await Helpers.CreateMessageDialog(Texts.TitleNoProject, Texts.NoProjectMessage);
                return;
            }
            if (!_projectHandler.CanModifyLink())
            {
                await Helpers.CreateMessageDialog(Texts.NotElevatedTitle, Texts.NotElevatedMessage);
                return;
            }

            var controller = await Helpers.CreateProgressDialog(Texts.TitleAlias, Texts.AliasCreatingMessage);
            await Task.Run(() => _projectHandler.CreateLink(SelectedProject.Item))
                .ContinueWith(x => controller.CloseAsync());
        }

        private async void DeleteLink_Command(object obj)
        {
            if (SelectedProject == null)
            {
                await Helpers.CreateMessageDialog(Texts.TitleNoProject, Texts.NoProjectMessage);
                return;
            }
            if (!_projectHandler.CanModifyLink())
            {
                await Helpers.CreateMessageDialog(Texts.NotElevatedTitle, Texts.NotElevatedMessage);
                return;
            }

            var controller = await Helpers.CreateProgressDialog(Texts.TitleAlias, Texts.AliasRemovingMessage);
            await Task.Run(() => _projectHandler.RemoveLink(SelectedProject.Item))
                .ContinueWith(x => controller.CloseAsync());
        }

        private async void Project_DoubleClick(object obj)
        {
            if (SelectedProject == null)
            {
                await Helpers.CreateMessageDialog(Texts.TitleNoProject, Texts.NoProjectMessage);
                return;
            }

            _projectHandler.Launch(SelectedProject.Item);
        }

        #endregion

        #region Private Methods

        private void LoadData()
        {
            var result = _projectManager.GetProjects().Select(x => new ProjectItem(x));
            Projects = new ObservableCollection<ProjectItem>(result);
        }

        #endregion
    }
}