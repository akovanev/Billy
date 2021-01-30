namespace Billy.Core.Commands
{
    /// <summary>
    /// Represents Command pattern.
    /// </summary>
    public abstract class Command<TRequest,TResult>
    {
        public abstract TResult Execute(TRequest request);
    }
}
