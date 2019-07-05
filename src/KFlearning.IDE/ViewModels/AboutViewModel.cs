// 
//  PROJECT  :   KFlearning
//  FILENAME :   AboutViewModel.cs
//  AUTHOR   :   Fahmi Noor Fiqri
//  NPM      :   065118116
// 
//  This file is part of KFlearning, licensed under MIT license.

using System.Windows.Input;
using KFlearning.IDE.ApplicationServices;
using KFlearning.IDE.Resources;

namespace KFlearning.IDE.ViewModels
{
    public class AboutViewModel : PropertyChangedBase
    {
        #region Constructor

        public AboutViewModel(IApplicationHelpers helpers)
        {
            WebCommand = new RelayCommand(x => helpers.OpenUrl(Strings.WebUrl));
            GitHubCommand = new RelayCommand(x => helpers.OpenUrl(Strings.GitHubUrl));
            TwitterCommand = new RelayCommand(x => helpers.OpenUrl(Strings.TwitterUrl));
            InstagramCommand = new RelayCommand(x => helpers.OpenUrl(Strings.InstagramUrl));
        }

        #endregion

        #region Properties

        public ICommand WebCommand { get; set; }

        public ICommand GitHubCommand { get; set; }

        public ICommand TwitterCommand { get; set; }

        public ICommand InstagramCommand { get; set; }

        #endregion
    }
}