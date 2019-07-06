// 
//  PROJECT  :   KFlearning
//  FILENAME :   ShellView.xaml.cs
//  AUTHOR   :   Fahmi Noor Fiqri
//  NPM      :   065118116
// 
//  This file is part of KFlearning, licensed under MIT license.

using MahApps.Metro.Controls;

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