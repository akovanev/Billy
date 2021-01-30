using System;

namespace Billy.Core.Commands
{
    public interface ICommandInvoker<in TRequest, out TResult>
    {
        event EventHandler<CommandArgs> BeforeExecuting;
        event EventHandler<CommandArgs> OnExecuted;
        TResult ExecuteCommand(TRequest request);
    }
}