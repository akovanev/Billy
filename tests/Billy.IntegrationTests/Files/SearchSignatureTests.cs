using Billy.IntegrationTests.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Xunit;

namespace Billy.IntegrationTests.Files
{
    public class SearchSignatureTests
    {
        private readonly AppSettings _settings;

        public SearchSignatureTests()
        {
            _settings = ConfigurationHelper.BuildProvider()
                .GetService<IOptions<AppSettings>>()
                .Value;

            new TestFilesGenerator(_settings).GenerateFiles(false);
        }

        [Fact]
        public void Test1()
        {
        }
    }
}
