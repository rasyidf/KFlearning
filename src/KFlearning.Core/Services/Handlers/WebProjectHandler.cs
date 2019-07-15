using System.IO;
using System.Threading;
using KFlearning.Core.DAL;
using KFlearning.Core.IO;

namespace KFlearning.Core.Services.Handlers
{
    public class WebProjectHandler : ProjectHandlerBase
    {
        private readonly IWebServer _webServer;
        private readonly IFileSystemManager _fileSystem;

        public WebProjectHandler(IWebServer webServer, IFileSystemManager fileSystem)
        {
            _webServer = webServer;
            _fileSystem = fileSystem;
        }

        public override Project Create(string title, string path)
        {
            var project = new Project
            {
                Title = title,
                Type = ProjectType.Web,
                Path = path,
                DomainName = _webServer.GenerateDomainName(title)
            };
            Directory.CreateDirectory(project.Path);
            Import(project);
            SaveMetadata(project);

            return project;
        }

        public override void Import(Project project)
        {
            _webServer.CreateAlias(project.DomainName, project.Path);
        }

        public override void Destroy(Project project)
        {
            _webServer.RemoveAlias(project.DomainName);
            _fileSystem.DeleteDirectory(project.Path, CancellationToken.None);
        }
    }
}
