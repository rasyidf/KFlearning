// 
//  PROJECT  :   KFlearning
//  FILENAME :   HostsFile.cs
//  AUTHOR   :   Fahmi Noor Fiqri
//  NPM      :   065118116
// 
//  This file is part of KFlearning, licensed under MIT license.

using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;
using KFlearning.Core.IO;

namespace KFlearning.Core.Hosts
{
    public class HostsFile : IHostsFile
    {
        private static readonly Regex HostLinePattern = new Regex("(?<ip>[0-9.]+)( +)(?<host>\\S+)");
        private readonly IProcessManager _processManager;

        public HostsFile(IProcessManager processManager)
        {
            _processManager = processManager;
        }

        public void AddEntry(string domain)
        {
            var lines = new List<string>();
            lines.AddRange(File.ReadAllLines(_processManager.GetPath(PathKind.HostsFile)));
            lines.Add($"127.0.0.1      {domain}      #KFLearning Magic");
            File.WriteAllLines(_processManager.GetPath(PathKind.HostsFile), lines);
        }

        public void RemoveEntry(string domain)
        {
            var lines = File.ReadAllLines(_processManager.GetPath(PathKind.HostsFile));
            var newLines = new List<string>();
            foreach (string line in lines)
            {
                if (line.Contains(domain)) continue;
                newLines.Add(line);
            }

            File.WriteAllLines(_processManager.GetPath(PathKind.HostsFile), newLines);
        }

        public IEnumerable<HostEntry> EnumerateDomains()
        {
            var lines = File.ReadAllLines(_processManager.GetPath(PathKind.HostsFile));
            foreach (string line in lines)
            {
                if (string.IsNullOrWhiteSpace(line) || line.StartsWith("#")) continue;
                var match = HostLinePattern.Match(line);
                yield return new HostEntry(match.Groups["ip"].Value, match.Groups["host"].Value);
            }
        }
    }
}