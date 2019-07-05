// // PROJECT :   KFlearning
// // FILENAME :  ViewModelLocator.cs
// // AUTHOR  :   Fahmi Noor Fiqri
// // NPM     :   065118116
// //
// // This file is part of KFlearning, licensed under MIT license.

using System;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Windows;

namespace KFlearning.IDE.ApplicationServices
{
    public class ViewModelLocator
    {
        public static readonly DependencyProperty IsAutomaticLocatorProperty =
            DependencyProperty.RegisterAttached("IsAutomaticLocator", typeof(bool), typeof(ViewModelLocator),
                new PropertyMetadata(false, IsAutomaticLocatorChanged));

        public static bool GetIsAutomaticLocator(DependencyObject obj)
        {
            return (bool) obj.GetValue(IsAutomaticLocatorProperty);
        }

        public static void SetIsAutomaticLocator(DependencyObject obj, bool value)
        {
            obj.SetValue(IsAutomaticLocatorProperty, value);
        }

        private static void IsAutomaticLocatorChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var view = (FrameworkElement) d;
            view.DataContext = DesignerProperties.GetIsInDesignMode(d) ? null : GetInstanceOf(view.GetType());
        }

        private static object GetInstanceOf(Type view)
        {
            var entryAssembly = Assembly.GetEntryAssembly() ?? Assembly.GetAssembly(view);
            var assemblyTypes = entryAssembly.GetTypes();
            var viewModelName = view.Name + "Model";
            var viewModelType = assemblyTypes.FirstOrDefault(a => a.Name == viewModelName);
            if (viewModelType == null)
                throw new ArgumentException($"Not exist a type {viewModelName} in the assembly.");

            return App.Container.Resolve(viewModelType);
        }
    }
}