// 
//  PROJECT  :   KFlearning
//  FILENAME :   ArticleViewModel.cs
//  AUTHOR   :   Fahmi Noor Fiqri
//  WEBSITE  : https://kodesiana.com
//  REPO     : https://github.com/Kodesiana or https://github.com/fahminlb33
// 
//  This file is part of KFlearning, licensed under MIT license.
//  See this code in repository URL above!

#region

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using KFlearning.Core.API;
using KFlearning.Core.IDE.Data;
using KFlearning.IDE.ApplicationServices;
using KFlearning.IDE.Models;
using KFlearning.IDE.Resources;
using LiteDB;

#endregion

namespace KFlearning.IDE.ViewModels
{
    public class ArticleViewModel : PropertyChangedBase
    {
        #region Fields

        public const int PageSize = 20;

        private readonly IDatabaseContext _database;
        private readonly IKodesianaService _kodesiana;

        private bool _isSearchMode;
        private int _currentPage = 1;
        private int _totalPage = 1;

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
            PageFirstCommand = new RelayCommand(PageFirst_Command);
            PagePreviousCommand = new RelayCommand(PagePrevious_Command);
            PageNextCommand = new RelayCommand(PageNext_Command);
            PageLastCommand = new RelayCommand(PageLast_Command);

            Task.Run(LoadData);
        }

        #endregion

        #region Properties

        public ICommand RefreshCommand { get; set; }

        public ICommand SearchCommand { get; set; }

        public ICommand OnlineChangedCommand { get; set; }

        public ICommand ArticleDoubleClickCommand { get; set; }

        public ICommand PageFirstCommand { get; set; }

        public ICommand PagePreviousCommand { get; set; }

        public ICommand PageNextCommand { get; set; }

        public ICommand PageLastCommand { get; set; }

        [NotifyChanged] 
        public virtual bool CommandIsLoading { get; set; }

        [NotifyChanged]
        public virtual ObservableCollection<ArticleItem> Articles { get; set; }

        [NotifyChanged] 
        public virtual ObservableCollection<SeriesItem> Series { get; set; }

        [NotifyChanged] 
        public virtual SeriesItem SelectedSeries { get; set; }

        [NotifyChanged] 
        public virtual bool OfflineIsChecked { get; set; } = true;

        [NotifyChanged] 
        public virtual string SearchText { get; set; }

        [NotifyChanged]
        public virtual string CurrentPage { get; set; }

        #endregion

        #region Commands

        private void ArticleDoubleClick_Command(object obj)
        {
            Helpers.ShowReaderWindow((ArticleItem) obj);
        }

        private async void Online_Command(object obj)
        {
            try
            {
                CommandIsLoading = true;
                _isSearchMode = false;
                await LoadData();
            }
            catch
            {
                await Helpers.CreateMessageDialog(Texts.CantLoadArticleTitle, Texts.CantLoadArticleMessage);
            }

            CommandIsLoading = false;
        }

        private async void Refresh_Command(object obj)
        {
            try
            {
                CommandIsLoading = true;
                _isSearchMode = false;
                await LoadData();
            }
            catch
            {
                await Helpers.CreateMessageDialog(Texts.CantLoadArticleTitle, Texts.CantLoadArticleMessage);
            }

            CommandIsLoading = false;
        }

        private async void Search_Command(object obj)
        {
            try
            {
                CommandIsLoading = true;
                _isSearchMode = true;
                await SearchData();
            }
            catch
            {
                await Helpers.CreateMessageDialog(Texts.CantLoadArticleTitle, Texts.CantLoadArticleMessage);
            }

            CommandIsLoading = false;
        }

        private async void PageLast_Command(object obj)
        {
            if (_currentPage == _totalPage) return;

            try
            {
                CommandIsLoading = true;
                _currentPage = _totalPage;
                await ProcessData();
            }
            catch
            {
                await Helpers.CreateMessageDialog(Texts.CantLoadArticleTitle, Texts.CantLoadArticleMessage);
            }

            CommandIsLoading = false;
        }

