using System.Collections.Generic;
using Billy.Core.Files.Models;

namespace Billy.Core.Files.Readers
{
    public interface IFileReader
    {
        IEnumerable<byte[]> ReadChunks(string path, FileInfo fileInfo);
    }
}