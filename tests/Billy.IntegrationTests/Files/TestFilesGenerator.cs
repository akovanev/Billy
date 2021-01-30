using System.IO;
using Billy.IntegrationTests.Configuration;
using Billy.Utilities.FileGenerator;

namespace Billy.IntegrationTests.Files
{
    internal class TestFilesGenerator
    {
        private readonly AppSettings _settings;
        private readonly FileGenerator _fileGenerator;

        public TestFilesGenerator(AppSettings settings)
        {
            _settings = settings;
            _fileGenerator = new FileGenerator();
        }

        public void GenerateFiles(bool rewrite)
        {
            foreach (var file in _settings.Files)
            {
                string absolutePath = Path.GetFullPath(_settings.TargetFolder);
                string filepath = Path.Combine(absolutePath, file.FileName);

                bool fileExists = File.Exists(filepath);

                if (!rewrite && fileExists)
                    continue;

                if(fileExists)
                    File.Delete(filepath);

                _fileGenerator.CreateFragmentedFile(
                    filepath,
                    file.FragmentSize,
                    file.FragmentCount,
                    file.Signature,
                    file.OccurrencesCount);
            }
        }
    }
}
