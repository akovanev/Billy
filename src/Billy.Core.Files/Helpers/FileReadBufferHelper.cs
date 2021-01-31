namespace Billy.Core.Files.Helpers
{
    public class FileReadBufferHelper : IFileReadBufferHelper
    {
        public int GetBufferLength(int signatureLength)
        {
            const int expectedBufferSize = 4;
            const int extendingSizeCoefficient = 2;

            int extendedLength = signatureLength * extendingSizeCoefficient;

            return extendedLength < expectedBufferSize
                ? expectedBufferSize
                : extendedLength;
        }
    }
}