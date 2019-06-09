using System;

namespace KFlearning.ApplicationServices.Models
{
    public class AppEventArgs : EventArgs
    {
        public AppAction Action { get; }
        public object State { get; }

        public AppEventArgs(AppAction action, object state = null)
        {
            Action = action;
            State = state;
        }
    }
}
