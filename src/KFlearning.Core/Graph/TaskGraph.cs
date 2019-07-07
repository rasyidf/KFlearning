using System;
using System.Threading;
using KFlearning.Core.IO;

namespace KFlearning.Core.Graph
{
    public class TaskGraph
    {
        public void RunGraph()
        {
            
        }

        private void InternalRunGraph(InstallerDefinition definition, ITaskNode rootNode, CancellationToken token)
        {
            rootNode.Configure(definition);
            if (rootNode.HasDependencies)
            {
                foreach (ITaskNode nodeDependency in rootNode.Dependencies)
                {
                    InternalRunGraph(definition, nodeDependency, token);
                }
            }
            
            rootNode.Run(token);
            if (rootNode is IDisposable disposable) disposable.Dispose();
        }

        

        private void BuildInstallGraph()
        {
            // step 1 - kill all process

            // step 2 - mingw

            // step 2 - mariadb

            // step 3 - apache httpd

            // step 4a - php
            
            // step 4b - xdebug
            
            // step 5a - phpmyadmin

            // step 6 - kflearning
        }
    }
}
