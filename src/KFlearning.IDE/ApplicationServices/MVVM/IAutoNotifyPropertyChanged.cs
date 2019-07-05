using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace KFlearning.IDE.ApplicationServices
{
    public interface IAutoNotifyPropertyChanged : INotifyPropertyChanged
    {
        void OnPropertyChanged([CallerMemberName] string propertyName = null);
    }
}