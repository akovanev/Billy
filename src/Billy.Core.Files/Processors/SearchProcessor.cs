using System;
using System.Collections.Generic;
using System.Diagnostics;
using Billy.Core.Files.Helpers;
using Billy.Core.Files.Models;
using Billy.Core.Files.Readers;

namespace Billy.Core.Files.Processors
{
    public class SearchProcessor : ISearchProcessor
    {
        private readonly IEncodingHelper _encodingHelper;
        private readonly IFileReaderFactory _fileReaderFactory;
        private readonly IFileReadBufferHelper _fileReadBufferHelper;

        public SearchProcessor(
            IEncodingHelper encodingHelper,
            IFileReaderFactory fileReaderFactory,
            IFileReadBufferHelper fileReadBufferHelper)
        {
            _encodingHelper = encodingHelper;
            _fileReaderFactory = fileReaderFactory;
            _fileReadBufferHelper = fileReadBufferHelper;
        }

        /// <summary>
        /// Counts occurrences of the signature in the source file.
        /// </summary>
        public SearchSignatureResult CountOccurrences(SearchSignatureRequest request)
        {
            //Detects the encoding before search.
            var encoding = _encodingHelper.DetectEncoding(request.FileFullName);

            //Gets the file reader for the specific encoding.
            IFileReader fileReader = _fileReaderFactory.Get(encoding);

            //Gets the number of bytes for the signature.
            int signatureByteCount = encoding.GetByteCount(request.Signature);

            //Gets the chunk size.
            int chunkSize = _fileReadBufferHelper.GetBufferLength(signatureByteCount);

            var chunkInfo = new ChunkInfo
            {
                Size = chunkSize,
                Preamble = encoding.Preamble.Length,
                BackShift = signatureByteCount
            };


            var occurrencesResult = new OccurrencesResult();
            int occurrencesCount = 0;

            //Iterates over the chunks.
            foreach (byte[] chunk in fileReader.ReadChunks(request.FileFullName!, chunkInfo))
            {
                //Converts array of chunk bytes to string.
                string? chunkString = encoding.GetString(chunk);

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
