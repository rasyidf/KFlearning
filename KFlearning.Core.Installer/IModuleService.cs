using System.Collections.Generic;

namespace KFlearning.Core.Installer
{
    public interface IModuleService
    {
        string GetModuleZipPath(ModuleZipFile module);
        List<string> GetVscodeExtensions();
    }
}
