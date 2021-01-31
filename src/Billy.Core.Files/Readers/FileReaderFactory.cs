using System.Text;

namespace Billy.Core.Files.Readers
{
    public class FileReaderFactory : IFileReaderFactory
    {
        public IFileReader Get(Encoding encoding) =>
            encoding.EncodingName switch
            {
                { } enc when enc == Encoding.UTF8.EncodingName => new Utf8FileReader(),
                _ => new FileReader()
            };
    }
}