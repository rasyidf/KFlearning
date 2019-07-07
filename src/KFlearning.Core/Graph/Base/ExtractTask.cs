using System;
using System.Collections.Generic;
using System.Threading;
using Ionic.Zip;

namespace KFlearning.Core.Graph
{
    public class ExtractTask : ITaskNode
    {
        private readonly string _zipFile, _outputPath;

        public string TaskName => "Extract archive";
        public bool HasDependencies => false;
        public Queue<ITaskNode> Dependencies => null;

        public ExtractTask(string zipFile, string outputPath)
        {
            _zipFile = zipFile;
            _outputPath = outputPath;
        }

        public bool Run(CancellationToken cancellation)
        {
            try
            {
                using (var zip = new ZipFile(_zipFile))
                {
                    zip.ExtractProgress += Zip_ExtractProgress;
                    zip.ExtractAll(_outputPath, ExtractExistingFileAction.OverwriteSilently);
                    zip.ExtractProgress -= Zip_ExtractProgress;

                    return true;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return false;
            }
        }

        private void Zip_ExtractProgress(object sender, ExtractProgressEventArgs e)
        {
            throw new NotImplementedException();
        }
    }
}
