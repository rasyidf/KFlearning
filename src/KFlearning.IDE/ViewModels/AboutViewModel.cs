// 
//  PROJECT  :   KFlearning
//  FILENAME :   AboutViewModel.cs
//  AUTHOR   :   Fahmi Noor Fiqri
//  WEBSITE  : https://kodesiana.com
//  REPO     : https://github.com/Kodesiana or https://github.com/fahminlb33
// 
//  This file is part of KFlearning, licensed under MIT license.
//  See this code in repository URL above!

#region

using System.Windows.Input;
using KFlearning.IDE.ApplicationServices;
using KFlearning.IDE.Resources;

#endregion

namespace KFlearning.IDE.ViewModels
{
    public class AboutViewModel : PropertyChangedBase
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
            WebCommand = new RelayCommand(x => helpers.OpenUrl(Strings.WebUrl, Strings.CampaignAbout));
            GitHubCommand = new RelayCommand(x => helpers.OpenUrl(Strings.GitHubUrl));
            TwitterCommand = new RelayCommand(x => helpers.OpenUrl(Strings.TwitterUrl));
            InstagramCommand = new RelayCommand(x => helpers.OpenUrl(Strings.InstagramUrl));
        }

        #endregion
    }
}