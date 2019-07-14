using System;
using System.Collections.Generic;

namespace KFlearning.Core.API
{
    public class PackageCatalog
    {
        public PackageEntry Mingw { get; set; }
        public PackageEntry Glut { get; set; }

        public PackageEntry Httpd { get; set; }
        public PackageEntry PhpMyAdmin { get; set; }

        public PackageEntry MariaDb { get; set; }

        public PackageEntry Php { get; set; }
        public PackageEntry Composer { get; set; }
        public PackageEntry Xdebug { get; set; }

        public PackageEntry Vscode { get; set; }
        public List<string> VscodeExtensions { get; set; }
        public List<PackageEntry> ProjectTemplates { get; set; }

        public PackageEntry Kflearning { get; set; }
    }
}
