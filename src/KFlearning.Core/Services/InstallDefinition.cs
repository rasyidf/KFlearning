using System;
using System.Collections.Generic;
using System.Linq;

namespace KFlearning.Core.Services
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
