using System;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Input;
using KFlearning.ApplicationServices;
using KFlearning.ApplicationServices.Models;
using KFlearning.IDE.Models;

namespace KFlearning.IDE.ViewModels
{
    public class ArticleViewModel : ViewModelBase
    {
        public ICommand RefreshCommand { get; set; }

        public ICommand SearchCommand { get; set; }

        [NotifyChanged]
        public virtual ObservableCollection<ArticleItem> Articles { get; set; }

        [NotifyChanged]
        public virtual ObservableCollection<CategoryItem> Categories { get; set; }

        [NotifyChanged]
        public virtual CategoryItem SelectedCategory { get; set; }

        [NotifyChanged]
        public virtual bool LoadingIsActive { get; set; }

        protected override void OnBootstrap(AppEventArgs e)
        {
            Articles = new ObservableCollection<ArticleItem>
            {
                new ArticleItem(11, "Intro to C++", DateTime.Now, "C++", 1),
                new ArticleItem(11, "Intro to Python", DateTime.Now, "Python", 1)
            }; 
            Categories=new ObservableCollection<CategoryItem>
            {
                new CategoryItem(1, "C++"),
                new CategoryItem(1, "C++ Tutorial"),
            };
            SelectedCategory = Categories[0];
        }
    }
}
