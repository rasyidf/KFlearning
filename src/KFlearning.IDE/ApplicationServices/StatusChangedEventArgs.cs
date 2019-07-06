using System;

namespace KFlearning.IDE.ApplicationServices
{
    public class StatusChangedEventArgs : EventArgs
    {
        public DateTime Timestamp { get; }

        public string Message { get; }

        public StatusChangedEventArgs(DateTime timestamp, string message)
        {
            Timestamp = timestamp;
            Message = message;
        }
    }
}
