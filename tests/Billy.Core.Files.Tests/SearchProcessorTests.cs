using System.Collections.Generic;
using System.Linq;
using System.Text;
using Billy.Core.Files.Models;
using Billy.Core.Files.Processors;
using Billy.Core.Files.Tests.Mocks;
using Xunit;

namespace Billy.Core.Files.Tests
{
    public class SearchProcessorTests
    {
        private readonly Encoding _encoding = Encoding.ASCII;

        [Theory]
        [InlineData("aaa", "bbaaaa", "aaaaaa", "aaaa", 2)]
        [InlineData("aaa", "bbaaaa", "aaaaaa", "aaaaa", 3)]
        public void CountOccurrences_ASCII_ReturnsNumber(
            string signature, 
            string chunk1, 
            string chunk2, 
            string chunk3,
            int occurrences)
        {
            var chunks = new List<byte[]>
            {
                _encoding.GetBytes(chunk1),
                _encoding.GetBytes(chunk2),
                _encoding.GetBytes(chunk3)
            };

            var fileReaderFactory = FileReaderMock.CreateFactory(ReaderFactory.Base, chunks);
            var fileSearchHelper = FileSearchHelperMock.Create(_encoding, signature.Length, chunks.First().Length);
            var searchProcessor = new SearchProcessor(fileReaderFactory, fileSearchHelper);

            var request = new SearchSignatureRequest
            {
                Signature = signature
            };
            
            var result = searchProcessor.CountOccurrences(request);
            
            Assert.Equal(occurrences, result.OccurrencesCount);
            Assert.Same(request, result.OriginalRequest);
        }
    }
}
