// 
//  PROJECT  :   KFlearning
//  FILENAME :   HostsFile.cs
//  AUTHOR   :   Fahmi Noor Fiqri
//  WEBSITE  : https://kodesiana.com
//  REPO     : https://github.com/Kodesiana or https://github.com/fahminlb33
// 
//  This file is part of KFlearning, licensed under MIT license.
//  See this code in repository URL above!

#region

using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using KFlearning.Core.IO;

#endregion

namespace KFlearning.Core.Services
{
    public class HostsFile : IHostsFile
    {
        private static readonly Regex HostLinePattern = new Regex("(?<ip>[0-9.]+)( +)(?<host>\\S+)");
        private readonly IPathManager _pathManager;

        public HostsFile(IPathManager pathManager)
        {
            _pathManager = pathManager;
        }

        public void AddEntry(string domain)
        {
            string content = $"127.0.0.1      {domain}      #KFLearning Magic";
            File.AppendAllText(_pathManager.GetPath(PathKind.TemplateHosts), content);
        }

        public void RemoveEntry(string domain)
        {
            var lines = File.ReadAllLines(_pathManager.GetPath(PathKind.TemplateHosts))
                .Where(line => !line.Contains(domain)).ToList();
            File.WriteAllLines(_pathManager.GetPath(PathKind.TemplateHosts), lines);
        }

        public IEnumerable<HostEntry> EnumerateDomains()
        {
            var lines = File.ReadAllLines(_pathManager.GetPath(PathKind.TemplateHosts));
            foreach (Match match in lines.Where(l => !string.IsNullOrWhiteSpace(l) && !l.StartsWith("#"))
                .Select(x => HostLinePattern.Match(x)))
            {
                yield return new HostEntry(match.Groups["ip"].Value, match.Groups["host"].Value);
            }
        }
    }
}