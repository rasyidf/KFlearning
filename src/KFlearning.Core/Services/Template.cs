// SOLUTION : KFlearning
// PROJECT  : KFlearning.Core
// FILENAME : Template.cs
// AUTHOR   : Fahmi Noor Fiqri, Kodesiana.com
// WEBSITE  : https://kodesiana.com
// REPO     : https://github.com/Kodesiana or https://github.com/fahminlb33
// 
// This file is part of KFlearning, see LICENSE.
// See this code in repository URL above!

#region

#endregion


using System.Collections.Generic;
using Newtonsoft.Json;

namespace KFlearning.Core.Services
{
    public class Template
    {
        public string Title { get; }

        [JsonIgnore] public List<Transformable> FileMapping { get; }

        public Template()
        {
        }

        public Template(string title, List<Transformable> fileMapping)
        {
            Title = title;
            FileMapping = fileMapping;
        }

        public override string ToString()
        {
            return Title;
        }
    }
}