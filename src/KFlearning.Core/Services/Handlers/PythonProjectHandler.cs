using System.IO;
using System.Threading;
using KFlearning.Core.DAL;
using KFlearning.Core.IO;

namespace KFlearning.Core.Services.Handlers
{
    public class PythonProjectHandler : ProjectHandlerBase
    {
        private readonly IFileSystemManager _fileSystem;

        public PythonProjectHandler(IFileSystemManager fileSystem)
        {
            _fileSystem = fileSystem;
        }

        public override Project Create(string title, string path)
        {
            var project = new Project
            {
                Title = title,
                Type = ProjectType.Python,
                Path = path
            };
            Directory.CreateDirectory(project.Path);
            SaveMetadata(project);

            return project;
        }

        public override void Import(Project project)
        {
            
        }

        public override void Destroy(Project project)
        {
            _fileSystem.DeleteDirectory(project.Path, CancellationToken.None);
        }
    }
}
