// 
//  PROJECT  :   KFlearning
//  FILENAME :   ProjectHandler.cs
//  AUTHOR   :   Fahmi Noor Fiqri
//  WEBSITE  : https://kodesiana.com
//  REPO     : https://github.com/Kodesiana or https://github.com/fahminlb33
// 
//  This file is part of KFlearning, licensed under MIT license.
//  See this code in repository URL above!

#region

using System;
using System.IO;
using System.Reflection;
using ICSharpCode.SharpZipLib.Zip;
using KFlearning.Core.DAL;
using KFlearning.Core.IO;
using Newtonsoft.Json;

#endregion

namespace KFlearning.Core.Services
{
    public class ProjectHandler : IProjectHandler
    {
        private readonly IWebServer _webServer;
        private readonly IVscode _vscode;

        public ProjectHandler(IWebServer webServer, IVscode vscode)
        {
            _webServer = webServer;
            _vscode = vscode;
        }

        public void Launch(Project project)
        {
            _vscode.OpenFolder(project.Path);
        }

        public void CreateAlias(Project project)
        {
            _webServer.CreateAlias(project.VirtualHostDomain, project.Path);
        }

        public void RemoveAlias(Project project)
        {
            _webServer.RemoveAlias(project.VirtualHostDomain);
        }

        public void SaveMetadata(Project project)
        {
            var path = Path.Combine(project.Path, Constants.MetadataFileName);
            using (var streamWriter = new StreamWriter(path))
            {
                var serializer = new JsonSerializer
                {
                    Formatting = Formatting.Indented
                };

                serializer.Serialize(streamWriter, project);
            }
        }

        public void InitializeCpp(Project project)
        {
            var zipPath = Path.Combine(project.Path, "templateCppKFL.zip");
            File.WriteAllBytes(zipPath, Constants.template_cpp);
            using (var zip = new ZipFile(zipPath))
            {
                zip.ExtractAll(project.Path);
            }
            File.Delete(zipPath);
        }
    }
}