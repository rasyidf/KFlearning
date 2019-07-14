using System;
using KFlearning.Core.API;

namespace KFlearning.Core.Services.Sequence
{
    public class InstallerDefinition
    {
        private readonly Func<Type, object> _resolveFunc;

        public InstallerDefinition(Func<Type, object> resolveFunc)
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
