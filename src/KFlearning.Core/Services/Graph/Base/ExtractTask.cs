using System;
using System.Collections.Generic;
using System.Threading;
using Ionic.Zip;

namespace KFlearning.Core.Services.Graph
{
    public class ExtractTask : ITaskNode
    {
        #region Fields
        
        private readonly string _zipFile, _outputPath;
        private CancellationToken _token;
        private IProgressBroker _broker;

        #endregion

        #region Properties
        
        public string TaskName => "Extract archive";
        public bool HasDependencies => false;
        public Queue<ITaskNode> Dependencies => null;

        #endregion

        #region Constructor
        
        public ExtractTask(string zipFile, string outputPath)
        {
            _zipFile = zipFile;
            _outputPath = outputPath;
        }

        #endregion

        #region ITaskNode Methods

        public void Configure(InstallerDefinition definition)
        {
            _broker = definition.ResolveService<IProgressBroker>();
        }

        public bool Run(CancellationToken cancellation)
        {
            _token = cancellation;

            try
            {
                _broker.ReportMessage("Extracting files...");
                using (var zip = new ZipFile(_zipFile))
                {
                    zip.ExtractProgress += Zip_ExtractProgress;
                    zip.ExtractAll(_outputPath, ExtractExistingFileAction.OverwriteSilently);
                    zip.ExtractProgress -= Zip_ExtractProgress;

                    _broker.ReportMessage("Extraction completed.");
                    return true;
                }
            }
            catch (Exception e)
            {
                _broker.ReportMessage(e.ToString());
                _broker.ReportProgress(100);
                return false;
            }
        }

        #endregion

        #region Private Methods

        private void Zip_ExtractProgress(object sender, ExtractProgressEventArgs e)
        {
            if (_token.IsCancellationRequested)
            {
                e.Cancel = true;
            }
            else
            {
                var percentage = (int) Math.Round((double) e.EntriesExtracted / e.EntriesTotal * 100, 0);
                _broker.ReportProgress(percentage);
            }
        }

        #endregion
    }
}
