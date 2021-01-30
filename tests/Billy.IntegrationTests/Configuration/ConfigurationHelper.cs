using System;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Billy.IntegrationTests.Configuration
{
    internal class ConfigurationHelper
    {
        public static IServiceProvider BuildProvider()
        {
            var configuration = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json", false)
                .Build();

            return new ServiceCollection()
                .AddOptions()
                .Configure<AppSettings>(configuration.GetSection("Billy.Files"))
                .BuildServiceProvider();
        }
    }
}
