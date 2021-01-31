using Billy.Core.Files.Models;

namespace Billy.Core.Files.Helpers
{
    public class FileSearchHelper : IFileSearchHelper
    {
        private readonly IEncodingHelper _encodingHelper;

        public FileSearchHelper(IEncodingHelper encodingHelper)
        {
            _encodingHelper = encodingHelper;
        }

        public FileInfo GetFileInfo(SearchSignatureRequest request)
        {
            // Detects the encoding before search.
            var encoding = _encodingHelper.DetectEncoding(request.FileFullName);

            //Gets the number of bytes for the signature.
            int signatureByteCount = encoding.GetByteCount(request.Signature);

            //Gets the chunk size.
            int chunkSize = GetBufferLength(signatureByteCount);

            return new FileInfo
            {
                Encoding = encoding,
                ChunkSize = chunkSize,
                BackShift = signatureByteCount
            };
        }

        internal int GetBufferLength(int signatureLength)
        {
            const int expectedBufferSize = 4096;
            const int extendingSizeCoefficient = 2;

            int extendedLength = signatureLength * extendingSizeCoefficient;

            return extendedLength < expectedBufferSize
                ? expectedBufferSize
                : extendedLength;
        }
    }
}