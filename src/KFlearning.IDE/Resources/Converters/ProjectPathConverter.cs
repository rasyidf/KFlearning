// 
//  PROJECT  :   KFlearning
//  FILENAME :   ProjectPathConverter.cs
//  AUTHOR   :   Fahmi Noor Fiqri
//  WEBSITE  : https://kodesiana.com
//  REPO     : https://github.com/Kodesiana or https://github.com/fahminlb33
// 
//  This file is part of KFlearning, licensed under MIT license.
//  See this code in repository URL above!

#region

using System;
using System.Globalization;
using System.Windows.Data;
using KFlearning.Core.Services;

#endregion

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