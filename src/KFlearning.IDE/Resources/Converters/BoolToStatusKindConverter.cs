// 
//  PROJECT  :   KFlearning
//  FILENAME :   BoolToStatusKindConverter.cs
//  AUTHOR   :   Fahmi Noor Fiqri
//  NPM      :   065118116
// 
//  This file is part of KFlearning, licensed under MIT license.

using System;
using System.Globalization;
using System.Windows.Data;
using MahApps.Metro.IconPacks;

namespace KFlearning.IDE.Resources.Converters
{
    [ValueConversion(typeof(bool), typeof(PackIconMaterialKind))]
    public class BoolToStatusKindConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is bool v)
            {
                return v ? PackIconMaterialKind.EmoticonExcited : PackIconMaterialKind.EmoticonSad;
            }

            return PackIconMaterialKind.EmoticonSad;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}