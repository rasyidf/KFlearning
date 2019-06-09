using System.Windows.Input;
using KFlearning.ApplicationServices;

namespace KFlearning.IDE.ViewModels
{
    public class AboutViewModel : ViewModelBase
    {
        #region Properties

        public ICommand WebCommand { get; set; }

        public ICommand GitHubCommand { get; set; }
        
        public ICommand TwitterCommand { get; set; }

        public ICommand InstagramCommand { get; set; }

        #endregion

        #region Constructor

        public AboutViewModel(IApplicationHelpers helpers)
        {
            WebCommand = new RelayCommand(x => helpers.OpenUrl(Strings.WebUrl));
            GitHubCommand = new RelayCommand(x => helpers.OpenUrl(Strings.GitHubUrl));
            TwitterCommand = new RelayCommand(x => helpers.OpenUrl(Strings.TwitterUrl));
            InstagramCommand = new RelayCommand(x => helpers.OpenUrl(Strings.InstagramUrl));
        } 

        #endregion
    }
}
