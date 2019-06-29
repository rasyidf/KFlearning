using System;
using System.Windows.Controls;
using MahApps.Metro.Controls.Dialogs;

namespace KFlearning.IDE.ApplicationServices
{
    public interface IApplicationHelpers
    {
        void OpenUrl(string url);
        void OpenUrl(Uri url);
        BaseMetroDialog CreateDialog<T>() where T : UserControl;
    }
}