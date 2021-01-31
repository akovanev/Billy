using System.IO;
using Billy.Console;
using Billy.IntegrationTests.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Xunit;

namespace Billy.IntegrationTests.Files
{
    public class SearchSignatureTests
    {
        private readonly AppSettings _settings;
        private readonly string _absolutePath;


        public SearchSignatureTests()
        {
            _settings = ConfigurationHelper.BuildProvider()
                .GetService<IOptions<AppSettings>>()
                .Value;

            new TestFilesGenerator(_settings).GenerateFiles(false);
            _absolutePath = Path.GetFullPath(_settings.TargetFolder);
        }

        [Fact]
        public void Test1()
        {
            const string template = "The number of occurrences of '{0}' in the file is {1}";

            foreach (var item in _settings.Files)
            {
                string actual = Program.Run(new []{ Path.Combine(_absolutePath,item.FileName)});

                Assert.Equal(string.Format(template, item.Signature, item.OccurrencesCount), actual);
            }
        }
    }
}
