using System;
using Billy.Console.CommandManagers;
using Billy.Console.Helpers;
using Billy.Core.Files.Models;
using Microsoft.Extensions.DependencyInjection;

namespace Billy.Console
{
    public class Program
    {
        static void Main(string[] args)
        {
            try
            {
                OutputWriter.Print(Run(args));
            }
            catch (Exception ex)
            {
                OutputWriter.PrintError(MessageHelper.GetErrorMessage(ex.Message));
            }
        }

        public static string Run(string[] args)
        {
            
            ServiceProvider serviceProvider = Startup.Configure();

            var commandManager = serviceProvider.GetService<ICommandManager>();

            SearchSignatureResult? result = commandManager?.ParseAndRun(args);

            return MessageHelper.GetSearchSignatureResultMessage(result);
        }
    }
}
