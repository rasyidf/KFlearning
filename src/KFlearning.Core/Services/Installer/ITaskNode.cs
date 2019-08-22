// 
//  PROJECT  :   KFlearning
//  FILENAME :   ITaskNode.cs
//  AUTHOR   :   Fahmi Noor Fiqri
//  WEBSITE  : https://kodesiana.com
//  REPO     : https://github.com/Kodesiana or https://github.com/fahminlb33
// 
//  This file is part of KFlearning, licensed under MIT license.
//  See this code in repository URL above!

#region

using System.Threading;

#endregion

namespace KFlearning.Core.Services.Installer
{
    public interface ITaskNode
    {
        string TaskName { get; }

        void Run(InstallDefinition definition, CancellationToken cancellation);
    }
}