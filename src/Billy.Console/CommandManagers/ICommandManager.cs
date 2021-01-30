namespace Billy.Console.CommandManagers
{
    internal interface ICommandManager
    {
        string ParseAndRun(string[] args);
    }
}