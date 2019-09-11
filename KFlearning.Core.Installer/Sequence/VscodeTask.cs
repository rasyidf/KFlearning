// 
//  PROJECT  :   KFlearning
//  FILENAME :   VscodeTask.cs
//  AUTHOR   :   Fahmi Noor Fiqri
//  WEBSITE  : https://kodesiana.com
//  REPO     : https://github.com/Kodesiana or https://github.com/fahminlb33
// 
//  This file is part of KFlearning, licensed under MIT license.
//  See this code in repository URL above!

#region

using System.IO;
using System.Threading;
using KFlearning.Core.IO;
using KFlearning.Core.Resources;
using KFlearning.Core.Services;

#endregion

namespace KFlearning.Core.Installer.Sequence
{
    public class VscodeTask : ITaskNode
    {
        private readonly IProgressBroker _progress;
        private readonly IPathManager _path;
        private readonly IModuleService _moduleService;
        private readonly IVscode _vscode;

        public string TaskName => "Visual Studio Code";

        public VscodeTask(IProgressBroker progress, IModuleService moduleService, IVscode vscode, IPathManager path)
        {
            _progress = progress;
            _moduleService = moduleService;
            _vscode = vscode;
            _path = path;
        }

        public void Run(InstallDefinition definition, CancellationToken cancellation)
        {
            var root = _path.GetModuleInstallPath(ModuleKind.Vscode);

            // find zip and extract
            _progress.ReportMessage("Extracting Visual Studio Code...");
            var vscodeZip = _moduleService.GetModuleZipPath(ModuleZipFile.Vscode);
            using (var extractor = new ZipExtractor((s, e) => _progress.ReportNodeProgress(e.ProgressPercentage)))
            {
                extractor.ExtractAll(vscodeZip, root);
            }

            // create data directory
            Directory.CreateDirectory(Path.Combine(root, "data"));
            Directory.CreateDirectory(Path.Combine(root, @"data\user-data"));

            // install extensions
            _progress.ReportMessage("Installing extensions...");
            var extensions = _moduleService.GetVscodeExtensions();
            for (var i = 0; i < extensions.Count; i++)
            {
                _vscode.InstallExtension(extensions[i]);
                _progress.ReportNodeProgress(MathHelper.CalculatePercentage(i + 1, extensions.Count));
            }

            // save settings
            _progress.ReportMessage("Configuring Visual Studio Code...");
            var vscodeSettingsFile = Path.Combine(root, @"data\user-data\settings.json");
            File.WriteAllText(vscodeSettingsFile, Constants.VscodeConfig);
        }
    }
}