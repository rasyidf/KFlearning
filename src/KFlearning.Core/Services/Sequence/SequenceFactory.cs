using System.Collections.Generic;
using KFlearning.Core.Services.Sequence.Impl;

namespace KFlearning.Core.Services.Sequence
{
    public class SequenceFactory
    {
        public Queue<ITaskNode> GetInstallGraph()
        {
            var dependencies = new Queue<ITaskNode>();
            dependencies.Enqueue(new InitializeDirectoriesTask(true));
            dependencies.Enqueue(new MingwTask());
            dependencies.Enqueue(new GlutTask());
            dependencies.Enqueue(new PhpTask());
            dependencies.Enqueue(new ComposerTask());
            dependencies.Enqueue(new HttpdTask());
            dependencies.Enqueue(new PhpMyAdminTask());
            dependencies.Enqueue(new MariaDbTask());
            dependencies.Enqueue(new VscodeTask());
            dependencies.Enqueue(new KflearningTask());
            dependencies.Enqueue(new EnvironmentPathTask(true));

            return dependencies;
        }

        public Queue<ITaskNode> GetUninstallGraph()
        {
            var dependencies = new Queue<ITaskNode>();
            dependencies.Enqueue(new InitializeDirectoriesTask(false));
            dependencies.Enqueue(new EnvironmentPathTask(false));

            return dependencies;
        }
    }
}
