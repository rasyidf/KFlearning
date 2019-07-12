using System;
using KFlearning.Core.API;

namespace KFlearning.Core.Services.Graph
{
    public class InstallerDefinition
    {
        private readonly Func<Type, object> _resolveFunc;

        public InstallerDefinition(Func<Type, object> resolveFunc)
        {
            _resolveFunc = resolveFunc;
        }

        public InstallMode Mode { get; set; }

        public PackageCatalog Packages { get; set; }
        
        public T ResolveService<T>()
        {
            return (T) _resolveFunc(typeof(T));
        }
    }
}
