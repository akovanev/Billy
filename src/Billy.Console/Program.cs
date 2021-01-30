using System;
using Billy.Console.CommandManagers;
using Billy.Console.Helpers;
using Microsoft.Extensions.DependencyInjection;

namespace Billy.Console
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                ServiceProvider serviceProvider = Startup.Configure();

                var commandManager = serviceProvider.GetService<ICommandManager>();

                string? result = commandManager?.ParseAndRun(args);

                OutputWriter.Print(result);
            }
            catch (Exception ex)
            {
                OutputWriter.Print(MessageHelper.GetErrorMessage(ex.Message));
            }
        }
    }
}
