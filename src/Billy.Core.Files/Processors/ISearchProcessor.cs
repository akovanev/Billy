using Billy.Core.Files.Models;

namespace Billy.Core.Files.Processors
{
    public interface ISearchProcessor
    {
        SearchSignatureResult CountOccurrences(SearchSignatureRequest request);
    }
}