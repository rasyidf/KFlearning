using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;

namespace KFlearning.Core.Installer.Graph
{
    public class DeleteFilesTask : ITaskNode
    {
        private readonly string _path;

        public string TaskName => "Delete files";
        public bool HasDependencies => false;
        public Queue<ITaskNode> Dependencies => null;

        public DeleteFilesTask(string path)
        {
            _path = path;
        }

        public bool Run(CancellationToken cancellation)
        {
            try
            {
                if (!Directory.Exists(_path) && !File.Exists(_path)) return false;
                if (Directory.Exists(_path))
                {
                    InternalRecursiveDelete(_path);
                }
                else
                {
                    File.Delete(_path);
                }

                return true;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return false;
            }
        }

        private void InternalRecursiveDelete(string path)
        {
            foreach (string file in Directory.EnumerateFiles(path))
            {
                File.SetAttributes(file, FileAttributes.Normal);
                File.Delete(file);
            }

            foreach (string currentDir in Directory.EnumerateDirectories(path))
            {
                InternalRecursiveDelete(currentDir);
            }

            Directory.Delete(path);
        }

    }
}
