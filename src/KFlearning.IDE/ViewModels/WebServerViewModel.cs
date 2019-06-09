using System;
using System.Collections.ObjectModel;
using System.Windows.Input;
using KFlearning.ApplicationServices;
using KFlearning.ApplicationServices.Models;
using KFlearning.IDE.Models;

namespace KFlearning.IDE.ViewModels
{
    public class WebServerViewModel : ViewModelBase
    {
        public ICommand ServerCommand { get; set; }

        public ICommand PhpInfoCommand { get; set; }

        public ICommand PhpMyAdminCommand { get; set; }

        public ICommand WorkspaceCommand { get; set; }

        [NotifyChanged]
        public virtual bool ServerIsChecked { get; set; }

        [NotifyChanged]
        public virtual bool ServerIsEnabled { get; set; }

        [NotifyChanged]
        public virtual ObservableCollection<ServerLogItem> Logs { get; set; }

        public WebServerViewModel()
        {
        }

        protected override void OnBootstrap(AppEventArgs e)
        {
            Logs = new ObservableCollection<ServerLogItem>
            {
                new ServerLogItem(DateTime.Now, "Server siap."),
                new ServerLogItem(DateTime.Now.AddMinutes(20), "Server aktif.")
            };
        }
    }
}
