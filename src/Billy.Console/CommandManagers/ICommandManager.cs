using Billy.Core.Files.Models;

namespace Billy.Console.CommandManagers
{
    internal interface ICommandManager
    {
        SearchSignatureResult ParseAndRun(string[] args);
    }
}