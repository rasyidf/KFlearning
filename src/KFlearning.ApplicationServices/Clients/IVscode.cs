namespace KFlearning.ApplicationServices.Clients
{
    public interface IVscode
    {
        void OpenFolder(string path);
        void InstallExtension(string path);
    }
}