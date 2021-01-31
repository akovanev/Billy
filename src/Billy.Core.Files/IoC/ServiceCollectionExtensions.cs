using Billy.Core.Files.Helpers;
using Billy.Core.Files.Processors;
using Billy.Core.Files.Readers;
using Microsoft.Extensions.DependencyInjection;

namespace Billy.Core.Files.IoC
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddBillyFiles(this IServiceCollection services)
        {
            services.AddScoped<ISearchProcessor, SearchProcessor>();
            services.AddScoped<IEncodingHelper, EncodingHelper>();
            services.AddScoped<IFileReaderFactory, FileReaderFactory>();
            services.AddScoped<IFileReadBufferHelper, FileReadBufferHelper>();
            return services;
        }
    }
}
