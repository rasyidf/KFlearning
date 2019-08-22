// 
//  PROJECT  :   KFlearning
//  FILENAME :   ProgressBroker.cs
//  AUTHOR   :   Fahmi Noor Fiqri
//  WEBSITE  : https://kodesiana.com
//  REPO     : https://github.com/Kodesiana or https://github.com/fahminlb33
// 
//  This file is part of KFlearning, licensed under MIT license.
//  See this code in repository URL above!

#region

using System;
using KFlearning.Core.Services.Installer;

#endregion

namespace KFlearning.Installer.ApplicationServices
{
    internal class ProgressBroker : IProgressBroker
    {
        public Action<string> MessageAction { get; set; }
        public Action<int> ProgressCurrentAction { get; set; }
        public Action<int> ProgressOverallAction { get; set; }

        public void ReportMessage(string message)
        {
            MessageAction?.Invoke(message);
        }

        public void ReportNodeProgress(int progressPercentage)
        {
            ProgressCurrentAction?.Invoke(progressPercentage);
        }

        public void ReportSequenceProgress(int progressPercentage)
        {
            ProgressOverallAction?.Invoke(progressPercentage);
        }
    }
}