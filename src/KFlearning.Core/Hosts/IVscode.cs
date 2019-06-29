namespace KFlearning.Core.Hosts
{
    public interface IVscode
    {
        void OpenFolder(string path);
        void InstallExtension(string path);
    }
}