// 
//  PROJECT  :   KFlearning
//  FILENAME :   ClientConfigurer.cs
//  AUTHOR   :   Fahmi Noor Fiqri
//  NPM      :   065118116
// 
//  This file is part of KFlearning, licensed under MIT license.

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
        private readonly IPathManager _pathManager;
        private readonly IVscode _vscode;

        public ClientConfigurer(IPathManager pathManager)
        {
            _pathManager = pathManager;
        }

        public void Configure()
        {
        }

        private void ConfigureEnvironmentVars()
        {
            //var original = Environment.GetEnvironmentVariable("PATH", EnvironmentVariableTarget.User);
            //Trace.Assert(original != null);
            //if (original.Contains(addToPath.First())) return;

            //var newPath = Path.Combine(original, string.Join(";", addToPath));
            //Environment.SetEnvironmentVariable("PATH", newPath, EnvironmentVariableTarget.User);
        }

        private void ConfigureVscode()
        {
            //var exts = Directory.GetFiles(_pathManager.GetPath(PathKind.VscodeExtensionRoot), "*.vsix");
            //foreach (string ext in exts)
            //{
            //    _vscode.InstallExtension(ext);
            //}
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