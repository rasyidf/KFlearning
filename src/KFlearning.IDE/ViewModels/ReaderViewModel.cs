// 
//  PROJECT  :   KFlearning
//  FILENAME :   ReaderViewModel.cs
//  AUTHOR   :   Fahmi Noor Fiqri
//  NPM      :   065118116
// 
//  This file is part of KFlearning, licensed under MIT license.

using System.Threading.Tasks;
using System.Windows.Input;
using KFlearning.API;
using KFlearning.Core.Entities;
using KFlearning.IDE.ApplicationServices;
using KFlearning.IDE.Models;
using MahApps.Metro.Controls.Dialogs;

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

            OpenWebCommand = new RelayCommand(OpenWeb_Command);
            OpenSourceCommand = new RelayCommand(OpenSource_Command);
            WindowClosingCommand = new RelayCommand(WindowClosing_Command);

            LoadPage();
        }

        #endregion

        #region Fields

        private readonly ArticleItem _item;
        private readonly IApplicationHelpers _helpers;
        private readonly IHtmlTransformer _htmlTransformer;
        private readonly IDatabaseContext _database;

        #endregion

        #region Properties

        public ICommand OpenWebCommand { get; set; }

        public ICommand OpenSourceCommand { get; set; }

        public ICommand WindowClosingCommand { get; set; }

        [NotifyChanged] public virtual string Title { get; set; }

        [NotifyChanged] public virtual string PageSource { get; set; }

        [NotifyChanged] public virtual bool SavedIsChecked { get; set; }

        #endregion

        #region Commands

        private void OpenWeb_Command(object obj)
        {
            _helpers.OpenUrl(_item.Url);
        }

        private void OpenSource_Command(object obj)
        {
            throw new System.NotImplementedException();
        }

        private void WindowClosing_Command(object obj)
        {
            Task.Run(SaveChanges);
        }

        #endregion

        #region Private Methods

        private void LoadPage()
        {
            switch (_item.Item)
            {
                case Article article:
                    PageSource = _database.Contents.FindOne(x => x.ArticleId == article.ArticleId).HtmlBody;
                    break;
                case Post post:
                    PageSource = post.Content;
                    break;
            }
        }

        private void SaveChanges()
        {
            // delete
            if (SavedIsChecked)
            {
                var post = (Post) _item.Item;

                // add series, if not already added
                if (!_database.Series.Exists(x => x.Title == post.Title))
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
                var content = new Content
                {
                    ArticleId = id,
                    HtmlBody = _htmlTransformer.TransformHtml(post.Content)
                };
                _database.Contents.Insert(content);
            }
            else
            {
                if (_item.Item is Article article)
                {
                    _database.Articles.Delete(article.ArticleId);
                }
            }
        }

        #endregion
    }
}