using Billy.Core.Files.Models;

namespace Billy.Core.Files.Processors
{
    public class SearchProcessor : ISearchProcessor
    {
        public SearchSignatureResult CountOccurrences(SearchSignatureRequest request)
        {
            return new SearchSignatureResult
            {
                OriginalRequest = request
            };
        }
    }
}
