// 
//  PROJECT  :   KFlearning
//  FILENAME :   ProjectViewModel.cs
//  AUTHOR   :   Fahmi Noor Fiqri
//  NPM      :   065118116
// 
//  This file is part of KFlearning, licensed under MIT license.

using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows.Input;
using KFlearning.IDE.ApplicationServices;
using KFlearning.IDE.Models;
using KFlearning.IDE.Resources;
using MahApps.Metro.Controls.Dialogs;
using Microsoft.Win32;

namespace KFlearning.IDE.ViewModels
{
    public class ProjectViewModel : PropertyChangedBase
    {
        #region Constructor

        public ProjectViewModel(IProjectManager projectManager, IApplicationHelpers helpers)
        {
            _projectManager = projectManager;
            _helpers = helpers;

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
            ProjectDoubleClickCommand = new RelayCommand(Project_DoubleClick);

            Task.Run(LoadData);
        }

        #endregion

        #region Private Methods

        private void LoadData()
        {
            var result = _projectManager.GetProjects();
            Projects = new ObservableCollection<ProjectItem>(result);
        }

        #endregion

        #region Fields

        private readonly IProjectManager _projectManager;
        private readonly IApplicationHelpers _helpers;
        private readonly OpenFileDialog _ofd;
        private readonly SaveFileDialog _sfd;

        #endregion

        #region Properties

        public ICommand CreateCommand { get; set; }

        public ICommand DeleteCommand { get; set; }

        public ICommand ImportCommand { get; set; }

        public ICommand ExportCommand { get; set; }

        public ICommand PurgeCommand { get; set; }

        public ICommand ProjectDoubleClickCommand { get; set; }

        [NotifyChanged] public virtual ObservableCollection<ProjectItem> Projects { get; set; }

        [NotifyChanged] public virtual ProjectItem SelectedProject { get; set; }

        #endregion

        #region Commands

        private async void Create_Command(object obj)
        {
            var dialog = await _helpers.CreateNewProjectDialog();
            if (dialog.Result != MessageDialogResult.Affirmative) return;

            var state = (CreateProjectState) dialog.State;
            if (_projectManager.Exists(state.Name))
            {
                await _helpers.CreateMessageDialog(Texts.TitleCreateProject, Texts.CreateProjectDuplicateMessage);
                return;
            }

            var controller = await _helpers.CreateProgressDialog(Texts.TitleCreateProject, Texts.CreateProjectMessage);
            await Task.Run(() => _projectManager.Create(state.Type, state.Name))
                .ContinueWith(x => controller.CloseAsync())
                .ContinueWith(x => LoadData());
        }

        private async void Delete_Command(object obj)
        {
            var controller = await _helpers.CreateProgressDialog(Texts.TitleDelete, Texts.DeleteMessage);
            await Task.Run(() => _projectManager.Delete(SelectedProject.Item))
                .ContinueWith(x => controller.CloseAsync())
                .ContinueWith(x => LoadData());
        }

        private async void Import_Command(object obj)
        {
            if (_sfd.ShowDialog() == false) return;
            if (!_projectManager.CheckImportZip(_sfd.FileName))
            {
                await _helpers.CreateMessageDialog(Texts.TitleImport, Strings.ImportIncompatibleMessage);
                return;
            }

            var controller = await _helpers.CreateProgressDialog(Texts.TitleImport, Texts.ImportMessage);
            await Task.Run(() => _projectManager.Import(_sfd.FileName))
                .ContinueWith(x => controller.CloseAsync())
                .ContinueWith(x => LoadData());
        }

        private async void Export_Command(object obj)
        {
            if (_ofd.ShowDialog() == false) return;
            var controller = await _helpers.CreateProgressDialog(Texts.TitleExport, Texts.ExportMessage);
            await Task.Run(() => _projectManager.Export(SelectedProject.Item, _ofd.FileName))
                .ContinueWith(x => controller.CloseAsync())
                .ContinueWith(x => LoadData());
        }

        private async void Purge_Command(object obj)
        {
            var result = await _helpers.CreateMessageDialog(Texts.TitlePurge, Texts.PurgeConfirmMessage,
                MessageDialogStyle.AffirmativeAndNegative);
            if (result != MessageDialogResult.Affirmative) return;
            var controller = await _helpers.CreateProgressDialog(Texts.TitlePurge, Texts.PurgeProcessMessage);
            await Task.Run(() => _projectManager.Purge())
                .ContinueWith(x => controller.CloseAsync())
                .ContinueWith(x => LoadData());
        }

        private void Project_DoubleClick(object obj)
        {
            _projectManager.Launch(SelectedProject.Item);
        }

        #endregion
    }
}