using System.IO;
using KFlearning.Core.IO;

namespace KFlearning.Core.Hosts
{
    public class Vscode : IVscode
    {
        private readonly IProcessManager _pathManager;

        public Vscode(IProcessManager pathManager)
        {
            _pathManager = pathManager;
        }

        public void OpenFolder(string path)
        {
            _pathManager.Run(_pathManager.GetPath(PathKind.VscodeExe), path);
        }

        public void InstallExtension(string path)
        {
            _pathManager.RunWait(_pathManager.GetPath(PathKind.VscodeExe), "--install-extension " + Path.GetFileName(path));
        }
    }
}
