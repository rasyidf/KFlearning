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
                DefaultExt = "zip",
                Filter = "KFlearning ZIP archive (*.zip)|*.zip",
                Multiselect = false,
                Title = "Pilih file untuk di impor."
            };
            _sfd = new SaveFileDialog
            {
                DefaultExt = ".zip",
                Filter = "KFlearning ZIP archive (*.zip)|*.zip",
                Title = "Pilih lokasi file untuk di ekspor."
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
                await _helpers.CreateMessageDialog("Proyek baru",
                    "Tidak dapat membuat proyek baru karena proyek dengan nama yang sama sudah ada.");
                return;
            }

            var controller = await _helpers.CreateProgressDialog("Proyek baru", "Membuat proyek...");
            await Task.Run(() => _projectManager.Create(state.Type, state.Name))
                .ContinueWith(x => controller.CloseAsync())
                .ContinueWith(x => LoadData());
        }

        private async void Delete_Command(object obj)
        {
            var controller = await _helpers.CreateProgressDialog("Hapus proyek", "Menghapus proyek...");
            await Task.Run(() => _projectManager.Delete(SelectedProject.Item))
                .ContinueWith(x => controller.CloseAsync())
                .ContinueWith(x => LoadData());
        }

        private async void Import_Command(object obj)
        {
            if (_sfd.ShowDialog() == false) return;
            var controller = await _helpers.CreateProgressDialog("Impor proyek", "Mengimpor proyek...");
            await Task.Run(() => _projectManager.Import(_sfd.FileName))
                .ContinueWith(x => controller.CloseAsync())
                .ContinueWith(x => LoadData());
        }

        private async void Export_Command(object obj)
        {
            if (_ofd.ShowDialog() == false) return;
            var controller = await _helpers.CreateProgressDialog("Ekspor proyek", "Mengekspor proyek...");
            await Task.Run(() => _projectManager.Export(SelectedProject.Item, _ofd.FileName))
                .ContinueWith(x => controller.CloseAsync())
                .ContinueWith(x => LoadData());
        }

        private async void Purge_Command(object obj)
        {
            var result = await _helpers.CreateMessageDialog("Operasi Purge", Strings.PurgeConfirmMessage,
                MessageDialogStyle.AffirmativeAndNegative);
            if (result != MessageDialogResult.Affirmative) return;
            var controller = await _helpers.CreateProgressDialog("Purge", "Menghapus semua proyek...");
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