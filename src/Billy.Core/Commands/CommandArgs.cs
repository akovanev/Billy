using System;

namespace Billy.Core.Commands
{
    public class CommandArgs : EventArgs
    {
        public CommandArgs(string command, TimeSpan timeSpan = default, string message = "")
        {
            Command = command;
            TimeSpan = timeSpan;
            Message = message;
        }

        public string Command { get; }
        public TimeSpan TimeSpan { get; }
        public string Message { get; }
    }
}