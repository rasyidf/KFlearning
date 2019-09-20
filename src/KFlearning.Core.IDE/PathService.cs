// 
//  PROJECT  :   KFlearning
//  FILENAME :   PathService.cs
//  AUTHOR   :   Fahmi Noor Fiqri
//  WEBSITE  : https://kodesiana.com
//  REPO     : https://github.com/Kodesiana or https://github.com/fahminlb33
// 
//  This file is part of KFlearning, licensed under MIT license.
//  See this code in repository URL above!

using System;
using System.IO;
using System.Reflection;
using KFlearning.Core.Resources;

namespace KFlearning.Core.IDE
{
    public class PathService : IPathService
    {
        private readonly string _dataPath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);

        private string _templateRoot;

        public string LaragonWebRoot { get; }

        public PathService()
        {
            var rootDrive = Path.GetPathRoot(Environment.SystemDirectory);
            _templateRoot = Path.Combine(rootDrive, @"kflearning\ide\ProjectTemplates");
            LaragonWebRoot = Path.Combine(rootDrive, @"laragon\www");
        }

        public string GetDatabasePath()
        {
            return Path.Combine(_dataPath, Constants.DatabaseConnectionString);
        }

        public string GetTemplateZipPath(TemplateZip template)
        {
            switch (template)
            {
                case TemplateZip.Cpp:
                    return Path.Combine(_templateRoot, "template-cpp.zip");
                default:
                    throw new ArgumentOutOfRangeException(nameof(template), template, null);
            }
        }
    }
}