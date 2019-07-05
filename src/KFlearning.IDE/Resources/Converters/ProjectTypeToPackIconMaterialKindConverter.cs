// // PROJECT :   KFlearning
// // FILENAME :  ProjectTypeToPackIconMaterialKindConverter.cs
// // AUTHOR  :   Fahmi Noor Fiqri
// // NPM     :   065118116
// //
// // This file is part of KFlearning, licensed under MIT license.

using System;
using System.Globalization;
using System.Windows.Data;
using KFlearning.Core.Entities;
using MahApps.Metro.IconPacks;

namespace KFlearning.IDE.Resources.Converters
{
    public class ProjectTypeToPackIconMaterialKindConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            switch ((ProjectType) value)
            {
                case ProjectType.Web:
                    return PackIconMaterialKind.Web;
                case ProjectType.Cpp:
                    return PackIconMaterialKind.LanguageCpp;
                case ProjectType.Python:
                    return PackIconMaterialKind.LanguagePython;
                default:
                    throw new ArgumentOutOfRangeException(nameof(value), value, null);
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}