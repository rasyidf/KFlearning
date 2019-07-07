using System;
using System.Threading;

namespace KFlearning.Core.Installer.Graph
{
    public class TaskGraph
    {
        public void RunGraph()
        {
            
        }

        private void InternalRunGraph(ITaskNode rootNode, CancellationToken token)
        {
            if (rootNode.HasDependencies)
            {
                foreach (ITaskNode nodeDependency in rootNode.Dependencies)
                {
                    InternalRunGraph(nodeDependency, token);
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
