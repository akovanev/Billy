using System;

namespace Billy.Core.Commands
{
    /// <summary>
    /// The class is used for sending data from the command executor to its events subscribers.
    /// </summary>
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