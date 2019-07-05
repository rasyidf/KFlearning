// 
//  PROJECT  :   KFlearning
//  FILENAME :   WebServerViewModel.cs
//  AUTHOR   :   Fahmi Noor Fiqri
//  NPM      :   065118116
// 
//  This file is part of KFlearning, licensed under MIT license.

using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Threading.Tasks;
using System.Windows.Input;
using KFlearning.IDE.ApplicationServices;
using KFlearning.IDE.Models;

namespace KFlearning.IDE.ViewModels
{
    public class WebServerViewModel : PropertyChangedBase
    {
        public WebServerViewModel()
        {
            Task.Run(LoadData);
        }

        public ICommand ServerCommand { get; set; }

        public ICommand PhpInfoCommand { get; set; }

        public ICommand PhpMyAdminCommand { get; set; }

        public ICommand WorkspaceCommand { get; set; }

        [NotifyChanged] public virtual bool ServerIsChecked { get; set; }

        [NotifyChanged] public virtual bool ServerIsEnabled { get; set; }

        [NotifyChanged] public virtual ObservableCollection<ServerLogItem> Logs { get; set; }

        private void LoadData()
        {
            Logs = new ObservableCollection<ServerLogItem>
            {
                new ServerLogItem(DateTime.Now, "Server siap."),
                new ServerLogItem(DateTime.Now.AddMinutes(20), "Server aktif.")
            };
        }
    }
}