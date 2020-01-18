namespace KFlearning.Core.Services
{
    public class Project
    {
        public string Name { get; set; }
        public Template Template { get; set; }
        public string Path { get; set; }

        public override string ToString()
        {
            return $"{Name} ({Helpers.TrimLongText(Path)})";
        }
    }
}
