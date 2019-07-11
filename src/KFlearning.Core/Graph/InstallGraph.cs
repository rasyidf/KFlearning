using System.Collections.Generic;
using System.Threading;
using KFlearning.Core.IO;

namespace KFlearning.Core.Graph
{
    public class InstallGraph : ITaskNode
    {
        #region ITaskNode Properties
        
        public string TaskName => "Install graph root";
        public bool HasDependencies => true;
        public Queue<ITaskNode> Dependencies { get; private set; }

        #endregion

        #region ITaskNode Methods

        public void Configure(InstallerDefinition definition)
        {
            Dependencies = new Queue<ITaskNode>();
            Dependencies.Enqueue(definition.ResolveService<InitializeDirectoriesTask>());
            Dependencies.Enqueue(definition.ResolveService<MingwTask>());
            //Dependencies.Enqueue(definition.ResolveService<InitializeDirectoriesTask>()); - apache
            //Dependencies.Enqueue(definition.ResolveService<InitializeDirectoriesTask>()); - php
            Dependencies.Enqueue(definition.ResolveService<MariaDbTask>());
            Dependencies.Enqueue(definition.ResolveService<VscodeTask>());
            Dependencies.Enqueue(definition.ResolveService<KflearningTask>());
        }

        public bool Run(CancellationToken cancellation)
        {
            return true;
        } 

        #endregion
    }
}
