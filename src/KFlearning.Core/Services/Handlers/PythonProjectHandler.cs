using System.IO;
using System.Threading;
using KFlearning.Core.DAL;
using KFlearning.Core.IO;

namespace KFlearning.Core.Services.Handlers
{
    class PythonProjectHandler : IProjectHandler
    {
        private readonly IFileSystemManager _fileSystem;

        public PythonProjectHandler(IFileSystemManager fileSystem)
        {
            _fileSystem = fileSystem;
        }

        public Project Initialize(string title, string path)
        {
            var project = new Project
            {
                Title = title,
                Type = ProjectType.Python,
                Path = path
            };
            Directory.CreateDirectory(project.Path);

            return project;
        }

        public void Initialize(Project project)
        {
            
        }

        public void Uninitialize(Project project)
        {
            _fileSystem.DeleteDirectory(project.Path, CancellationToken.None);
        }
    }
}
