using System.Text;

namespace Billy.Core.Files.Readers
{
    public interface IFileReaderFactory
    {
        IFileReader Get(Encoding encoding);
    }
}