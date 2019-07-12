using System;
using System.Collections.Generic;

namespace KFlearning.Core.API
{
    public class PackageCatalog
    {
        public Uri MingwUri { get; set; }
        public Uri GlutUri { get; set; }

        public Uri ApacheUri { get; set; }
        public Uri PhpmyadminUri { get; set; }

        public Uri MariaDbUri { get; set; }

        public Uri PhpUri { get; set; }
        public Uri ComposerUri { get; set; }
        public Uri Xdebuguri { get; set; }

        public Uri VscodeUri { get; set; }
        public List<Uri> VscodeExtensions { get; set; }
        public List<Uri> ProjectTemplates { get; set; }

        public Uri KflearningUri { get; set; }
    }
}
