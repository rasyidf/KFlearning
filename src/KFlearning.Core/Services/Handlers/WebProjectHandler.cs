using System.IO;
using System.Threading;
using KFlearning.Core.DAL;
using KFlearning.Core.IO;

namespace KFlearning.Core.Services.Handlers
{
    public class WebProjectHandler : IProjectHandler
    {
        private readonly IWebServer _webServer;
        private readonly IFileSystemManager _fileSystem;

        public WebProjectHandler(IWebServer webServer, IFileSystemManager fileSystem)
        {
            _webServer = webServer;
            _fileSystem = fileSystem;
        }

        public Project Initialize(string title, string path)
        {
            var project = new Project
            {
                Title = title,
                Type = ProjectType.Web,
                Path = path,
                DomainName = _webServer.GenerateDomainName(title)
            };
            Directory.CreateDirectory(project.Path);
            Initialize(project);
            
            return project;
        }

        public void Initialize(Project project)
        {
            _webServer.CreateAlias(project.DomainName, project.Path);
        }

        public void Uninitialize(Project project)
        {
            _webServer.RemoveAlias(project.DomainName);
            _fileSystem.DeleteDirectory(project.Path, CancellationToken.None);
        }
    }
}
