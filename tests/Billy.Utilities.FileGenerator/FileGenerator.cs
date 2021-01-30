using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Billy.Utilities.FileGenerator
{
    public class FileGenerator
    {
        private readonly RandomHelper _randomHelper = new RandomHelper();

        /// <summary>
        /// Creates a file containing the signature. The file size == fragmentSize*fragmentCount.
        /// The signature occurence number should not be greater than the fragment count.
        /// The signature length should not be greater than the fragmentSize.
        /// </summary>
        public bool CreateFragmentedFile(string name, int fragmentSize, int fragmentCount, string signature, int signatureOccurence)
        {
            if (fragmentSize < 1 || fragmentCount < 1 || signatureOccurence < 1 ||
                fragmentSize < signature.Length || fragmentCount < signatureOccurence)
                return false;

            long fileSize = (long)fragmentSize * fragmentCount;

            using var file = new FileStream(name, FileMode.Create);
            file.SetLength(fileSize);

            List<int> fragmentIndexes = _randomHelper.GenerateUniqueRandomNumbers(fragmentCount, signatureOccurence);
            List<int> fragmentOffsets = _randomHelper.GenerateRandomNumbers(fragmentSize - signature.Length, signatureOccurence);

            var utf8 = new UTF8Encoding();

            for (int index = 0; index < signatureOccurence; index++)
            {
                long offset = (long)fragmentIndexes[index] * fragmentSize + fragmentOffsets[index];

                file.Seek(offset, SeekOrigin.Begin);
                file.Write(utf8.GetBytes(signature));
            }

            return true;
        }
    }
}