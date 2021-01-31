using System.Text;

namespace Billy.Core.Files.Models
{
    public class FileInfo
    {
        /// <summary>
        /// /The file encoding.
        /// </summary>
        public Encoding Encoding { get; set; } = default!;

        /// <summary>
        /// The chunk size.
        /// </summary>
        public int ChunkSize { get; set; }

        /// <summary>
        /// The number of bytes which will be re-read in next chunk.
        /// </summary>
        public int BackShift { get; set; }

        /// <summary>
        /// The start position in the file. 
        /// </summary>
        public int Preamble => Encoding.Preamble.Length;
    }
}
