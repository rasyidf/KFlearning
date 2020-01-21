// SOLUTION : KFlearning
// PROJECT  : KFlearning.Core
// FILENAME : Project.cs
// AUTHOR   : Fahmi Noor Fiqri, Kodesiana.com
// WEBSITE  : https://kodesiana.com
// REPO     : https://github.com/Kodesiana or https://github.com/fahminlb33
// 
// This file is part of KFlearning, see LICENSE.
// See this code in repository URL above!

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