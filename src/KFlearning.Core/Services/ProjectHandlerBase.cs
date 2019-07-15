using System.IO;
using KFlearning.Core.DAL;
using Newtonsoft.Json;

namespace KFlearning.Core.Services
{
    public abstract class ProjectHandlerBase : IProjectHandler
    {
        public abstract Project Create(string title, string path);
        public abstract void Import(Project project);
        public abstract void Destroy(Project project);

        protected void SaveMetadata(Project project)
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
    }
}
