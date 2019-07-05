// // PROJECT :   KFlearning
// // FILENAME :  ServerLogItem.cs
// // AUTHOR  :   Fahmi Noor Fiqri
// // NPM     :   065118116
// //
// // This file is part of KFlearning, licensed under MIT license.

using System;

namespace KFlearning.IDE.Models
{
    public class ServerLogItem
    {
        public ServerLogItem(DateTime timestamp, string message)
        {
            Timestamp = timestamp;
            Message = message;
        }

        public DateTime Timestamp { get; }

        public string Message { get; }
    }
}