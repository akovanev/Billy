using System;
using System.IO;
using Billy.Console.CommandManagers;
using Billy.Console.Configuration;
using Billy.Core.Files.IoC;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Billy.Console
{
    public class Startup
    {
        public static ServiceProvider Configure()
        {
            string? environmentName = Environment.GetEnvironmentVariable("ENVIRONMENT");
            
            // build config
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", false);
            
            if(!(environmentName is null))
                builder.AddJsonFile($"appsettings.{environmentName}.json", true);

            var configuration = builder
                .AddEnvironmentVariables()
                .Build();

            return new ServiceCollection()
                .AddLogging()
                .AddOptions()
                .Configure<AppSettings>(configuration.GetSection("App"))
                .AddSingleton<ICommandManager, CommandManager>()
                .AddBillyFiles()
                .BuildServiceProvider();
        }
    }
}