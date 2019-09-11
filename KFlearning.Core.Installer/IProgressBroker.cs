// 
//  PROJECT  :   KFlearning
//  FILENAME :   IProgressBroker.cs
//  AUTHOR   :   Fahmi Noor Fiqri
//  WEBSITE  : https://kodesiana.com
//  REPO     : https://github.com/Kodesiana or https://github.com/fahminlb33
// 
//  This file is part of KFlearning, licensed under MIT license.
//  See this code in repository URL above!

namespace KFlearning.Core.Installer
{
    public interface IProgressBroker
    {
        void ReportMessage(string message);
        void ReportNodeProgress(int progressPercentage);
        void ReportSequenceProgress(int progressPercentage);
    }
}