// 
//  PROJECT  :   KFlearning
//  FILENAME :   ProgressBroker.cs
//  AUTHOR   :   Fahmi Noor Fiqri
//  NPM      :   065118116
// 
//  This file is part of KFlearning, licensed under MIT license.

using System;
using KFlearning.Core.Services;

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