using System;
using System.Collections.Generic;
using System.IO;
using Ionic.Zip;
using KFlearning.ApplicationServices.Clients;
using KFlearning.ApplicationServices.Models;
using Newtonsoft.Json;

namespace KFlearning.ApplicationServices
{
    public class ProjectManager : IProjectManager
    {
        private readonly IApacheServer _apache;
        private readonly IHostsFile _hosts;
        private readonly IProcessManager _pathManager;

        public ProjectManager(IApacheServer apache, IHostsFile hosts, IProcessManager pathManager)
        {
            _apache = apache;
            _hosts = hosts;
            _pathManager = pathManager;
        }

        public IEnumerable<ProjectDefinition> GetProjects()
        {
            var projectDirs = Directory.GetDirectories(_pathManager.GetPath(PathKind.ReposRoot), "*",
                SearchOption.TopDirectoryOnly);
            foreach (string projectDir in projectDirs)
            {
                var metadataPath = Path.Combine(_pathManager.GetPath(PathKind.ReposRoot), projectDir,
                    Strings.MetadataFileName);
                if (!File.Exists(metadataPath)) continue;
                yield return JsonConvert.DeserializeObject<ProjectDefinition>(File.ReadAllText(metadataPath));
            }
        }

        public void Create(ProjectType type, string title)
        {
            var alias = CreateAliasName(title);
            var project = new ProjectDefinition
            {
                Title = title,
                Alias = alias,
                Domain = type == ProjectType.Web ? _apache.CreateDomainName(alias) : "",
                Path = GetPathForProject(title),
                Type = type
            };
            Directory.CreateDirectory(project.Path);

            if (type == ProjectType.Web)
            {
                _apache.CreateAlias(project.Alias, project.Path);
                _hosts.AddEntry(project.Domain);
            }

            ExtractTemplate(project);
            SaveMetadata(project);
        }

        public void Delete(ProjectDefinition project)
        {
            Directory.Delete(project.Path, true);

            if (project.Type != ProjectType.Web) return;
            _apache.RemoveAlias(project.Alias);
            _hosts.RemoveEntry(project.Domain);
        }

        public void Import(string zipFile)
        {
            using (var zip = new ZipFile(zipFile))
            {
                if (!zip.EntryFileNames.Contains(Strings.MetadataFileName))
                    throw new InvalidOperationException("No metadata file exists on import ZIP file.");

                var extractPath = GetPathForProject(zip.Comment);
                zip.ExtractAll(extractPath);

                var metadataFile = Path.Combine(extractPath, Strings.MetadataFileName);
                var project = JsonConvert.DeserializeObject<ProjectDefinition>(File.ReadAllText(metadataFile));

                if (project.Type != ProjectType.Web) return;
                _apache.CreateAlias(project.Alias, project.Path);
                _hosts.AddEntry(project.Domain);
            }
        }

        public void Export(ProjectDefinition project, string savePath)
        {
            using (var zip = new ZipFile(savePath))
            {
                var files = Directory.EnumerateFiles(project.Path, "*", SearchOption.AllDirectories);
                foreach (string dire in files)
                {
                    zip.AddFile(dire);
                }

                zip.Comment = project.Alias;
                zip.Save();
            }
        }

        public string GetPathForProject(string title)
        {
            return Path.Combine(_pathManager.GetPath(PathKind.ReposRoot), CreateAliasName(title));
        }

        #region Private Methods
        
        private void SaveMetadata(ProjectDefinition project)
        {
            var path = Path.Combine(project.Path, Strings.MetadataFileName);
            File.WriteAllText(path, JsonConvert.SerializeObject(project));
        }

        private string CreateAliasName(string title)
        {
            return title.ToLowerInvariant().Replace(" ", "_");
        }

        private void ExtractTemplate(ProjectDefinition project)
        {
            string path;
            switch (project.Type)
            {
                case ProjectType.Web:
                    path = _pathManager.GetPath(PathKind.VscodeWebZip);
                    break;
                case ProjectType.Cpp:
                    path = _pathManager.GetPath(PathKind.VscodeCppZip);
                    break;
                case ProjectType.Python:
                    path = _pathManager.GetPath(PathKind.VscodePythonZip);
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(project.Type), project.Type, null);
            }

            using (var zip = new ZipFile(path))
            {
                zip.ExtractAll(project.Path, ExtractExistingFileAction.OverwriteSilently);
            }
        } 

        #endregion
    }
}
