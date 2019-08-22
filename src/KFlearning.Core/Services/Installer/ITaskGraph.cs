// 
//  PROJECT  :   KFlearning
//  FILENAME :   ITaskGraph.cs
//  AUTHOR   :   Fahmi Noor Fiqri
//  NPM      :   065118116
// 
//  This file is part of KFlearning, licensed under MIT license.

using System.Collections.Generic;

namespace KFlearning.Core.Services.Installer
{
    public interface ITaskGraph
    {
        bool IsInstalled();
        void RunSequence(InstallDefinition definition, Queue<ITaskNode> sequence);
        void Cancel();
    }
}