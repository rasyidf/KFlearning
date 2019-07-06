// 
//  PROJECT  :   KFlearning
//  FILENAME :   IApplicationHelpers.cs
//  AUTHOR   :   Fahmi Noor Fiqri
//  NPM      :   065118116
// 
//  This file is part of KFlearning, licensed under MIT license.

using System;
using System.Threading.Tasks;
using KFlearning.IDE.Models;
using MahApps.Metro.Controls.Dialogs;

namespace KFlearning.IDE.ApplicationServices
{
    public interface IApplicationHelpers
    {
        void OpenUrl(string url);

        void ShowReaderWindow(ArticleItem item);
        Task<DialogResultState> CreateNewProjectDialog();
        Task<ProgressDialogController> CreateProgressDialog(string title, string message);
        Task<MessageDialogResult> CreateMessageDialog(string title, string message,
            MessageDialogStyle style = MessageDialogStyle.Affirmative);
    }
}