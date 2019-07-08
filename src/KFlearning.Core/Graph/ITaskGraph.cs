// 
//  PROJECT  :   KFlearning
//  FILENAME :   ITaskGraph.cs
//  AUTHOR   :   Fahmi Noor Fiqri
//  NPM      :   065118116
// 
//  This file is part of KFlearning, licensed under MIT license.

using KFlearning.Core.IO;

namespace KFlearning.Core.Graph
{
    public interface ITaskGraph
    {
        void RunGraph(InstallerDefinition definition, ITaskNode rootNode);
        void Cancel();
    }
}