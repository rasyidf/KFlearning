using KFlearning.Core.API;

namespace KFlearning.Core.IO
{
    public class InstallerDefinition
    {
        public InstallMode Mode { get; set; }

        public PackageConfig Packages { get; set; }

        public string TempPath { get; set; }

        
    }
}
