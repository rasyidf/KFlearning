using System.IO;
using System.Threading;
using KFlearning.Core.DAL;
using KFlearning.Core.IO;

namespace KFlearning.Core.Services.Handlers
{
    public class CppProjectHandler : IProjectHandler
    {
        private readonly IFileSystemManager _fileSystem;

        public CppProjectHandler(IFileSystemManager fileSystem)
        {
            _fileSystem = fileSystem;
        }

        public Project Initialize(string title, string path)
        {
            var project = new Project
            {
                Title = title,
                Type = ProjectType.Cpp,
                Path = path
            };
            Directory.CreateDirectory(project.Path);
            // extract template

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
