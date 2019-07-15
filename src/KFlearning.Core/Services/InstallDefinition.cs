using System;
using KFlearning.Core.API;

namespace KFlearning.Core.Services
{
    public class InstallDefinition
    {
        private readonly Func<Type, object> _resolveFunc;

        public InstallDefinition(Func<Type, object> resolveFunc)
        {
            _resolveFunc = resolveFunc;
        }
        
        public PackageCatalog Packages { get; set; }

        public string DataPath { get; set; }

        public T ResolveService<T>()
        {
            return (T) _resolveFunc(typeof(T));
        }
    }
}
