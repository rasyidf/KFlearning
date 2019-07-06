// 
//  PROJECT  :   KFlearning
//  FILENAME :   ApplicationHelpers.cs
//  AUTHOR   :   Fahmi Noor Fiqri
//  NPM      :   065118116
// 
//  This file is part of KFlearning, licensed under MIT license.

using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;
using System.Windows;
using Castle.MicroKernel;
using KFlearning.IDE.Models;
using KFlearning.IDE.ViewModels;
using KFlearning.IDE.Views;
using MahApps.Metro.Controls;
using MahApps.Metro.Controls.Dialogs;

namespace KFlearning.IDE.ApplicationServices
{
    public class ApplicationHelpers : IApplicationHelpers
    {
        public void OpenUrl(string url)
        {
            Process.Start(url);
        }

        public async Task<DialogResultState> CreateNewProjectDialog()
        {
            var window = (MetroWindow) Application.Current.MainWindow;
            var dialog = new CustomDialog();
            var view = App.Container.Resolve<CreateProjectView>();
            var vm = (IDialog) view.DataContext;
            vm.DialogInstance = dialog;
            dialog.Content = view;

            await window.ShowMetroDialogAsync(dialog);
            await dialog.WaitUntilUnloadedAsync();

            return new DialogResultState(vm.DialogResult, vm.State);
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