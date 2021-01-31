using System.Collections.Generic;
using System.Text;
using Billy.Core.Files.Readers;
using Moq;
using FileInfo = Billy.Core.Files.Models.FileInfo;

namespace Billy.Core.Files.Tests.Mocks
{
    public class FileReaderMock
    {
        public static IFileReader Create<T>(List<byte[]> chunks)
            where T : class, IFileReader
        {
            var fileReader = new Mock<T>();

            fileReader.Setup(x => x.ReadChunks(It.IsAny<string>(), It.IsAny<FileInfo>()))
                .Returns(chunks);

            return fileReader.Object;
        }

        public static IFileReaderFactory CreateFactory(ReaderFactory factory, List<byte[]> chunks)
        {
            IFileReader fileReader = factory switch
            {
                ReaderFactory.Utf8 => Create<Utf8FileReader>(chunks),
                _ => Create<FileReader>(chunks)
            };

            var fileReaderFactory = new Mock<IFileReaderFactory>();
            fileReaderFactory.Setup(x => x.Get(It.IsAny<Encoding>()))
                .Returns(fileReader);

            return fileReaderFactory.Object;
        }
    }

    public enum ReaderFactory
    {
        Base,
        Utf8
    }
}