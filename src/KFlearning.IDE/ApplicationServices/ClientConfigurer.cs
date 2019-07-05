using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using KFlearning.Core.Hosts;
using KFlearning.Core.IO;

namespace KFlearning.IDE.ApplicationServices
{
    public class ClientConfigurer
    {
        private readonly IProcessManager _pathManager;
        private readonly IVscode _vscode;

        public ClientConfigurer(IProcessManager pathManager)
        {
            _pathManager = pathManager;
        }

        public void Configure()
        {

        }

        private void ConfigureEnvironmentVars()
        {
            var addToPath = new[]
            {
                _pathManager.GetPath(PathKind.MingwBinRoot),
                _pathManager.GetPath(PathKind.VscodeRoot),
                _pathManager.GetPath(PathKind.PhpRoot),
                _pathManager.GetPath(PathKind.ComposerRoot)
            };
            var original = Environment.GetEnvironmentVariable("PATH", EnvironmentVariableTarget.User);
            Trace.Assert(original != null);
            if (original.Contains(addToPath.First())) return;

            var newPath = Path.Combine(original, string.Join(";", addToPath));
            Environment.SetEnvironmentVariable("PATH", newPath, EnvironmentVariableTarget.User);
        }

        private void ConfigureVscode()
        {
            var exts = Directory.GetFiles(_pathManager.GetPath(PathKind.VscodeExtensionRoot), "*.vsix");
            foreach (string ext in exts)
            {
                _vscode.InstallExtension(ext);
            }
        }

        private void ConfigureApache()
        {

        }

        private void ConfigureMinGw()
        {
            // copy freeglut


            // copy winbgim

        }
    }
}
