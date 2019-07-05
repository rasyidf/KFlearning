// // PROJECT :   KFlearning
// // FILENAME :  ProjectPathConverter.cs
// // AUTHOR  :   Fahmi Noor Fiqri
// // NPM     :   065118116
// //
// // This file is part of KFlearning, licensed under MIT license.

using System;
using System.Globalization;
using System.Windows.Data;
using KFlearning.IDE.ApplicationServices;

namespace KFlearning.IDE.Resources.Converters
{
    public class ProjectPathConverter : IValueConverter
    {
        private static IProjectManager _projectManager = App.Container.Resolve<IProjectManager>();

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return value == null ? "" : _projectManager.GetPathForProject(value.ToString());
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}