// 
//  PROJECT  :   KFlearning
//  FILENAME :   ModuleService.cs
//  AUTHOR   :   Fahmi Noor Fiqri
//  WEBSITE  : https://kodesiana.com
//  REPO     : https://github.com/Kodesiana or https://github.com/fahminlb33
// 
//  This file is part of KFlearning, licensed under MIT license.
//  See this code in repository URL above!

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using KFlearning.Core.IO;

namespace KFlearning.Core.Installer
{
    public class ModuleService : IModuleService
    {
        private readonly string _dataPath =
            Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), "data");

        private readonly IFileSystemManager _fileSystem;
        
        public ModuleService(IFileSystemManager fileSystem)
        {
            _fileSystem = fileSystem;
        }
        
        public string GetModuleZipPath(ModuleZipFile module)
        {
            switch (module)
            {
                case ModuleZipFile.Ide:
                    return _fileSystem.FindFile(_dataPath, "KFlearning-IDE-*");
                case ModuleZipFile.Mingw:
                    return _fileSystem.FindFile(_dataPath, "mingw-*");
                case ModuleZipFile.Glut:
                    return _fileSystem.FindFile(_dataPath, "freeglut-*");
                case ModuleZipFile.Vscode:
                {
                    var vscodeName = Environment.Is64BitOperatingSystem ? "VSCode-win32-x64*" : "VSCode-win32-ia32*";
                    return _fileSystem.FindFile(_dataPath, vscodeName);
                }
                default:
                    throw new ArgumentOutOfRangeException(nameof(module), module, null);
            }
        }

        public List<string> GetVscodeExtensions()
        {
            return Directory.GetFiles(Path.Combine(_dataPath, "vscode-exts"), "*.vsix", SearchOption.TopDirectoryOnly)
                .ToList();
        }
    }
}