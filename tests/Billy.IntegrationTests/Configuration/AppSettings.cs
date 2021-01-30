using System.Collections.Generic;
using System.IO;

namespace Billy.IntegrationTests.Configuration
{
    internal class AppSettings
    {
        public string TargetFolder { get; set; }
        public List<FilesSection> Files { get; set; }
    }

    internal class FilesSection
    {
        public string FileName { get; set; }
        public int FragmentSize { get; set; } // in bytes
        public int FragmentCount { get; set; }
        public int OccurrencesCount { get; set; }

        public string Signature => Path.GetFileNameWithoutExtension(FileName);
    }
}
