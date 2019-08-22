// 
//  PROJECT  :   KFlearning
//  FILENAME :   WebBrowserUtility.cs
//  AUTHOR   :   Fahmi Noor Fiqri
//  WEBSITE  : https://kodesiana.com
//  REPO     : https://github.com/Kodesiana or https://github.com/fahminlb33
// 
//  This file is part of KFlearning, licensed under MIT license.
//  See this code in repository URL above!

#region

using System;
using System.Windows;
using System.Windows.Controls;

#endregion

namespace KFlearning.IDE.ApplicationServices
{
    public class WebBrowserUtility
    {
        public static readonly DependencyProperty BindableSourceProperty =
            DependencyProperty.RegisterAttached("BindableSource", typeof(string), typeof(WebBrowserUtility),
                new UIPropertyMetadata(null, BindableSourcePropertyChanged));

        public static string GetBindableSource(DependencyObject obj)
        {
            return (string) obj.GetValue(BindableSourceProperty);
        }

        public static void SetBindableSource(DependencyObject obj, object value)
        {
            obj.SetValue(BindableSourceProperty, value);
        }

        private static void BindableSourcePropertyChanged(DependencyObject obj, DependencyPropertyChangedEventArgs e)
        {
            var browser = obj as WebBrowser;
            if (browser == null) return;

            var uri = e.NewValue as string;
            if (uri == null) return;

            browser.Navigate(new Uri(uri));
        }
    }
}