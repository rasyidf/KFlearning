// 
//  PROJECT  :   KFlearning
//  FILENAME :   ApplicationHelpers.cs
//  AUTHOR   :   Fahmi Noor Fiqri
//  WEBSITE  : https://kodesiana.com
//  REPO     : https://github.com/Kodesiana or https://github.com/fahminlb33
// 
//  This file is part of KFlearning, licensed under MIT license.
//  See this code in repository URL above!

#region

using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;
using System.Windows;
using Castle.MicroKernel;
using KFlearning.IDE.Models;
using KFlearning.IDE.Resources;
using KFlearning.IDE.ViewModels;
using KFlearning.IDE.Views;
using MahApps.Metro.Controls;
using MahApps.Metro.Controls.Dialogs;

#endregion

namespace KFlearning.IDE.ApplicationServices
{
    public class ApplicationHelpers : IApplicationHelpers
    {
        public void OpenUrl(string url)
        {
            Process.Start(url);
        }

        public void OpenUrl(string url, string campaign)
        {
            OpenUrl(url + Strings.AnalyticsCampaignQuery + campaign);
        }

        public async Task<string> CreateNewProjectDialog()
        {
            var window = (MetroWindow) Application.Current.MainWindow;
            return await window.ShowInputAsync("Buat Project baru", "Nama project");
        }

        public void ShowReaderWindow(ArticleItem item)
        {
            var args = new Dictionary<string, object>
            {
                {"item", item}
            };
            var view = App.Container.Resolve<ReaderView>();
            view.DataContext = App.Container.Resolve<ReaderViewModel>(Arguments.FromNamed(args));

            view.Show();
        }

        public async Task<ProgressDialogController> CreateProgressDialog(string title, string message)
        {
            var window = (MetroWindow) Application.Current.MainWindow;
            var controller = await window.ShowProgressAsync(title, message);
            controller.SetIndeterminate();
            return controller;
        }

        public async Task<MessageDialogResult> CreateMessageDialog(string title, string message,
            MessageDialogStyle style = MessageDialogStyle.Affirmative)
        {
            var window = (MetroWindow) Application.Current.MainWindow;
            return await window.ShowMessageAsync(title, message, style);
        }
    }
}