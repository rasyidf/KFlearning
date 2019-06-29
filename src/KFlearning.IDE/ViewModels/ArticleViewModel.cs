using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using Castle.MicroKernel;
using KFlearning.API;
using KFlearning.DAL;
using KFlearning.IDE.ApplicationServices;
using KFlearning.IDE.Models;
using KFlearning.IDE.Views;

namespace KFlearning.IDE.ViewModels
{
    public class ArticleViewModel : PropertyChangedBase
    {
        #region Fields
        
        private readonly IDatabaseContext _database;
        private readonly IKodesianaService _kodesiana;
        
        #endregion

        #region Properties

        public ICommand RefreshCommand { get; set; }

        public ICommand SearchCommand { get; set; }

        public ICommand OnlineChangedCommand { get; set; }

        public ICommand ArticleDoubleClickCommand { get; set; }

        [NotifyChanged]
        public virtual bool CommandIsLoading { get; set; }

        [NotifyChanged]
        public virtual ObservableCollection<ArticleItem> Articles { get; set; }
        
        [NotifyChanged]
        public virtual ObservableCollection<Series> Series { get; set; }
        
        [NotifyChanged]
        public virtual Series SelectedSeries { get; set; }
        
        [NotifyChanged]
        public virtual bool OnlineIsChecked { get; set; }
        
        [NotifyChanged]
        public virtual string SearchText { get;set; }
        
        #endregion

        #region Constructor

        public ArticleViewModel(IDatabaseContext database, IKodesianaService kodesiana)
        {
            _database = database;
            _kodesiana = kodesiana;

            RefreshCommand = new RelayCommand(Refresh_Command);
            SearchCommand = new RelayCommand(Search_Command);
            OnlineChangedCommand = new RelayCommand(Online_Command);
            ArticleDoubleClickCommand = new RelayCommand(ArticleDoubleClick_Command);

            Task.Run(LoadData);
        }

        #endregion

        #region Commands

        private void ArticleDoubleClick_Command(object obj)
        {
            var args = new Dictionary<string, object>
            {
                {"item", obj}
            };

            App.Container.Resolve<ReaderView>(Arguments.FromNamed(args)).Show();
        }

        private async void Online_Command(object obj)
        {
            await LoadData();
        }

        private async void Refresh_Command(object obj)
        {
            await LoadData();
        }

        private async void Search_Command(object obj)
        {
            CommandIsLoading = true;
            if (OnlineIsChecked && await _kodesiana.IsOnline())
            {
                var result = await _kodesiana.FindPostAsync(SearchText, SelectedSeries.Title);
                Articles = new ObservableCollection<ArticleItem>(result.Select(x => new ArticleItem(x)));
            }
            else
            {
                var result = await _database.Articles
                    .Where(x => SelectedSeries.Title == x.Series.Title && x.Title.Contains(SearchText))
                    .Select(x => new ArticleItem(x)).ToListAsync();
                Articles = new ObservableCollection<ArticleItem>(result);
            }

            CommandIsLoading = false;
        }

        #endregion

        #region Private Methods

        private async Task LoadData()
        {
            CommandIsLoading = true;
            if (OnlineIsChecked && await _kodesiana.IsOnline())
            {
                await LoadOnlineData();
            }
            else
            {
                await LoadDatabaseData();
            }

            CommandIsLoading = false;
        }

        private async Task LoadDatabaseData()
        {
            var series = await _database.Series.ToListAsync();
            Series = new ObservableCollection<Series>(series);
            SelectedSeries = Series.Count == 0 ? null : Series.First();

            if (SelectedSeries == null) return;
            var articles = await _database.Articles.Select(x => new ArticleItem(x)).ToListAsync();
            Articles = new ObservableCollection<ArticleItem>(articles);
        }

        private async Task LoadOnlineData()
        {
            var cats = await _kodesiana.GetSeriesAsync();
            Series = new ObservableCollection<Series>(cats.Select(x => new Series {Title = x}));
            SelectedSeries = Series.First();

            var articles = await _kodesiana.GetPostsAsync(SelectedSeries.Title);
            Articles = new ObservableCollection<ArticleItem>(articles.Select(x => new ArticleItem(x)).ToList());
        }

        #endregion
    }
}
