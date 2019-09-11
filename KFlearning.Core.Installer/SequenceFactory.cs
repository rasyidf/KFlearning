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

using System;
using System.Collections.Generic;
using KFlearning.Core.Installer.Sequence;

#endregion

namespace KFlearning.Core.Installer
{
    public class SequenceFactory : ISequenceFactory
    {
        public Queue<ITaskNode> GetInstallSequence(Func<Type, object> resolver)
        {
            var dependencies = new Queue<ITaskNode>();
            dependencies.Enqueue((ITaskNode) resolver(typeof(InitializeDirectoriesTask)));
            dependencies.Enqueue((ITaskNode) resolver(typeof(MingwTask)));
            dependencies.Enqueue((ITaskNode) resolver(typeof(GlutTask)));
            dependencies.Enqueue((ITaskNode) resolver(typeof(VscodeTask)));
            dependencies.Enqueue((ITaskNode) resolver(typeof(KflearningTask)));
            dependencies.Enqueue((ITaskNode) resolver(typeof(KflearningShortcutTask)));
            dependencies.Enqueue((ITaskNode) resolver(typeof(EnvironmentPathTask)));
            
            return dependencies;
        }

        public Queue<ITaskNode> GetUninstallSequence(Func<Type, object> resolver)
        {
            var dependencies = new Queue<ITaskNode>();
            dependencies.Enqueue((ITaskNode) resolver(typeof(InitializeDirectoriesTask)));
            dependencies.Enqueue((ITaskNode) resolver(typeof(KflearningShortcutTask)));
            dependencies.Enqueue((ITaskNode) resolver(typeof(EnvironmentPathTask)));

            return dependencies;
        }
    }
}