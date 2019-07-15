using System;
using System.IO;
using System.Linq;
using System.Threading;

namespace KFlearning.Core.IO
{
    public class FileSystemManager : IFileSystemManager
    {
        public string FindFile(string searchPath, string filename)
        {
            return Directory.EnumerateFiles(searchPath, filename, SearchOption.TopDirectoryOnly).FirstOrDefault();
        }

        public string FindDirectory(string searchPath, string directoryName)
        {
            return Directory.EnumerateDirectories(searchPath, directoryName, SearchOption.TopDirectoryOnly).FirstOrDefault();
        }

        public void CreateDirectory(string path)
        {
            Directory.CreateDirectory(path);
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

        public void MoveDirectory(string source, string destination, CancellationToken token)
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

                    File.Move(libFile, destPath);
                }

                foreach (string name in Directory.EnumerateDirectories(source).Select(Path.GetFileName))
                {
                    var sourceToProcess = Path.Combine(source, name);
                    var destToProcess = Path.Combine(destination, name);

                    Directory.CreateDirectory(destToProcess);
                    MoveDirectory(sourceToProcess, destToProcess, token);
                }

                Directory.Delete(source);
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

        public void MoveFile(string source, string destination)
        {
            if (File.Exists(destination))
            {
                File.SetAttributes(destination, FileAttributes.Normal);
                File.Delete(destination);
            }

            File.Move(source, destination);
        }

        public void CopyFile(string source, string destination)
        {
            if (File.Exists(destination))
            {
                File.SetAttributes(destination, FileAttributes.Normal);
                File.Delete(destination);
            }

            File.Copy(source, destination);
        }

        public void WriteFile(string path, string content)
        {
            File.WriteAllText(path, content);
        }

        public void CreateShortcutOnDesktop(string linkName, string description, string path)
        {
            object shDesktop = "Desktop";
            var shell = new IWshRuntimeLibrary.WshShell();
            string shortcutAddress = shell.SpecialFolders.Item(ref shDesktop) + $@"\{linkName}.lnk";
            var shortcut = (IWshRuntimeLibrary.IWshShortcut)shell.CreateShortcut(shortcutAddress);
            shortcut.Description = description;
            shortcut.TargetPath = path;
            shortcut.Save();
        }
    }
}
