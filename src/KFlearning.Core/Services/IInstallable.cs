// 
//  PROJECT  :   KFlearning
//  FILENAME :   IInstallable.cs
//  AUTHOR   :   Fahmi Noor Fiqri
//  WEBSITE  : https://kodesiana.com
//  REPO     : https://github.com/Kodesiana or https://github.com/fahminlb33
// 
//  This file is part of KFlearning, licensed under MIT license.
//  See this code in repository URL above!

using System;
using System.Threading;

namespace KFlearning.Core.Services
{
    public interface IInstallable
    {
        void Install(Action<int> progressCallback, CancellationToken cancellation);
        void Uninstall(Action<int> progressCallback, CancellationToken cancellation);
    }
}