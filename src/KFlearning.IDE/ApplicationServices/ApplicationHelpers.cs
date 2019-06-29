using System;
using System.Diagnostics;
using System.Windows.Controls;
using MahApps.Metro.Controls.Dialogs;

namespace KFlearning.IDE.ApplicationServices
{
    public class ApplicationHelpers : IApplicationHelpers
    {
        public void OpenUrl(string url)
        {
            Process.Start(url);
        }

        public void OpenUrl(Uri url)
        {
            OpenUrl(url.ToString());
        }

        public BaseMetroDialog CreateDialog<T>() where T : UserControl
        {
            var dialog = new CustomDialog();
            var view = App.Container.Resolve<T>();
            ((IDialog) view.DataContext).DialogInstance = dialog;
            dialog.Content = view;

            return dialog;
        }
    }
}
