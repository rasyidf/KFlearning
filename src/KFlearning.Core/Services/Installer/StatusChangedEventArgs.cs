// 
//  PROJECT  :   KFlearning
//  FILENAME :   StatusChangedEventArgs.cs
//  AUTHOR   :   Fahmi Noor Fiqri
//  WEBSITE  : https://kodesiana.com
//  REPO     : https://github.com/Kodesiana or https://github.com/fahminlb33
// 
//  This file is part of KFlearning, licensed under MIT license.
//  See this code in repository URL above!

#region

using System;

#endregion

namespace KFlearning.Core.Services.Installer
{
    public class StatusChangedEventArgs : EventArgs
    {
        public DateTime Timestamp { get; }

        public string Message { get; }

        public StatusChangedEventArgs(DateTime timestamp, string message)
        {
            Timestamp = timestamp;
            Message = message;
        }
    }
}