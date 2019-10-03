// 
//  PROJECT  :   KFlearning
//  FILENAME :   FileSystemManager.cs
//  AUTHOR   :   Fahmi Noor Fiqri
//  WEBSITE  : https://kodesiana.com
//  REPO     : https://github.com/Kodesiana or https://github.com/fahminlb33
// 
//  This file is part of KFlearning, licensed under MIT license.
//  See this code in repository URL above!

#region

using System;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Threading;
using IWshRuntimeLibrary;
using KFlearning.Core.Native;
using File = System.IO.File;

#endregion

namespace KFlearning.Core.IO
{
    public interface IFileSystemManager
    {
        string FindFile(string searchPath, string filename);

        void DeleteDirectory(string path, CancellationToken token);
        void CopyDirectory(string source, string destination, CancellationToken token);

        void WriteFile(string path, string content);
        void DeleteFile(string source);

        void CreateDirectoryLink(string link, string target);
        void RemoveDirectoryLink(string link);
        bool DirectoryLinkExists(string dir);
        void CreateShortcutOnDesktop(string linkName, string description, string path);
    }

    public class FileSystemManager : IFileSystemManager
    {
        public string FindFile(string searchPath, string filename)
        {
            return Directory.EnumerateFiles(searchPath, filename, SearchOption.TopDirectoryOnly).FirstOrDefault();
        }

        public void DeleteDirectory(string path, CancellationToken token)
        {
            try
            {
                foreach (string file in Directory.EnumerateFiles(path))
                {
                    token.ThrowIfCancellationRequested();
                    File.SetAttributes(file, FileAttributes.Normal);
                    File.Delete(file);
                }

                foreach (string currentDir in Directory.EnumerateDirectories(path))
                {
                    token.ThrowIfCancellationRequested();
                    DeleteDirectory(currentDir, token);
                }

                Directory.Delete(path);
            }
            catch (OperationCanceledException)
            {
                // ignored
            }
            catch
            {
                // ignored
            }
        }

        public void CopyDirectory(string source, string destination, CancellationToken token)
        {
            try
            {
                Directory.CreateDirectory(destination);
                foreach (string libFile in Directory.EnumerateFiles(source))
                {
                    var destPath = Path.Combine(destination, Path.GetFileName(libFile) ?? "");
                    if (File.Exists(destPath))
                    {
                        File.SetAttributes(destPath, FileAttributes.Normal);
                        File.Delete(destPath);
                    }

                    File.Copy(libFile, destPath);
                }

                foreach (string name in Directory.EnumerateDirectories(source).Select(Path.GetFileName))
                {
                    var sourceToProcess = Path.Combine(source, name);
                    var destToProcess = Path.Combine(destination, name);

                    Directory.CreateDirectory(destToProcess);
                    CopyDirectory(sourceToProcess, destToProcess, token);
                }
            }
            catch (OperationCanceledException)
            {
                // ignored
            }
            catch
            {
                // ignored
            }
        }

        public void WriteFile(string path, string content)
        {
            File.WriteAllText(path, content);
        }

        public void DeleteFile(string source)
        {
            File.Delete(source);
        }

        public void CreateDirectoryLink(string link, string target)
        {
            var result = NativeMethods.CreateSymbolicLink(link, target, NativeConstants.SYMBOLIC_LINK_FLAG_DIRECTORY);
            if (!result) throw new Win32Exception();
        }

        public void RemoveDirectoryLink(string link)
        {
            var result = NativeMethods.DeleteFile(link);
            if (!result) throw new Win32Exception();
        }

        public bool DirectoryLinkExists(string dir)
        {
            return File.Exists(dir);
        }

        public void CreateShortcutOnDesktop(string linkName, string description, string path)
        {
            object shDesktop = "Desktop";
            var shell = new WshShell();
            string shortcutAddress = shell.SpecialFolders.Item(ref shDesktop) + $@"\{linkName}.lnk";
            var shortcut = (IWshShortcut) shell.CreateShortcut(shortcutAddress);
            shortcut.Description = description;
            shortcut.TargetPath = path;
            shortcut.Save();
        }
    }
}