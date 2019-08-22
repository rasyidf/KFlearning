// 
//  PROJECT  :   KFlearning
//  FILENAME :   ShellView.xaml.cs
//  AUTHOR   :   Fahmi Noor Fiqri
//  WEBSITE  : https://kodesiana.com
//  REPO     : https://github.com/Kodesiana or https://github.com/fahminlb33
// 
//  This file is part of KFlearning, licensed under MIT license.
//  See this code in repository URL above!

#region

using MahApps.Metro.Controls;

#endregion

namespace KFlearning.IDE.Views
{
    /// <summary>
    ///     Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class ShellView
    {
        public ShellView()
        {
            InitializeComponent();
        }

        private void Menu_OnItemClick(object sender, ItemClickEventArgs e)
        {
            Menu.IsPaneOpen = false;
        }
    }
}