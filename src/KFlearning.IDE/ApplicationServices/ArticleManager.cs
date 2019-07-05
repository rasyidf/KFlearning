using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using KFlearning.API;
using KFlearning.Core.Entities;
using KFlearning.IDE.Models;

namespace KFlearning.IDE.ApplicationServices
{
    public class ArticleManager : IArticleManager
    {
        #region Fields

        private readonly IDatabaseContext _database;
        private readonly IKodesianaService _kodesiana;

        #endregion

        #region Properties
        
        public bool Online { get; set; }

        #endregion

        #region Constructor
        
        public ArticleManager(IKodesianaService kodesiana, IDatabaseContext database)
        {
            _kodesiana = kodesiana;
            _database = database;
        }

        #endregion

        #region Public Methods

        public async Task<IEnumerable<SeriesItem>> GetSeries()
        {
            if (!Online)
            {
                return _database.Series.FindAll().Select(x => new SeriesItem(x.Title));
            }

            var items = await _kodesiana.GetSeriesAsync();
            return items.Select(x => new SeriesItem(x)).ToList();
        }

        public async Task<IEnumerable<ArticleItem>> GetArticles(SeriesItem series)
        {
            if (!Online)
            {
                return _database.Articles.Find(x => x.Title == series.Title)
                    .Select(x => new ArticleItem(x)).ToList();
            }

            var items = await _kodesiana.GetPostsAsync(series.Title);
            return items.Select(x => new ArticleItem(x)).ToList();
        }

        public async Task<IEnumerable<ArticleItem>> FindArticles(string title, SeriesItem series)
        {
            if (!Online)
            {
                return _database.Articles.Find(x => series.Title == x.Series && x.Title.Contains(title))
                    .Select(x => new ArticleItem(x)).ToList();
            }

            var result = await _kodesiana.FindPostAsync(title, series.Title);
            return result.Select(x => new ArticleItem(x)).ToList();
        } 

        #endregion
    }
}
