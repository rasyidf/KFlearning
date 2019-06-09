using System;
using System.Collections.Generic;
using System.Windows;

namespace KFlearning.ApplicationServices
{
    public static class ViewLocator
    {
        private static readonly Dictionary<Type, object> Views = new Dictionary<Type, object>();
        private static Window _shellWindow;

        public static void Register<T>(T instance)
        {
            Views.Add(typeof(T), instance);
        }

        public static void RegisterShell<T>(T instance) where T : Window
        {
            _shellWindow = instance;
        }

        public static T GetView<T>()
        {
            return (T)Views[typeof(T)];
        }
        
        public static Window GetShell()
        {
            return _shellWindow;
        }

    }
}
