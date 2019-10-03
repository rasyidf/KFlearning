// 
//  PROJECT  :   KFlearning
//  FILENAME :   MingwTask.cs
//  AUTHOR   :   Fahmi Noor Fiqri
//  WEBSITE  : https://kodesiana.com
//  REPO     : https://github.com/Kodesiana or https://github.com/fahminlb33
// 
//  This file is part of KFlearning, licensed under MIT license.
//  See this code in repository URL above!

#region

using System;
using System.IO;
using System.Threading;
using ICSharpCode.SharpZipLib.Zip;
using KFlearning.Core.IO;

#endregion

namespace KFlearning.Core.Services
{
    public interface IMinGW
    {
    }

    public class MinGW : IMinGW, IInstallable
    {
        private readonly IFileSystemManager _fileSystem;
        private readonly IPathManager _path;

        public MinGW(IPathManager path, IFileSystemManager fileSystem)
        {
            _path = path;
            _fileSystem = fileSystem;
        }

        public void Install(Action<int> progressCallback, CancellationToken cancellation)
        {
            var root = Path.Combine(Path.GetPathRoot(Environment.SystemDirectory), "MinGW");

            // find zip and extract
            var mingwZip = _path.GetPath(PathKind.MinGwZip);
            using (var extractor = new ZipFile(mingwZip))
            {
                extractor.ExtractAll(root, progressCallback, cancellation);
            }

            // add to env
            _path.AddVariable(Path.Combine(root, "bin"));

            // find zip and extract
            var glutZipPath = _path.GetPath(PathKind.FreeglutZip);
            var extractPath = _path.GetPath(PathKind.Temp);
            using (var extractor = new ZipFile(glutZipPath))
            {
                extractor.ExtractAll(root, progressCallback, cancellation);
            }

            // install glut to MinGW
            var freeglutRoot = Path.Combine(extractPath, "freeglut");
            _fileSystem.CopyDirectory(freeglutRoot, root, cancellation);

            // install lib to MinGW 8.2.0
            var sourcePath = Path.Combine(freeglutRoot, @"lib");
            var destPath = Path.Combine(root, @"lib\gcc\mingw32\8.2.0");
            _fileSystem.CopyDirectory(sourcePath, destPath, cancellation);
        }

        public void Uninstall(Action<int> progressCallback, CancellationToken cancellation)
        {
            throw new NotImplementedException();
        }
    }
}