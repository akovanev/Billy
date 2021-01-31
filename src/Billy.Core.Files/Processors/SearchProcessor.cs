using System;
using Billy.Core.Files.Helpers;
using Billy.Core.Files.Models;
using Billy.Core.Files.Readers;

namespace Billy.Core.Files.Processors
{
    public class SearchProcessor : ISearchProcessor
    {
        private readonly IFileReaderFactory _fileReaderFactory;
        private readonly IFileSearchHelper _fileSearchHelper;

        public SearchProcessor(IFileReaderFactory fileReaderFactory, IFileSearchHelper fileSearchHelper)
        {
            _fileReaderFactory = fileReaderFactory;
            _fileSearchHelper = fileSearchHelper;
        }

        /// <summary>
        /// Counts occurrences of the signature in the source file.
        /// </summary>
        public SearchSignatureResult CountOccurrences(SearchSignatureRequest request)
        {
            //Gets file characteristics.
            FileInfo fileInfo = _fileSearchHelper.GetFileInfo(request);

            //Gets the file reader for the specific encoding.
            IFileReader fileReader = _fileReaderFactory.Get(fileInfo.Encoding);

            var occurrencesResult = new OccurrencesResult();
            int occurrencesCount = 0;

            //Iterates over the chunks.
            foreach (byte[] chunk in fileReader.ReadChunks(request.FileFullName!, fileInfo))
            {
                //Converts array of chunk bytes to string.
                string? chunkString = fileInfo.Encoding.GetString(chunk);

                if (occurrencesResult.TailCount > 0)
                {
                    //Cuts first characters as they were counted in the previous result.
                    chunkString = chunkString.Substring(occurrencesResult.TailCount);
                }

                //Gets occurrences result for the chunk.
                occurrencesResult = CountOccurrences(chunkString, request.Signature);
                occurrencesCount += occurrencesResult.Count;
            }

            return new SearchSignatureResult
            {
                OriginalRequest = request,
                OccurrencesCount = occurrencesCount
            };
        }

        /// <summary>
        /// Counts occurrences of the signature in the source string.
        /// </summary>
        internal OccurrencesResult CountOccurrences(string source, string signature)
        {
            var result = new OccurrencesResult();

            if (source == string.Empty ||
                signature == string.Empty ||
                signature.Length > source.Length)
                return result;

            int position = 0;
            
            while ((position = source.IndexOf(signature, position, StringComparison.InvariantCulture)) != -1)
            {
                result.Count++;
                position += signature.Length;
                result.TailCount = signature.Length - (source.Length - position);
            }

            return result;
        }

        internal struct OccurrencesResult
        {
            //Count of occurrences.
            public int Count;

            //Tail count of characters after last occurrences index
            public int TailCount;
        }
    }
}
