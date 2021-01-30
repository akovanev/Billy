using System;
using System.Diagnostics;

namespace Billy.Core.Commands
{
    public class CommandInvoker<TRequest, TResult>
    {
        private readonly Command<TRequest, TResult> _command;
        private readonly string _commandName;

        public CommandInvoker(Command<TRequest, TResult> command)
        {
            _command = command;
            _commandName = _command.GetType().Name;
        }

        public event EventHandler<CommandArgs>? BeforeExecuting;
        public event EventHandler<CommandArgs>? OnExecuted;

        public TResult ExecuteCommand(TRequest request)
        {
            BeforeExecuting?.Invoke(this, new CommandArgs(_commandName));

            var stopwatch = Stopwatch.StartNew();

            TResult result = _command.Execute(request);

            stopwatch.Stop();

            OnExecuted?.Invoke(this, new CommandArgs(_commandName, stopwatch.Elapsed));

            return result;
        }
    }
}
