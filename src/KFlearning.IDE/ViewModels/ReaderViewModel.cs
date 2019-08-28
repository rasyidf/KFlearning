// 
//  PROJECT  :   KFlearning
//  FILENAME :   ReaderViewModel.cs
//  AUTHOR   :   Fahmi Noor Fiqri
//  WEBSITE  : https://kodesiana.com
//  REPO     : https://github.com/Kodesiana or https://github.com/fahminlb33
// 
//  This file is part of KFlearning, licensed under MIT license.
//  See this code in repository URL above!

#region

using System;
using System.IO;
using System.Threading.Tasks;
using System.Windows.Input;
using KFlearning.Core.API;
using KFlearning.Core.DAL;
using KFlearning.IDE.ApplicationServices;
using KFlearning.IDE.Models;
using KFlearning.IDE.Resources;

#endregion

namespace KFlearning.IDE.ViewModels
{
    public class ReaderViewModel : PropertyChangedBase
    {
        #region Fields

        private readonly ArticleItem _item;
        private readonly IHtmlTransformer _htmlTransformer;
        private readonly IDatabaseContext _database;
        private readonly IKodesianaService _kodesiana;

        private string _tempFile;

        #endregion

        #region Properties

        public ICommand WindowClosingCommand { get; set; }

        public ICommand OpenWebCommand { get; set; }

        public ICommand OpenSourceCommand { get; set; }

        [NotifyChanged] public virtual string Title { get; set; }

        [NotifyChanged] public virtual string PageSource { get; set; }

        [NotifyChanged] public virtual bool SavedIsChecked { get; set; }

        #endregion

        #region Constructor

        public ReaderViewModel(ArticleItem item, IHtmlTransformer htmlTransformer, IDatabaseContext database,
            IKodesianaService kodesiana)
        {
            _item = item;
            _htmlTransformer = htmlTransformer;
            _database = database;
            _kodesiana = kodesiana;

            WindowClosingCommand = new RelayCommand(WindowClosing_Command);
            OpenWebCommand = new RelayCommand(OpenWeb_Command);
            OpenSourceCommand = new RelayCommand(OpenSource_Command);

            Task.Run(LoadPage);
        }

        #endregion

        #region Commands

        private void WindowClosing_Command(object obj)
        {
            Task.Run(SaveChanges);
        }

        private void OpenWeb_Command(object obj)
        {
            Helpers.OpenUrl(_item.Url, Strings.CampaignReader);
        }

        private void OpenSource_Command(object obj)
        {
            Helpers.OpenUrl(_item.SourceUrl);
        }

        #endregion

        #region Private Methods

        private async Task LoadPage()
        {
            _tempFile = Path.GetTempFileName();
            switch (_item.Item)
            {
                case Article article:
                {
                    using (var tempFile = new StreamWriter(_tempFile))
                    {
                        _database.Storage.Download(article.ArticleId.ToString(), tempFile.BaseStream);

                        PageSource = _tempFile;
                        Title = article.Title;
                        SavedIsChecked = true;
                    }

                    break;
                }

                case Post post:
                {
                    var postContent = await _kodesiana.GetPostAsync(post.Id);
                    File.WriteAllText(_tempFile, postContent);
                    _htmlTransformer.TransformHtmlForStyle(_tempFile);

                    PageSource = _tempFile;
                    Title = post.Title;
                    SavedIsChecked = false;
                    break;
                }
            }
        }

        private void SaveChanges()
        {
            // delete
            if (SavedIsChecked)
            {
                if (!(_item.Item is Post post)) return;
                if (_database.Articles.Exists(x => x.Title == post.Title)) return;

                // add series, if not already added
                if (!_database.Series.Exists(x => x.Title == post.Series))
                {
                    _database.Series.Insert(new Series {Title = post.Series});
                }

                // add article
                var item = new Article
                {
                    Date = post.Date,
                    Series = post.Series,
                    Level = post.Level,
                    Title = post.Title,
                    Url = post.Url
                };
                var id = _database.Articles.Insert(item);

                // add content
                _htmlTransformer.TransformHtmlForSave(_tempFile);
                _database.Storage.Upload(id.ToString(), _tempFile);
            }
            else
            {
                if (!(_item.Item is Article article)) return;
                _database.Articles.Delete(article.ArticleId);
                _database.Storage.Delete(article.ArticleId.ToString());
            }
        }

        #endregion
    }
}