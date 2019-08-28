using System;
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

namespace KFlearning.IDE.ApplicationServices
{
    public static class Helpers
    {
        // -- URLs

        public static void OpenUrl(string url)
        {
            Process.Start(url);
        }

        public static void OpenUrl(string url, string campaign)
        {
            OpenUrl(url + Strings.AnalyticsCampaignQuery + campaign);
        }

        // -- Dialogs

        public static void ShowReaderWindow(ArticleItem item)
        {
            var args = new Dictionary<string, object>
            {
                {"item", item}
            };
            var view = App.Container.Resolve<ReaderView>();
            view.DataContext = App.Container.Resolve<ReaderViewModel>(Arguments.FromNamed(args));

            view.Show();
        }

        public static async Task<string> CreateNewProjectDialog()
        {
            var window = (MetroWindow) Application.Current.MainWindow;
            return await window.ShowInputAsync(Texts.NewProjectTitle, Texts.NewProjectName);
        }

        public static async Task<ProgressDialogController> CreateProgressDialog(string title, string message)
        {
            var window = (MetroWindow) Application.Current.MainWindow;
            var controller = await window.ShowProgressAsync(title, message);
            controller.SetIndeterminate();
            return controller;
        }

        public static async Task<MessageDialogResult> CreateMessageDialog(string title, string message,
            MessageDialogStyle style = MessageDialogStyle.Affirmative)
        {
            var window = (MetroWindow) Application.Current.MainWindow;
            return await window.ShowMessageAsync(title, message, style);
        }

        // -- Pagination 

        public static int CalculateTotalPage(int total, int size)
        {
            return (int) Math.Ceiling(decimal.Divide(total, size));
        }

        public static int CalculatePage(int offset, int size)
        {
            var page = offset / size;
            return page < 1 ? 1 : page;
        }
    }
}