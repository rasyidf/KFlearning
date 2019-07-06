// 
//  PROJECT  :   KFlearning
//  FILENAME :   ReaderViewModel.cs
//  AUTHOR   :   Fahmi Noor Fiqri
//  NPM      :   065118116
// 
//  This file is part of KFlearning, licensed under MIT license.

using System.IO;
using System.Threading.Tasks;
using System.Windows.Input;
using KFlearning.Core.API;
using KFlearning.Core.Entities;
using KFlearning.IDE.ApplicationServices;
using KFlearning.IDE.Models;
using KFlearning.IDE.Resources;

namespace KFlearning.IDE.ViewModels
{
    public class ReaderViewModel : PropertyChangedBase
    {
        #region Constructor

        public ReaderViewModel(ArticleItem item, IApplicationHelpers helpers, IHtmlTransformer htmlTransformer,
            IDatabaseContext database)
        {
            _item = item;
            _helpers = helpers;
            _htmlTransformer = htmlTransformer;
            _database = database;

            WindowClosingCommand = new RelayCommand(WindowClosing_Command);
            OpenWebCommand = new RelayCommand(OpenWeb_Command);
            OpenSourceCommand = new RelayCommand(OpenSource_Command);

            LoadPage();
        }
        
        #endregion

        #region Fields

        private readonly ArticleItem _item;
        private readonly IApplicationHelpers _helpers;
        private readonly IHtmlTransformer _htmlTransformer;
        private readonly IDatabaseContext _database;
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

        #region Commands

        private void WindowClosing_Command(object obj)
        {
            Task.Run(SaveChanges);
        }

        private void OpenWeb_Command(object obj)
        {
            _helpers.OpenUrl(_item.Url, Strings.CampaignReader);
        }

        private void OpenSource_Command(object obj)
        {
            throw new System.NotImplementedException();
        }
        
        #endregion

        #region Private Methods

        private void LoadPage()
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
                    File.WriteAllText(_tempFile, post.Content);
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
                    Url = post.Url,
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