// 
//  PROJECT  :   KFlearning
//  FILENAME :   SequenceFactory.cs
//  AUTHOR   :   Fahmi Noor Fiqri
//  WEBSITE  : https://kodesiana.com
//  REPO     : https://github.com/Kodesiana or https://github.com/fahminlb33
// 
//  This file is part of KFlearning, licensed under MIT license.
//  See this code in repository URL above!

#region

using System.Collections.Generic;
using KFlearning.Core.Services.Sequence;

#endregion

namespace KFlearning.Core.Services.Installer
{
    public class SequenceFactory : ISequenceFactory
    {
        public Queue<ITaskNode> GetInstallSequence()
        {
            var dependencies = new Queue<ITaskNode>();
            dependencies.Enqueue(new InitializeDirectoriesTask(true));
            dependencies.Enqueue(new MingwTask());
            dependencies.Enqueue(new GlutTask());
            dependencies.Enqueue(new VscodeTask());
            dependencies.Enqueue(new KflearningTask());
            dependencies.Enqueue(new KflearningShortcutTask(true));
            dependencies.Enqueue(new EnvironmentPathTask(true));

            return dependencies;
        }

        public Queue<ITaskNode> GetUninstallSequence()
        {
            var dependencies = new Queue<ITaskNode>();
            dependencies.Enqueue(new InitializeDirectoriesTask(false));
            dependencies.Enqueue(new KflearningShortcutTask(false));
            dependencies.Enqueue(new EnvironmentPathTask(false));

            return dependencies;
        }
    }
}