// 
//  PROJECT  :   KFlearning
//  FILENAME :   IApplicationHelpers.cs
//  AUTHOR   :   Fahmi Noor Fiqri
//  WEBSITE  : https://kodesiana.com
//  REPO     : https://github.com/Kodesiana or https://github.com/fahminlb33
// 
//  This file is part of KFlearning, licensed under MIT license.
//  See this code in repository URL above!

#region

using System.Threading.Tasks;
using KFlearning.IDE.Models;
using MahApps.Metro.Controls.Dialogs;

#endregion

namespace KFlearning.IDE.ApplicationServices
{
    public interface IApplicationHelpers
    {
        void OpenUrl(string url);
        void OpenUrl(string url, string campaign);

        void ShowReaderWindow(ArticleItem item);
        Task<string> CreateNewProjectDialog();
        Task<ProgressDialogController> CreateProgressDialog(string title, string message);

        Task<MessageDialogResult> CreateMessageDialog(string title, string message,
            MessageDialogStyle style = MessageDialogStyle.Affirmative);
    }
}