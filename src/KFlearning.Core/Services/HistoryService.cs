using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using KFlearning.Core.IO;
using Newtonsoft.Json;

namespace KFlearning.Core.Services
{
    public interface IHistoryService
    {
        bool RecordHistory { get; set; }

        void Add(Project project);
        void Clear();
        IEnumerable<Project> GetAll();
        void Save();
    }

    public class HistoryService : IHistoryService
    {
        public const int HistorySize = 10;
        private readonly List<Project> _projects = new List<Project>();
        private readonly JsonSerializer _serializer = new JsonSerializer();

        private readonly IPathManager _path;

        public bool RecordHistory { get; set; } = true;

        public HistoryService(IPathManager path)
        {
            _path = path;
            Reload();
        }

        public void Add(Project project)
        {
            if (!RecordHistory) return;
            _projects.RemoveAll(x => x.Path == project.Path);
            _projects.Add(project);
            EnsureSize();
        }

        public void Clear()
        {
            if (!RecordHistory) return;
            _projects.Clear();
            File.Delete(_path.GetPath(PathKind.HistoryFile));
        }

        public IEnumerable<Project> GetAll()
        {
            return !RecordHistory ? Enumerable.Empty<Project>() : _projects;
        }

        public void Save()
        {
            if (!RecordHistory) return;
            try
            {
                var saveFile = _path.GetPath(PathKind.HistoryFile);
                using (var writer = new StreamWriter(saveFile))
                using (var jsonWriter = new JsonTextWriter(writer))
                {
                    _serializer.Serialize(jsonWriter, _projects);
                }
            }
            catch (Exception)
            {
                // ignore
            }
        }

        private void EnsureSize()
        {
            if (!RecordHistory) return;
            if (_projects.Count <= HistorySize) return;
            _projects.RemoveRange(HistorySize - 1, _projects.Count - HistorySize);
        }

        private void Reload()
        {
            if (!RecordHistory) return;
            try
            {
                var saveFile = _path.GetPath(PathKind.HistoryFile);
                if (!File.Exists(saveFile)) return;

                _projects.Clear();
                
                using (var reader = new StreamReader(saveFile))
                using (var jsonReader = new JsonTextReader(reader))
                {
                    var list = _serializer.Deserialize<List<Project>>(jsonReader);
                    _projects.AddRange(list);
                }
            }
            catch (Exception)
            {
                // ignore
            }
        }
    }
}
