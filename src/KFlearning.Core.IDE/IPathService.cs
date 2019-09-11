namespace KFlearning.Core.IDE
{
    public interface IPathService
    {
        string LaragonWebRoot { get; }

        string GetDatabasePath();
        string GetTemplateZipPath(TemplateZip template);
    }
}
