﻿// 
//  PROJECT  :   KFlearning
//  FILENAME :   WebServerViewModel.cs
//  AUTHOR   :   Fahmi Noor Fiqri
//  WEBSITE  : https://kodesiana.com
//  REPO     : https://github.com/Kodesiana or https://github.com/fahminlb33
// 
//  This file is part of KFlearning, licensed under MIT license.
//  See this code in repository URL above!

#region

using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows.Input;
using KFlearning.Core.IO;
using KFlearning.Core.Services;
using KFlearning.Core.Services.Installer;
using KFlearning.IDE.ApplicationServices;
using KFlearning.IDE.Models;
using KFlearning.IDE.Resources;

#endregion

namespace KFlearning.IDE.ViewModels
{
    public class WebServerViewModel : PropertyChangedBase
    {
        #region Fields

        private readonly IWebServer _webServer;
        private readonly IPathManager _pathManager;

        #endregion

        #region Properties

        public ICommand ServerCommand { get; set; }

        public ICommand PhpInfoCommand { get; set; }

        public ICommand PhpMyAdminCommand { get; set; }

        public ICommand WorkspaceCommand { get; set; }

        [NotifyChanged] public virtual bool ServerIsChecked { get; set; }

        [NotifyChanged] public virtual bool ServerIsEnabled { get; set; }

        [NotifyChanged] public virtual ObservableCollection<ServerLogItem> Logs { get; set; }

        #endregion

        #region Constructor

        public WebServerViewModel(IWebServer webServer, IPathManager pathManager)
        {
            _pathManager = pathManager;
            _webServer = webServer;
            _webServer.RunningStatusChanged += WebServer_RunningStatusChanged;
            _webServer.StatusUpdate += WebServer_StatusUpdate;

            ServerCommand = new RelayCommand(Server_Command);
            PhpInfoCommand = new RelayCommand(PhpInfo_Command);
            PhpMyAdminCommand = new RelayCommand(PhpMyAdmin_Command);
            WorkspaceCommand = new RelayCommand(Workspace_Command);

            Task.Run(LoadData);
        }

        #endregion

        #region Commands

        private void Workspace_Command(object obj)
        {
            _pathManager.LaunchExplorer(_pathManager.GetPath(PathKind.PathReposRoot));
        }

        private void PhpMyAdmin_Command(object obj)
        {
            _pathManager.LaunchUri("http://localhost/phpmyadmin");
        }

        private void PhpInfo_Command(object obj)
        {
            _pathManager.LaunchUri("http://localhost/phpinfo.php");
        }

        private void Server_Command(object obj)
        {
            ServerIsEnabled = false;
            if (_webServer.IsRunning)
            {
                _webServer.Stop();
            }
            else
            {
                _webServer.Start();
            }
        }

        #endregion

        #region Private Methods

        private void LoadData()
        {
            Logs = new ObservableCollection<ServerLogItem>
            {
                new ServerLogItem(DateTime.Now, Texts.WebServerStatusReady)
            };
        }

        private void WebServer_StatusUpdate(object sender, StatusChangedEventArgs e)
        {
            Synchronization.Invoke(() => Logs.Add(new ServerLogItem(e.Timestamp, e.Message)));
        }

        private void WebServer_RunningStatusChanged(object sender, EventArgs e)
        {
            ServerIsEnabled = true;
            ServerIsChecked = _webServer.IsRunning;
        }

        #endregion
    }
}