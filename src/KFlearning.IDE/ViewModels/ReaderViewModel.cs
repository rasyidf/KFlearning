using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using KFlearning.API;
using KFlearning.DAL;
using KFlearning.IDE.ApplicationServices;
using KFlearning.IDE.Models;
using MahApps.Metro.Controls.Dialogs;

namespace KFlearning.IDE.ViewModels
{
    public class ReaderViewModel : PropertyChangedBase, IDialog
    {
        private readonly ArticleItem _item;
        private readonly IApplicationHelpers _helpers;
        private readonly IHtmlTransformer _htmlTransformer;
        private readonly IDatabaseContext _database;
        
        #region Properties
        
        public ICommand SaveCommand { get; set; }

        public ICommand OpenWebCommand { get; set; }

        public ICommand OpenSourceCommand { get; set; }

        public ICommand WindowClosingCommand { get; set; }

        public BaseMetroDialog DialogInstance { get; set; }

        [NotifyChanged]
        public virtual string Title { get; set; }

        [NotifyChanged]
        public virtual string PageSource { get; set; }

        [NotifyChanged]
        public virtual bool SavedIsChecked { get; set; }

        #endregion

        #region Constructor

        public ReaderViewModel(ArticleItem item, IApplicationHelpers helpers, IHtmlTransformer htmlTransformer,
            IDatabaseContext database)
        {
            _item = item;
            _helpers = helpers;
            _htmlTransformer = htmlTransformer;
            _database = database;

            SaveCommand = new RelayCommand(Save_Command);
            OpenWebCommand = new RelayCommand(OpenWeb_Command);
            OpenSourceCommand = new RelayCommand(OpenSource_Command);
            WindowClosingCommand = new RelayCommand(WindowClosing_Command);

            LoadPage();
        }

        #endregion

        #region Commands
        
        private void Save_Command(object obj)
        {
            throw new System.NotImplementedException();
        }

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
                    PageSource = article.Content.HtmlBody;
                    break;
                case Post post:
                    PageSource = post.Content;
                    break;
            }
        }

        private void SaveChanges()
        {
            if (!SavedIsChecked && _item.Item is Article article)
            {
                _database.Articles.Remove(article);
            }

            if (!SavedIsChecked || !(_item.Item is Post post)) return;

            // add series
            Series series;
            if ((series = _database.Series.FirstOrDefault(x => x.Title == post.Title)) == null)
            {
                series = new Series {Title = post.Series};
                _database.Series.Add(series);
            }
            
            // add article
            var item = new Article
            {
                Date = post.Date,
                ServerId = post.Id,
                Series = series,
                Level = post.Level,
                Title = post.Title,
                Url = post.Url,
                Content = new ArticleContent
                {
                    HtmlBody = _htmlTransformer.TransformHtml(post.Content)
                }
            };
            _database.Articles.Add(item);
        }

        #endregion
    }
}