        private async void PageNext_Command(object obj)
        {
            if (_currentPage < _totalPage) return;

            try
            {
                CommandIsLoading = true;
                _currentPage++;
                await ProcessData();
            }
            catch
            {
                await Helpers.CreateMessageDialog(Texts.CantLoadArticleTitle, Texts.CantLoadArticleMessage);
            }

            CommandIsLoading = false;
        }

        private async void PagePrevious_Command(object obj)
        {
            if (_currentPage < _totalPage) return;

            try
            {
                CommandIsLoading = true;
                _currentPage--;
                await ProcessData();
            }
            catch
            {
                await Helpers.CreateMessageDialog(Texts.CantLoadArticleTitle, Texts.CantLoadArticleMessage);
            }
        }

        private async void PageFirst_Command(object obj)
        {
            if (_currentPage == 1) return;

            try
            {
                CommandIsLoading = true;
                _currentPage = 1;
                await ProcessData();
            }
            catch
            {
                await Helpers.CreateMessageDialog(Texts.CantLoadArticleTitle, Texts.CantLoadArticleMessage);
            }

            CommandIsLoading = false;
        }

        #endregion

        #region Private Methods

        private async Task ProcessData()
        {
            if (_isSearchMode)
            {
                await SearchData();
            }
            else
            {
                await LoadData();
            }
        }

        private async Task SearchData()
        {
            List<ArticleItem> articles;
            var offset = _currentPage * PageSize;

            if (OfflineIsChecked)
            {
                articles = _database.Articles
                    .Find(x => SelectedSeries.Title == x.Series && x.Title.Contains(SearchText))
                    .Select(x => new ArticleItem(x)).ToList();

                _currentPage = Helpers.CalculatePage(offset, PageSize);
                _totalPage = Helpers.CalculateTotalPage(articles.Count, PageSize);

                var skip = _currentPage * PageSize;
                articles = articles.Skip(skip).Take(PageSize).ToList();
            }
            else
            {
                var result = await _kodesiana.FindPostAsync(offset, PageSize, SearchText, SelectedSeries.Title);
                articles = result.Posts.Select(x => new ArticleItem(x)).ToList();

                _currentPage = Helpers.CalculatePage(result.Offset, PageSize);
                _totalPage = Helpers.CalculateTotalPage(result.Total, PageSize);
            }

            Articles = new ObservableCollection<ArticleItem>(articles);
            CurrentPage = _currentPage.ToString();
        }

        private async Task LoadData()
        {
            List<SeriesItem> series;
            List<ArticleItem> articles = null;

            if (OfflineIsChecked)
            {
                series = _database.Series.FindAll().Select(x => new SeriesItem(x.Title)).ToList();
                if (series.Any())
                {
                    articles = _database.Articles.Find(x => x.Series == series.First().Title)
                        .Select(x => new ArticleItem(x)).ToList();

                    _currentPage = Helpers.CalculatePage(_currentPage * PageSize, PageSize);
                    _totalPage = Helpers.CalculateTotalPage(articles.Count, PageSize);

                    var skip = (_currentPage - 1) * PageSize;
                    articles = articles.Skip(skip).Take(PageSize).ToList();
                }
                else
                {
                    _currentPage = 1;
                    _totalPage = 1;
                }
            }
            else
            {
                var seriesResult = await _kodesiana.GetSeriesAsync();
                series = seriesResult.Select(x => new SeriesItem(x)).ToList();
                if (series.Any())
                {
                    var offset = (_currentPage - 1) * PageSize;
                    var articlesResult = await _kodesiana.GetPostsAsync(offset, PageSize, series.First().Title);
                    articles = articlesResult.Posts.Select(x => new ArticleItem(x)).ToList();

                    _currentPage = Helpers.CalculatePage(articlesResult.Offset, PageSize);
                    _totalPage = Helpers.CalculateTotalPage(articlesResult.Total, PageSize);
                }
                else
                {
                    _currentPage = 1;
                    _totalPage = 1;
                }
            }

            Series = new ObservableCollection<SeriesItem>(series);
            SelectedSeries = series.Any() ? Series[0] : null;
            Articles = articles != null ? new ObservableCollection<ArticleItem>(articles) : null;
            CurrentPage = _currentPage.ToString();
        }

        #endregion
    }
}