using System;
using System.IO;
using KFlearning.Core.DAL;
using KFlearning.Core.IO;
using Newtonsoft.Json;

namespace KFlearning.Core.Services
{
    public class ProjectHandler : IProjectHandler
    {
        private readonly IWebServer _webServer;
        private readonly IFileSystemManager _fileSystem;
        private readonly IVscode _vscode;

        public ProjectHandler(IWebServer webServer, IFileSystemManager fileSystem, IVscode vscode)
        {
            _webServer = webServer;
            _fileSystem = fileSystem;
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

        public void ExtractTemplate(Project project, ProjectTemplate template)
        {
            throw new NotImplementedException();
        }
    }
}
