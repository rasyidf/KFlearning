using System.Collections.ObjectModel;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using KFlearning.DAL;
using KFlearning.IDE.ApplicationServices;
using KFlearning.IDE.Models;
using KFlearning.IDE.Views;
using MahApps.Metro.Controls;
using MahApps.Metro.Controls.Dialogs;

namespace KFlearning.IDE.ViewModels
{
    public class ProjectViewModel : PropertyChangedBase
    {
        #region Fields

        private readonly IDatabaseContext _database;
        private readonly IApplicationHelpers _helpers;

        #endregion

        #region Properties

        public ICommand CreateCommand { get; set; }

        public ICommand DeleteCommand { get; set; }

        public ICommand ImportCommand { get; set; }

        public ICommand ExportCommand { get; set; }

        public ICommand PurgeCommand { get; set; }

        [NotifyChanged] public virtual ObservableCollection<ProjectItem> Projects { get; set; }

        [NotifyChanged] public virtual ObservableCollection<ProjectItem> SelectedProject { get; set; }

        #endregion

        #region Constructor

        public ProjectViewModel(IDatabaseContext database, IApplicationHelpers helpers)
        {
            _database = database;
            _helpers = helpers;
            CreateCommand = new RelayCommand(Create_Command);
            DeleteCommand = new RelayCommand(Delete_Command);
            ImportCommand = new RelayCommand(Import_Command);
            ExportCommand = new RelayCommand(Export_Command);
            PurgeCommand = new RelayCommand(Purge_Command);

            Task.Run(LoadData);
        }

        #endregion

        #region Commands

        private async void Create_Command(object obj)
        {
            var mainWindow = (MetroWindow) Application.Current.MainWindow;
            var dialog = _helpers.CreateDialog<CreateProjectView>();
            await mainWindow.ShowMetroDialogAsync(dialog);
        }

        private void Delete_Command(object obj)
        {
            throw new System.NotImplementedException();
        }

        private void Import_Command(object obj)
        {
            throw new System.NotImplementedException();
        }

        private void Export_Command(object obj)
        {
            throw new System.NotImplementedException();
        }

        private void Purge_Command(object obj)
        {
            throw new System.NotImplementedException();
        }

        #endregion

        #region Private Methods

        private async void LoadData()
        {
            var projects = await _database.Projects.Select(x => new ProjectItem(x)).ToListAsync();
            Projects = new ObservableCollection<ProjectItem>(projects);
        }

        #endregion
    }
}
