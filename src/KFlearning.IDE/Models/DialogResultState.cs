// // PROJECT :   KFlearning
// // FILENAME :  DialogResultState.cs
// // AUTHOR  :   Fahmi Noor Fiqri
// // NPM     :   065118116
// //
// // This file is part of KFlearning, licensed under MIT license.

using MahApps.Metro.Controls.Dialogs;

namespace KFlearning.IDE.Models
{
    public class DialogResultState
    {
        public DialogResultState(MessageDialogResult result, object state)
        {
            Result = result;
            State = state;
        }

        public MessageDialogResult Result { get; }

        public object State { get; }
    }
}