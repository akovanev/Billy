using Billy.Core.Commands;
using Billy.Core.Files.Models;
using Billy.Core.Files.Processors;

namespace Billy.Core.Files.Commands
{
    public class SearchSignatureCommand : Command<SearchSignatureRequest, SearchSignatureResult>
    {
        private readonly ISearchProcessor _searchProcessor;

        public SearchSignatureCommand(ISearchProcessor searchProcessor)
        {
            _searchProcessor = searchProcessor;
        }

        public override SearchSignatureResult Execute(SearchSignatureRequest request)
            => _searchProcessor.CountOccurrences(request);
    }
}
