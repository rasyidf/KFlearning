// // PROJECT :   KFlearning
// // FILENAME :  IDialog.cs
// // AUTHOR  :   Fahmi Noor Fiqri
// // NPM     :   065118116
// //
// // This file is part of KFlearning, licensed under MIT license.

using MahApps.Metro.Controls.Dialogs;

namespace KFlearning.IDE.ApplicationServices
{
    public interface IDialog
    {
        BaseMetroDialog DialogInstance { get; set; }
        MessageDialogResult DialogResult { get; set; }
        object State { get; set; }
    }
}