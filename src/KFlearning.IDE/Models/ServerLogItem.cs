using System;

namespace KFlearning.IDE.Models
{
    public class ServerLogItem
    {
        public DateTime Timestamp { get; }

        public string Message { get; }

        public ServerLogItem(DateTime timestamp, string message)
        {
            Timestamp = timestamp;
            Message = message;
        }
    }
}
