﻿using System.Collections.Generic;
using System.Threading;
using KFlearning.Core.IO;

namespace KFlearning.Core.Graph
{
    public class InstallGraph : ITaskNode
    {
        public string TaskName => "Install graph root";
        public bool HasDependencies => true;
        public Queue<ITaskNode> Dependencies { get; }

        public InstallGraph(MingwTask mingw, MariaDbTask mariaDb, VscodeTask vscode, InitializeDirectoriesTask initializeDirectories)
        {
            Dependencies = new Queue<ITaskNode>();
            Dependencies.Enqueue(initializeDirectories);
            Dependencies.Enqueue(mingw);
            // apache
            // php
            Dependencies.Enqueue(mariaDb);
            Dependencies.Enqueue(vscode);
            // kflearning
        }
        
        public void Configure(InstallerDefinition definition)
        {
        }

        public bool Run(CancellationToken cancellation)
        {
            return true;
        }
    }
}
