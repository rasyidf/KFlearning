// 
//  PROJECT  :   KFlearning
//  FILENAME :   InstallDefinition.cs
//  AUTHOR   :   Fahmi Noor Fiqri
//  WEBSITE  : https://kodesiana.com
//  REPO     : https://github.com/Kodesiana or https://github.com/fahminlb33
// 
//  This file is part of KFlearning, licensed under MIT license.
//  See this code in repository URL above!

#region

using System;
using System.Collections.Generic;
using System.Linq;

#endregion

namespace KFlearning.Core.Services.Installer
{
    public class InstallDefinition
    {
        private readonly Func<Type, object> _resolveFunc;

        public string DataPath { get; }

        public List<string> VscodeExtensions { get; }

        public InstallDefinition(string dataPath, IEnumerable<string> vscodeExtensions, Func<Type, object> resolveFunc)
        {
            DataPath = dataPath;
            VscodeExtensions = vscodeExtensions.ToList();
            _resolveFunc = resolveFunc;
        }

        public T ResolveService<T>()
        {
            return (T) _resolveFunc(typeof(T));
        }
    }
}