// 
//  PROJECT  :   KFlearning
//  FILENAME :   ArticleViewModel.cs
//  AUTHOR   :   Fahmi Noor Fiqri
//  NPM      :   065118116
// 
//  This file is part of KFlearning, licensed under MIT license.

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using KFlearning.Core.API;
using KFlearning.Core.DAL;
using KFlearning.IDE.ApplicationServices;
using KFlearning.IDE.Models;

namespace KFlearning.IDE.ViewModels
{
    public class ArticleViewModel : PropertyChangedBase
    {
        #region Fields

        private readonly IApplicationHelpers _helpers;
        private readonly IDatabaseContext _database;
        private readonly IKodesianaService _kodesiana;

        #endregion

        #region Constructor

        public ArticleViewModel(IApplicationHelpers helpers, IDatabaseContext database, IKodesianaService kodesiana)
        {
            _helpers = helpers;
            _database = database;
            _kodesiana = kodesiana;

            RefreshCommand = new RelayCommand(Refresh_Command);
            SearchCommand = new RelayCommand(Search_Command);
            OnlineChangedCommand = new RelayCommand(Online_Command);
            ArticleDoubleClickCommand = new RelayCommand(ArticleDoubleClick_Command);

            Task.Run(LoadData);
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
            _helpers.ShowReaderWindow((ArticleItem) obj);
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
            try
            {
                CommandIsLoading = true;

                List<ArticleItem> articles;
                if (OnlineIsChecked)
                {
                    articles = _database.Articles
                        .Find(x => SelectedSeries.Title == x.Series && x.Title.Contains(SearchText))
                        .Select(x => new ArticleItem(x)).ToList();

                }
                else
                {
                    var result = await _kodesiana.FindPostAsync(SearchText, SelectedSeries.Title);
                    articles = result.Select(x => new ArticleItem(x)).ToList();
                }

                Articles = new ObservableCollection<ArticleItem>(articles);                
            }
            catch (Exception)
            {
                await _helpers.CreateMessageDialog("Error", "Maaf, tidak dapat memuat daftar artikel. Coba lagi.");
            }
            
            CommandIsLoading = false;
        }

        #endregion

        #region Private Methods

        private async Task LoadData()
        {
            try
            {
                CommandIsLoading = true;

                List<SeriesItem> series;
                List<ArticleItem> articles = null;
                if (OnlineIsChecked)
                {
                    series = _database.Series.FindAll().Select(x => new SeriesItem(x.Title)).ToList();
                    if (series.Any())
                    {
                        articles = _database.Articles.Find(x => x.Series == series.First().Title)
                            .Select(x => new ArticleItem(x)).ToList();
                    }
                }
                else
                {
                    var seriesResult = await _kodesiana.GetSeriesAsync();
                    series = seriesResult.Select(x => new SeriesItem(x)).ToList();
                    if (series.Any())
                    {
                        var articlesResult = await _kodesiana.GetPostsAsync(series.First().Title);
                        articles = articlesResult.Select(x => new ArticleItem(x)).ToList();
                    }
                }

                Series = new ObservableCollection<SeriesItem>(series);
                SelectedSeries = series.Any() ? null : Series[0];
                Articles = articles == null ? null : new ObservableCollection<ArticleItem>(articles);
            }
            catch (Exception)
            {
                await _helpers.CreateMessageDialog("Error", "Maaf, tidak dapat memuat daftar artikel. Coba lagi.");
            }

            CommandIsLoading = false;
        }

        #endregion
    }
}