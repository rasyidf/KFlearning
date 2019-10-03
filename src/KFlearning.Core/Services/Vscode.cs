// 
//  PROJECT  :   KFlearning
//  FILENAME :   Vscode.cs
//  AUTHOR   :   Fahmi Noor Fiqri
//  WEBSITE  : https://kodesiana.com
//  REPO     : https://github.com/Kodesiana or https://github.com/fahminlb33
// 
//  This file is part of KFlearning, licensed under MIT license.
//  See this code in repository URL above!

#region

using System;
using System.IO;
using System.Threading;
using ICSharpCode.SharpZipLib.Zip;
using KFlearning.Core.Diagnostics;
using KFlearning.Core.IO;

#endregion

namespace KFlearning.Core.Services
{    
    public interface IVscode
    {
        void InstallExtension(string path);
        void OpenFolder(string path);
    }

    public class Vscode : IVscode, IInstallable
    {
        private readonly IProcessManager _processManager;
        private readonly IPathManager _path;
        
        public Vscode(IProcessManager processManager, IPathManager path)
        {
            _processManager = processManager;
            _path = path;
        }

        public void InstallExtension(string path)
        {
            var filePath = Path.Combine(_path.GetPath(PathKind.InstallRoot), @"vscode\bin\code.cmd");
            _processManager.RunWait(filePath, $"--install-extension \"{path}\"");
        }

        public void OpenFolder(string path)
        {
            var filePath = Path.Combine(_path.GetPath(PathKind.InstallRoot), @"vscode\code.exe");
            _processManager.Run(filePath, $"\"{path}\"");
        }

        public void Install(Action<int> progressCallback, CancellationToken cancellation)
        {
            var root = Path.Combine(_path.GetPath(PathKind.InstallRoot), @"vscode");

            // find zip and extract
            var vscodeZip = _path.GetPath(PathKind.VscodeZip);
            using (var extractor = new ZipFile(vscodeZip))
            {
                extractor.ExtractAll(root, progressCallback, cancellation);
            }

            // create data directory
            Directory.CreateDirectory(Path.Combine(root, "data"));
            Directory.CreateDirectory(Path.Combine(root, @"data\user-data"));

            // install extensions
            var extensions = Directory.GetFiles(_path.GetPath(PathKind.ExtensionRoot), "*.vsix");
            for (var i = 0; i < extensions.Length; i++)
            {
                InstallExtension(extensions[i]);
                progressCallback?.Invoke(Helpers.CalculatePercentage(i + 1, extensions.Length));
            }

            // save settings
            var vscodeSettingsFile = Path.Combine(root, @"data\user-data\User\settings.json");
            File.WriteAllText(vscodeSettingsFile, CoreResources.VscodeConfig);
        }

        public void Uninstall(Action<int> progressCallback, CancellationToken cancellation)
        {
            throw new NotImplementedException();
        }
    }
}