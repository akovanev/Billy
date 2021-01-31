using System.Text;
using Billy.Core.Files.Helpers;
using Billy.Core.Files.Models;
using Moq;

namespace Billy.Core.Files.Tests.Mocks
{
    public class FileSearchHelperMock
    {
        public static IFileSearchHelper Create(Encoding encoding, int backShift, int chunkSize)
        {
            var fileSearchHelper = new Mock<IFileSearchHelper>();
            fileSearchHelper.Setup(x => x.GetFileInfo(It.IsAny<SearchSignatureRequest>()))
                .Returns(new FileInfo
                {
                    Encoding = encoding,
                    ChunkSize = chunkSize,
                    BackShift = backShift
                });

            return fileSearchHelper.Object;
        }
    }
}
