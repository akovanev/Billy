namespace Billy.Core.Files.Models
{
    public class ChunkInfo
    {
        /// <summary>
        /// The chunk size.
        /// </summary>
        public int Size { get; set; }
        
        /// <summary>
        /// The start position in the file. 
        /// </summary>
        public int Preamble { get; set; }

        /// <summary>
        /// The number of bytes which will be re-read in next chunk.
        /// </summary>
        public int BackShift { get; set; }
    }
}
