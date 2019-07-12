// 
//  PROJECT  :   KFlearning
//  FILENAME :   ProgressBroker.cs
//  AUTHOR   :   Fahmi Noor Fiqri
//  NPM      :   065118116
// 
//  This file is part of KFlearning, licensed under MIT license.

using System;
using KFlearning.Core.Services.Graph;

namespace KFlearning.Installer.Views
{
    internal class ProgressBroker : IProgressBroker
    {
        public Action<string> MessageAction { get; set; }
        public Action<int> ProgressAction { get; set; }

        public void ReportMessage(string message)
        {
            MessageAction?.Invoke(message);
        }

        public void ReportProgress(int progressPercentage)
        {
            ProgressAction?.Invoke(progressPercentage);
        }
    }
}