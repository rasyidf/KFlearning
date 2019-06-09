using System.Windows.Input;
using KFlearning.ApplicationServices;

namespace KFlearning.IDE.ViewModels
{
    public class ReaderViewModel : ViewModelBase
    {
        public ICommand SaveCommand { get; set; }

        public ICommand PrintCommand { get; set; }

        public ICommand OpenWebCommand { get; set; }

        public ICommand OpenSourceCommand { get; set; }

        [NotifyChanged]
        public virtual string Title { get; set; }

        public ReaderViewModel()
        {
            
        }
    }
}
