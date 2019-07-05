// // PROJECT :   KFlearning
// // FILENAME :  ArticleViewModel.cs
// // AUTHOR  :   Fahmi Noor Fiqri
// // NPM     :   065118116
// //
// // This file is part of KFlearning, licensed under MIT license.

using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows.Input;
using Castle.MicroKernel;
using KFlearning.IDE.ApplicationServices;
using KFlearning.IDE.Models;
using KFlearning.IDE.Views;

namespace KFlearning.IDE.ViewModels
{
    public class ArticleViewModel : PropertyChangedBase
    {
        #region Fields

        private readonly IArticleManager _articleManager;

        #endregion

        #region Constructor

        public ArticleViewModel(IArticleManager articleManager)
        {
            _articleManager = articleManager;

            RefreshCommand = new RelayCommand(Refresh_Command);
            SearchCommand = new RelayCommand(Search_Command);
            OnlineChangedCommand = new RelayCommand(Online_Command);
            ArticleDoubleClickCommand = new RelayCommand(ArticleDoubleClick_Command);

            Task.Run(LoadData);
        }

        #endregion

        #region Private Methods

        private async Task LoadData()
        {
            CommandIsLoading = true;
            _articleManager.Online = OnlineIsChecked;

            var series = await _articleManager.GetSeries();
            Series = new ObservableCollection<SeriesItem>(series);
            SelectedSeries = Series.Count == 0 ? null : Series[0];

            if (SelectedSeries != null)
            {
                var articles = await _articleManager.GetArticles(SelectedSeries);
                Articles = new ObservableCollection<ArticleItem>(articles);
            }

            CommandIsLoading = false;
        }

        #endregion

        #region Properties

        public ICommand RefreshCommand { get; set; }

        public ICommand SearchCommand { get; set; }

        public ICommand OnlineChangedCommand { get; set; }

        public ICommand ArticleDoubleClickCommand { get; set; }

        [NotifyChanged] public virtual bool CommandIsLoading { get; set; }

        [NotifyChanged] public virtual ObservableCollection<ArticleItem> Articles { get; set; }

        [NotifyChanged] public virtual ObservableCollection<SeriesItem> Series { get; set; }

        [NotifyChanged] public virtual SeriesItem SelectedSeries { get; set; }

        [NotifyChanged] public virtual bool OnlineIsChecked { get; set; }

        [NotifyChanged] public virtual string SearchText { get; set; }

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
            _articleManager.Online = OnlineIsChecked;
            var result = await _articleManager.FindArticles(SearchText, SelectedSeries);
            Articles = new ObservableCollection<ArticleItem>(result);
            CommandIsLoading = false;
        }

        #endregion
    }
}