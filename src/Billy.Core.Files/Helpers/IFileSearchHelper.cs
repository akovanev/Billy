using Billy.Core.Files.Models;

namespace Billy.Core.Files.Helpers
{
    public interface IFileSearchHelper
    {
        public FileInfo GetFileInfo(SearchSignatureRequest request);
    }
}