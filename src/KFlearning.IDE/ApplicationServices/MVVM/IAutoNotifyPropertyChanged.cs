// 
//  PROJECT  :   KFlearning
//  FILENAME :   IAutoNotifyPropertyChanged.cs
//  AUTHOR   :   Fahmi Noor Fiqri
//  NPM      :   065118116
// 
//  This file is part of KFlearning, licensed under MIT license.

using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace KFlearning.IDE.ApplicationServices
{
    public interface IAutoNotifyPropertyChanged : INotifyPropertyChanged
    {
        void OnPropertyChanged([CallerMemberName] string propertyName = null);
    }
}