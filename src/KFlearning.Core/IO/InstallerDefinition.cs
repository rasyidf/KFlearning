using System;
using KFlearning.Core.API;

namespace KFlearning.Core.IO
{
    public class InstallerDefinition
    {
        private readonly Func<Type, object> _resolveFunc;

        public InstallerDefinition(Func<Type, object> resolveFunc)
        {
            _resolveFunc = resolveFunc;
        }

        public InstallMode Mode { get; set; }

        public PackageConfig Packages { get; set; }
        
        public T ResolveService<T>()
        {
            return (T) _resolveFunc(typeof(T));
        }
    }
}
