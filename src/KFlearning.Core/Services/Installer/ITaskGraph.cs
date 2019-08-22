// 
//  PROJECT  :   KFlearning
//  FILENAME :   ITaskGraph.cs
//  AUTHOR   :   Fahmi Noor Fiqri
//  WEBSITE  : https://kodesiana.com
//  REPO     : https://github.com/Kodesiana or https://github.com/fahminlb33
// 
//  This file is part of KFlearning, licensed under MIT license.
//  See this code in repository URL above!

#region

using System.Collections.Generic;

#endregion

namespace KFlearning.Core.Services.Installer
{
    public interface ITaskGraph
    {
        bool IsInstalled();
        void RunSequence(InstallDefinition definition, Queue<ITaskNode> sequence);
        void Cancel();
    }
}